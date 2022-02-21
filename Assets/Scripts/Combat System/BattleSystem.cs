using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
	public static BattleSystem instance;

	[Header("Units")]
	[SerializeField, ReadOnlyInspector] EnemyUnit[] enemies;
	[ReadOnlyInspector] public List<EnemyUnit> enemiesAlive;

	[Header("Refernces")]
	[SerializeField] GameObject enemyParent;

	[field: SerializeField] public GameObject playerChoices { get; private set; }

	[field: Header("Adjustements")]
    [field: SerializeField]	public float backToBlockAnimationDelay { get; private set; } = 1;

	private void Awake()
	{
		instance = this;

		SetupCombat();
	}
	private void Start()
	{
		SetupCombatStepTwo();
	}

	void SetupCombat()
	{
		// Clear Enemies
		//ClearEnemies();

		// Assign New Enemies
		for (int i = 0; i < BattleSetupData._enemies.Length; i++)
		{
			enemies[i].enemySO = BattleSetupData._enemies[i];
			enemies[i].SetupEnemy();
		}

		EnemyUnit[] tempActiveEnemies = enemyParent.GetComponentsInChildren<EnemyUnit>();

		for (int i = 0; i < tempActiveEnemies.Length; i++)
		{
			enemiesAlive.Add(tempActiveEnemies[i]);
		}
	}
	void SetupCombatStepTwo()
	{
		// Put New Enemies In A List
		//EnemyUnit[] tempActiveEnemies = enemyParent.GetComponentsInChildren<EnemyUnit>();

		//for (int i = 0; i < tempActiveEnemies.Length; i++)
		//{
		//	enemiesAlive.Add(tempActiveEnemies[i]);
		//}
	}
	void ClearEnemies()
	{
		for (int i = 0; i < enemies.Length; i++)
		{
			enemies[i].enemySO = null;
			enemies[i].gameObject.SetActive(false);
		}
	}

	private void OnValidate()
	{
		if(enemyParent != null)
		{
			enemies = enemyParent.GetComponentsInChildren<EnemyUnit>(true);
		}
	}
}
