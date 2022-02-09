using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public List<JunkItem> junkItems;
	public List<ConsumableItem> consumableItems;
	public List<QuestItem> questItems;

	public List<GearItem> gear;

	[SerializeField] GameObject junkItemSlotParent;
	[SerializeField] GameObject consumableItemSlotParent;
	[SerializeField] GameObject questItemSlotParent;

	[SerializeField] GameObject itemSlotPrefab;

	private void Awake()
	{
		SetItemSlots();
	}

	void SetItemSlots()
	{
		#region Clear Items
		foreach (Transform child in junkItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in consumableItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in questItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}
		#endregion
		#region Set Items
		for (int i = 0; i < junkItems.Count; i++)
		{
			Instantiate(itemSlotPrefab, junkItemSlotParent.transform).GetComponent<ItemSlot>().SetSlot(junkItems[i]);
		}

		for (int i = 0; i < consumableItems.Count; i++)
		{
			Instantiate(itemSlotPrefab, consumableItemSlotParent.transform).GetComponent<ItemSlot>().SetSlot(consumableItems[i]);
		}

		for (int i = 0; i < questItems.Count; i++)
		{
			Instantiate(itemSlotPrefab, questItemSlotParent.transform).GetComponent<ItemSlot>().SetSlot(questItems[i]);
		}
		#endregion
	}
}
