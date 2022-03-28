using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatsSaveData
{
	// Stats
	public int maxHealth = 100;
	public int currentHealth = 0;
	public int defense = 0;
	public int meleeDamage = 10;
	public int rangedDamage = 10;
	public int critChance = 0;

	// Modifiers
	[Header("Stat Modifiers")]
	public int defenseModifier;
	public int meleeDamageModifier;
	public int rangedDamageModifier;

	public PlayerStatsSaveData(PlayerStats playerStats)
	{
		maxHealth = playerStats.maxHealth;
		currentHealth = playerStats.currentHealth;
		defense = playerStats.defense;
		meleeDamage = playerStats.meleeDamage;
		rangedDamage = playerStats.rangedDamage;
		critChance = playerStats.critChance;

		// Modifiers
		//defenseModifier = playerStats.defenseModifier;
		//meleeDamageModifier = playerStats.meleeDamageModifier;
		//rangedDamageModifier = playerStats.rangedDamageModifier;
	}
}
