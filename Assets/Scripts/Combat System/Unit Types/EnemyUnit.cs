using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyUnit : BaseUnit
{
	 [field: Header("Refernces")]
	 [field: SerializeField, ReadOnlyInspector] public SOEnemy enemySO { get; set; }

	[Space]

	[SerializeField, ReadOnlyInspector] int level = 1;

	ProgressBar healthBar;

	BaseUnit target;

	private void Start()
	{
		ScaleLevel();
	}


	public void SetupEnemy(SOEnemy _enemySO)
	{
		if (_enemySO == null) return;

		enemySO = _enemySO;

		unitName = enemySO.enemyName;
		name = unitName;
		gameObject.GetComponent<Animator>().runtimeAnimatorController = enemySO.chosenAnimatorController;

		healthBar = transform.GetChild(0).GetChild(0).GetComponent<ProgressBar>();

		gameObject.SetActive(true);
	}

	void ScaleLevel()
	{
		int playerLevel = PlayerStats.instance.GetComponent<PlayerLevel>().playerLevel;

		int enemyLevel;
		do
		{
			enemyLevel = Random.Range(playerLevel - 2, playerLevel + 2);
		} while (enemyLevel <= 0);

		level = enemyLevel;

		transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = "LV: " + level;

		// Scale Stats

		enemyLevel--;

        for (int i = 0; i < enemyLevel; i++)
        {
			enemySO.defaultHealth += Random.Range(enemySO.minHealthIncreasePerLevelUp, enemySO.maxHealthIncreasePerLevelUp + 1);
            enemySO.damage += Random.Range(enemySO.minDamageIncreasePerLevelUp, enemySO.maxDamageIncreasePerLevelUp + 1);
			enemySO.defense += Random.Range(enemySO.minDefenseIncreasePerLevelUp, enemySO.maxDefenseIncreasePerLevelUp + 1);
			enemySO.critChance += Random.Range(enemySO.minCritChanceIncreasePerLevelUp, enemySO.maxCritChanceIncreasePerLevelUp + 1);
		}

        maxHealth = enemySO.defaultHealth;
		currentHealth = maxHealth;
        damage = enemySO.damage;
        defense = enemySO.defense;
        critChance = enemySO.critChance;

		healthBar.max = maxHealth;
		healthBar.current = maxHealth;
	}

	public override void ChooseAction()
	{
		base.ChooseAction();

		battleSystem.state = BattleState.EnemyTurn;

		int temp = Random.Range(0, 5);

		if (temp <= 1)
			Block();
		else
			BasicAttack();
	}

	void BasicAttack()
	{
		ChooseTarget();

		locationToAttackTarget = target.transform.GetChild(2).position;
		anim.Play("Walk Animation");
		currentMode = CurrentMode.Attacking;
	}
	void ChooseTarget()
	{
		if (battleSystem.playerHasPlant)
		{
			int temp = Random.Range(0, 2);
			if (temp == 0)
				target = battleSystem.playerUnit;
			else
				target = battleSystem.playerPlantUnit;
		}
		else
		{
			target = battleSystem.playerUnit;
		}
	}
	protected override void OnReturnedToBasePosition()
	{
		base.OnReturnedToBasePosition();
		StartCoroutine(NextTurn());
	}
	public override IEnumerator NextTurn()
	{

		// If this unit is the last enemy to choose an action then go to player turn
		if (battleSystem.enemiesAlive[battleSystem.enemiesAlive.Count - 1] == this)
		{
			StartCoroutine(battleSystem.SwitchTurn());
		}
		else // If not then go to the next enemy
		{
			if(isBlocking)
			yield return new WaitForSeconds(battleSystem.delayBeforeNextEnemyActionAfterBlocking);

			for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
			{
				if (battleSystem.enemiesAlive[i] == this)
				{
					battleSystem.enemiesAlive[i + 1].ChooseAction();
				}
			}
		}
	}

	public override void TakeDamage(int _damage, bool isCritical)
	{
		base.TakeDamage(_damage, isCritical);
		healthBar.current = currentHealth;
	}

	protected override void Die()
	{
		base.Die();
		battleSystem.enemiesAlive.Remove(this);
	}

	protected override void OnAttack()
	{
		base.OnAttack();

		float modifiedDamage = GetAttackModifier();

		bool isCritical;

		if (modifiedDamage > damage)
			isCritical = true;
		else
			isCritical = false;

		target.TakeDamage((int)modifiedDamage, isCritical);
	}

	//protected override void OnValidate()
	//{
	//	if (enemySO == null)
	//	{
	//		gameObject.SetActive(false);
	//		name = "Unit";
	//	}
	//	else
	//	{
	//		gameObject.SetActive(true);
	//		SetupEnemy();
	//		base.OnValidate();
	//	}
	//}
}
