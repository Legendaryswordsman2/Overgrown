using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlantUnit : BaseUnit
{
	public CombatInfoHUD playerPlantHUD;

	public void SetStats(PlantStats stats)
	{
        maxHealth = stats.maxHealth;
        currentHealth = stats.currentHealth;

        damage = stats.damage;

        defense = stats.defense;

        critChance = stats.critChance;

        playerPlantHUD.SetHUD(this);
    }

	protected override void Update()
	{
		base.Update();

		if (currentMode == CurrentMode.AwaitingTargetToAttack)
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
	public override void ChooseAction()
	{
		base.ChooseAction();

		battleSystem.state = BattleState.PlayerPlantTurn;
		if (anim != null)
			anim.Play("Idle Animation");
		battleSystem.playerPlantChoices.SetActive(true);
	}
	public void SelectTargetToAttack()
	{

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
		}
		else
		{
			playerPlantHUD.SetHealth(0);
		}
	}
	public override void Heal(int amount)
	{
		base.Heal(amount);
		StartCoroutine(playerPlantHUD.SetHealthFromItem(currentHealth));
		battleSystem.playerUnit.UseItem();
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
}
