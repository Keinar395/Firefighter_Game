using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform weapoon;
    public GameObject bullet;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            {
            shoot();
        }
    }
    void shoot()
    {
        Instantiate(bullet,weapoon.position,weapoon.rotation);
    }
}
