using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Heal")]
public class HealthItemEffect : ConsumableItemEffect
{
	public int healAmount;

	public override void ExecuteEffect(BaseUnit unit)
	{
		unit.Heal(healAmount);
	}
}
