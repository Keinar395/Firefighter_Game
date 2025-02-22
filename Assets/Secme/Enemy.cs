using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject pointA;
    public GameObject pointB;
    private float speed = 3f;
    private float chaseSpeed = 4f;
    private Rigidbody2D rb;
    private Transform currentPoint;

    public Transform player;
    public bool isChasing;
    private float distance = 10f;
    
    
    public int maxHealth = 100;
    public int enmyDieTime = 3;
    int currentHealth;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentPoint = pointB.transform;
    }

    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;

        if(isChasing)
        {
            if(transform.position.x > player.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.position += Vector3.left * chaseSpeed * Time.deltaTime;
            }

            if (transform.position.x < player.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.position += Vector3.right * chaseSpeed * Time.deltaTime;
            }
        }
        else
        {
            if(Vector2.Distance(transform.position, player.position) < distance)
            {
                isChasing = true;
            }
            
            
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                currentPoint = pointB.transform;
            }
        }

        

        Flip();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // sonradan hurt anim ve ses eklenecek buraya
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("düþman öldü");
        rb.velocity = Vector2.zero;
        //ölüm animasyonu ve sesi buraya eklenecek 
        Destroy(gameObject, enmyDieTime);
    }

    private void Flip()
    {
    
        if (currentPoint == pointB.transform || transform.position.x < player.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Saða bak
        }
        else if (currentPoint == pointA.transform || transform.position.x > player.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Sola bak
        }

    }
}
