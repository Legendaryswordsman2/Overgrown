using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
	public static Inventory instance;

	[Header("Items")]
	public List<JunkItem> junkItems;
	public List<ConsumableItem> consumableItems;
	public List<QuestItem> questItems;
	public List<EquipablePlantItem> equippablePlantItems;

	[Space]

	[SerializeField] GameObject categoriesParent;
	[SerializeField] GameObject categoryButtonsParent;
	[SerializeField] GameObject junkItemSlotParent;
	[SerializeField] GameObject consumableItemSlotParent;
	[SerializeField] GameObject questItemSlotParent;
	[SerializeField] GameObject EquipablePlantItemSlotParent;

	ItemSlot[] equippablePlantItemSlots;

	[SerializeField] GameObject itemSlotPrefab;

	[Space]

	[SerializeField] Database database;

	public event EventHandler OnPlantItemSelected;

	SaveManager saveManager;

	private void Awake()
	{
		instance = this;
		saveManager = SaveManager.instance;
		saveManager.OnLoadingGame += SaveManager_OnLoadingGame;
		saveManager.OnSavingGame += SaveManager_OnSavingGame;

		GetComponent<CanvasGroup>().alpha = 1;
		gameObject.SetActive(false);

		ClearItems();
	}

	private void Start()
	{
		SetItemSlots();
	}

	private void SaveManager_OnSavingGame(object sender, EventArgs e)
	{
		SaveEquippablePlantItems();
		SaveJunkItems();
		SaveConsumableItems();
		SaveQuestItems();
	}


	private void SaveManager_OnLoadingGame(object sender, EventArgs e)
	{
		LoadEquippablePlantItems();
		LoadJunkItems();
		LoadConsumableItems();
		LoadQuestItems();
	}

	private void LoadQuestItems()
	{
		List<string> questItemIDs = SaveSystem.LoadFile<List<string>>("/Player/Inventory/QuestItemData.json");
		if (questItemIDs == null) return;

		questItems.Clear();

		foreach (string itemID in questItemIDs)
		{
			questItems.Add(database.GetQuestItem(itemID));
		}
	}

	private void LoadConsumableItems()
	{
		List<string> consumableItemIDs = SaveSystem.LoadFile<List<string>>("/Player/Inventory/ConsumableItemData.json");
		if (consumableItemIDs == null) return;

		consumableItems.Clear();

		foreach (string itemID in consumableItemIDs)
		{
			consumableItems.Add(database.GetConsumableItem(itemID));
		}
	}

	private void LoadJunkItems()
	{
		List<string> junkItemIDs = SaveSystem.LoadFile<List<string>>("/Player/Inventory/JunkItemData.json");
		if (junkItemIDs == null) return;

		junkItems.Clear();

		foreach (string itemID in junkItemIDs)
		{
			junkItems.Add(database.GetJunkItem(itemID));
		}
	}

	private void SaveEquippablePlantItems()
	{
		List<PlantItemSaveData> equippablePlantitemsSave = new List<PlantItemSaveData>();

		for (int i = 0; i < equippablePlantItems.Count; i++)
		{
			equippablePlantitemsSave.Add(new PlantItemSaveData(equippablePlantItems[i]));
		}
		SaveSystem.SaveFile("/Player/Inventory", "/PlantItemData.json", equippablePlantitemsSave);
	}

	private void SaveQuestItems()
	{
		List<string> questItemIDs = new List<string>();

		for (int i = 0; i < questItems.Count; i++)
		{
			questItemIDs.Add(questItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/questItemData.json", questItemIDs);
	}

	private void SaveConsumableItems()
	{
		List<string> consumableItemIDs = new List<string>();

		for (int i = 0; i < consumableItems.Count; i++)
		{
			consumableItemIDs.Add(consumableItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/ConsumableItemData.json", consumableItemIDs);
	}

	private void SaveJunkItems()
	{
		List<string> junkItemIDs = new List<string>();

		for (int i = 0; i < junkItems.Count; i++)
		{
			junkItemIDs.Add(junkItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/JunkItemData.json", junkItemIDs);
	}

	void LoadEquippablePlantItems()
	{
		List<PlantItemSaveData> equippablePlantitemsSave = SaveSystem.LoadFile<List<PlantItemSaveData>>("/Player/Inventory/PlantItemData.json");
		if (equippablePlantitemsSave == null) return;

		equippablePlantItems.Clear();

		foreach (PlantItemSaveData plantItem in equippablePlantitemsSave)
		{
			equippablePlantItems.Add(database.GetEquippablePlantItem(plantItem.itemID));
		}

		for (int i = 0; i < equippablePlantItems.Count; i++)
		{
			equippablePlantItems[i].plantSO.defaultHealth = equippablePlantitemsSave[i].defaultHealth;
			equippablePlantItems[i].plantSO.attackDamage = equippablePlantitemsSave[i].attackDamage;
			equippablePlantItems[i].plantSO.defense = equippablePlantitemsSave[i].defense;
			equippablePlantItems[i].plantSO.critChance = equippablePlantitemsSave[i].critChance;
			equippablePlantItems[i].isEquipped = equippablePlantitemsSave[i].isEquipped;
		}
	}

	public void GoToCategory(GameObject categoryToOpen)
	{
		foreach (Transform child in categoriesParent.transform)
		{
			child.gameObject.SetActive(false);
		}
		categoryButtonsParent.SetActive(false);

		categoryToOpen.SetActive(true);
	}

	public void GoToStartOfList()
	{
		junkItemSlotParent.transform.position = new Vector3(junkItemSlotParent.transform.position.x, 0);
		consumableItemSlotParent.transform.position = new Vector3(consumableItemSlotParent.transform.position.x, 0);
		questItemSlotParent.transform.position = new Vector3(questItemSlotParent.transform.position.x, 0);
	}

	void SetItemSlots()
	{
		SetItems();

		if (BattleSetupData.plantSO == null) return;

		SetSelectedPlantInInventory();
	}

	private void SetSelectedPlantInInventory()
	{
		foreach (ItemSlot plantItemSlot in equippablePlantItemSlots)
		{
			if(plantItemSlot.item is EquipablePlantItem c)
			{
				if (c.plantSO == BattleSetupData.plantSO) 
			    {
					plantItemSlot.ItemSelected();
			    }

			}

		}
	}

	void ClearItems()
	{
		foreach (Transform child in junkItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in consumableItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in questItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in EquipablePlantItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}
	}
	private void SetItems()
	{
		for (int i = 0; i < junkItems.Count; i++)
		{
			Instantiate(itemSlotPrefab, junkItemSlotParent.transform).GetComponent<ItemSlot>().SetSlot(junkItems[i]);
		}

		for (int i = 0; i < consumableItems.Count; i++)
		{
			Instantiate(itemSlotPrefab, consumableItemSlotParent.transform).GetComponent<ItemSlot>().SetSlot(consumableItems[i]);
		}

		for (int i = 0; i < questItems.Count; i++)
		{
			Instantiate(itemSlotPrefab, questItemSlotParent.transform).GetComponent<ItemSlot>().SetSlot(questItems[i]);
		}


		for (int i = 0; i < equippablePlantItems.Count; i++)
		{
			equippablePlantItems[i] = Instantiate(equippablePlantItems[i]);

			var tempItemSlot = Instantiate(itemSlotPrefab, EquipablePlantItemSlotParent.transform);
			tempItemSlot.GetComponent<ItemSlot>().SetSlot(equippablePlantItems[i]);

			if (tempItemSlot.GetComponent<ItemSlot>().item is EquipablePlantItem c)
				if (c.isEquipped)
					c.ItemSelected(tempItemSlot.GetComponent<ItemSlot>());
		}
		equippablePlantItemSlots = EquipablePlantItemSlotParent.GetComponentsInChildren<ItemSlot>();
	}

	public void InvokeOnPlantItemSelected()
	{
		OnPlantItemSelected?.Invoke(this, EventArgs.Empty);
	}

}
