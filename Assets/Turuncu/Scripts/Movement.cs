using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public static Movement Instance { get; private set; }

    private int maxHealth = 1000;
    private int currentHealth;
    private int dieTime = 2;

    private float speed = 7f;
    private float jumpingPower = 20f;
    private bool doubleJump;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Transform hands;

    private float movement;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }
    //movement kodlarý aaabisi
    void Update()
    {
        if(isDashing)
        {
            return;
        }


        movement = Input.GetAxisRaw("Horizontal");

        Flip();

        

        if(OtherMovement.Instance.StopControl())
        {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
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
            doubleJump = false;
        }
 
        
        if (IsGrounded() || doubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            doubleJump = !doubleJump;
        }

    }



    // yere deðme kontrolü
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

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
        canDash = false;
        isDashing = true;

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * dashingPower;
        yield return new WaitForSeconds(dashingTime);

        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void DashC()
    {
        StartCoroutine(Dash());
    }

    public void Hitted(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Öldün!");
        
        //ölüm animasyonu ve sesi buraya eklenecek 
        Destroy(gameObject, dieTime);
    }


}
