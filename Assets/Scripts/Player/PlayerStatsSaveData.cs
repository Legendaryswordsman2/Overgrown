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
	public int damage = 10;
	public int critChance = 0;

	public PlayerStatsSaveData(PlayerStats playerStats)
	{
		maxHealth = playerStats.maxHealth;
		currentHealth = playerStats.currentHealth;
		defense = playerStats.defense;
		damage = playerStats.damage;
		critChance = playerStats.critChance;
	}
}
