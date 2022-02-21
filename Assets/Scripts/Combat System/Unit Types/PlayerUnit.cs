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
		BattleSystem.instance.playerChoices.SetActive(true);
	}

	public void EscapeBattle()
	{
		StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", BattleSetupData.sceneIndex, BattleSetupData.playerPosition));
	}
}
