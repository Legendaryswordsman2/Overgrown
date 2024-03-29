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
		SaveJunkItems();
		SaveConsumableItems();
		SaveQuestItems();

		// Gear
		SaveArmorItems();
		SaveWeaponItems();
	}


	private void SaveManager_OnLoadingGame(object sender, EventArgs e)
	{
		LoadJunkItems();
		LoadConsumableItems();
		LoadQuestItems();
		LoadWeaponItems();
		LoadArmorItems();

		inventory.CreateItemCopies();
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
		SaveSystem.SaveFile("/Player/Inventory", "/QuestItemData", questItemIDs);
	}
	private void SaveConsumableItems()
	{
		List<string> consumableItemIDs = new List<string>();

		for (int i = 0; i < inventory.consumableItems.Count; i++)
		{
			consumableItemIDs.Add(inventory.consumableItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/ConsumableItemData", consumableItemIDs);
	}
	private void SaveJunkItems()
	{
		List<string> junkItemIDs = new List<string>();

		for (int i = 0; i < inventory.junkItems.Count; i++)
		{
			junkItemIDs.Add(inventory.junkItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/JunkItemData", junkItemIDs);
	}
	private void SaveWeaponItems()
	{
		List<GearSaveData> weaponItems = new List<GearSaveData>();

		for (int i = 0; i < inventory.weaponItems.Count; i++)
		{
			weaponItems.Add(new GearSaveData(inventory.weaponItems[i].ID, inventory.weaponItems[i].isEquipped));
		}
		SaveSystem.SaveFile("/Player/Inventory/Gear", "/WeaponItemData", weaponItems);
	}
	private void SaveArmorItems()
	{
		List<GearSaveData> armorItemIDs = new List<GearSaveData>();

		for (int i = 0; i < inventory.armorItems.Count; i++)
		{
			armorItemIDs.Add(new GearSaveData(inventory.armorItems[i].ID, inventory.armorItems[i].isEquipped));
		}
		SaveSystem.SaveFile("/Player/Inventory/Gear", "/ArmorItemData", armorItemIDs);
	}

	private void LoadQuestItems()
	{
		List<string> questItemIDs = SaveSystem.LoadFile<List<string>>("/Player/Inventory/QuestItemData");
		if (questItemIDs == null) return;

		inventory.questItems.Clear();

		foreach (string itemID in questItemIDs)
		{
			inventory.questItems.Add(database.GetQuestItem(itemID));
		}
	}
	private void LoadConsumableItems()
	{
		List<string> consumableItemIDs = SaveSystem.LoadFile<List<string>>("/Player/Inventory/ConsumableItemData");
		if (consumableItemIDs == null) return;

		inventory.consumableItems.Clear();

		foreach (string itemID in consumableItemIDs)
		{
			inventory.consumableItems.Add(database.GetConsumableItem(itemID));
		}
	}
	private void LoadJunkItems()
	{
		List<string> junkItemIDs = SaveSystem.LoadFile<List<string>>("/Player/Inventory/JunkItemData");
		if (junkItemIDs == null) return;

		inventory.junkItems.Clear();

		foreach (string itemID in junkItemIDs)
		{
			inventory.junkItems.Add(database.GetJunkItem(itemID));
		}
	}
	private void LoadWeaponItems()
	{
		List<GearSaveData> weaponItems = SaveSystem.LoadFile<List<GearSaveData>>("/Player/Inventory/Gear/WeaponItemData");
		if (weaponItems == null) return;

		inventory.weaponItems.Clear();

		foreach (GearSaveData item in weaponItems)
		{
			inventory.weaponItems.Add(database.GetWeaponItem(item.itemID));
		}
		inventory.meleeWeaponItemsSave = weaponItems;
	}
	private void LoadArmorItems()
	{
		List<GearSaveData> armorItems = SaveSystem.LoadFile<List<GearSaveData>>("/Player/Inventory/Gear/ArmorItemData");
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
