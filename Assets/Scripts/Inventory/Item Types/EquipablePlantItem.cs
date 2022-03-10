using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipable Plant Item")]
public class EquipablePlantItem : Item
{
	bool isEquipped = false;
	public override void ItemSelected(ItemSlot itemSlot)
	{
		isEquipped = !isEquipped;
	}
}
