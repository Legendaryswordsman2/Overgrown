using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlantItemSaveData
{
	public string itemID;
	public int defaultHealth = 100;
	public int currentHealth = 0;
	public int meleeDamage = 10;
	public int defense = 0;
	public int critChance = 0;

	public bool isEquipped;

	public PlantItemSaveData(EquipablePlantItem plantItem)
	{
		itemID = plantItem.ID;
		isEquipped = plantItem.isEquipped;

		defaultHealth = plantItem.plantSO.defaultHealth;
		currentHealth = plantItem.plantSO.currentHealth;
		meleeDamage = plantItem.plantSO.meleeDamage;
		defense = plantItem.plantSO.defense;
		critChance = plantItem.plantSO.critChance;
	}
}
