using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Equipable Plant Item")]
public class EquipablePlantItem : Item
{
	[Header("Unit Settings")]
	public string unitName = "Some Random Plant";
	public int defaultHealth = 100;
	public int attackDamage = 10;

	[Header("Leveling")]
	public int healthIncreasePerLevelUp = 10;
	public int damageIncreasePerLevelUp = 2;

	public RuntimeAnimatorController animatorController;

	Image equippedCheckmark;
	Inventory inventory;

	bool isEquipped = false;
	public override void ItemSelected(ItemSlot itemSlot)
	{
		inventory = Inventory.instance;
		inventory.InvokeOnPlantItemSelected();

		inventory.OnPlantItemSelected += UnequipPlantItem;

		isEquipped = true;
		Debug.Log("Plant Item Selected");
		BattleSetupData.plantSO = this;
		equippedCheckmark = itemSlot.equippedCheckmark;
		equippedCheckmark.enabled = true;

		if (BattleSystem.instance == null) return;

		BattleSystem battleSystem = BattleSystem.instance;

		battleSystem.playerPlantUnit.plantSO = BattleSetupData.plantSO;
		battleSystem.playerPlantUnit.SetupPlant();

		battleSystem.inventory.gameObject.SetActive(false);

		battleSystem.playerUnit.CallNextTurn();
	}

	private void UnequipPlantItem(object sender, System.EventArgs e)
	{
		isEquipped = false;
		if(equippedCheckmark != null) equippedCheckmark.enabled = false;
		Debug.Log("Unequipped Plant");
		inventory.OnPlantItemSelected -= UnequipPlantItem;
	}
}
