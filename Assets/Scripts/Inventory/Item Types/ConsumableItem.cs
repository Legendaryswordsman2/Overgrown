using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable Item")]
public class ConsumableItem : Item
{
	public List<ConsumableItemEffect> effects;

	public override void ItemSelected(ItemSlot itemSlot)
	{
		BattleSystem battleSystem = BattleSystem.instance;
		
		if(battleSystem == null)
		{
			return;
		}

		UseItem(battleSystem.playerUnit);
		battleSystem.inventory.gameObject.SetActive(false);

		Destroy(itemSlot.gameObject);
	}

	void UseItem(PlayerUnit playerUnit)
	{
		foreach (ConsumableItemEffect effect in effects)
		{
			effect.ExecuteEffect(playerUnit);
		}
		 playerUnit.CallNextTurn();
		
	}
}
