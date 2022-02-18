using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleSetupData
{
	public static SOEnemy[] enemies;
	public static int sceneIndex;
	public static Vector3 playerPosition;
	public static bool playerStartsTurn = true;

	public static bool variablesAssigned = false;

	public static void AssignVariables(SOEnemy[] _enemies, int _sceneIndex, Vector3 _playerPosition, bool _playerStartsTurn = true)
	{
		enemies = _enemies;
		sceneIndex = _sceneIndex;
		playerPosition = _playerPosition;
		playerStartsTurn = _playerStartsTurn;
		variablesAssigned = true;
	}

	//public static void Reset()
	//{
	//	enemies = new SOEnemy[0];
	//	sceneIndex = 0;
	//	playerPosition = new Vector3();
	//}
}
