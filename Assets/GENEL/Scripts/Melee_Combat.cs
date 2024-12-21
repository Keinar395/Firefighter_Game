using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Combat : MonoBehaviour
{
    private Animator animator;
    private bool hasDrawn = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //k�l�c� �ekmek i�in f ye bas!
        if (Input.GetKeyDown(KeyCode.F) && !hasDrawn)
        {
            animator.SetTrigger("DrawSwordTrigger");
            hasDrawn = true;
            //k ye basarsan basit vuru� yapar 

            if (Input.GetKeyUp(KeyCode.K))
            {
                Attack();
            }
        }
    }
    public void ResetDraw()
    {
        hasDrawn = false;
    }
    void Attack()
    {
        //d��manlar� tespit et
    }
}
