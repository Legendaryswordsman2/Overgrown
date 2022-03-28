using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Gear/Armor")]
public class Armor : Item
{
	public int armorModifier = 1;

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
			GameManager.instance.player.GetComponent<PlayerStats>().UnequipArmor();
			return;
		}

		GameManager.instance.player.GetComponent<PlayerStats>().EquipArmor(this);

		isEquipped = true;
		equippedCheckmark.enabled = true;
	}

	public void EquipForNewScene(ItemSlot itemSlot)
	{
		if (BattleSystem.instance != null)
			BattleSystem.instance.GetComponent<PlayerStats>().EquipArmor(this);

		if (GameManager.instance != null)
			GameManager.instance.player.GetComponent<PlayerStats>().EquipArmor(this);

		equippedCheckmark = itemSlot.equippedCheckmark;

		isEquipped = true;
		equippedCheckmark.enabled = true;
	}

	public void Unequip()
	{
		isEquipped = false;
		if (equippedCheckmark != null) equippedCheckmark.enabled = false;
	}
}
