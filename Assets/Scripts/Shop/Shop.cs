using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shop : MonoBehaviour
{
	public static Shop instance;

	[SerializeField] GameObject shopMainMenu;
	[SerializeField] GameObject buyingCategoriesParent;
	[SerializeField] GameObject sellingCategoriesParent;
	public ItemInfoBox itemInfoBox;
	[SerializeField] GameObject sellSubMenu;

	[Header("Buying Items")]
	[SerializeField] GameObject itemsCategory;
	[SerializeField] GameObject gearCategory;

	[SerializeField] GameObject junkItemsCategory;
	[SerializeField] GameObject consumableItemsCategory;
	[SerializeField] GameObject weaponItemsCategory;
	[SerializeField] GameObject armorItemsCategory;

	[SerializeField] GameObject junkItemSlotsParent;
	[SerializeField] GameObject consumableItemSlotsParent;
	[SerializeField] GameObject weaponItemSlotsParent;
	[SerializeField] GameObject armorItemSlotsParent;
	public Color buyColor = Color.red;

	[Header("Selling Items")]
	[SerializeField] GameObject sellingItemsCategory;
	[SerializeField] GameObject sellingGearCategory;

	[SerializeField] GameObject sellingJunkItemsCategory;
	[SerializeField] GameObject sellingConsumableItemsCategory;
	[SerializeField] GameObject sellingWeaponItemsCategory;
	[SerializeField] GameObject sellingArmorItemsCategory;


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
		Debug.Log("Initializing");
		// Setup Sellable Item Slots
		SetupSellableItemSlots();
		initialized = true;
	}

    private void OnEnable()
    {
		GameManager.playerInputActions.Player.Back.performed += Back_performed;
	}

    private void OnDisable()
    {
		GameManager.playerInputActions.Player.Back.performed -= Back_performed;
	}

    private void SetupSellableItemSlots()
	{
		for (int i = 0; i < inventory.junkItems.Count; i++)
		{
			if (inventory.junkItems[i].Sellable == true)
				Instantiate(shopItemSlotPrefab, sellingJunkItemSlotsParent.transform).GetComponent<ShopItemSlot>().SetSlot(inventory.junkItems[i], ShopItemSlotMode.Selling);
		}

		for (int i = 0; i < inventory.consumableItems.Count; i++)
		{
			if (inventory.consumableItems[i].Sellable == true)
				Instantiate(shopItemSlotPrefab, sellingConsumableItemSlotsParent.transform).GetComponent<ShopItemSlot>().SetSlot(inventory.consumableItems[i], ShopItemSlotMode.Selling);
		}

		for (int i = 0; i < inventory.weaponItems.Count; i++)
		{
			if (inventory.weaponItems[i].Sellable == true)
				Instantiate(shopItemSlotPrefab, sellingWeaponItemSlotsParent.transform).GetComponent<ShopItemSlot>().SetSlot(inventory.weaponItems[i], ShopItemSlotMode.Selling);
		}

		for (int i = 0; i < inventory.armorItems.Count; i++)
		{
			if (inventory.armorItems[i].Sellable == true)
				Instantiate(shopItemSlotPrefab, sellingArmorItemSlotsParent.transform).GetComponent<ShopItemSlot>().SetSlot(inventory.armorItems[i], ShopItemSlotMode.Selling);
		}
	}

	private void Back_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
			ResetShopView(true);
	}

	public void GoToStartOfList()
	{
		junkItemSlotsParent.transform.position = new Vector3(junkItemSlotsParent.transform.position.x, 0);
		consumableItemSlotsParent.transform.position = new Vector3(consumableItemSlotsParent.transform.position.x, 0);
	}

	public void ResetShopView(bool canCloseShop)
	{
		if (canCloseShop && shopMainMenu.activeSelf)
		{
			bool success = GameManager.CloseOverlay(gameObject);
			if (success) GameManager.instance.playerHealthBar.SetActive(true);
			if (success)
			{
				inventory.ClearItemSlots();
				inventory.SetItemSlots();
				return;
			}

		}

		ResetTabs();

		if (initialized)
		{
			ClearSellItemSlots();
			SetupSellableItemSlots();
		}

		if(sellingItemsCategory.activeSelf || sellingGearCategory.activeSelf)
		{

			foreach (Transform child in sellingCategoriesParent.transform)
			{
				child.gameObject.SetActive(false);
			}

			itemInfoBox.gameObject.SetActive(false);

			sellSubMenu.SetActive(true);
			return;
		}

		shopMainMenu.SetActive(true);

		foreach (Transform child in buyingCategoriesParent.transform)
		{
			child.gameObject.SetActive(false);
		}

		foreach (Transform child in sellingCategoriesParent.transform)
		{
			child.gameObject.SetActive(false);
		}

		itemInfoBox.gameObject.SetActive(false);

		sellSubMenu.SetActive(false);
	}

	void ResetTabs()
	{
		junkItemsCategory.SetActive(true);
		consumableItemsCategory.SetActive(false);

		sellingJunkItemsCategory.SetActive(true);
		sellingConsumableItemsCategory.SetActive(false);

		weaponItemsCategory.SetActive(true);
		armorItemsCategory.SetActive(false);

		sellingWeaponItemsCategory.SetActive(true);
		sellingArmorItemsCategory.SetActive(false);
	}

	public void GoToCategory(GameObject categoryToOpen)
	{
		foreach (Transform child in buyingCategoriesParent.transform)
		{
			child.gameObject.SetActive(false);
		}

		foreach (Transform child in sellingCategoriesParent.transform)
		{
			child.gameObject.SetActive(false);
		}

		shopMainMenu.SetActive(false);

		sellSubMenu.SetActive(false);

		itemInfoBox.gameObject.SetActive(false);

		categoryToOpen.SetActive(true);

	}

	public void ClearSellItemSlots()
	{
		foreach (Transform child in sellingJunkItemSlotsParent.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in sellingConsumableItemSlotsParent.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in sellingWeaponItemSlotsParent.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in sellingArmorItemSlotsParent.transform)
		{
			Destroy(child.gameObject);
		}
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
