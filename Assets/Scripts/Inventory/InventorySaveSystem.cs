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

		inventory.SetItemSlots();
		gameObject.SetActive(false);
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

	private void SaveEquippablePlantItems()
	{
		List<PlantItemSaveData> equippablePlantitemsSave = new List<PlantItemSaveData>();

		for (int i = 0; i < inventory.equippablePlantItems.Count; i++)
		{
			equippablePlantitemsSave.Add(new PlantItemSaveData(inventory.equippablePlantItems[i]));
		}
		SaveSystem.SaveFile("/Player/Inventory", "/PlantItemData.json", equippablePlantitemsSave);
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

	private void SaveArmorItems()
	{
		List<string> armorItemIDs = new List<string>();

		for (int i = 0; i < inventory.armorItems.Count; i++)
		{
			armorItemIDs.Add(inventory.armorItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/ArmorItemData.json", armorItemIDs);
	}

	private void SaveMeleeWeaponItems()
	{
		List<string> meleeWeaponItemIDs = new List<string>();

		for (int i = 0; i < inventory.meleeWeaponItems.Count; i++)
		{
			meleeWeaponItemIDs.Add(inventory.meleeWeaponItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/MeleeWeaponItemData", meleeWeaponItemIDs);
	}
	private void SaveRangedWeaponItems()
	{
		List<string> rangedWeaponItemIDs = new List<string>();

		for (int i = 0; i < inventory.rangedWeaponItems.Count; i++)
		{
			rangedWeaponItemIDs.Add(inventory.rangedWeaponItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/RangedWeaponItemData", rangedWeaponItemIDs);
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

	void LoadEquippablePlantItems()
	{
		List<PlantItemSaveData> equippablePlantitemsSave = SaveSystem.LoadFile<List<PlantItemSaveData>>("/Player/Inventory/PlantItemData.json");
		if (equippablePlantitemsSave == null) return;

		inventory.equippablePlantItems.Clear();

		foreach (PlantItemSaveData plantItem in equippablePlantitemsSave)
		{
			inventory.equippablePlantItems.Add(database.GetEquippablePlantItem(plantItem.itemID));
		}

		for (int i = 0; i < inventory.equippablePlantItems.Count; i++)
		{
			inventory.equippablePlantItems[i] = Instantiate(inventory.equippablePlantItems[i]);
			inventory.equippablePlantItems[i].InitNewCopy();
		}

		for (int i = 0; i < inventory.equippablePlantItems.Count; i++)
		{
			inventory.equippablePlantItems[i].plantSO.defaultHealth = equippablePlantitemsSave[i].defaultHealth;
			inventory.equippablePlantItems[i].plantSO.currentHealth = equippablePlantitemsSave[i].currentHealth;
			inventory.equippablePlantItems[i].plantSO.attackDamage = equippablePlantitemsSave[i].attackDamage;
			inventory.equippablePlantItems[i].plantSO.defense = equippablePlantitemsSave[i].defense;
			inventory.equippablePlantItems[i].plantSO.critChance = equippablePlantitemsSave[i].critChance;
			inventory.equippablePlantItems[i].isEquipped = equippablePlantitemsSave[i].isEquipped;
		}
	}
	#endregion
}
