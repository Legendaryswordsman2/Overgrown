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
	[SerializeField] TMP_Text sellPriceText;


	public Image equippedCheckmark;
	public void SetSlot(Item _item)
	{
		item = _item;
		icon.sprite = item.Icon;
		icon.enabled = true;
		nameText.text = item.ItemName;
		item.itemSlotReference = this;
		
	}
	public void ItemSelected()
	{
		if (Inventory.instance.selectionMode == SelectionMode.Sell)
		{
			if (!item.Sellable)
			{
				Inventory.instance.textPopup.SetPopup("CANT SELL ITEM", 0.5f, false, Color.red);
				return;
			}

			GameManager.instance.player.GetComponent<PlayerLevel>().GiveMoney(item.sellPrice);
			Inventory.instance.textPopup.SetPopup("ITEM SOLD", 0.5f);
			Inventory.instance.itemInfoBox.gameObject.SetActive(false);
			Inventory.instance.RemoveItem(item);
			//Destroy(gameObject);
			return;
		}

		item.ItemSelected(this);
	}

	public void ShowSellPrice()
	{
		sellPriceText.text = item.sellPrice.ToString("#,#");
		sellPriceText.gameObject.SetActive(true);
	}

	public void HideSellPrice()
	{
		sellPriceText.gameObject.SetActive(false);
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
