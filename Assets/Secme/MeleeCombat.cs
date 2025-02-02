using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    private Animator animator;
    private bool hasDrawn = false;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleSwordDraw();
        HandleAttack();
    }


    private void HandleSwordDraw()
    {
        if (Input.GetKeyDown(KeyCode.F) && !hasDrawn)
        {
            Debug.Log("F tuþuna basýldý, kýlýç çekiliyor...");
            //animator.SetBool("HasDrawn", true);
            hasDrawn = true;
        }
    }


    private void HandleAttack()
    {
        if (hasDrawn && Input.GetKeyDown(KeyCode.K))
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Placeholder for attack logic (e.g., detecting and damaging enemies).
        Debug.Log("Performed attack!");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); ;

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
