using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlantUnit : BaseUnit
{
	[SerializeField] CombatInfoHUD playerPlantHUD;

	AttackType attackType;

	int enemySelectionIndex = 0;

	public EquipablePlantItem plantSO;

	public void SetupPlant()
	{
		if (plantSO == null) return;

		unitName = plantSO.unitName;
		maxHealth = plantSO.defaultHealth;
		damage = plantSO.attackDamage;
		gameObject.GetComponent<Animator>().runtimeAnimatorController = plantSO.animatorController;

		gameObject.SetActive(true);
	}

	protected override void Update()
	{
		base.Update();

		if (currentMode == CurrentMode.AwaitingTargetToAttack)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				currentMode = CurrentMode.Null;
				if (attackType == AttackType.Basic)
					BasicAttack();
				//else
				//	RangedAttack();
			}

			if (Input.GetKeyDown(KeyCode.W))
			{
				if (enemySelectionIndex <= 0) return;
				enemySelectionIndex--;

				for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
				{
					battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
				}
				battleSystem.enemiesAlive[enemySelectionIndex].transform.GetChild(1).gameObject.SetActive(true);
			}

			if (Input.GetKeyDown(KeyCode.S))
			{
				if (enemySelectionIndex >= battleSystem.enemiesAlive.Count - 1) return;
				enemySelectionIndex++;

				for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
				{
					battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
				}
				battleSystem.enemiesAlive[enemySelectionIndex].transform.GetChild(1).gameObject.SetActive(true);
			}
		}
	}
	public override void ChooseAction()
	{
		if (anim != null)
			anim.Play("Idle Animation");
		battleSystem.playerPlantChoices.SetActive(true);
	}
	public void SelectTargetToAttack(bool isBasicAttack)
	{
		if (isBasicAttack)
			attackType = AttackType.Basic;
		else
			attackType = AttackType.Ranged;

		battleSystem.playerPlantChoices.SetActive(false);

		if (enemySelectionIndex < 0) enemySelectionIndex = 0;

		if (enemySelectionIndex > battleSystem.enemiesAlive.Count - 1) enemySelectionIndex = battleSystem.enemiesAlive.Count - 1;

		battleSystem.enemiesAlive[enemySelectionIndex].transform.GetChild(1).gameObject.SetActive(true);

		currentMode = CurrentMode.AwaitingTargetToAttack;
	}
	void BasicAttack()
	{
		for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
		{
			battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
		}

		locationToAttackTarget = battleSystem.enemiesAlive[enemySelectionIndex].transform.GetChild(2).position;
		anim.Play("Walk Animation");
		currentMode = CurrentMode.Attacking;
	}
	public override void TakeDamage(int _damage)
	{
		base.TakeDamage(_damage);
		if (currentHealth >= 0)
		{
			playerPlantHUD.SetHealth(currentHealth);
		}
		else
		{
			playerPlantHUD.SetHealth(0);
		}
	}
	protected override void Die()
	{
		base.Die();
		battleSystem.playerHasPlant = false;
	}
	protected override void OnAttack()
	{
		base.OnAttack();
		battleSystem.enemiesAlive[enemySelectionIndex].TakeDamage(damage);
	}
	protected override void OnReturnedToBasePosition()
	{
		base.OnReturnedToBasePosition();
		StartCoroutine(NextTurn());
	}

	public override void Block()
	{
		battleSystem.playerPlantChoices.SetActive(false);
		base.Block();
	}
	public override IEnumerator NextTurn()
	{
		yield return new WaitForSeconds(0);
		StartCoroutine(battleSystem.SwitchTurn());
	}
}
