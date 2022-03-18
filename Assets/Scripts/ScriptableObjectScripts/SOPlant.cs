using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plant", fileName = "New Plant")]
public class SOPlant : ScriptableObject
{
	[Header("Unit Settings")]
	public string UnitName;
	public int defaultHealth;
	public int damage;

	[Header("Leveling")]
	public int healthIncreasePerLevelUp = 10;
	public int damageIncreasePerLevelUp = 2;
	public RuntimeAnimatorController animatorController;
}
