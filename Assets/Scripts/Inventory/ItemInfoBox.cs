using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInfoBox : MonoBehaviour
{
	[SerializeField] TMP_Text itemName;
	[SerializeField] TMP_Text itemDescription;

	public void SetInfoBox(string _itemName, string _itemDescription)
	{
		itemName.text = _itemName;
		itemDescription.text = _itemDescription;
	}
}
