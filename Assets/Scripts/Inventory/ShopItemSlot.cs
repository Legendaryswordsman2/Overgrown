using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public enum ShopItemSlotMode { Buying, Selling }
public class ShopItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[field: SerializeField] public Item item { get; private set; }
	[SerializeField] Image icon;
	[SerializeField] TMP_Text nameText;
	[SerializeField] TMP_Text costText;

	[Space]

	Shop shop;

	public ShopItemSlotMode itemSlotMode { get; private set; } = ShopItemSlotMode.Buying;

	public void SetSlot(Item _item, ShopItemSlotMode _itemSlotMode)
	{
		shop = Shop.instance;

		itemSlotMode = _itemSlotMode;

		item = _item;
		icon.sprite = item.Icon;
		icon.enabled = true;
		nameText.text = item.ItemName;

		if(itemSlotMode == ShopItemSlotMode.Buying)
		{
			costText.color = shop.buyColor;
			costText.text = "$" + item.price.ToString("#,#");
		}
		else
		{
			costText.color = shop.sellColor;
			costText.text = "$" + item.sellPrice.ToString("#,#");
		}

	}
	public void ItemSelected()
	{

		if (itemSlotMode == ShopItemSlotMode.Buying)
			BuyItem();
		else if(itemSlotMode == ShopItemSlotMode.Selling)
		{
			// Sell Item
		}
	}
	public void BuyItem()
	{
		Inventory inventory = Inventory.instance;

		if (item.price <= GameManager.instance.player.GetComponent<PlayerLevel>().money)
		{
			inventory.AddItem(item);
			inventory.textPopup.SetPopup("ITEM BOUGHT", 0.5f);
			GameManager.instance.player.GetComponent<PlayerLevel>().TakeMoney(item.price);
		}
		else
		{
			inventory.textPopup.SetPopup("NOT ENOUGH MONEY", 0.5f, false, Color.red);
		}
	}

	private void OnValidate()
	{
		if (item == null)
		{
			icon.enabled = false;
			nameText.text = "";
			costText.text = "";
			return;
		}

		icon.sprite = item.Icon;
		icon.enabled = true;
		nameText.text = item.ItemName;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (item is EquipablePlantItem plantItem)
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
