using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float bulletspeed = 30;
    public float endTime = 2;

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

        BossHealth boss = collision.GetComponent<BossHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(20, 40, transform.position);
        }

        if (boss != null)
        {
            boss.TakeDamage(20, 40);
        }

        Destroy(gameObject);
    }

}
