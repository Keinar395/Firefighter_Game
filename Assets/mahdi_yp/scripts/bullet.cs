using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float bulletspeed;
    public float endTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletspeed;
        Destroy(gameObject, endTime);
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if(enemy != null)
        {
            enemy.TakeDamage(20, transform.position);

        }

        Destroy(gameObject);
    }

}
