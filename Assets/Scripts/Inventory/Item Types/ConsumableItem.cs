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

		if (battleSystem == null) return;

		Inventory.instance.itemInfoBox.gameObject.SetActive(false);
		Inventory.instance.plantInfoBox.gameObject.SetActive(false);


		if (!battleSystem.playerHasPlant)
		{
			UseItem(battleSystem.playerUnit);
			battleSystem.inventory.gameObject.SetActive(false);
			Inventory.instance.RemoveItem(this);
		}
		else
		{
			battleSystem.inventory.gameObject.SetActive(false);
			battleSystem.GetComponent<ChooseTargetToUseItemOn>().ChooseTargetToUseItem(this);
		}

	}

	public void UseItem(BaseUnit unit)
	{
		foreach (ConsumableItemEffect effect in effects)
		{
			effect.ExecuteEffect(unit);
		}
	}
}
