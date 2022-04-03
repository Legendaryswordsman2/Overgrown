using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStorefront : MonoBehaviour
{

	[Header("Items on sale")]
	[SerializeField] JunkItem[] junkItems;
	[SerializeField] ConsumableItem[] consumableItems;
	[SerializeField] MeleeWeapon[] weapons;
	[SerializeField] Armor[] armor;

	[SerializeField] Shop shop;

	[SerializeField] GameObject openShopIcon;

	bool isInRange;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		isInRange = true;
		openShopIcon.SetActive(true);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		isInRange = false;
		openShopIcon.SetActive(false);
	}

	private void Update()
	{
		if(isInRange && Input.GetKeyDown(KeyCode.E))
		{
			shop.ResetShopView(false);
			bool successfullyOpenedShop = GameManager.OpenOverlay(shop.gameObject);
			if (successfullyOpenedShop == false) return;

			SetShopItemSlots();

			GameManager.instance.playerHealthBar.SetActive(false);
			GameManager.StopTime();
		}
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
