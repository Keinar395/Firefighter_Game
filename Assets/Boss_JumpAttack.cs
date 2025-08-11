using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_JumpAttack : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    float waitTime = 1f; // bekleme süresi
    float timer;

    public float damageRadius = 2f; // hasar yarýçapý
    public int damageAmount = 20;   // verilecek hasar
    public LayerMask playerLayer;   // Player layer'ýný buraya atayacaksýn

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb.velocity = Vector2.zero; // Boss'u durdur
        timer = 0f; // sayaç baþlasýn
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        animator.applyRootMotion = false;


        // 1 saniye dolunca ýþýnlan
        if (timer >= waitTime)
        {
            rb.transform.position = player.position; // oyuncunun üstüne ýþýnlan

            // Etrafýna hasar ver
            Collider2D hit = Physics2D.OverlapCircle(rb.position, damageRadius, playerLayer);
            if (hit != null)
            {
                // Burada damage scriptine eriþip hasar uygula
                hit.GetComponent<Movement>()?.Hitted(damageAmount);
            }

            // State bitir
            animator.SetTrigger("JumpAttackFinished");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.transform.position = player.position;

        Collider2D hit = Physics2D.OverlapCircle(rb.position, damageRadius, playerLayer);
        if (hit != null)
        {
            hit.GetComponent<Movement>()?.Hitted(damageAmount);
        }
    }


    // Editor'de yarýçapý görmek için
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (rb != null)
            Gizmos.DrawWireSphere(rb.position, damageRadius);
    }
}
