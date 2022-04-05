using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shop : MonoBehaviour
{
	public static Shop instance;

	[SerializeField] GameObject shopMainMenu;
	[SerializeField] GameObject categoriesParent;
	[SerializeField] ItemInfoBox itemInfoBox;
	[SerializeField] GameObject sellSubMenu;

	[Header("Buying Items")]
	[SerializeField] GameObject junkItemSlotsParent;
	[SerializeField] GameObject consumableItemSlotsParent;
	[SerializeField] GameObject weaponItemSlotsParent;
	[SerializeField] GameObject armorItemSlotsParent;
	public Color buyColor = Color.red;

	[Header("Selling Items")]
	[SerializeField] GameObject sellingJunkItemSlotsParent;
	[SerializeField] GameObject sellingConsumableItemSlotsParent;
	[SerializeField] GameObject sellingWeaponItemSlotsParent;
	[SerializeField] GameObject sellingArmorItemSlotsParent;
	public Color sellColor = Color.green;

	[Space]

	[SerializeField] GameObject shopItemSlotPrefab;

	bool initialized = false;

	Inventory inventory;

	private void Awake()
	{
		instance = this;

		inventory = Inventory.instance;

		Initialize();
	}
	public void Initialize()
	{
		for (int i = 0; i < inventory.junkItems.Count; i++)
		{
			Instantiate(shopItemSlotPrefab, sellingJunkItemSlotsParent.transform).GetComponent<ShopItemSlot>().SetSlot(inventory.junkItems[i], ShopItemSlotMode.Selling);
		}
	}


	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			ResetShopView(true);
	}

	public void GoToStartOfList()
	{
		junkItemSlotsParent.transform.position = new Vector3(junkItemSlotsParent.transform.position.x, 0);
		consumableItemSlotsParent.transform.position = new Vector3(consumableItemSlotsParent.transform.position.x, 0);
	}

	public void ResetShopView(bool canCloseShop)
	{
		if(Inventory.instance.gameObject.activeSelf)
		{
			sellSubMenu.SetActive(true);
			shopMainMenu.SetActive(false);

			foreach (Transform child in categoriesParent.transform)
			{
				child.gameObject.SetActive(false);
			}

			itemInfoBox.gameObject.SetActive(false);
			return;
		}

		if (canCloseShop && shopMainMenu.activeSelf)
		{
			bool success = GameManager.CloseOverlay(gameObject);
			if (success) GameManager.instance.playerHealthBar.SetActive(true);
			if(success) return;
		}

		shopMainMenu.SetActive(true);

		foreach (Transform child in categoriesParent.transform)
		{
			child.gameObject.SetActive(false);
		}

		itemInfoBox.gameObject.SetActive(false);

		sellSubMenu.SetActive(false);
	}

	public void GoToCategory(GameObject categoryToOpen)
	{
		foreach (Transform child in categoriesParent.transform)
		{
			child.gameObject.SetActive(false);
		}
		shopMainMenu.SetActive(false);

		categoryToOpen.SetActive(true);
	}

	#region Buying Items
	public void clearShopItemSlots()
	{
		foreach (Transform child in junkItemSlotsParent.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in consumableItemSlotsParent.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in weaponItemSlotsParent.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in armorItemSlotsParent.transform)
		{
			Destroy(child.gameObject);
		}
	}
	public void SetJunkItemSlots(JunkItem[] junkItems)
	{
		for (int i = 0; i < junkItems.Length; i++)
		{
			Instantiate(shopItemSlotPrefab, junkItemSlotsParent.transform).GetComponent<ShopItemSlot>().SetSlot(junkItems[i], ShopItemSlotMode.Buying);
		}
	}
	public void SetConsumableItemSlots(ConsumableItem[] consumableItems)
	{
		for (int i = 0; i < consumableItems.Length; i++)
		{
			Instantiate(shopItemSlotPrefab, consumableItemSlotsParent.transform).GetComponent<ShopItemSlot>().SetSlot(consumableItems[i], ShopItemSlotMode.Buying);
		}
	}
	public void SetWeaponItemSlots(MeleeWeapon[] weaponItems)
	{
		for (int i = 0; i < weaponItems.Length; i++)
		{
			Instantiate(shopItemSlotPrefab, weaponItemSlotsParent.transform).GetComponent<ShopItemSlot>().SetSlot(weaponItems[i], ShopItemSlotMode.Buying);
		}
	}
	public void SetArmorItemSlots(Armor[] armorItems)
	{
		for (int i = 0; i < armorItems.Length; i++)
		{
			Instantiate(shopItemSlotPrefab, armorItemSlotsParent.transform).GetComponent<ShopItemSlot>().SetSlot(armorItems[i], ShopItemSlotMode.Buying);
		}
	}
	#endregion

}
