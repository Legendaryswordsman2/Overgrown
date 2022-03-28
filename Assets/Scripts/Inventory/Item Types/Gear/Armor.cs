using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Gear/Ranged Weapon")]
public class Armor : Item
{
	public int armorModifier = 1;

	[HideInInspector] public bool isEquipped = false;

	Image equippedCheckmark;
	Inventory inventory;
	public override void ItemSelected(ItemSlot itemSlot)
	{
		
	}
}
