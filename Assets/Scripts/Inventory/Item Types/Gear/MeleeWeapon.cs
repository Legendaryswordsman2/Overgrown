using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Gear/Melee Weapon")]
public class MeleeWeapon : Item
{
	[SerializeField] int meleeDamageModifier = 1;
	public override void ItemSelected(ItemSlot itemSlot)
	{
		
	}
}
