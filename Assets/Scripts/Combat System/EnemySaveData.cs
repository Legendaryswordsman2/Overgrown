using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySaveData
{
	public SOEnemy enemyType;
	public Vector3 spawnPosition;

	public EnemySaveData(SOEnemy _enemyType, Vector3 _spawnPosition)
	{
		enemyType = _enemyType;
		spawnPosition = _spawnPosition;
	}
}
