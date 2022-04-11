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

	int selectionIndex = 0;

	bool hasIncreasedStats = false;
	bool choosingStat = false;

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
		int statIncreaseNumber = statIncreases[0];
		for (int i = 0; i < statIncreases[0]; i++)
		{
			previousHealth++;
			statIncreaseNumber--;
			healthTextStat.text = "HEALTH: " + previousHealth;
			healthTextStatIncrease.text = "+ " + statIncreaseNumber;

			if (previousHealth >= playerStats.maxHealth) yield break;

			yield return new WaitForSecondsRealtime(0.1f);
		}
	}
	IEnumerator ApplyStatIncreaseToDamageStat()
	{
		int statIncreaseNumber = statIncreases[1];
		for (int i = 0; i < statIncreases[1]; i++)
		{
			previousDamage++;
			statIncreaseNumber--;
			damageTextStat.text = "DAMAGE: " + previousDamage;
			damageTextStatIncrease.text = "+ " + statIncreaseNumber;

			if (previousDamage >= playerStats.damage) yield break;

			yield return new WaitForSecondsRealtime(0.1f);
		}
	}

	IEnumerator ApplyStatIncreaseToDefenseStat()
	{
		int statIncreaseNumber = statIncreases[2];
		for (int i = 0; i < statIncreases[2]; i++)
		{
			previousDefense++;
			statIncreaseNumber--;
			defenseTextStat.text = "DEFENSE: " + previousDefense;
			defenseTextStatIncrease.text = "+ " + statIncreaseNumber;

			if (previousDefense >= playerStats.defense) yield break;

			yield return new WaitForSecondsRealtime(0.1f);
		}
	}

	IEnumerator ApplyStatIncreaseToCritChanceStat()
	{
		int statIncreaseNumber = statIncreases[3];
		for (int i = 0; i < statIncreases[3]; i++)
		{
			previousCritChance++;
			statIncreaseNumber--;
			critChanceTextStat.text = "CRIT CHANCE: " + previousCritChance;
			critChanceTextStatIncrease.text = "+ " + statIncreaseNumber;

			if (previousCritChance >= playerStats.critChance) break;

			yield return new WaitForSecondsRealtime(0.1f);
		}

		if (choosingStat) yield break;

		yield return new WaitForSecondsRealtime(1);

		selectionIndex = 0;
		healthTextStatIncrease.gameObject.SetActive(true);
		damageTextStatIncrease.gameObject.SetActive(false);
		defenseTextStatIncrease.gameObject.SetActive(false);
		critChanceTextStatIncrease.gameObject.SetActive(false);

		choosingStat = true;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && !hasIncreasedStats)
		{
			pressKeyToContinueText.SetActive(false);
			StartCoroutine(AddStats());
			hasIncreasedStats = true;
		}

		if (choosingStat) ChooseStat();
	}

	void ChooseStat()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			switch (selectionIndex)
			{
				case (0):
					healthTextStatIncrease.gameObject.SetActive(true);
					break;
				case (1):
					damageTextStatIncrease.gameObject.SetActive(true);
					break;
				case (2):
					defenseTextStatIncrease.gameObject.SetActive(true);
					break;
				case (3):
					critChanceTextStatIncrease.gameObject.SetActive(true);
					break;
			}
		}

		if (Input.GetKeyDown(KeyCode.W))
		{
			if (selectionIndex <= 0) return;
			selectionIndex--;
			HighlightStat();
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			if (selectionIndex >= 3) return;
			selectionIndex++;
			HighlightStat();
		}
	}

	void HighlightStat()
	{
		healthTextStatIncrease.gameObject.SetActive(false);
		damageTextStatIncrease.gameObject.SetActive(false);
		defenseTextStatIncrease.gameObject.SetActive(false);
		critChanceTextStatIncrease.gameObject.SetActive(false);

		switch (selectionIndex)
		{
			case (0):
				healthTextStatIncrease.gameObject.SetActive(true);
				break;
			case (1):
				damageTextStatIncrease.gameObject.SetActive(true);
				break;
			case (2):
				defenseTextStatIncrease.gameObject.SetActive(true);
				break;
			case (3):
				critChanceTextStatIncrease.gameObject.SetActive(true);
				break;
		}
	}
}
