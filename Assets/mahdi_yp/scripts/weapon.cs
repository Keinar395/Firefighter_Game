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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if(Time.time - lastfire > 1 /fireRate)
            {
                lastfire = Time.time;
                
            }
            shoot();
        }
    }
    public void shoot()
    {
        Instantiate(bullet,weapoon.position,weapoon.rotation);
    }
}
