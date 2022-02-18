using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AttackType { Basic, Ranged}
public class PlayerUnit : BaseUnit
{
	[Header("References")]
	[SerializeField] CombatInfoHUD playerHUD;
	protected override void Awake()
	{
		base.Awake();

	}

	protected override void Setup()
	{
		playerHUD.SetHUD(this);
	}
	public override void ChooseAction()
	{
		if (anim != null)
			anim.Play("Idle Animation");
		BattleSystem.instance.playerChoices.SetActive(true);
	}
}
