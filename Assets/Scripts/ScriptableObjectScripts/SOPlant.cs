using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plant", fileName = "New Plant")]
public class SOPlant : ScriptableObject
{
	[Header("Unit Settings")]
	public string unitName = "Some Random Plant";
	public int defaultHealth = 100;
	public int attackDamage = 10;
	public int defense = 0;
	public int critChance = 0;

	public RuntimeAnimatorController animatorController;
}
