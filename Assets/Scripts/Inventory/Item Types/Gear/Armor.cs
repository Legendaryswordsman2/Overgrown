using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Gear/Ranged Weapon")]
public class Armor : Item
{
	[SerializeField] int armorModifier = 1;
	public override void ItemSelected(ItemSlot itemSlot)
	{
		
	}
}
