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

	public static bool variablesAssigned = false;

	public static void AssignVariables(SOEnemy[] __enemies, int _sceneIndex, Vector3 _playerPosition, EnemySaveData[] _enemySaveData, bool _playerStartsTurn = true)
	{
		_enemies = __enemies;
		sceneIndex = _sceneIndex;
		playerPosition = _playerPosition;
		playerStartsTurn = _playerStartsTurn;
		enemySaveData = _enemySaveData;

		//Debug.Log(enemySaveData.Length);

		for (int i = 0; i < enemySaveData.Length; i++)
		{
			Debug.Log(enemySaveData[i].enemyType);
		}
		variablesAssigned = true;
	}

	//public static void Reset()
	//{
	//	enemies = new SOEnemy[0];
	//	sceneIndex = 0;
	//	playerPosition = new Vector3();
	//}
}