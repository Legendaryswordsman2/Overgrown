using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CharacterToLevelUp { Player, Plant }
public class PlayerLevelUpScreen : MonoBehaviour
{
	[SerializeField] GameObject levelUpParent;
	
	[Header("Popup Texts")]
	[SerializeField] TMP_Text popupText;
	[SerializeField] TMP_Text leveledUpText;

	[Header("Player Stats")]
	[SerializeField] TMP_Text healthTextStat;
	[SerializeField] TMP_Text damageTextStat;
	[SerializeField] TMP_Text defenseTextStat;
	[SerializeField] TMP_Text critChanceTextStat;

	[Header("Increase Stat Texts")]
	[SerializeField] TMP_Text healthTextStatIncrease;
	[SerializeField] TMP_Text damageTextStatIncrease;
	[SerializeField] TMP_Text defenseTextStatIncrease;
	[SerializeField] TMP_Text critChanceTextStatIncrease;

	[Space]

	int previousHealth;
	int previousDamage;
	int previousDefense;
	int previousCritChance;

	PlayerStats playerStats;
	int[] statIncreases;

	bool hasIncreasedStats = false;
	bool finishedLevelUp = false;

	public void SetLevelUpScreen(PlayerStats _playerStats, int[] _statIncreases)
	{
		finishedLevelUp = false;

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

		leveledUpText.text = "YOU LEVELED UP TO LEVEL " + playerStats.playerLevelSystem.level;

		healthTextStatIncrease.gameObject.SetActive(true);
		damageTextStatIncrease.gameObject.SetActive(true);
		defenseTextStatIncrease.gameObject.SetActive(true);
		critChanceTextStatIncrease.gameObject.SetActive(true);

		popupText.text = "PRESS SPACE TO CONTINUE...";
		popupText.gameObject.SetActive(true);

		hasIncreasedStats = false;

		GameManager.OpenOverlay(levelUpParent);
		//levelupParent.SetActive(true);
		gameObject.SetActive(true);
	}
	//public void SetPlantLevelUpScreen(SOPlant plant, int[] _statIncreases)
	//{
	//	choosingStat = false;
	//	finishedLevelUp = false;

	//	plantToLevelUp = plant;

	//	statIncreases = _statIncreases;

	//	previousHealth = plant.defaultHealth;
 //       previousDamage = plant.damage;
 //       previousDefense = plant.defense;
 //       previousCritChance = plant.critChance;

 //       healthTextStat.text = "HEALTH: " + plant.defaultHealth;
	//	damageTextStat.text = "DAMAGE: " + plant.damage;
	//	defenseTextStat.text = "DEFENSE: " + plant.defense;
	//	critChanceTextStat.text = "CRIT CHANCE: " + plant.critChance;

	//	healthTextStatIncrease.text = "+ " + statIncreases[0];
	//	damageTextStatIncrease.text = "+ " + statIncreases[1];
	//	defenseTextStatIncrease.text = "+ " + statIncreases[2];
	//	critChanceTextStatIncrease.text = "+ " + statIncreases[3];

	//	leveledUpText.text = plant.unitName.ToUpper() + " LEVELED UP TO LEVEL " + plant.level;

	//	healthTextStatIncrease.gameObject.SetActive(true);
	//	damageTextStatIncrease.gameObject.SetActive(true);
	//	defenseTextStatIncrease.gameObject.SetActive(true);
	//	critChanceTextStatIncrease.gameObject.SetActive(true);

	//	popupText.text = "PRESS SPACE TO CONTINUE...";
	//	popupText.gameObject.SetActive(true);

	//	hasIncreasedStats = false;

	//	GameManager.OpenOverlay(levelUpParent);
	//	gameObject.SetActive(true);
	//}
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

			int maxStat;
            maxStat = playerStats.maxHealth;

			if (previousHealth >= maxStat) yield break;

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

			int maxStat;
            maxStat = playerStats.damage;

			if (previousDamage >= maxStat) yield break;

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

			int maxStat;
            maxStat = playerStats.defense;

			if (previousDefense >= maxStat) yield break;

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

			int maxStat;
            maxStat = playerStats.critChance;

            if (previousCritChance >= maxStat) break;

			yield return new WaitForSecondsRealtime(0.1f);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && !hasIncreasedStats)
		{
			if (finishedLevelUp)
			{
				bool succesfulyLevelUp = playerStats.playerLevelSystem.TryToLevelUp();

				if (succesfulyLevelUp == false)
                { 
					if (BattleSystem.instance != null)
						BattleSystem.instance.ChangeSceneAfterWinning();
					else
						GameManager.CloseOverlay(levelUpParent);
                }
			}
			else
			{
			popupText.gameObject.SetActive(false);
			StartCoroutine(AddStats());
			hasIncreasedStats = true;
			}
		}
	}
}
