using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Quest Item")]
public class QuestItem : Item
{
	public override void ItemSelected(ItemSlot itemSlot)
	{

	}

	public override void BuyItem(BuyableItemSlot itemSlot, Inventory inventory)
	{
		throw new System.NotImplementedException();
	}
}
