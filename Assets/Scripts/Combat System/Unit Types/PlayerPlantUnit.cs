using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlantUnit : BaseUnit
{
	public CombatInfoHUD playerPlantHUD;

	AttackType attackType;

	[ReadOnlyInspector] public SOPlant plantSO;

	public void SetupPlant()
	{
		if (plantSO == null) return;

		unitName = plantSO.unitName;
		maxHealth = plantSO.defaultHealth;
		currentHealth = plantSO.currentHealth;
		damage = plantSO.attackDamage;
		gameObject.GetComponent<Animator>().runtimeAnimatorController = plantSO.animatorController;
		playerPlantHUD.SetHUD(this);
		playerPlantHUD.gameObject.SetActive(true);

		BattleSystem.instance.playerHasPlant = true;

		gameObject.SetActive(true);
	}

	public void DisablePlant()
	{
		plantSO = null;

		playerPlantHUD.gameObject.SetActive(false);
		battleSystem.playerHasPlant = false;

		gameObject.SetActive(false);
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
				if (battleSystem.enemySelectionIndex <= 0) return;
				battleSystem.enemySelectionIndex--;

				for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
				{
					battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
				}
				battleSystem.enemiesAlive[battleSystem.enemySelectionIndex].transform.GetChild(1).gameObject.SetActive(true);
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
			}
		}
	}
	public override void ChooseAction()
	{
		base.ChooseAction();

		battleSystem.state = BattleState.PlayerPlantTurn;
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

		if (battleSystem.enemySelectionIndex < 0) battleSystem.enemySelectionIndex = 0;

		if (battleSystem.enemySelectionIndex > battleSystem.enemiesAlive.Count - 1) battleSystem.enemySelectionIndex = battleSystem.enemiesAlive.Count - 1;

		battleSystem.enemiesAlive[battleSystem.enemySelectionIndex].transform.GetChild(1).gameObject.SetActive(true);

		currentMode = CurrentMode.AwaitingTargetToAttack;
	}
	void BasicAttack()
	{
		for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
		{
			battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
		}

		locationToAttackTarget = battleSystem.enemiesAlive[battleSystem.enemySelectionIndex].transform.GetChild(2).position;
		anim.Play("Walk Animation");
		currentMode = CurrentMode.Attacking;
	}
	public override void TakeDamage(int _damage, bool isCritical)
	{
		base.TakeDamage(_damage, isCritical);
		if (currentHealth >= 0)
		{
			playerPlantHUD.SetHealth(currentHealth);
			plantSO.currentHealth = currentHealth;
		}
		else
		{
			playerPlantHUD.SetHealth(0);
			plantSO.currentHealth = 0;
		}
	}
	public override void Heal(int amount)
	{
		base.Heal(amount);
		StartCoroutine(playerPlantHUD.SetHealthFromItem(currentHealth));
		battleSystem.playerUnit.UseItem();
		plantSO.currentHealth = currentHealth;
	}
	protected override void Die()
	{
		base.Die();
		battleSystem.playerHasPlant = false;
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

		battleSystem.enemiesAlive[battleSystem.enemySelectionIndex].TakeDamage((int)modifiedDamage, isCritical);
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
	protected override void OnValidate()
	{
		if (plantSO == null)
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);
			base.OnValidate();
		}
	}
}
