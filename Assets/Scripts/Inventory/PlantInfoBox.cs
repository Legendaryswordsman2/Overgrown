using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantInfoBox : MonoBehaviour
{
	[SerializeField] TMP_Text plantName;
	[SerializeField] ProgressBar plantHealthBar;
	[SerializeField] ProgressBar plantXPBar;

	[Space]

	[SerializeField] TMP_Text levelTextStat;
	[SerializeField] TMP_Text healthTextStat;
	[SerializeField] TMP_Text damageTextStat;
	[SerializeField] TMP_Text defenseTextStat;
	[SerializeField] TMP_Text critChanceTextStat;
	public void SetInfoBoxForPlayer(PlayerStats playerStats, PlayerLevel playerLevel)
    {
		plantName.text = "PLAYER";
		plantHealthBar.max = playerStats.maxHealth;
		plantHealthBar.current = playerStats.currentHealth;

		plantXPBar.max = playerLevel.xpToLevelUp;
		plantXPBar.current = playerLevel.xp;

		levelTextStat.text = "Level: " + playerLevel.playerLevel;

		healthTextStat.text = "Health: " + playerStats.currentHealth + " / " + playerStats.maxHealth;
		damageTextStat.text = "Damage: " + playerStats.damage;
		defenseTextStat.text = "Defense: " + playerStats.defense;
		critChanceTextStat.text = "Crit: " + playerStats.critChance;

		gameObject.SetActive(true);
    }
}
