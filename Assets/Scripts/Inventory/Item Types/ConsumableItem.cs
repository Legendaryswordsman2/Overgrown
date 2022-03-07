using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable Item")]
public class ConsumableItem : Item
{
	public List<ConsumableItemEffect> effects;

	public override void ItemSelected()
	{
		throw new System.NotImplementedException();
	}

	public void UseItem(PlayerUnit playerUnit)
	{
		foreach (ConsumableItemEffect effect in effects)
		{
			effect.ExecuteEffect(playerUnit);
		}
		 playerUnit.CallNextTurn();
		
	}
}
