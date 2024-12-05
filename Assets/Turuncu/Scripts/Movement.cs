using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    //movement kodlarý aaabisi
    void Update()
    {

        movement = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) 
        {
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * 0.5f);
        }
        Flip();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
    }
    // yere deðme kontrolü
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }


    //yönünü deðiþtiriyoruz (test edilmedi)
    private void Flip()
    {
        if (isFacingRight && movement < 0f || !isFacingRight && movement > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            
        }

    }

}
