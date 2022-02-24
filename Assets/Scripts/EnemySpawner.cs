using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] SOEnemy[] enemySpawnPool;

	GameManager gameManager;

	private void Awake()
	{
		gameManager = GameManager.instance;
		int temp = Random.Range(0, 11);

			SpawnEnemy();
		if(temp <= 3)
		{
		}
	}

	void SpawnEnemy()
	{
		gameManager.SpawnEnemy(enemySpawnPool[Random.Range(0, enemySpawnPool.Length)], transform.position);
	}
}
