using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Gear/Melee Weapon")]
public class MeleeWeapon : Item
{
	[SerializeField] int meleeDamageModifier = 1;

	public bool isEquipped = false;

	Image equippedCheckmark;
	Inventory inventory;

	public override void ItemSelected(ItemSlot itemSlot)
	{
		equippedCheckmark = itemSlot.equippedCheckmark;
		Equip();
	}

	void Equip()
	{
		if (isEquipped)
		{
			GameManager.instance.player.GetComponent<PlayerStats>().UnequipMeleeWeapon();
			return;
		}

		GameManager.instance.player.GetComponent<PlayerStats>().EquipMeleeWeapon(this);

		isEquipped = true;
		equippedCheckmark.enabled = true;

	}

	public void EquipForNewScene(ItemSlot itemSlot)
	{
		GameManager.instance.player.GetComponent<PlayerStats>().EquipMeleeWeapon(this);

		equippedCheckmark = itemSlot.equippedCheckmark;

		isEquipped = true;
		equippedCheckmark.enabled = true;
	}

	public void EquipOnSceneLoaded(ItemSlot itemSlot)
	{
		inventory = Inventory.instance;

		isEquipped = true;

		//GameManager.instance.player.GetComponent<PlayerStats>().meleeDamageModifier = meleeDamageModifier;
		equippedCheckmark = itemSlot.equippedCheckmark;
		equippedCheckmark.enabled = true;
	}

	public void Unequip()
	{
		isEquipped = false;
		if (equippedCheckmark != null) equippedCheckmark.enabled = false;
	}
}
