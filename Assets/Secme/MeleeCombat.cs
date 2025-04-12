using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{

    public static MeleeCombat Instance { get; private set; }

    private Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleAttack();
    }


    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
        }
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); ;

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<Enemy>(out var e))
            {
                e.TakeDamage(attackDamage, 10, transform.position);
            }
            else if (enemy.TryGetComponent<BossHealth>(out var b))
            {
                b.TakeDamage(attackDamage, 10);
            }
        }

    }

    //public void AttackBoss()
    //{
    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); ;

    //    foreach (Collider2D boss in hitEnemies)
    //    {
    //        boss.GetComponent<BossHealth>().TakeDamage(attackDamage, 10);
    //    }

    //}
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
