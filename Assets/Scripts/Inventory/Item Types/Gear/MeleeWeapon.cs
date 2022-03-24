using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Gear/Melee Weapon")]
public class MeleeWeapon : Item
{
	[SerializeField] int meleeDamageModifier = 1;

	[HideInInspector] public bool isEquipped = false;

	Image equippedCheckmark;
	Inventory inventory;
	public override void ItemSelected(ItemSlot itemSlot)
	{
		inventory = Inventory.instance;

		if (isEquipped)
		{
			GameManager.instance.player.GetComponent<PlayerStats>().meleeDamageModifier = 0;

			isEquipped = false;
			if (equippedCheckmark != null) equippedCheckmark.enabled = false;

			return;
		}

		inventory.InvokeOnMeleeWeaponItemSelected();

		inventory.OnMeleeWeaponitemSelected += UnequipMeleeWeaponItem;

		isEquipped = true;

		GameManager.instance.player.GetComponent<PlayerStats>().meleeDamageModifier = meleeDamageModifier;
		equippedCheckmark = itemSlot.equippedCheckmark;
		equippedCheckmark.enabled = true;
	}

	private void UnequipMeleeWeaponItem(object sender, EventArgs e)
	{
		isEquipped = false;
		if (equippedCheckmark != null) equippedCheckmark.enabled = false;
		inventory.OnMeleeWeaponitemSelected -= UnequipMeleeWeaponItem;
	}
}
