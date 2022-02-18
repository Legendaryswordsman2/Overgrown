using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatInventory : MonoBehaviour
{
	[Header("Items")]
	public List<ConsumableItem> consumableItems;

	[SerializeField] GameObject consumableItemSlotParent;

	[SerializeField] GameObject itemSlotPrefab;

	[SerializeField] List<GameObject> consumableItemSlots;

	//int previousIndex = 0;
	//int index = 0;

	private void Awake()
	{
		SetItemSlots();
	}

	private void Update()
	{
		//previousIndex = index;
		//consumableItemSlotParent.transform.GetChild(index).GetChild(0).gameObject.SetActive(true);

		//// Scroll Up
		//if (Input.GetKeyDown(KeyCode.W))
		//{
		//	if (index <= 0) return;
		//}

		//if (Input.GetKeyDown(KeyCode.S)) // Scroll Down
		//{
		//	//if(index >= )
		//}
	}

	public void GoToStartOfList()
	{
		consumableItemSlotParent.transform.position = new Vector3(consumableItemSlotParent.transform.position.x, 0);
	}

	void SetItemSlots()
	{
		#region Clear Items

		consumableItemSlots.Clear();

		foreach (Transform child in consumableItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}
		#endregion
		#region Set Items

		for (int i = 0; i < consumableItems.Count; i++)
		{
			consumableItemSlots.Add(Instantiate(itemSlotPrefab, consumableItemSlotParent.transform));
			consumableItemSlots[i].GetComponent<ItemSlot>().SetSlot(consumableItems[i]);
		}
		#endregion
	}
}
