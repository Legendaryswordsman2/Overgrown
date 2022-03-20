using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Equipable Plant Item")]
public class EquipablePlantItem : Item
{
	public SOPlant plantSO;

	[HideInInspector] public bool isEquipped = false;

	Image equippedCheckmark;
	Inventory inventory;
	public override void ItemSelected(ItemSlot itemSlot)
	{
;		inventory = Inventory.instance;
		inventory.InvokeOnPlantItemSelected();

		inventory.OnPlantItemSelected += UnequipPlantItem;

		isEquipped = true;
		BattleSetupData.plantSO = plantSO;
		equippedCheckmark = itemSlot.equippedCheckmark;
		equippedCheckmark.enabled = true;

		if (BattleSystem.instance == null) return;

		BattleSystem battleSystem = BattleSystem.instance;

		battleSystem.playerPlantUnit.plantSO = BattleSetupData.plantSO;
		battleSystem.playerPlantUnit.SetupPlant();

		battleSystem.inventory.gameObject.SetActive(false);

		battleSystem.playerUnit.CallNextTurn();
	}

	public void EquipPlantOnSceneLoaded(ItemSlot itemSlot)
	{
		inventory = Inventory.instance;

		inventory.OnPlantItemSelected += UnequipPlantItem;

		isEquipped = true;
		BattleSetupData.plantSO = plantSO;
		equippedCheckmark = itemSlot.equippedCheckmark;
		equippedCheckmark.enabled = true;

		if (BattleSystem.instance == null) return;

		BattleSystem battleSystem = BattleSystem.instance;

		battleSystem.playerPlantUnit.plantSO = BattleSetupData.plantSO;
		battleSystem.playerPlantUnit.SetupPlant();
	}

	private void Awake() // Called when a new instance is createed
	{
		plantSO = Instantiate(plantSO); // Create a copy
	}

	private void UnequipPlantItem(object sender, System.EventArgs e)
	{
		isEquipped = false;
		if(equippedCheckmark != null) equippedCheckmark.enabled = false;
		inventory.OnPlantItemSelected -= UnequipPlantItem;
	}
}
