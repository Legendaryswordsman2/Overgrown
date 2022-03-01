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
		SpawnEnemy();
	}

	void SpawnEnemy()
	{
		gameManager.SpawnEnemy(enemySpawnPool[Random.Range(0, enemySpawnPool.Length)], transform.position);
	}
}
