using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { PlayerTurn, EnemyTurn, GameOver}
public class BattleSystem : MonoBehaviour
{
	public static BattleSystem instance;

	public BattleState state { get; private set; }

	[Header("Units")]
	[SerializeField, ReadOnlyInspector] EnemyUnit[] enemies;
	[ReadOnlyInspector] public List<EnemyUnit> enemiesAlive;
	[Space]
	public PlayerUnit playerUnit;
	public PlayerPlantUnit playerPlantUnit;

	[Header("Refernces")]
	[SerializeField] GameObject enemyParent;
	[field: SerializeField] public Inventory inventory { get; private set; }

	[field: SerializeField] public GameObject playerChoices { get; private set; }
	[field: SerializeField] public GameObject playerPlantChoices { get; private set; }
	[field: SerializeField] public GameObject arrowPrefab { get; private set; }
	[field: SerializeField] public GameObject gameOverScreen { get; private set; }
	[field: SerializeField] public GameObject battleLostScreen { get; private set; }
	[field: SerializeField] public GameObject battleWonScreen { get; private set; }

	[field: Header("Adjustements")]
	[field: SerializeField] public float walkSpeed { get; private set; } = 4;
	[field: Tooltip("The delay before switching from the player team to the enemy team and vise versa")]
	[field: SerializeField] public float DelayBeforeSwitchingTurn { get; private set; } = 2;
	[field: SerializeField] public float backToBlockAnimationDelay { get; private set; } = 1;
	[field: SerializeField] public float AttackDuration { get; private set; } = 1;
	[field: SerializeField] public float delayBeforeNextEnemyActionAfterBlocking { get; private set; } = 1;

	// Private
	[HideInInspector] public bool playerHasPlant = true;


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

		if(BattleSetupData.plantSO == null)
		{
			playerPlantUnit.gameObject.SetActive(false);
		}
		else
		{
			playerPlantUnit.plantSO = BattleSetupData.plantSO;
			playerPlantUnit.SetupPlant();
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

	public IEnumerator SwitchTurn()
	{
		yield return new WaitForSeconds(DelayBeforeSwitchingTurn);
		if (state == BattleState.PlayerTurn)
		{
			playerChoices.SetActive(false);
			state = BattleState.EnemyTurn;

			for (int i = 0; i < enemiesAlive.Count; i++)
			{
				enemiesAlive[i].ResetForNewRound();
			}

			enemiesAlive[0].ChooseAction();
		} 
		else if(state == BattleState.EnemyTurn)
		{
			state = BattleState.PlayerTurn;
			playerUnit.ChooseAction();
			if(playerHasPlant)
			playerPlantUnit.ResetForNewRound();
		}


		if(state != BattleState.PlayerTurn && state != BattleState.EnemyTurn)
		{
			Debug.LogWarning("Its no ones turn");
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
