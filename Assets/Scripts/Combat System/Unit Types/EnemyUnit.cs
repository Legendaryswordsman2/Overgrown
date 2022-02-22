using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : BaseUnit
{
	 [field: Header("Refernces")]
	 [field: SerializeField] public SOEnemy enemySO { get; set; }

	ProgressBar healthBar;

	public void SetupEnemy()
	{
		unitName = enemySO.enemyName;
		maxHealth = enemySO.defaultHealth;
		damage = enemySO.attackDamage;
		gameObject.GetComponent<Animator>().runtimeAnimatorController = enemySO.animatorController;

		healthBar = transform.GetChild(0).GetChild(0).GetComponent<ProgressBar>();

		healthBar.maximum = maxHealth;
		healthBar.current = maxHealth;

		gameObject.SetActive(true);
	}

	public override void ChooseAction()
	{
		BasicAttack();
	}

	void BasicAttack()
	{
		locationToAttackTarget = battleSystem.playerUnit.transform.GetChild(2).position;
		anim.Play("Walk Animation");
		currentMode = CurrentMode.Attacking;
	}
	protected override void OnReturnedToBasePosition()
	{
		base.OnReturnedToBasePosition();
		StartCoroutine(NextTurn());
	}

	IEnumerator NextTurn()
	{
		print("Next Turn");
		yield return new WaitForSeconds(battleSystem.DelayBetweenEachTurn);

		// If this unit is the last enemy to choose an action then go to player turn
		if (battleSystem.enemiesAlive[battleSystem.enemiesAlive.Count - 1] == this)
		{
			battleSystem.SwitchTurn();
		}
		else // If not then go to the next enemy
		{
			for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
			{
				if (battleSystem.enemiesAlive[i] == this)
				{
					battleSystem.enemiesAlive[i + 1].ChooseAction();
				}
			}
		}
	}

	public override void TakeDamage(int _damage)
	{
		base.TakeDamage(_damage);
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
		battleSystem.playerUnit.TakeDamage(damage);
	}

	protected override void OnValidate()
	{
		if (enemySO == null)
		{
			gameObject.SetActive(false);
			name = "Unit";
		} 
		else
		{
			gameObject.SetActive(true);
			SetupEnemy();
			base.OnValidate();
		}
	}
}
