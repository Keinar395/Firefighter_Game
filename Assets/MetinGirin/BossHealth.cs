using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    public HealthBar healthBar;
    public PostureBar postureBar;

    public Animator animator;

    [SerializeField] private AudioSource src;
    [SerializeField] private AudioClip bhurt, bdie, posturebroken;


    public int health = 500;
    int currentHealth;
    public int posture = 0;
    public bool isInvulnerable = false;

    void Start()
    {
        currentHealth = health;
        healthBar.SetMaxHealth(health);
        postureBar.SetMaxPosture(500);
        postureBar.SetPosture(0);

        Boss_Run boss_Run = GetComponent<Boss_Run>();
        animator = GetComponent<Animator>();
        src = GetComponent<AudioSource>();

    }

    public void TakeDamage(int damage, int pdamage)
    {
        if (isInvulnerable)
            return;

        currentHealth -= damage;
        posture += pdamage;

        if (src == null)
        {
            Debug.LogError("AudioSource yok!");
        }

        if (bhurt == null)
        {
            Debug.LogError("bhurt sesi atanmadý!");
        }

        src.clip = bhurt;
        src.Play();

        healthBar.SetHealth(currentHealth);
        postureBar.AddPosture(pdamage);

        if (currentHealth <= 0)
        {
            Die();
        }

        if (posture >= 500)
        {
            Invoke("PostureBroken", 0.5f);
            Invoke("PostureNotBroken", 5);
        }
    }

    public void PostureBroken()
    {
        postureBar.SetPosture(500);
        animator.SetBool("Stun", true);
        src.clip = posturebroken;
        src.Play();

    }

    public void PostureNotBroken()
    {
        postureBar.SetPosture(0);
        posture = 0;
        animator.SetBool("Stun", false);  
        
    }

    void Die()
    {
        Destroy(gameObject);
        src.clip = bdie;
        src.Play();
    }

}