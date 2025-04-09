using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_DashAttack : StateMachineBehaviour
{
    Rigidbody2D rb;
    Transform player;
    Vector2 dashDir;
    float dashSpeed = 10f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        dashDir = (player.position - rb.transform.position).normalized;

        rb.velocity = dashDir * dashSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Ýstersen dash süresi boyunca hasar verme, efekt vs. eklersin
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = Vector2.zero; // Dash bittiðinde durdur
    }
}
