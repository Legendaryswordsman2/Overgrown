using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plant", fileName = "New Plant")]
public class SOPlant : ScriptableObject
{
	[Header("Unit Settings")]
	public string UnitName = "Some Random Plant";
	public int defaultHealth = 100;
	public int damage = 10;

	[Header("Leveling")]
	public int healthIncreasePerLevelUp = 10;
	public int damageIncreasePerLevelUp = 2;
	public RuntimeAnimatorController animatorController;
}
