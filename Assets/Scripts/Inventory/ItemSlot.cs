using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
	[SerializeField] Item item;
	[SerializeField] Image icon;
	[SerializeField] TMP_Text nameText;

	public void SetSlot(Item _item)
	{
		item = _item;
		icon.sprite = item.Icon;
		icon.enabled = true;
		nameText.text = item.ItemName;
		
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
}
