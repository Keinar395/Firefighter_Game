using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public static Movement Instance { get; private set; }
    
    
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


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    //movement kodlarý aaabisi
    void Update()
    {

        movement = Input.GetAxisRaw("Horizontal");

        HandleJump();

        Flip();
    }

    void FixedUpdate()
    {
        Stop();
        
    }

    public void Stop()
    {
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
    }

    public void HandleJump()
    {
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
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
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


}
