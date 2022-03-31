using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System;
public enum AttackType { Basic, Ranged}
public class PlayerUnit : BaseUnit
{
	[field: Header("References")]
	[field: SerializeField] public CombatInfoHUD playerHUD { get; private set; }

	AttackType attackType;

	[SerializeField] PlayerStats playerStats;

	protected override void Setup()
	{
		base.Setup();
	}

	protected override void Update()
	{
		base.Update();

		if(currentMode == CurrentMode.AwaitingTargetToAttack)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				currentMode = CurrentMode.Null;
				if (attackType == AttackType.Basic)
					BasicAttack();
				else
					RangedAttack();
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
	public override void TakeDamage(int _damage, bool isCritical)
	{
		base.TakeDamage(_damage, isCritical);
		if (currentHealth >= 0)
		{
			playerHUD.SetHealth(currentHealth);
		}
		else
		{
			playerHUD.SetHealth(0);
		}
	}
	public override void ChooseAction()
	{
		base.ChooseAction();

		BattleSystem.instance.state = BattleState.PlayerTurn;

		if (anim != null)
			anim.Play("Idle Animation");
		BattleSystem.instance.playerChoices.SetActive(true);
	}

	#region Actions
	public void SelectTargetToAttack(bool isBasicAttack)
	{
		if (isBasicAttack)
			attackType = AttackType.Basic;
		else
			attackType = AttackType.Ranged;

		battleSystem.playerChoices.SetActive(false);

		if (battleSystem.enemySelectionIndex < 0) battleSystem.enemySelectionIndex = 0;

		if(battleSystem.enemySelectionIndex > battleSystem.enemiesAlive.Count - 1) battleSystem.enemySelectionIndex = battleSystem.enemiesAlive.Count - 1;

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
	void RangedAttack()
	{
		for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
		{
			battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
		}

		locationToAttackTarget = battleSystem.enemiesAlive[battleSystem.enemySelectionIndex].transform.GetChild(2).position;
		ArrowLerp arrow = Instantiate(battleSystem.arrowPrefab, this.transform).GetComponent<ArrowLerp>();
		arrow.damage = rangedDamage;
		arrow.selectionIndex = battleSystem.enemySelectionIndex;
		arrow.playerUnit = this;
		arrow.endPosition = battleSystem.enemiesAlive[battleSystem.enemySelectionIndex].transform.position;
	}
	public override void Block()
	{
		battleSystem.playerChoices.SetActive(false);
		base.Block();
	}
	protected override void OnAttack()
	{
		base.OnAttack();


		float modifiedDamage = GetAttackModifier();

		bool isCritical;

		if (modifiedDamage > meleeDamage)
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

	public override IEnumerator NextTurn()
	{
		yield return new WaitForSeconds(0);
		if (battleSystem.playerHasPlant)
		{
			battleSystem.playerChoices.SetActive(false);
			battleSystem.playerPlantUnit.ChooseAction();
		}
		else
		{
			StartCoroutine(battleSystem.SwitchTurn());
		}
	}
	public void CallNextTurn()
	{
		StartCoroutine(NextTurn());
	}

	public override void Heal(int amount)
	{
		base.Heal(amount);
		StartCoroutine(playerHUD.SetHealthFromItem(currentHealth));
		UseItem();
	}
	public void EscapeBattle()
	{
		StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", BattleSetupData.sceneIndex, BattleSetupData.playerPosition));
	}
	#endregion
}
