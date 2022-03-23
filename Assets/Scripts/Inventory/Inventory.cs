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

	[Header("Gear")]
	public List<Armor> armorItems;
	public List<MeleeWeapon> meleeWeaponItems;
	public List<RangedWeapon> rangedWeaponItems;

	[Header("Parents")]
	[SerializeField] GameObject categoriesParent;
	[SerializeField] GameObject categoryButtonsParent;
	[SerializeField] GameObject junkItemSlotParent;
	[SerializeField] GameObject consumableItemSlotParent;
	[SerializeField] GameObject questItemSlotParent;
	[SerializeField] GameObject EquipablePlantItemSlotParent;

	[field: Header("Other Refernces")]
	[field: SerializeField] public ItemInfoBox itemInfoBox { get; private set; }
	[field: SerializeField] public PlantInfoBox plantInfoBox { get; private set; }
	[field: SerializeField] public GameObject CantEquipPlantPopup { get; private set; }


	[Header("Item Slots")]
	[SerializeField] List<ItemSlot> equippablePlantItemSlots = new List<ItemSlot>();
	[SerializeField] List<ItemSlot> junkItemSlots = new List<ItemSlot>();
	[SerializeField] List<ItemSlot> consumableItemSlots = new List<ItemSlot>();
	[SerializeField] List<ItemSlot> questItemSlots = new List<ItemSlot>();

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

		ClearItemSlots();
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

		SetItemSlots();
		gameObject.SetActive(false);
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
		SaveSystem.SaveFile("/Player/Inventory", "/QuestItemData.json", questItemIDs);
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

	private void SaveArmorItems()
	{
		List<string> armorItemIDs = new List<string>();

		for (int i = 0; i < armorItems.Count; i++)
		{
			armorItemIDs.Add(armorItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/ArmorItemData.json", armorItemIDs);
	}
	
	private void SaveMeleeWeaponItems()
	{
		List<string> meleeWeaponItemIDs = new List<string>();

		for (int i = 0; i < meleeWeaponItems.Count; i++)
		{
			meleeWeaponItemIDs.Add(meleeWeaponItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/MeleeWeaponItemData", meleeWeaponItemIDs);
	}
	private void SaveRangedWeaponItems()
	{
		List<string> rangedWeaponItemIDs = new List<string>();

		for (int i = 0; i < rangedWeaponItems.Count; i++)
		{
			rangedWeaponItemIDs.Add(rangedWeaponItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/RangedWeaponItemData", rangedWeaponItemIDs);
	}

	public void UnequipPlant()
	{
		for (int i = 0; i < equippablePlantItems.Count; i++)
		{
			equippablePlantItems[i].isEquipped = false;
		}
		BattleSetupData.plantSO = null;
		RefreshInventory();
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
			equippablePlantItems[i] = Instantiate(equippablePlantItems[i]);
			equippablePlantItems[i].InitNewCopy();
		}

		for (int i = 0; i < equippablePlantItems.Count; i++)
		{
			equippablePlantItems[i].plantSO.defaultHealth = equippablePlantitemsSave[i].defaultHealth;
			equippablePlantItems[i].plantSO.currentHealth = equippablePlantitemsSave[i].currentHealth;
			equippablePlantItems[i].plantSO.attackDamage = equippablePlantitemsSave[i].attackDamage;
			equippablePlantItems[i].plantSO.defense = equippablePlantitemsSave[i].defense;
			equippablePlantItems[i].plantSO.critChance = equippablePlantitemsSave[i].critChance;
			equippablePlantItems[i].isEquipped = equippablePlantitemsSave[i].isEquipped;
		}
	}
	#endregion

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

	public void RefreshInventory()
	{
		ClearItemSlots();
		SetItemSlots();
	}

	public void RefreshJunkItemSlots()
	{
		foreach (Transform child in junkItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i < junkItems.Count; i++)
		{
			var itemSlot = Instantiate(itemSlotPrefab, junkItemSlotParent.transform).GetComponent<ItemSlot>();
			itemSlot.SetSlot(junkItems[i]);
			junkItemSlots.Add(itemSlot);
		}
	}
	public void RefreshConsumableItemSlots()
	{
		foreach (Transform child in consumableItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i < consumableItems.Count; i++)
		{
			var itemSlot = Instantiate(itemSlotPrefab, consumableItemSlotParent.transform).GetComponent<ItemSlot>();
			itemSlot.SetSlot(consumableItems[i]);
			consumableItemSlots.Add(itemSlot);
		}
	}

	public void RefreshQuestItemSlots()
	{
		foreach (Transform child in questItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i < questItems.Count; i++)
		{
			var itemSlot = Instantiate(itemSlotPrefab, questItemSlotParent.transform).GetComponent<ItemSlot>();
			itemSlot.SetSlot(questItems[i]);
			questItemSlots.Add(itemSlot);
		}
	}

	public void RefreshEquippablePlantItemSlots()
	{
		foreach (Transform child in EquipablePlantItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i < equippablePlantItems.Count; i++)
		{
			var ItemSlot = Instantiate(itemSlotPrefab, EquipablePlantItemSlotParent.transform).GetComponent<ItemSlot>();
			ItemSlot.SetSlot(equippablePlantItems[i]);
			equippablePlantItemSlots.Add(ItemSlot);

			if (ItemSlot.item is EquipablePlantItem c)
				if (c.isEquipped)
				{
					c.EquipPlantOnSceneLoaded(ItemSlot.GetComponent<ItemSlot>());
				}
		}
	}

	void SetItemSlots()
	{
		for (int i = 0; i < junkItems.Count; i++)
		{
			var itemSlot = Instantiate(itemSlotPrefab, junkItemSlotParent.transform).GetComponent<ItemSlot>();
			itemSlot.SetSlot(junkItems[i]);
			junkItemSlots.Add(itemSlot);
		}

		for (int i = 0; i < consumableItems.Count; i++)
		{
			var itemSlot = Instantiate(itemSlotPrefab, consumableItemSlotParent.transform).GetComponent<ItemSlot>();
			itemSlot.SetSlot(consumableItems[i]);
			consumableItemSlots.Add(itemSlot);
		}

		for (int i = 0; i < questItems.Count; i++)
		{
			var itemSlot = Instantiate(itemSlotPrefab, questItemSlotParent.transform).GetComponent<ItemSlot>();
			itemSlot.SetSlot(questItems[i]);
			questItemSlots.Add(itemSlot);
		}

		for (int i = 0; i < equippablePlantItems.Count; i++)
		{
			var ItemSlot = Instantiate(itemSlotPrefab, EquipablePlantItemSlotParent.transform).GetComponent<ItemSlot>();
			ItemSlot.SetSlot(equippablePlantItems[i]);
			equippablePlantItemSlots.Add(ItemSlot);

			if (ItemSlot.item is EquipablePlantItem c)
				if (c.isEquipped)
				{
					c.EquipPlantOnSceneLoaded(ItemSlot.GetComponent<ItemSlot>());
				}
		}
	}

	void ClearItemSlots()
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

	public void InvokeOnPlantItemSelected()
	{
		OnPlantItemSelected?.Invoke(this, EventArgs.Empty);
	}

	public void AddItem(Item item)
	{
		if(item is JunkItem junkItem)
		{
			junkItems.Add(junkItem);
			RefreshJunkItemSlots();
		}
		else if(item is ConsumableItem consumableItem)
		{
			consumableItems.Add(consumableItem);
			RefreshConsumableItemSlots();
		}
		else if (item is QuestItem questItem)
		{
			questItems.Add(questItem);
			RefreshQuestItemSlots();
		}
		//else if (item is MeleeWeapon meleeWeapon)
		//{
			
		//}
		//else if (item is RangedWeapon rangedWeapon)
		//{

		//}
		//else if (item is Armor armor)
		//{

		//}
	}

	public void RemoveItem(Item item)
	{
		if(item is JunkItem junkItem)
		{
			junkItems.Remove(junkItem);
			RefreshJunkItemSlots();
		}
		else if (item is ConsumableItem consumableItem)
		{
			consumableItems.Remove(consumableItem);
			RefreshConsumableItemSlots();
		}
		else if (item is QuestItem questItem)
		{
			questItems.Remove(questItem);
			RefreshQuestItemSlots();
		}
	}
}
