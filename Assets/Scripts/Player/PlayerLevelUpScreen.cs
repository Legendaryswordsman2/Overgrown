using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLevelUpScreen : MonoBehaviour
{
	[SerializeField] GameObject levelupParent;

	[Header("Player Stats")]
	[SerializeField] TMP_Text healthTextStat;
	[SerializeField] TMP_Text damageTextStat;
	[SerializeField] TMP_Text defenseTextStat;
	[SerializeField] TMP_Text critChanceTextStat;

	[Header("Increase Stats")]
	[SerializeField] TMP_Text healthTextStatIncrease;
	[SerializeField] TMP_Text damageTextStatIncrease;
	[SerializeField] TMP_Text defenseTextStatIncrease;
	[SerializeField] TMP_Text critChanceTextStatIncrease;

	int previousHealth;
	int previousDamage;
	int previousDefense;
	int previousCritChance;

	PlayerStats playerStats;
	int[] statIncreases;

	public void SetLevelUpScreen(PlayerStats _playerStats, int[] _statIncreases)
	{
		playerStats = _playerStats;

		statIncreases = _statIncreases;

		previousHealth = playerStats.maxHealth;
		previousDamage = playerStats.damage;
		previousDefense = playerStats.defense;
		previousCritChance = playerStats.critChance;

		healthTextStat.text = "HEALTH: " + playerStats.maxHealth;
		damageTextStat.text = "DAMAGE: " + playerStats.damage;
		defenseTextStat.text = "DEFENSE: " + playerStats.defense;
		critChanceTextStat.text = "CRIT: " + playerStats.critChance;

		healthTextStatIncrease.text = "+ " + statIncreases[0];
		damageTextStatIncrease.text = "+ " + statIncreases[1];
		defenseTextStatIncrease.text = "+ " + statIncreases[2];
		critChanceTextStatIncrease.text = "+ " + statIncreases[3];

		levelupParent.SetActive(true);
		gameObject.SetActive(true);
	}
	void AddStats()
	{
		StartCoroutine(ApplyStatIncreaseToHealthStat());
	}

	IEnumerator ApplyStatIncreaseToHealthStat()
	{
		previousHealth++;
		healthTextStat.text = "HEALTH: " + previousHealth;

		if (previousHealth >= playerStats.maxHealth) 
		{
			yield break;
		}

		//Debug.Log("Added One");
		yield return new WaitForSeconds(0.1f);

		StartCoroutine(ApplyStatIncreaseToHealthStat());
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Space Clicked");
			AddStats();
		}
	}
}
