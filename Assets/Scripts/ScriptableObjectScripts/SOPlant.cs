using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plant", fileName = "New Plant")]
[System.Serializable]
public class SOPlant : ScriptableObject
{
	[Header("Unit Settings")]
	public string unitName = "Some Random Plant";
	public int defaultHealth = 100;
	public int attackDamage = 10;
	public int critChance = 0;

	[Header("Leveling")]
	public int healthIncreasePerLevelUp = 10;
	public int damageIncreasePerLevelUp = 2;
	public RuntimeAnimatorController animatorController;
}
