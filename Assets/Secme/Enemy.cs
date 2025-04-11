using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isRanged = false;

    public GameObject pointA;
    public GameObject pointB;
    private float speed = 3f;
    private float chaseSpeed = 4f;
    private Rigidbody2D rb;
    private Transform currentPoint;

    public Transform player;
    public bool isChasing;
    private float distance = 10f;

    private bool isKnockedBack = false; // Knockback sýrasýnda kontrolü durdurmak için

    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.2f; // Knockback süresi

    public HealthBar healthBar;
    public PostureBar postureBar;
    public GameObject enemyWeapon;

    public int maxHealth = 100;
    public int enmyDieTime = 3;
    int currentHealth;
    public int posture = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        postureBar.SetMaxPosture(100);
        postureBar.SetPosture(0);
        currentPoint = pointB.transform;
    }

    void Update()
    {
        if (isChasing)
        {
            if (transform.position.x > player.position.x)
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
            if (Vector2.Distance(transform.position, player.position) < distance)
            {
                if(isRanged)
                {
                    speed = 0;
                }
                else
                {
                    isChasing = true;
                }
                
            }


            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == pointB.transform)
            {
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == pointA.transform)
            {
                currentPoint = pointB.transform;
            }
        }









        Flip();
    }

    public void TakeDamage(int damage, int pdamage, Vector2 attackPosition)
    {
        currentHealth -= damage;
        posture += pdamage;

        healthBar.SetHealth(currentHealth);
        postureBar.AddPosture(pdamage);
        
        // sonradan hurt anim ve ses eklenecek buraya
        if (currentHealth <= 0)
        {
            Die();
        }

        if(posture >= 100)
        {
            Invoke("PostureBroken", 0.5f);
            Invoke("PostureNotBroken", 5);
            currentHealth -= damage * 10;
        }

        if (!isKnockedBack)
        {
            StartCoroutine(KnockbackRoutine(attackPosition));
        }

    }

    public void PostureBroken()
    {
        postureBar.SetPosture(100);
        speed = 0;
        chaseSpeed = 0;
        enemyWeapon.SetActive(false);
        isKnockedBack = true;
    }

    public void PostureNotBroken()
    {
        postureBar.SetPosture(0);
        posture = 0;
        speed = 3f;
        chaseSpeed = 4f;
        enemyWeapon.SetActive(true);
        isKnockedBack = false;
    }

    private IEnumerator KnockbackRoutine(Vector2 attackPosition)
    {
        isKnockedBack = true;

        // Hasar aldýðý noktayla düþman arasýndaki yönü bul
        Vector2 knockbackDirection = (transform.position - (Vector3)attackPosition).normalized;

        // Knockback kuvvetini uygula
        rb.velocity = knockbackDirection * knockbackForce;

        // Belirtilen süre kadar bekle
        yield return new WaitForSeconds(knockbackDuration);

        // Knockback bitti, düþman tekrar kontrol edilebilir
        isKnockedBack = false;
        rb.velocity = Vector2.zero; // Knockback'ten sonra durmasý için
    }

    void Die()
    {
        Debug.Log("düþman öldü");
        speed = 0;
        chaseSpeed = 0;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Movement player = collision.gameObject.GetComponent<Movement>(); 

            if(player != null)
            {
                player.Hitted(100);
            }
        }
    }
}
