using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    public HealthBar healthBar;
    public PostureBar postureBar;

    


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
    }

    public void TakeDamage(int damage, int pdamage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        healthBar.SetHealth(currentHealth);
        postureBar.AddPosture(pdamage);

        if (health <= 0)
        {
            Die();
        }

        if (posture >= 100)
        {
            postureBar.SetPosture(100);
            //Boss_Run.speed = 0;
            currentHealth -= damage * 10;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}