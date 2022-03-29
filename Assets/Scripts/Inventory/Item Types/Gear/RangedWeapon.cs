using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Gear/Ranged Weapon")]
public class RangedWeapon : Item
{
	public int rangedDamageModifier = 1;

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
			GameManager.instance.player.GetComponent<PlayerStats>().UnequipRangedWeapon();
			GameManager.instance.player.GetComponent<PlayerStats>().RefreshRangedDamageStat();
			return;
		}

		GameManager.instance.player.GetComponent<PlayerStats>().EquipRangedWeapon(this);

		GameManager.instance.player.GetComponent<PlayerStats>().RefreshRangedDamageStat();

		isEquipped = true;
		equippedCheckmark.enabled = true;
	}

	public void EquipForNewScene(ItemSlot itemSlot)
	{
		if (BattleSystem.instance != null)
			BattleSystem.instance.GetComponent<PlayerStats>().EquipRangedWeapon(this);

		if (GameManager.instance != null)
			GameManager.instance.player.GetComponent<PlayerStats>().EquipRangedWeapon(this);

		equippedCheckmark = itemSlot.equippedCheckmark;

		isEquipped = true;
		equippedCheckmark.enabled = true;
	}

	public void Unequip()
	{
		GameManager.instance.player.GetComponent<PlayerStats>().RefreshRangedDamageStat();

		isEquipped = false;
		if (equippedCheckmark != null) equippedCheckmark.enabled = false;
	}
}
