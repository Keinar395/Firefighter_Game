using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public static Movement Instance { get; private set; }

    private int maxHealth = 3000;
    private int currentHealth;
    private int dieTime = 2;

    public HealthBar healthBar;
    public Animator animator;

    private float speed = 14f;
    private float jumpingPower = 25f;
    private bool doubleJump;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 36f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private bool hasDashed = false;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform hands;

    public GameObject blinkPanel;

    private AudioSource src;
    [SerializeField] private AudioClip jump, dash, hurt, die;

    private float movement;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        src = GetComponent<AudioSource>();
        


    }
    //movement kodlarý aaabisi
    void Update()
    {
        if(isDashing)
        {
            return;
        }


        movement = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(movement));

        Flip();

        Invoke("ResetT", 5f);

        if (OtherMovement.Instance.StopControl())
        {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        }

        //if (Input.GetButtonDown("Jump"))
        //{
        //    animator.SetBool("isJumping", true);
        //    Debug.Log("Zýplama animasyonu tetiklendi!");
        //}

        //if (IsGrounded())
        //{
        //    animator.SetBool("isJumping", false);
        //}

        if(Input.GetKeyDown(KeyCode.X))
        {
            CommitSuicide();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            RR();
        }

    }



    public void Stop()
    {
        rb.velocity = new Vector2(0,0);
    }

    public void HandleJump()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Zýplama baþladý!");
            doubleJump = false;
            src.clip = jump;
            src.Play();
            animator.SetBool("isJumping", true);
        }
 
        
        if (IsGrounded() || doubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            doubleJump = !doubleJump;
            
        }

        if(IsGrounded())
        {
            Debug.Log("Yere deðdi!");
            animator.SetBool("isJumping", false);
        }

    }



    // yere deðme kontrolü
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayer);

    }


    //yönünü deðiþtiriyoruz (test edilmedi) (test edildi istenen sonucu vermedi düzeltildi)
    private void Flip()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Saða bak
        }
        else if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Sola bak
        }

        
    }

    private IEnumerator Dash()
    {
        if (IsGrounded())
        {
            hasDashed = false;

            canDash = false;
            isDashing = true;

            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), rb.velocity.y) * dashingPower;
            yield return new WaitForSeconds(dashingTime);

            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
            src.clip = dash;
            src.Play();
        }


        else if (!IsGrounded() && !hasDashed)
        {

            hasDashed = true;

            canDash = false;
            isDashing = true;

            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * dashingPower;
            yield return new WaitForSeconds(dashingTime);

            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            //canDash = true;

        }


        
    }

    public void DashC()
    {
        StartCoroutine(Dash());
    }

    public void Hitted(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        src.clip = hurt;
        src.Play();

        if (currentHealth <= 0)
        {
            Die();
        }

        Invoke("Blink", 0f);
        Invoke("UnBlink", 0.1f);
    }

    public void CommitSuicide()
    {
        currentHealth = 0;
        healthBar.SetHealth(currentHealth);
        Die();
    }

    public void RR()
    {
        SceneManager.LoadScene("Demo");
    }

    void Die()
    {
        Debug.Log("Öldün!");
        
        //ölüm animasyonu ve sesi buraya eklenecek 
        Destroy(gameObject, dieTime);
        src.clip = die;
        src.Play();
        FindObjectOfType<GameOverUI>().ShowLose();
    }


    public void ResetT()
    {
        animator.SetBool("Attack", false);
    }

    public void Blink()
    {
        blinkPanel.SetActive(true);
    }

    public void UnBlink()
    {
        blinkPanel.SetActive(false);
    }


}
