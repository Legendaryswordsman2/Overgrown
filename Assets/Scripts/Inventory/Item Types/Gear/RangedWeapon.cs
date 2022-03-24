using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Gear/Armor")]
public class RangedWeapon : Item
{
   [SerializeField]	int rangedDamageModifier = 1;

	[HideInInspector] public bool isEquipped = false;

	Image equippedCheckmark;
	Inventory inventory;
	public override void ItemSelected(ItemSlot itemSlot)
	{
		
	}
}
