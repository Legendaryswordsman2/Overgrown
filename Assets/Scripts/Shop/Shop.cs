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
}
