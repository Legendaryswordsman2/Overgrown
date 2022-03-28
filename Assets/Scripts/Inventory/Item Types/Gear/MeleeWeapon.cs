using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Gear/Melee Weapon")]
public class MeleeWeapon : Item
{
	public int meleeDamageModifier = 1;

	[HideInInspector] public bool isEquipped = false;

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
		if (BattleSystem.instance != null)
			BattleSystem.instance.GetComponent<PlayerStats>().EquipMeleeWeapon(this);

		if(GameManager.instance != null)
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
