using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSaveData
{
	string ID;
	public int defaultHealth = 100;
	public int meleeDamage = 10;
	public int defense = 0;
	public int critChance = 0;

	public PlantSaveData(SOPlant plantSO)
	{
		defaultHealth = plantSO.defaultHealth;
		meleeDamage = plantSO.meleeDamage;
		defense = plantSO.defense;
		critChance = plantSO.critChance;
	}
}
