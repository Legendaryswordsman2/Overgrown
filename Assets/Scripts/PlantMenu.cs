using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlantMenu : MonoBehaviour
{
	//[SerializeField] Item item;
    GameObject inventory;

    TMP_Text amountText;
	GameManager gameManager;
	Button plantButton;
	
	private void Start()
	{
		gameManager = GameManager.instance;
		amountText = transform.GetChild(2).GetComponent<TMP_Text>();
		inventory = gameManager.inventory;
		plantButton = GetComponent<Button>();

		//amountText.text = inventory.GetItemCount(item).ToString();

		if(amountText.text == "0")
		{
			plantButton.enabled = false;
		}
	}
	public void plant()
	{
		//Debug.Log("hoherg" + item.ID);
		//if (inventory.RemoveItem(item))
		//{
		//	Debug.Log("Removed Item");
		//}
		//else
		//{
		//	Debug.Log("couldn't remove item");
		//}

		//amountText.text = inventory.GetItemCount(item).ToString();

		if (amountText.text == "0")
		{
			plantButton.enabled = false;
		}
	}
}
