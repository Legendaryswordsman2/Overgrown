using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum battleState { Start, PlayerTurn, EnemyTurn, Won, Lost}

public class BattleSystem : MonoBehaviour
{
	public static BattleSystem instance;

	[field: Header("References")]
	[field: SerializeField] public GameObject arrowPrefab { get; private set; }
	[field: SerializeField] public GameObject playerChoices { get; private set; }
	[field: SerializeField] public GameObject playerPlantChoices { get; private set; }
	[field: SerializeField] public CombatInventory inventory { get; private set; }

	[field: SerializeField] public PlayerUnit playerUnit { get; private set; }
	[field: SerializeField] public PlayerPlantUnit playerPlantUnit { get; private set; }

	[SerializeField] GameObject enemiesParent;
	[SerializeField, ReadOnlyInspector] EnemyUnit[] _enemies;
	[ReadOnlyInspector] public List<EnemyUnit> enemiesAlive;

	public battleState state;
	[HideInInspector] public int enemySelectionIndex = 0;

	// Lerping
	[Min(0)]
	public float walkSpeed = 1;
	float elapsedTime;
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
		state = battleState.Start;
		ClearEnemies();
		for (int i = 0; i < BattleData.enemies.Length; i++)
		{
			_enemies[i].enemySO = BattleData.enemies[i];
			_enemies[i].Setup();
		}
	}
	void SetupCombatStepTwo()
	{
		EnemyUnit[] tempActiveEnemies = enemiesParent.GetComponentsInChildren<EnemyUnit>();

		enemiesAlive.Clear();
		for (int i = 0; i < tempActiveEnemies.Length; i++)
		{
			enemiesAlive.Add(tempActiveEnemies[i]);
		}

		if (BattleData.playerStartsTurn)
			PlayerTurn();
		else
			StartCoroutine(EnemyTurn());

	}
	public void PlayerTurn()
	{
		playerUnit.ChooseAction();
	}
	public IEnumerator EnemyTurn()
	{
		if(enemiesAlive.Count == 0)
		{
			state = battleState.Won;
			EndBattle();
		}
		state = battleState.EnemyTurn;
		playerChoices.SetActive(false);
		playerPlantChoices.SetActive(false);

		for (int i = 0; i < enemiesAlive.Count; i++)
		{
			enemiesAlive[i].ResetForNewRound();
		}
		Debug.Log("Enemy Turn");
		yield return new WaitForSeconds(2);
		enemiesAlive[0].ChooseAction();
	}

	void EndBattle()
	{
		if(state == battleState.Won)
		{
			Debug.Log("PLAYER WON THE BATTLE!");
			StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", BattleData.sceneIndex, BattleData.playerPosition));
		}
		if(state == battleState.Lost)
		{
			LevelLoader.instance.LoadLevel("Title", "Battle");
		}
	}


	void ClearEnemies()
	{
		for (int i = 0; i < _enemies.Length; i++)
		{
			_enemies[i].enemySO = null;
			_enemies[i].gameObject.SetActive(false);
		}
	}
	private void OnValidate()
	{
		if(enemiesParent != null)
		{
			_enemies = enemiesParent.GetComponentsInChildren<EnemyUnit>(true);
		}
	}
}