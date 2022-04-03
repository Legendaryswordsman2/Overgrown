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

	public void SetJunkItemSlots(JunkItem[] junkItems)
	{
		for (int i = 0; i < junkItems.Length; i++)
		{
			Instantiate(buyableItemSlotPrefab, junkItemSlotsParent.transform).GetComponent<BuyableItemSlot>().SetSlot(junkItems[i]);
		}
	}
}
