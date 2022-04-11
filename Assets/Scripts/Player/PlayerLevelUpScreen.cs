using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLevelUpScreen : MonoBehaviour
{
	[SerializeField] GameObject levelUpParent;
	[SerializeField] GameObject pressKeyToContinueText;
	[SerializeField] TMP_Text leveledUpText;

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

	bool hasIncreasedStats = false;

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
		critChanceTextStat.text = "CRIT CHANCE: " + playerStats.critChance;

		healthTextStatIncrease.text = "+ " + statIncreases[0];
		damageTextStatIncrease.text = "+ " + statIncreases[1];
		defenseTextStatIncrease.text = "+ " + statIncreases[2];
		critChanceTextStatIncrease.text = "+ " + statIncreases[3];

		leveledUpText.text = "YOU LEVELED UP TO LEVEL " + playerStats.GetComponent<PlayerLevel>().playerLevel;

		hasIncreasedStats = false;

		GameManager.OpenOverlay(levelUpParent);
		//levelupParent.SetActive(true);
		gameObject.SetActive(true);
	}
	IEnumerator AddStats()
	{
		StartCoroutine(ApplyStatIncreaseToHealthStat());
		yield return new WaitForSecondsRealtime(0.1f);
		StartCoroutine(ApplyStatIncreaseToDamageStat());
		yield return new WaitForSecondsRealtime(0.1f);
		StartCoroutine(ApplyStatIncreaseToDefenseStat());
		yield return new WaitForSecondsRealtime(0.1f);
		StartCoroutine(ApplyStatIncreaseToCritChanceStat());
	}

	IEnumerator ApplyStatIncreaseToHealthStat()
	{
		for (int i = 0; i < statIncreases[0]; i++)
		{
			previousHealth++;
			healthTextStat.text = "HEALTH: " + previousHealth;

			if (previousHealth >= playerStats.maxHealth) yield break;

			yield return new WaitForSecondsRealtime(0.1f);
		}
	}
	IEnumerator ApplyStatIncreaseToDamageStat()
	{
		for (int i = 0; i < statIncreases[1]; i++)
		{
			previousDamage++;
			damageTextStat.text = "DAMAGE: " + previousDamage;

			if (previousDamage >= playerStats.damage) yield break;

			yield return new WaitForSecondsRealtime(0.1f);
		}
	}

	IEnumerator ApplyStatIncreaseToDefenseStat()
	{
		for (int i = 0; i < statIncreases[2]; i++)
		{
			previousDefense++;
			defenseTextStat.text = "DEFENSE: " + previousDefense;

			if (previousDefense >= playerStats.defense) yield break;

			yield return new WaitForSecondsRealtime(0.1f);
		}
	}

	IEnumerator ApplyStatIncreaseToCritChanceStat()
	{
		for (int i = 0; i < statIncreases[3]; i++)
		{
			previousCritChance++;
			critChanceTextStat.text = "CRIT CHANCE: " + previousCritChance;

			if (previousCritChance >= playerStats.critChance) yield break;

			yield return new WaitForSecondsRealtime(0.1f);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && !hasIncreasedStats)
		{
			pressKeyToContinueText.SetActive(false);
			StartCoroutine(AddStats());
			hasIncreasedStats = true;
		}
	}
}
