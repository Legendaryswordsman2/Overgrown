using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Gear/Weapon")]
public class MeleeWeapon : Item
{
	[Space]

	public int meleeDamageModifier = 1;

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
			GameManager.instance.player.GetComponent<PlayerStats>().RefreshMeleeDamageStat();
			return;
		}

		GameManager.instance.player.GetComponent<PlayerStats>().EquipMeleeWeapon(this);

		GameManager.instance.player.GetComponent<PlayerStats>().RefreshMeleeDamageStat();

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

	public void Unequip()
	{
		GameManager.instance.player.GetComponent<PlayerStats>().RefreshMeleeDamageStat();

		isEquipped = false;
		if (equippedCheckmark != null) equippedCheckmark.enabled = false;
	}
}
