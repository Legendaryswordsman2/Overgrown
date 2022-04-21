using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SelectionMode { Default, Sell }
public class Inventory : MonoBehaviour
{
	public static Inventory instance;

	[Header("Items")]
	public List<JunkItem> junkItems;
	public List<ConsumableItem> consumableItems;
	public List<QuestItem> questItems;
	public List<EquipablePlantItem> equippablePlantItems;

	[Header("Gear")]
	public List<MeleeWeapon> weaponItems;
	public List<Armor> armorItems;

	[Header("Parents")]
	[SerializeField] GameObject categoriesParent;
	[SerializeField] GameObject categoryButtonsParent;
	[SerializeField] GameObject junkItemSlotParent;
	[SerializeField] GameObject consumableItemSlotParent;
	[SerializeField] GameObject questItemSlotParent;
	[SerializeField] GameObject EquipablePlantItemSlotParent;

	[SerializeField] GameObject meleeWeaponItemSlotParent;
	[SerializeField] GameObject armorItemSlotParent;

	[field: Header("Other Refernces")]
	[field: SerializeField] public ItemInfoBox itemInfoBox { get; private set; }
	[field: SerializeField] public PlantInfoBox plantInfoBox { get; private set; }
	[field: SerializeField] public TextPopup textPopup { get; private set; }

	[SerializeField] public GameObject junkItemsCategory;
	[SerializeField] public GameObject consumableItemsCategory;
	[SerializeField] public GameObject questItemsCategory;
	[SerializeField] public GameObject weaponItemsCategory;
	[SerializeField] public GameObject armorItemsCategory;

	[field: SerializeField] public UseableItemManager useItemScreen { get; private set; }
	[field: SerializeField] public GameObject ItemsCategory { get; private set; }


	[Header("Item Slots")]
	[SerializeField] List<ItemSlot> equippablePlantItemSlots = new List<ItemSlot>();
	[SerializeField] List<ItemSlot> junkItemSlots = new List<ItemSlot>();
	[SerializeField] List<ItemSlot> consumableItemSlots = new List<ItemSlot>();
	[SerializeField] List<ItemSlot> questItemSlots = new List<ItemSlot>();

	[SerializeField] List<ItemSlot> meleeWeaponItemSlots = new List<ItemSlot>();
	[SerializeField] List<ItemSlot> armorItemSlots = new List<ItemSlot>();

	[SerializeField] GameObject itemSlotPrefab;

	public event EventHandler OnPlantItemSelected;

	[field: Space]

	[field: SerializeField] public SelectionMode selectionMode { get; private set; } = SelectionMode.Default;


	[HideInInspector] public List<PlantItemSaveData> equippablePlantitemsSave;
	[HideInInspector] public List<GearSaveData> meleeWeaponItemsSave;
	[HideInInspector] public List<GearSaveData> armorItemsSave;

	[Space]

	[ReadOnlyInspector] public EquipablePlantItem equippedPlantItem;

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

		useItemScreen.gameObject.SetActive(false);

		categoryToOpen.SetActive(true);
	}

	public void GoToStartOfList()
	{
		junkItemSlotParent.transform.position = new Vector3(junkItemSlotParent.transform.position.x, 0);
		consumableItemSlotParent.transform.position = new Vector3(consumableItemSlotParent.transform.position.x, 0);
		questItemSlotParent.transform.position = new Vector3(questItemSlotParent.transform.position.x, 0);

		meleeWeaponItemSlotParent.transform.position = new Vector3(junkItemSlotParent.transform.position.x, 0);
		armorItemSlotParent.transform.position = new Vector3(armorItemSlotParent.transform.position.x, 0);
	}

	public void RefreshInventory()
	{
		ClearItemSlots();
		CreateItemCopies();
		SetItemSlots();
	}
	public void ResetTabs()
	{
		junkItemsCategory.SetActive(true);
		consumableItemsCategory.SetActive(false);
		questItemsCategory.SetActive(false);

		weaponItemsCategory.SetActive(true);
		armorItemsCategory.SetActive(false);
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

		consumableItemSlots.Clear();
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
	public void RefreshMeleeWeaponItemSlots()
	{
		foreach (Transform child in meleeWeaponItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i < weaponItems.Count; i++)
		{
			var ItemSlot = Instantiate(itemSlotPrefab, meleeWeaponItemSlotParent.transform).GetComponent<ItemSlot>();
			ItemSlot.SetSlot(weaponItems[i]);
			meleeWeaponItemSlots.Add(ItemSlot);

			if (ItemSlot.item is MeleeWeapon c)
				if (c.isEquipped)
					c.EquipForNewScene(ItemSlot);
		}
	}
	public void RefreshArmorItemSlots()
	{
		foreach (Transform child in armorItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i < armorItems.Count; i++)
		{
			var ItemSlot = Instantiate(itemSlotPrefab, armorItemSlotParent.transform).GetComponent<ItemSlot>();
			ItemSlot.SetSlot(armorItems[i]);
			armorItemSlots.Add(ItemSlot);

			if (ItemSlot.item is Armor c)
				if (c.isEquipped)
					c.EquipForNewScene(ItemSlot);
		}
	}
	public void UnequipPlant()
	{
		for (int i = 0; i < equippablePlantItems.Count; i++)
		{
			equippablePlantItems[i].isEquipped = false;
		}
		BattleSetupData.plantSO = null;
		equippedPlantItem = null;
		RefreshEquippablePlantItemSlots();
	}
	public void CreateItemCopies()
	{
		for (int i = 0; i < equippablePlantItems.Count; i++)
		{
			equippablePlantItems[i] = Instantiate(equippablePlantItems[i]);
			equippablePlantItems[i].InitNewCopy();
		}

		if(equippablePlantitemsSave != null && equippablePlantitemsSave.Count != 0)
		{
			for (int i = 0; i < equippablePlantItems.Count; i++)
			{
				equippablePlantItems[i].plantSO.defaultHealth = equippablePlantitemsSave[i].defaultHealth;
				equippablePlantItems[i].plantSO.level = equippablePlantitemsSave[i].level;
				equippablePlantItems[i].plantSO.currentHealth = equippablePlantitemsSave[i].currentHealth;
				equippablePlantItems[i].plantSO.meleeDamage = equippablePlantitemsSave[i].meleeDamage;
				equippablePlantItems[i].plantSO.defense = equippablePlantitemsSave[i].defense;
				equippablePlantItems[i].plantSO.critChance = equippablePlantitemsSave[i].critChance;
				equippablePlantItems[i].isEquipped = equippablePlantitemsSave[i].isEquipped;

				equippablePlantItems[i].plantSO.xp = equippablePlantitemsSave[i].xp;
				equippablePlantItems[i].plantSO.xpToLevelUp = equippablePlantitemsSave[i].xpToLevelUp;
				equippablePlantItems[i].plantSO.xpIncreaseOnLevelUp = equippablePlantitemsSave[i].xpIncreaseOnLevelUp;
				equippablePlantItems[i].plantSO.xpIncreaseIncreaseOnLevelUp = equippablePlantitemsSave[i].xpIncreaseIncreaseOnLevelUp;
			}
		}
		for (int i = 0; i < weaponItems.Count; i++)
		{
			weaponItems[i] = Instantiate(weaponItems[i]);
			if(meleeWeaponItemsSave.Count != 0) weaponItems[i].isEquipped = meleeWeaponItemsSave[i].isEquipped;

		}

		for (int i = 0; i < armorItems.Count; i++)
		{
			armorItems[i] = Instantiate(armorItems[i]);
			if(armorItemsSave.Count != 0) armorItems[i].isEquipped = armorItemsSave[i].isEquipped;
		}
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
					c.EquipPlantOnSceneLoaded(ItemSlot);
					equippedPlantItem = c;
				}
		}

		for (int i = 0; i < weaponItems.Count; i++)
		{
			var ItemSlot = Instantiate(itemSlotPrefab, meleeWeaponItemSlotParent.transform).GetComponent<ItemSlot>();
			ItemSlot.SetSlot(weaponItems[i]);
			meleeWeaponItemSlots.Add(ItemSlot);

			if (ItemSlot.item is MeleeWeapon c)
				if (c.isEquipped)
					c.EquipForNewScene(ItemSlot);
		}

		for (int i = 0; i < armorItems.Count; i++)
		{
			var ItemSlot = Instantiate(itemSlotPrefab, armorItemSlotParent.transform).GetComponent<ItemSlot>();
			ItemSlot.SetSlot(armorItems[i]);
			armorItemSlots.Add(ItemSlot);

			if (ItemSlot.item is Armor c)
				if (c.isEquipped)
					c.EquipForNewScene(ItemSlot);
		}
	}

	public void ClearItemSlots()
	{
		foreach (Transform child in junkItemSlotParent.transform)
		{
			junkItemSlots.Clear();
			Destroy(child.gameObject);
		}

		foreach (Transform child in consumableItemSlotParent.transform)
		{
			consumableItemSlots.Clear();
			Destroy(child.gameObject);
		}

		foreach (Transform child in questItemSlotParent.transform)
		{
			questItemSlots.Clear();
			Destroy(child.gameObject);
		}

		foreach (Transform child in EquipablePlantItemSlotParent.transform)
		{
			equippablePlantItemSlots.Clear();
			Destroy(child.gameObject);
		}

		foreach (Transform child in meleeWeaponItemSlotParent.transform)
		{
			meleeWeaponItemSlots.Clear();
			Destroy(child.gameObject);
		}

		foreach (Transform child in armorItemSlotParent.transform)
		{
			armorItemSlots.Clear();
			Destroy(child.gameObject);
		}
	}

	public void ResetPlantsHealth()
	{
		for (int i = 0; i < equippablePlantItems.Count; i++)
		{
			equippablePlantItems[i].plantSO.currentHealth = equippablePlantItems[i].plantSO.defaultHealth;
		}
	}

	#region Invoke Events
	public void InvokeOnPlantItemSelected()
	{
		OnPlantItemSelected?.Invoke(this, EventArgs.Empty);
	}
	#endregion

	public void AddItem(Item item)
	{
		item = Instantiate(item);

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
		else if (item is MeleeWeapon meleeWeapon)
		{
			weaponItems.Add(meleeWeapon);
			RefreshMeleeWeaponItemSlots();
		}
		else if (item is Armor armor)
		{
			armorItems.Add(armor);
			RefreshArmorItemSlots();
		}
	}

	public void RemoveItem(Item item)
	{

        if(item is JunkItem junkItem)
		{
			junkItems.Remove(junkItem);
			//RefreshJunkItemSlots();
		}
		else if (item is ConsumableItem consumableItem)
		{
			consumableItems.Remove(consumableItem);
			RefreshConsumableItemSlots();
		}
		else if (item is QuestItem questItem)
		{
			questItems.Remove(questItem);
			//RefreshQuestItemSlots();
		}
		else if(item is MeleeWeapon meleeWeapon)
		{
			weaponItems.Remove(meleeWeapon);
			//RefreshMeleeWeaponItemSlots();
		}
		else if(item is Armor armorItem)
		{
			armorItems.Remove(armorItem);
			//RefreshArmorItemSlots();
		}
	}
}
