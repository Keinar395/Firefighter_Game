using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{

    public float speed = 2.5f;
    public float attackRange = 3f;
    public float dashAttackRange = 7f;
    public float dashAttackRange_ = 7.5f;

    Transform player;
    Rigidbody2D rb;
    Boss boss;
    BossHealth health;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        health = animator.GetComponent<BossHealth>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();


        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }

        if (Vector2.Distance(player.position, rb.position) <= dashAttackRange_ && Vector2.Distance(player.position, rb.position) >= dashAttackRange)
        {
            animator.SetTrigger("DashAttack");
        }

        //if(health.posture >= 100)
        //{
        //    health.PostureBroken();
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("DashAttack");
    }
    
    //public void PostureBroken(Animator animator)
    //{
    //    health.postureBar.SetPosture(100);
    //    animator.SetBool("Stun", true);

    //}

    //public void PostureNotBroken(Animator animator)
    //{
    //    health.postureBar.SetPosture(0);
    //    health.posture = 0;
    //    animator.SetBool("Stun", false);

    //}

}