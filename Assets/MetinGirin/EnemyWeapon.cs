using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Transform player;
    public Transform firePoint;
    public GameObject enemyBullet;
    Enemy enemy;

    private float shotCooldown;
    private float startShotCooldown = 1f;
    void Start()
    {
        shotCooldown = startShotCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        transform.up = direction;

        if(shotCooldown <= 0)
        {
            Instantiate(enemyBullet, firePoint.position, firePoint.rotation);
            shotCooldown = startShotCooldown;
        }
        else
        {
            shotCooldown -= Time.deltaTime;
        }
    }
}
