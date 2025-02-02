using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool doubleJump;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Transform hands;

    private float movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    //movement kodlar� aaabisi
    void Update()
    {


        movement = Input.GetAxisRaw("Horizontal");

        //Yukar� bas�nca yukar� bakmas� a�a�� bas�nca a�a�o bakmas� falan fi�man i�ler
        if(Input.GetKeyDown(KeyCode.W))
        {
            hands.transform.eulerAngles = Vector3.forward * 90;
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            hands.transform.eulerAngles = Vector3.forward * -90;
        }

        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            hands.transform.eulerAngles = Vector3.forward * 45;
        }

        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            hands.transform.eulerAngles = Vector3.forward * 135; //SPRITE HATALI
        }

        if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            hands.transform.eulerAngles = Vector3.forward * -45;
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            hands.transform.eulerAngles = Vector3.forward * -135;  //SPRITE HATALI
        }

        // D�zeltme i�i
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            hands.transform.eulerAngles = transform.eulerAngles;
        }
       



        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                doubleJump = !doubleJump;
            }
            
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) 
        {
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    void FixedUpdate()
    {
        if(!Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        }
        
    }
    // yere de�me kontrol�
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }


    //y�n�n� de�i�tiriyoruz (test edilmedi) (test edildi istenen sonucu vermedi d�zeltildi)
    private void Flip()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Sa�a bak
        }
        else if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Sola bak
        }

    }


}
