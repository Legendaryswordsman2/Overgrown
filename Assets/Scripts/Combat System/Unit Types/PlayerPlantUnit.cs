using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlantUnit : BaseUnit
{
	[SerializeField] CombatInfoHUD playerPlantHUD;

	protected override void Setup()
	{
		base.Setup();
		playerPlantHUD.SetHUD(this);
	}
	public override void ChooseAction()
	{
		if (anim != null)
			anim.Play("Idle Animation");
		battleSystem.playerPlantChoices.SetActive(true);
	}
	protected override IEnumerator NextTurn()
	{
		yield return new WaitForSeconds(battleSystem.DelayBetweenEachTurn);
		battleSystem.SwitchTurn();
	}
}
