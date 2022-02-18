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

	public void UseItemInCombat()
	{
		BattleSystem battleSystem = BattleSystem.instance;

		Debug.Log("Clicked " + name);

		if(item is ConsumableItem c)
		{
			c.ExecuteEffect();
		}
		//battleSystem.inventory.gameObject.SetActive(false);
		Destroy(gameObject);

	}
	void DestroyItem()
	{
		Destroy(gameObject);
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
