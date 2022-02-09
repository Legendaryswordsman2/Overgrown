using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : BaseUnit
{
	[Header("References")]
	[SerializeField] BattleHUD playerHud;
	[HideInInspector] public bool awaitingTargetToAttack = false;

	BattleSystem battleSystem;

	bool isAttacking;
	bool isReturningToBasePOS;

	// Lerping
	[Min(0)]
	public float walkSpeed = 1f;
	float elapsedTime;

	Vector3 basePosition;
	Transform locationToAttackEnemy;
	protected override void Awake()
	{
		basePosition = transform.position;
		SetupBattleStepOne();
		base.Awake();
		SetupBattleStepTwo();
	}
	private void Update()
	{
		if (awaitingTargetToAttack)
		{

			if (Input.GetKeyDown(KeyCode.Return))
			{
				Attack();
			}

			if (Input.GetKeyDown(KeyCode.W))
			{
				if (battleSystem.enemySelectionIndex <= 0) return;
				battleSystem.enemySelectionIndex--;

				for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
				{
					battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
				}
				battleSystem.enemiesAlive[battleSystem.enemySelectionIndex].transform.GetChild(1).gameObject.SetActive(true);

				Debug.Log(battleSystem.enemySelectionIndex);
			}

			if (Input.GetKeyDown(KeyCode.S))
			{
				if (battleSystem.enemySelectionIndex >= battleSystem.enemiesAlive.Count - 1) return;
				battleSystem.enemySelectionIndex++;

				for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
				{
					battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
				}
				battleSystem.enemiesAlive[battleSystem.enemySelectionIndex].transform.GetChild(1).gameObject.SetActive(true);

				Debug.Log(battleSystem.enemySelectionIndex);
			}
		}

		if (isAttacking)
		{
			elapsedTime += Time.deltaTime;
			float percentageComplete = elapsedTime / walkSpeed;

			transform.position = Vector3.Lerp(basePosition, locationToAttackEnemy.position, percentageComplete);

			if (transform.position == locationToAttackEnemy.position)
			{
				isAttacking = false;
				anim.Play("Attack Animation");
				battleSystem.enemiesAlive[battleSystem.enemySelectionIndex].TakeDamage(damage);
				StartCoroutine(ReturnToBasePOS());
			}
		}

		if (isReturningToBasePOS)
		{
			elapsedTime += Time.deltaTime;
			float percentageComplete = elapsedTime / walkSpeed;

			transform.position = Vector3.Lerp(locationToAttackEnemy.position, basePosition, percentageComplete);

			if (transform.position == basePosition)
			{
				isReturningToBasePOS = false;
				GetComponent<SpriteRenderer>().flipX = true;
				anim.Play("Idle Animation");
				StartCoroutine(battleSystem.EnemyTurn());
			}
		}
	}

	void Attack()
	{
		awaitingTargetToAttack = false;

		for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
		{
			battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
		}

		locationToAttackEnemy = battleSystem.enemiesAlive[battleSystem.enemySelectionIndex].transform.GetChild(2);
		elapsedTime = 0;
		anim.Play("Walk Animation");
		isAttacking = true;
		//enemiesAlive[tempSelectionIndex].TakeDamage(playerUnit.damage);
		//StartCoroutine(EnemyTurn());
	}

	void RangedAttack()
	{

	}

	IEnumerator ReturnToBasePOS()
	{
		elapsedTime = 0;
		yield return new WaitForSeconds(1);
		GetComponent<SpriteRenderer>().flipX = false;
		isReturningToBasePOS = true;
		anim.Play("Walk Animation");
	}

	void SetupBattleStepOne()
	{
		battleSystem = BattleSystem.instance;
		PlayerHealthData health = SaveSystem.LoadFile<PlayerHealthData>("/Player/PlayerHealth.json");
		//maxHealth = health.playerHealth;
	}
	void SetupBattleStepTwo()
	{
		playerHud.SetHUD(this);
	}
	public override void ChooseAction()
	{
		base.ChooseAction();

		// When the battle first starts if the player starts the turn then the anim won't be set since battle system is called before base unit
		if(anim != null)
		anim.Play("Idle Animation");

		BattleSystem.instance.state = battleState.PlayerTurn;
		BattleSystem.instance.playerChoices.SetActive(true);
	}

	public void SelectEnemyToAttack()
	{
		BattleSystem.instance.playerChoices.SetActive(false);

		if (battleSystem.enemySelectionIndex < 0) battleSystem.enemySelectionIndex = 0;

		Debug.Log(battleSystem.enemySelectionIndex);
		Debug.Log(battleSystem.enemiesAlive.Count - 1);

		if (battleSystem.enemySelectionIndex > battleSystem.enemiesAlive.Count - 1) battleSystem.enemySelectionIndex = battleSystem.enemiesAlive.Count - 1;

		awaitingTargetToAttack = true;
		BattleSystem.instance.enemiesAlive[battleSystem.enemySelectionIndex].transform.GetChild(1).gameObject.SetActive(true);
	}

	public override void Block()
	{
		BattleSystem.instance.playerChoices.SetActive(false);
		base.Block();
		StartCoroutine(BattleSystem.instance.EnemyTurn());
	}

	public override void TakeDamage(int damage)
	{
		base.TakeDamage(damage);
		if(currentHealth >= 0)
		{
		playerHud.SetHealth(currentHealth);
		}
		else
		{
			playerHud.SetHealth(0);
		}
	}
	protected override void Die()
	{
		StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", "Title"));
	}

	public void EscapeBattle()
	{
		StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", BattleData.sceneIndex, BattleData.playerPosition));
	}
}
