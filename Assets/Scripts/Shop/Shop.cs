using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	[SerializeField] GameObject shopMainMenu;
	[SerializeField] GameObject categoriesParent;
	[SerializeField] ItemInfoBox itemInfoBox;

	[SerializeField] GameObject junkItemSlotsParent;
	[SerializeField] GameObject consumableItemSlotsParent;
	[SerializeField] GameObject weaponItemSlotsParent;
	[SerializeField] GameObject armorItemSlotsParent;

	[Space]

	[SerializeField] GameObject buyableItemSlotPrefab;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			GameManager.CloseOverlay(gameObject);
	}

	public void GoToStartOfList()
	{
		junkItemSlotsParent.transform.position = new Vector3(junkItemSlotsParent.transform.position.x, 0);
		consumableItemSlotsParent.transform.position = new Vector3(consumableItemSlotsParent.transform.position.x, 0);
	}

	public void ResetShopView()
	{
		shopMainMenu.SetActive(true);

		foreach (Transform child in categoriesParent.transform)
		{
			child.gameObject.SetActive(false);
		}

		itemInfoBox.gameObject.SetActive(false);
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
			Instantiate(buyableItemSlotPrefab, junkItemSlotsParent.transform).GetComponent<BuyableItemSlot>().SetSlot(junkItems[i]);
		}
	}
	public void SetConsumableItemSlots(ConsumableItem[] consumableItems)
	{
		for (int i = 0; i < consumableItems.Length; i++)
		{
			Instantiate(buyableItemSlotPrefab, consumableItemSlotsParent.transform).GetComponent<BuyableItemSlot>().SetSlot(consumableItems[i]);
		}
	}
	public void SetWeaponItemSlots(MeleeWeapon[] weaponItems)
	{
		for (int i = 0; i < weaponItems.Length; i++)
		{
			Instantiate(buyableItemSlotPrefab, weaponItemSlotsParent.transform).GetComponent<BuyableItemSlot>().SetSlot(weaponItems[i]);
		}
	}
	public void SetArmorItemSlots(Armor[] armorItems)
	{
		for (int i = 0; i < armorItems.Length; i++)
		{
			Instantiate(buyableItemSlotPrefab, armorItemSlotsParent.transform).GetComponent<BuyableItemSlot>().SetSlot(armorItems[i]);
		}
	}
}
