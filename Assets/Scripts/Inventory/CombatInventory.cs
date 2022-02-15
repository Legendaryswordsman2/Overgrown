using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatInventory : MonoBehaviour
{
	[Header("Items")]
	public List<ConsumableItem> consumableItems;

	[SerializeField] GameObject consumableItemSlotParent;

	[SerializeField] GameObject itemSlotPrefab;

	private void Awake()
	{
		SetItemSlots();
	}

	public void GoToStartOfList()
	{
		consumableItemSlotParent.transform.position = new Vector3(consumableItemSlotParent.transform.position.x, 0);
	}

	void SetItemSlots()
	{
		#region Clear Items

		foreach (Transform child in consumableItemSlotParent.transform)
		{
			Destroy(child.gameObject);
		}
		#endregion
		#region Set Items

		for (int i = 0; i < consumableItems.Count; i++)
		{
			Instantiate(itemSlotPrefab, consumableItemSlotParent.transform).GetComponent<ItemSlot>().SetSlot(consumableItems[i]);
		}
		#endregion
	}
}
