using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	GameManager gameManager;
	EnemySpawnManager enemySpawnManager;

	private void Awake()
	{
		gameManager = GameManager.instance;
		enemySpawnManager = transform.GetComponentInParent<EnemySpawnManager>();
	}

	private void Start()
	{
		SpawnEnemy();
	}

	void SpawnEnemy()
	{
		if (!enemySpawnManager.spawnEnemies) return;

		if (enemySpawnManager.NumberOfSpawns >= enemySpawnManager.MaxSpawns) return;

		int temp = Random.Range(0, 4);

		if(temp == 0 || enemySpawnManager.NumberOfSpawns < enemySpawnManager.MinSpawns)
		{
			enemySpawnManager.enemiesAlive.Add(gameManager.SpawnEnemy(enemySpawnManager.EnemySpawnPool[Random.Range(0, enemySpawnManager.EnemySpawnPool.Length)], transform.position));
			enemySpawnManager.NumberOfSpawns++;
		}

	}
}
