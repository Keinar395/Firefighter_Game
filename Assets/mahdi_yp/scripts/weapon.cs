using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public static weapon Instance { get; private set; }

    public Rigidbody2D rig;
    public Transform weapoon;
    public GameObject bullet;
    public float fireRate = 20;
    public float lastfire;

    private void Awake()
    {
        Instance = this;
    }

    
    public void shoot()
    {
        Instantiate(bullet,weapoon.position,weapoon.rotation);

        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());

    }
}
