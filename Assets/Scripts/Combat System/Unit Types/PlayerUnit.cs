using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AttackType { Basic, Ranged}
public class PlayerUnit : BaseUnit
{
	[Header("References")]
	[SerializeField] CombatInfoHUD playerHUD;
	protected override void Setup()
	{
		base.Setup();
		playerHUD.SetHUD(this);
	}
	private void Update()
	{
		if (currentMode == CurrentMode.Attacking)
		{
			bool finished = LerpToTarget(basePosition, locationToAttackTarget);

			if(finished)
			{
				currentMode = CurrentMode.ReturningHome;
				FlipSprite();
			}
		}

		if (currentMode == CurrentMode.ReturningHome)
		{
			bool finished = LerpToTarget(locationToAttackTarget, basePosition);

			if (finished)
			{
				currentMode = CurrentMode.Null;
				FlipSprite();
				anim.Play("Idle Animation");
			}
		}
	}
	public override void TakeDamage(int _damage)
	{
		base.TakeDamage(_damage);
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
		if (anim != null)
			anim.Play("Idle Animation");
		battleSystem.playerChoices.SetActive(true);
	}

	#region Actions
	void BasicAttack()
	{
		locationToAttackTarget = battleSystem.enemiesAlive[0].transform.GetChild(2).position;
		anim.Play("Walk Animation");
		currentMode = CurrentMode.Attacking;
	}
	public void EscapeBattle()
	{
		StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", BattleSetupData.sceneIndex, BattleSetupData.playerPosition));
	}
	#endregion
}
