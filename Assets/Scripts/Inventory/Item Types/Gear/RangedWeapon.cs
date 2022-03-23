using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Gear/Armor")]
public class RangedWeapon : Item
{
   [SerializeField]	int rangedDamageModifier = 1;
	public override void ItemSelected(ItemSlot itemSlot)
	{
		
	}
}
