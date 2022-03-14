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
	[HideInInspector] public List<QuestItem> questItems;
	public List<EquipablePlantItem> equippablePlantItems;

	[Header("Gear")]
	[HideInInspector] public List<GearItem> gear;

	[Space]

	[SerializeField] GameObject junkItemSlotParent;
	[SerializeField] GameObject consumableItemSlotParent;
	[SerializeField] GameObject questItemSlotParent;
	[SerializeField] GameObject EquipablePlantItemSlotParent;

	[SerializeField] ItemSlot[] equippablePlantItemSlots;

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
		List<string> itemIDs = new List<string>();

		for (int i = 0; i < equippablePlantItems.Count; i++)
		{
			itemIDs.Add(equippablePlantItems[i].ID);
		}
		SaveSystem.SaveFile("/Player/Inventory", "/InventoryPlantItemData.json", itemIDs);
	}

	private void SaveManager_OnLoadingGame(object sender, EventArgs e)
	{
		Debug.Log("Loading Inventory Plants");
		List<string> itemIDs = SaveSystem.LoadFile<List<string>>("/Player/Inventory/InventoryPlantItemData.json");
		if (itemIDs == null) return;

		equippablePlantItems.Clear();

		foreach (string itemID in itemIDs)
		{
			equippablePlantItems.Add(database.GetEquippablePlantItem(itemID));
		}
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

		Debug.Log("Something is selected");
		SetSelectedPlantInInventory();
	}

	private void SetSelectedPlantInInventory()
	{
		foreach (ItemSlot plantItemSlot in equippablePlantItemSlots)
		{
			if (plantItemSlot.item == BattleSetupData.plantSO)
			{
				plantItemSlot.ItemSelected();
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
			Instantiate(itemSlotPrefab, EquipablePlantItemSlotParent.transform).GetComponent<ItemSlot>().SetSlot(equippablePlantItems[i]);
		}
		equippablePlantItemSlots = EquipablePlantItemSlotParent.GetComponentsInChildren<ItemSlot>();
	}

	public void InvokeOnPlantItemSelected()
	{
		OnPlantItemSelected?.Invoke(this, EventArgs.Empty);
	}

}
