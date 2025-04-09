using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Transform player;

    public bool isFlipped = false;

    //private Animator anim;
    //private Rigidbody2D rb;

    //void Start()
    //{
    //    anim = GetComponent<Animator>();
    //    rb = GetComponent<Rigidbody2D>();
    //    anim.applyRootMotion = false; // Root Motion KAPALI
    //}

    //void Update()
    //{
    //    // 1. Dönme kontrolü (sorunsuz çalýþacak)
    //    float facingDirection = Mathf.Sign(player.position.x - transform.position.x);
    //    transform.localScale = new Vector3(facingDirection * -1.7071f, 1.7071f, 1.7071f);

        
    //}
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

}