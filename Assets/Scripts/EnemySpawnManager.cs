using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemySpawnManager : MonoBehaviour
{
	public static EnemySpawnManager instance { get; private set; }
	[field: SerializeField] public SOEnemy[] EnemySpawnPool { get; private set; }
	[field: SerializeField, ReadOnlyInspector] public List<GameObject> enemiesAlive { get; set; }
	[field: SerializeField, ReadOnlyInspector] public List<EnemySaveData> enemiesAliveSaveData { get; set; } = new List<EnemySaveData>();
	[field:SerializeField] public int MinSpawns { get; private set; } = 2;
	[field: SerializeField] public int MaxSpawns { get; private set; } = 5;

	[SerializeField] List<Transform> availableSpawnLocations;

	public int NumberOfSpawns { get; set; }
	public bool spawnEnemies { get; private set; } = true;

	int chosenNumberOfSpawns;

	GameManager gameManager;

	private void Awake()
	{
		instance = this;
		gameManager = GameManager.instance;
		if (BattleSetupData.enemySaveData.Length != 0)
		{
			spawnEnemies = false;
			SpawnEnemiesFromBeforeCombat();
		}

		foreach (Transform child in transform)
		{
			availableSpawnLocations.Add(child);
		}

		if (spawnEnemies)
		{
			chosenNumberOfSpawns = Random.Range(MinSpawns, MaxSpawns + 1);
			SpawnEnemies();
		}
	}

	void SpawnEnemies()
	{

			int chosenLocationIndex = Random.Range(0, availableSpawnLocations.Count);
			enemiesAlive.Add(gameManager.SpawnEnemy(EnemySpawnPool[Random.Range(0, EnemySpawnPool.Length)], availableSpawnLocations[chosenLocationIndex].position));
			availableSpawnLocations.RemoveAt(chosenLocationIndex);
			NumberOfSpawns++;

		if(NumberOfSpawns < chosenNumberOfSpawns)
			SpawnEnemies();
	}
	void SpawnEnemiesFromBeforeCombat()
	{
		Debug.Log("Spawning enemies from before");
		foreach (var enemy in BattleSetupData.enemySaveData)
		{
			enemiesAlive.Add(gameManager.SpawnEnemy(enemy.enemyType, enemy.spawnPosition));
		}
	}

	bool showSpawnLocations = true;
    float spawnLocationMarkerSize = 0.5f;
	private void OnDrawGizmos()
	{
		if (!showSpawnLocations) return;

		foreach (Transform child in transform)
		{
			Gizmos.DrawWireSphere(child.position, spawnLocationMarkerSize);
		}
	}
	#region Editor

#if UNITY_EDITOR
	[CustomEditor(typeof(EnemySpawnManager))]
	public class EnemySpawnManagerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			EnemySpawnManager enemySpawnManager = (EnemySpawnManager)target;

			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField("Show Spawn Locations", GUILayout.MaxWidth(280));
			enemySpawnManager.showSpawnLocations = EditorGUILayout.Toggle(enemySpawnManager.showSpawnLocations);

			EditorGUILayout.EndHorizontal();

			if (!enemySpawnManager.showSpawnLocations) return;
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField("Spawn Location Marker Size", GUILayout.MaxWidth(280));
			enemySpawnManager.spawnLocationMarkerSize = EditorGUILayout.FloatField(enemySpawnManager.spawnLocationMarkerSize);

			EditorGUILayout.EndHorizontal();
		}
	}
#endif
	#endregion
}
