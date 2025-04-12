using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    public HealthBar healthBar;
    public PostureBar postureBar;

    public Animator animator;

    


    public int health = 500;
    int currentHealth;
    public int posture = 0;
    public bool isInvulnerable = false;

    void Start()
    {
        currentHealth = health;
        healthBar.SetMaxHealth(health);
        postureBar.SetMaxPosture(100);
        postureBar.SetPosture(0);

        Boss_Run boss_Run = GetComponent<Boss_Run>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage, int pdamage)
    {
        if (isInvulnerable)
            return;

        currentHealth -= damage;
        posture += pdamage;

        healthBar.SetHealth(currentHealth);
        postureBar.AddPosture(pdamage);

        if (currentHealth <= 0)
        {
            Die();
        }

        if (posture >= 100)
        {
            Invoke("PostureBroken", 0.5f);
            Invoke("PostureNotBroken", 5);
        }
    }

    public void PostureBroken()
    {
        postureBar.SetPosture(100);
        animator.SetBool("Stun", true);
        
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
    }

}