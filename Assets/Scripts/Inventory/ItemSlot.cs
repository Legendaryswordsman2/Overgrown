using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[field: SerializeField] public Item item { get; private set; }
	[SerializeField] Image icon;
	[SerializeField] TMP_Text nameText;

	public Image equippedCheckmark;

	public void SetSlot(Item _item)
	{
		item = _item;
		icon.sprite = item.Icon;
		icon.enabled = true;
		nameText.text = item.ItemName;
		
	}
	public void ItemSelected()
	{
		item.ItemSelected(this);
	}

	private void OnValidate()
	{
		if (item == null)
		{
			icon.enabled = false;
			nameText.text = "";
			return;
		}


		icon.sprite = item.Icon;
		icon.enabled = true;
		nameText.text = item.ItemName;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(item is EquipablePlantItem plantItem)
		{
			Inventory.instance.plantInfoBox.SetInfoBox(plantItem.plantSO);
			return;
		}
		Inventory.instance.itemInfoBox.SetInfoBox(item.ItemName.ToUpper(), item.ItemDescription.ToUpper());
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Inventory.instance.itemInfoBox.gameObject.SetActive(false);
		Inventory.instance.plantInfoBox.gameObject.SetActive(false);
	}
}
