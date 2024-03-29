using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStorefront : MonoBehaviour
{

	[Header("Items For Sale")]
	[SerializeField] JunkItem[] junkItems;
	[SerializeField] ConsumableItem[] consumableItems;
	[SerializeField] MeleeWeapon[] weapons;
	[SerializeField] Armor[] armor;

	[SerializeField] Shop shop;

	[SerializeField] GameObject openShopIcon;
	private void OnTriggerEnter2D(Collider2D collision)
	{
        InputManager.playerInputActions.Player.Interact.performed += Interact_performed;
		openShopIcon.SetActive(true);
	}

    private void OnTriggerExit2D(Collider2D collision)
	{
		InputManager.playerInputActions.Player.Interact.performed -= Interact_performed;
		openShopIcon.SetActive(false);
	}

	private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
			shop.ResetShopView(false);
			bool successfullyOpenedShop = GameManager.OpenOverlay(shop.gameObject);
			if (successfullyOpenedShop == false) return;

			SetShopItemSlots();

			GameManager.instance.playerHealthBar.SetActive(false);
			GameManager.StopTime();
	}
	void SetShopItemSlots()
	{
		shop.clearShopItemSlots();

		shop.SetJunkItemSlots(junkItems);
		shop.SetConsumableItemSlots(consumableItems);
		shop.SetWeaponItemSlots(weapons);
		shop.SetArmorItemSlots(armor);
	}
}
