using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantInfoBox : MonoBehaviour
{
	[SerializeField] TMP_Text plantName;
	[SerializeField] ProgressBar plantHealthBar;

	[SerializeField] TMP_Text healthTextStat;
	[SerializeField] TMP_Text damageTextStat;
	[SerializeField] TMP_Text defenseTextStat;
	[SerializeField] TMP_Text critChanceTextStat;

	public void SetInfoBox(SOPlant plantSO)
	{
		plantName.text = plantSO.unitName.ToUpper();
		plantHealthBar.max = plantSO.defaultHealth;
		plantHealthBar.current = plantSO.currentHealth;

		healthTextStat.text = "Health: " + plantSO.currentHealth + " / " + plantSO.defaultHealth;
		damageTextStat.text = "Damage: " + plantSO.meleeDamage;
		defenseTextStat.text = "Defense: " + plantSO.defense;
		critChanceTextStat.text = "Crit: " + plantSO.critChance;

		gameObject.SetActive(true);
	}

	public void SetInfoBoxForPlayer(PlayerStats playerStats)
    {
		plantName.text = "PLAYER";
		plantHealthBar.max = playerStats.maxHealth;
		plantHealthBar.current = playerStats.currentHealth;

		healthTextStat.text = "Health: " + playerStats.currentHealth + " / " + playerStats.maxHealth;
		damageTextStat.text = "Damage: " + playerStats.damage;
		defenseTextStat.text = "Defense: " + playerStats.defense;
		critChanceTextStat.text = "Crit: " + playerStats.critChance;

		gameObject.SetActive(true);
    }
}
