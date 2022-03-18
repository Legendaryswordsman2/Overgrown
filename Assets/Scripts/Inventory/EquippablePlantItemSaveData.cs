using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquippablePlantItemSaveData
{
	public string ID;

	public EquippablePlantItemSaveData(EquipablePlantItem plant)
	{
		ID = plant.ID;
	}
}
