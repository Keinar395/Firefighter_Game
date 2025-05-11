using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; 
    [SerializeField]
    private GameObject eenemyPrefab;

    private float spawnTime = 5f; // Time between spawns
    private float sspawnTime = 7f; 
    void Start()
    {
        StartCoroutine(SpawnEnemy(spawnTime, enemyPrefab));
        StartCoroutine(SpawnEnemy(sspawnTime, eenemyPrefab));
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-33f, 33), Random.Range(-8f, 25), 0), Quaternion.identity);
        
        StartCoroutine(SpawnEnemy(interval, enemy));

    }
}
