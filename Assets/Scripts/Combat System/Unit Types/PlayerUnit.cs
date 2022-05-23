using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System;
using UnityEngine.InputSystem;

public class PlayerUnit : BaseUnit
{
	[field: Header("References")]
	[field: SerializeField] public CombatInfoHUD playerHUD { get; private set; }

	[SerializeField] PlayerStats playerStats;

	protected override void Setup()
	{
		base.Setup();
	}
	public void SetStats(PlayerStats stats)
    {
		maxHealth = stats.maxHealth;
		currentHealth = stats.currentHealth;

		if (stats.meleeWeapon != null)
            damage = stats.damage + stats.meleeWeapon.meleeDamageModifier;
		else
			damage = stats.damage;

		if (stats.armor != null)
			defense = stats.defense + stats.armor.defenseModifier;
		else
			defense = stats.defense;

		critChance = stats.critChance;

		playerHUD.SetHUD(this);
	}

	protected override void Update()
	{
		base.Update();

		if(currentMode == CurrentMode.AwaitingTargetToAttack)
		{
			if (Keyboard.current.enterKey.wasPressedThisFrame)
			{
				currentMode = CurrentMode.Null;
					BasicAttack();
			}

			if (Keyboard.current.wKey.wasPressedThisFrame)
			{
				if (battleSystem.enemySelectionIndex <= 0) return;
				battleSystem.enemySelectionIndex--;

				for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
				{
					battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
				}
				battleSystem.enemiesAlive[battleSystem.enemySelectionIndex].transform.GetChild(1).gameObject.SetActive(true);
			}

			if (Keyboard.current.sKey.wasPressedThisFrame)
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
	public void SelectTargetToAttack()
	{

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

	public override IEnumerator NextTurn()
	{
		yield return new WaitForSeconds(0);
			battleSystem.playerChoices.SetActive(false);
			battleSystem.playerPlantUnit.ChooseAction();
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
