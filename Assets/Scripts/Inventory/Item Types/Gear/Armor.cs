using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Gear/Armor")]
public class Armor : Item
{
	public int defenseModifier = 1;

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
			GameManager.instance.player.GetComponent<PlayerStats>().RefreshDefenseStat();
			return;
		}

		GameManager.instance.player.GetComponent<PlayerStats>().EquipArmor(this);

		GameManager.instance.player.GetComponent<PlayerStats>().RefreshDefenseStat();

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

		GameManager.instance.player.GetComponent<PlayerStats>().RefreshDefenseStat();

		isEquipped = true;
		equippedCheckmark.enabled = true;
	}

	public void Unequip()
	{
		GameManager.instance.player.GetComponent<PlayerStats>().RefreshDefenseStat();

		isEquipped = false;
		if (equippedCheckmark != null) equippedCheckmark.enabled = false;
	}
}
