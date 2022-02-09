using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    Transform leftSpawner, rightSpawner;

    [SerializeField]
    GameObject enemyPrefab;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3);
        if(Random.Range(0, 2) == 0)
        {
            Instantiate(enemyPrefab, leftSpawner);
        }
        else
        {
            Instantiate(enemyPrefab, rightSpawner);
        }
        StartCoroutine(SpawnEnemy());
    }
}
