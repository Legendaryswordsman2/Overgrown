using System;
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

		if (BattleSystem.instance == null)
			GameManager.playerInputActions.Player.Back.performed += Back_performed;
		else
			CombatManager.playerInputActions.Player.Back.performed += Back_performed;
	}

    private void Back_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
		if(GameManager.currentlyOpenOverlay == gameObject)
		ResetInventoryView();
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
            inventory.itemInfoBox.gameObject.SetActive(false);
            inventory.plantInfoBox.gameObject.SetActive(false);
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
