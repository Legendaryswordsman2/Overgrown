using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStorefront : MonoBehaviour
{
	bool isInRange;

	[SerializeField] Shop shop;

	[SerializeField] GameObject openShopIcon;

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
			shop.ResetShopView();
			bool successfullyOpenedShop = GameManager.OpenOverlay(shop.gameObject);
			if (successfullyOpenedShop == false) return;


			GameManager.instance.playerHealthBar.SetActive(false);
			GameManager.StopTime();
		}

		//if (Input.GetKeyDown(KeyCode.Escape) && shop.gameObject.activeSelf)
		//{
		//	bool successfullyClosedShop = GameManager.CloseOverlay(shop.gameObject);
		//	if(successfullyClosedShop) GameManager.StartTime();
		//}
	}

}
