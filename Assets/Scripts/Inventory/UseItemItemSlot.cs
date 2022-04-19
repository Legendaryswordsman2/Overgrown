using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class UseItemItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[field: SerializeField] public Item item { get; private set; }
	[SerializeField] Image icon;
	[SerializeField] TMP_Text nameText;
	public void SetSlot(Item _item)
	{
		item = _item;
		icon.sprite = item.Icon;
		icon.enabled = true;
		nameText.text = item.ItemName;
		
	}
	public void Heal()
	{
		if (item is EquipablePlantItem plantItem && UseableItemManager.instance.currentItemToBeUsed.effects[0] is HealthItemEffect healthEffect)
        {
			plantItem.HealPlant(healthEffect.healAmount);
        }
		else if(item is PlayerItem && UseableItemManager.instance.currentItemToBeUsed.effects[0] is HealthItemEffect healEffect)
        {
			PlayerStats.instance.Heal(healEffect.healAmount);
        }
		Inventory inventory = Inventory.instance;
		inventory.junkItemsCategory.SetActive(false);
		inventory.questItemsCategory.SetActive(false);
		inventory.consumableItemsCategory.SetActive(true);
		inventory.GoToCategory(Inventory.instance.ItemsCategory);
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
