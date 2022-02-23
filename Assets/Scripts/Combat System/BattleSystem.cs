using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { PlayerTurn, EnemyTurn}
public class BattleSystem : MonoBehaviour
{
	public static BattleSystem instance;

	BattleState state;

	[Header("Units")]
	[SerializeField, ReadOnlyInspector] EnemyUnit[] enemies;
	[ReadOnlyInspector] public List<EnemyUnit> enemiesAlive;
	[Space]
	public PlayerUnit playerUnit;
	public PlayerPlantUnit playerPlantUnit;

	[Header("Refernces")]
	[SerializeField] GameObject enemyParent;

	[field: SerializeField] public GameObject playerChoices { get; private set; }
	[field: SerializeField] public GameObject playerPlantChoices { get; private set; }
	[field: SerializeField] public GameObject arrowPrefab { get; private set; }

	[field: Header("Adjustements")]
	[field: SerializeField] public float DelayBetweenEachTurn { get; private set; } = 1;
	[field: SerializeField] public float WalkDuration { get; private set; } = 1;
	[field: SerializeField]	public float backToBlockAnimationDelay { get; private set; } = 1;
	[field: SerializeField] public float AttackDuration { get; private set; } = 1;

	// Private
	[HideInInspector] public bool playerHasPlant = false;


	private void Awake()
	{
		instance = this;
		SetupCombat();
	}

	void SetupCombat()
	{
		// Clear Enemies
		ClearEnemies();

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

		// Start First Turn

		if (BattleSetupData.playerStartsTurn)
		{
			state = BattleState.PlayerTurn;
			playerUnit.ChooseAction();
		}
		else
		{
			playerChoices.SetActive(false);
			state = BattleState.EnemyTurn;
			enemiesAlive[0].ChooseAction();
		}
	}

	public void SwitchTurn()
	{
		if(state == BattleState.PlayerTurn)
		{
			playerChoices.SetActive(false);
			state = BattleState.EnemyTurn;
			enemiesAlive[0].ChooseAction();
		} 
		else if(state == BattleState.EnemyTurn)
		{
			state = BattleState.PlayerTurn;
			playerUnit.ChooseAction();
		}
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
