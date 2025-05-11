using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float bulletspeed = 20;
    public float endTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletspeed;
        Destroy(gameObject, endTime);


    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Movement player = collision.GetComponent<Movement>();


        if (player != null)
        {
            player.Hitted(80);

        }

        Debug.Log("Mermi þuna çarptý: " + collision.gameObject.name);

        Destroy(gameObject);
    }

}
