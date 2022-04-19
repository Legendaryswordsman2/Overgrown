using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Player Item")]
public class PlayerItem : Item
{
	Inventory inventory;
	public override void ItemSelected(ItemSlot itemSlot)
	{
		
	}

	public override void BuyItem(ShopItemSlot itemSlot, Inventory inventory)
	{
		throw new System.NotImplementedException();
	}
}
