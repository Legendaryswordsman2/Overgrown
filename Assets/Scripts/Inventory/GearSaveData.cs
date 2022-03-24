using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GearSaveData
{
	public string itemID;
	public bool isEquipped;

	public GearSaveData(string _itemID, bool _isEquipped)
	{
		itemID = _itemID;
		isEquipped = _isEquipped;
	}
}
