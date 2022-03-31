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
			if (battleSystem.playerUnit.currentHealth >= battleSystem.playerUnit.maxHealth)
			{
				Inventory.instance.textPopup.SetPopup("PLAYER ALREADY HAS FULL HEALTH");
				return;
			}

			UseItem(battleSystem.playerUnit);
			battleSystem.inventory.gameObject.SetActive(false);
			Inventory.instance.RemoveItem(this);
		}
		else
		{
			if(battleSystem.playerUnit.currentHealth >= battleSystem.playerUnit.maxHealth && battleSystem.playerPlantUnit.currentHealth >= battleSystem.playerPlantUnit.maxHealth)
			{
				Inventory.instance.textPopup.SetPopup("PARTY ALREADY HAS FULL HEALTH");
				return;
			}
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

	public override void BuyItem(BuyableItemSlot itemSlot, Inventory inventory)
	{
		throw new System.NotImplementedException();
	}
}
