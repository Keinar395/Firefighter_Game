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

        //kýlýcý çekmek için f ye bas!
        if (Input.GetKeyDown(KeyCode.F) && !hasDrawn)
        {
            animator.SetTrigger("DrawSwordTrigger");
            hasDrawn = true;
            //k ye basarsan basit vuruþ yapar 

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
        //düþmanlarý tespit et
    }
}
