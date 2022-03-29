using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUp : MonoBehaviour
{
	[SerializeField] TMP_Text HealthTextStat;
	[SerializeField] TMP_Text MeleeDamageTextStat;
	[SerializeField] TMP_Text rangedDamageTextStat;
	[SerializeField] TMP_Text defenseTextStat;
	[SerializeField] TMP_Text critChanceTextStat;

	public void SetStatsOnLevelUp()
	{
		PlayerStats playerStats = BattleSystem.instance.GetComponent<PlayerStats>();
		HealthTextStat.text = "HEALTH: " + playerStats.maxHealth + " + 2";
	}
}
