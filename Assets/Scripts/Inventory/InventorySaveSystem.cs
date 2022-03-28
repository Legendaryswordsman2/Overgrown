using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventorySaveSystem : MonoBehaviour
{
	SaveManager saveManager;
	Inventory inventory;

	[SerializeField] Database database;

	private void Awake()
	{
		saveManager = SaveManager.instance;
		inventory = GetComponent<Inventory>();

		saveManager.OnSavingGame += SaveManager_OnSavingGame;
		saveManager.OnLoadingGame += SaveManager_OnLoadingGame;
	}

	#region Save and Load
	private void SaveManager_OnSavingGame(object sender, EventArgs e)
	{
		// Items
		SaveEquippablePlantItems();
		SaveJunkItems();
		SaveConsumableItems();
		SaveQuestItems();

		// Gear
		SaveArmorItems();
		SaveMeleeWeaponItems();
		SaveRangedWeaponItems();
	}


	private void SaveManager_OnLoadingGame(object sender, EventArgs e)
	{
		LoadEquippablePlantItems();
		LoadJunkItems();
		LoadConsumableItems();
		LoadQuestItems();
		LoadMeleeWeaponItems();
		LoadRangedWeaponItems();
		LoadArmorItems();

		inventory.SetItemSlots();
		gameObject.SetActive(false);
	}

	private void SaveQuestItems()
	{
		List<string> questItemIDs = new List<string>();

		for (int i = 0; i < inventory.questItems.Count; i++)
		{
			questItemIDs.Add(inventory.questItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/QuestItemData.json", questItemIDs);
	}
	private void SaveConsumableItems()
	{
		List<string> consumableItemIDs = new List<string>();

		for (int i = 0; i < inventory.consumableItems.Count; i++)
		{
			consumableItemIDs.Add(inventory.consumableItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/ConsumableItemData.json", consumableItemIDs);
	}
	private void SaveJunkItems()
	{
		List<string> junkItemIDs = new List<string>();

		for (int i = 0; i < inventory.junkItems.Count; i++)
		{
			junkItemIDs.Add(inventory.junkItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/JunkItemData.json", junkItemIDs);
	}
	private void SaveEquippablePlantItems()
	{
		List<PlantItemSaveData> equippablePlantitemsSave = new List<PlantItemSaveData>();

		for (int i = 0; i < inventory.equippablePlantItems.Count; i++)
		{
			equippablePlantitemsSave.Add(new PlantItemSaveData(inventory.equippablePlantItems[i]));
		}
		SaveSystem.SaveFile("/Player/Inventory", "/PlantItemData.json", equippablePlantitemsSave);
	}
	private void SaveMeleeWeaponItems()
	{
		List<GearSaveData> meleeWeaponItems = new List<GearSaveData>();

		for (int i = 0; i < inventory.meleeWeaponItems.Count; i++)
		{
			meleeWeaponItems.Add(new GearSaveData(inventory.meleeWeaponItems[i].ID, inventory.meleeWeaponItems[i].isEquipped));
		}
		SaveSystem.SaveFile("/Player/Inventory/Gear", "/MeleeWeaponItemData.json", meleeWeaponItems);
	}
	private void SaveRangedWeaponItems()
	{
		List<GearSaveData> rangedWeaponItemIDs = new List<GearSaveData>();

		for (int i = 0; i < inventory.rangedWeaponItems.Count; i++)
		{
			rangedWeaponItemIDs.Add(new GearSaveData(inventory.rangedWeaponItems[i].ID, inventory.rangedWeaponItems[i].isEquipped));
		}
		SaveSystem.SaveFile("/Player/Inventory/Gear", "/RangedWeaponItemData.json", rangedWeaponItemIDs);
	}
	private void SaveArmorItems()
	{
		List<GearSaveData> armorItemIDs = new List<GearSaveData>();

		for (int i = 0; i < inventory.armorItems.Count; i++)
		{
			armorItemIDs.Add(new GearSaveData(inventory.armorItems[i].ID, inventory.armorItems[i].isEquipped));
		}
		SaveSystem.SaveFile("/Player/Inventory/Gear", "/ArmorItemData.json", armorItemIDs);
	}

	private void LoadQuestItems()
	{
		List<string> questItemIDs = SaveSystem.LoadFile<List<string>>("/Player/Inventory/QuestItemData.json");
		if (questItemIDs == null) return;

		inventory.questItems.Clear();

		foreach (string itemID in questItemIDs)
		{
			inventory.questItems.Add(database.GetQuestItem(itemID));
		}
	}
	private void LoadConsumableItems()
	{
		List<string> consumableItemIDs = SaveSystem.LoadFile<List<string>>("/Player/Inventory/ConsumableItemData.json");
		if (consumableItemIDs == null) return;

		inventory.consumableItems.Clear();

		foreach (string itemID in consumableItemIDs)
		{
			inventory.consumableItems.Add(database.GetConsumableItem(itemID));
		}
	}
	private void LoadJunkItems()
	{
		List<string> junkItemIDs = SaveSystem.LoadFile<List<string>>("/Player/Inventory/JunkItemData.json");
		if (junkItemIDs == null) return;

		inventory.junkItems.Clear();

		foreach (string itemID in junkItemIDs)
		{
			inventory.junkItems.Add(database.GetJunkItem(itemID));
		}
	}
	private void LoadEquippablePlantItems()
	{
		List<PlantItemSaveData> equippablePlantitemsSave = SaveSystem.LoadFile<List<PlantItemSaveData>>("/Player/Inventory/PlantItemData.json");
		if (equippablePlantitemsSave == null) return;

		inventory.equippablePlantItems.Clear();

		foreach (PlantItemSaveData plantItem in equippablePlantitemsSave)
		{
			inventory.equippablePlantItems.Add(database.GetEquippablePlantItem(plantItem.itemID));
		}
		inventory.equippablePlantitemsSave = equippablePlantitemsSave;
	}
	private void LoadMeleeWeaponItems()
	{
		List<GearSaveData> meleeWeaponItems = SaveSystem.LoadFile<List<GearSaveData>>("/Player/Inventory/Gear/MeleeWeaponItemData.json");
		if (meleeWeaponItems == null) return;

		inventory.meleeWeaponItems.Clear();

		foreach (GearSaveData item in meleeWeaponItems)
		{
			inventory.meleeWeaponItems.Add(database.GetMeleeWeaponItem(item.itemID));
		}
		inventory.meleeWeaponItemsSave = meleeWeaponItems;
	}
	private void LoadRangedWeaponItems()
	{
		List<GearSaveData> rangedWeapons = SaveSystem.LoadFile<List<GearSaveData>>("/Player/Inventory/Gear/RangedWeaponItemData.json");
		if (rangedWeapons == null) return;

		inventory.rangedWeaponItems.Clear();

		foreach (GearSaveData item in rangedWeapons)
		{
			inventory.rangedWeaponItems.Add(database.GetRangedWeaponItem(item.itemID));
		}
		inventory.rangedWeaponItemsSave = rangedWeapons;
	}
	private void LoadArmorItems()
	{
		List<GearSaveData> armorItems = SaveSystem.LoadFile<List<GearSaveData>>("/Player/Inventory/Gear/ArmorItemData.json");
		if (armorItems == null) return;

		inventory.armorItems.Clear();

		foreach (GearSaveData item in armorItems)
		{
			inventory.armorItems.Add(database.GetArmorItem(item.itemID));
		}
		inventory.armorItemsSave = armorItems;
	}
	#endregion
}
