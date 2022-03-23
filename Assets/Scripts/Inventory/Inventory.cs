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

	public event EventHandler OnPlantItemSelected;

	private void Awake()
	{
		instance = this;

		GetComponent<CanvasGroup>().alpha = 1;

		ClearItemSlots();
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
	public void UnequipPlant()
	{
		for (int i = 0; i < equippablePlantItems.Count; i++)
		{
			equippablePlantItems[i].isEquipped = false;
		}
		BattleSetupData.plantSO = null;
		RefreshInventory();
	}

	public void SetItemSlots()
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
