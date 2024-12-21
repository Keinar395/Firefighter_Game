using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int enmyDieTime = 3;
    int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // sonradan hurt anim ve ses eklenecek buraya
        if(currentHealth <= 0) 
        {
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log("düþman öldü");
        //ölüm animasyonu ve sesi buraya eklenecek 
        Destroy(gameObject, enmyDieTime);
    }
}
