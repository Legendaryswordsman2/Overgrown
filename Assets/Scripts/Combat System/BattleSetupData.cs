using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleSetupData
{
	public static SOEnemy[] _enemies;
	public static int sceneIndex;
	public static Vector3 playerPosition;
	public static bool playerStartsTurn = true;
	public static EnemySaveData[] enemySaveData = new EnemySaveData[0];

	public static int minEnemyLevel;
	public static int maxEnemyLevel;

	// After Combat
	public static int xpToAwardPlayer;

	public static void AssignVariables(SOEnemy[] __enemies, int _sceneIndex, Vector3 _playerPosition, EnemySaveData[] _enemySaveData, bool _playerStartsTurn, int _minEnemyLevel, int _maxEnemyLevel)
	{
		_enemies = __enemies;
		sceneIndex = _sceneIndex;
		playerPosition = _playerPosition;
		playerStartsTurn = _playerStartsTurn;
		enemySaveData = _enemySaveData;

		minEnemyLevel = _minEnemyLevel;
		maxEnemyLevel = _maxEnemyLevel;
	}
}