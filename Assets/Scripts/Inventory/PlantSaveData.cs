using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSaveData
{
	string ID;
	public int defaultHealth = 100;
	public int attackDamage = 10;
	public int defense = 0;
	public int critChance = 0;

	public PlantSaveData(SOPlant plantSO)
	{
		defaultHealth = plantSO.defaultHealth;
		attackDamage = plantSO.attackDamage;
		defense = plantSO.defense;
		critChance = plantSO.critChance;
	}
}
