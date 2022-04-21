using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState { PlayerTurn, PlayerPlantTurn, EnemyTurn, GameOver}
public class BattleSystem : MonoBehaviour
{
	public static BattleSystem instance { get; private set; }

	public BattleState state { get; set; }

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

	[field: Header("Battle Over Screen Refernces")]
	[field: SerializeField] public GameObject gameOverScreen { get; private set; }
	[field: SerializeField] public GameObject battleLostScreen { get; private set; }
	[field: SerializeField] public GameObject battleWonScreen { get; private set; }
	[field: SerializeField] public TMP_Text xpGainedText { get; private set; }
	[field: SerializeField] public TMP_Text moneyGainedText { get; private set; }


	[field: Header("Adjustements")]
	[field: SerializeField] public float walkSpeed { get; private set; } = 4;
	[field: Tooltip("The delay before switching from the player team to the enemy team and vise versa")]
	[field: SerializeField] public float DelayBeforeSwitchingTurn { get; private set; } = 2;
	[field: SerializeField] public float backToBlockAnimationDelay { get; private set; } = 1;
	[field: SerializeField] public float AttackDuration { get; private set; } = 1;
	[field: SerializeField] public float delayBeforeNextEnemyActionAfterBlocking { get; private set; } = 1;
	[field: SerializeField] public float defensePercentagePerDefensePoint { get; private set; } = 0.4f;
	[field: SerializeField] public float critPercentagePerCritPoint { get; private set; } = 0.2f;
	[field: SerializeField] public float baseCritChancePercantage { get; private set; } = 20;

	// Private
	[HideInInspector] public bool playerHasPlant = true;

	public int enemySelectionIndex = 0;

	// Rewards Given when battle won
	[HideInInspector] public int xpGiven;
	[HideInInspector] public int moneyGiven;
	private void Awake()
	{
		instance = this;
		SetupCombat();
		SetupRewards();
	}

	void SetupCombat()
	{
		// Clear Enemies
		ClearEnemies();

		// Assign New Enemies
		for (int i = 0; i < BattleSetupData._enemies.Length; i++)
		{
			enemies[i].SetupEnemy(BattleSetupData._enemies[i]);
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
			playerUnit.ChooseAction();
		}
		else
		{
			playerChoices.SetActive(false);
			enemiesAlive[0].ChooseAction();
		}
	}

	void SetupRewards()
	{
		foreach (EnemyUnit enemy in enemiesAlive)
		{
			xpGiven += enemy.enemySO.XPOnDeath;
			moneyGiven += enemy.enemySO.MoneyOnDeath;
		}
	}

	public IEnumerator SwitchTurn()
	{
		yield return new WaitForSeconds(DelayBeforeSwitchingTurn);
		if (state == BattleState.PlayerTurn || state == BattleState.PlayerPlantTurn)
		{
			playerChoices.SetActive(false);

			for (int i = 0; i < enemiesAlive.Count; i++)
			{
				enemiesAlive[i].ResetForNewRound();
			}
			yield return new WaitForSeconds(0.5f);
			enemiesAlive[0].ChooseAction();
		} 
		else if(state == BattleState.EnemyTurn)
		{
			playerUnit.ChooseAction();
			if(playerHasPlant)
			playerPlantUnit.ResetForNewRound();
		}


		if(state != BattleState.PlayerTurn && state != BattleState.EnemyTurn && state != BattleState.PlayerPlantTurn)
		{
			Debug.LogWarning("It's no ones turn");
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

	public void ContinueAfterWinning()
	{
		bool temp = GetComponent<PlayerLevel>().GiveXp(xpGiven);
		inventory.equippedPlantItem.plantSO.GiveXP(xpGiven);
		GetComponent<PlayerLevel>().GiveMoney(moneyGiven);

		if (temp == false)
			ChangeSceneAfterWinning();
	}
	public void ChangeSceneAfterWinning()
	{
		Statistics.IncreaseBattleWins();
		StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", BattleSetupData.sceneIndex, BattleSetupData.playerPosition));
	}

	private void OnValidate()
	{
		if(enemyParent != null)
		{
			enemies = enemyParent.GetComponentsInChildren<EnemyUnit>(true);
		}
	}
}
