using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInputManager : MonoBehaviour
{
	Inventory inventory;

	[SerializeField] GameObject categoryButtons;
	[SerializeField] GameObject categories;

	private void Awake()
	{
		inventory = Inventory.instance;
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && inventory.selectionMode != SelectionMode.Sell)
		{
			ResetInventoryView();
		}
	}
	public void ResetInventoryView(bool canCloseInventory = true)
	{
		if (canCloseInventory && categoryButtons.activeSelf)
		{
			GameManager.instance.CloseInventory();
			return;
		}

        if (inventory.useItemScreen.gameObject.activeSelf)
        {
			inventory.GoToCategory(Inventory.instance.consumableItemsCategory);
			inventory.ItemsCategory.SetActive(true);
			return;
			//inventory.useItemScreen.gameObject.SetActive(false);
		}

		inventory.ResetTabs();

		foreach (Transform child in categories.transform)
		{
			child.gameObject.SetActive(false);
		}

		inventory.itemInfoBox.gameObject.SetActive(false);
		inventory.plantInfoBox.gameObject.SetActive(false);

		categoryButtons.SetActive(true);
	}
}
