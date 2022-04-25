using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

	[SerializeField, ReadOnlyInspector] SOPlant plantToLevelUp;

	int previousHealth;
	int previousDamage;
	int previousDefense;
	int previousCritChance;

	PlayerStats playerStats;
	int[] statIncreases;

	int selectionIndex = 0;

	bool hasIncreasedStats = false;
	bool choosingStat = false;
	bool finishedLevelUp = false;

	TMP_Text chosenBonusStat;

	int rotationIndex = 1;

	private void Awake()
	{
		chosenBonusStat = healthTextStatIncrease;
	}

	public void SetPlayerLevelUpScreen(PlayerStats _playerStats, int[] _statIncreases)
	{
		choosingStat = false;
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

		leveledUpText.text = "YOU LEVELED UP TO LEVEL " + playerStats.GetComponent<PlayerLevel>().playerLevel;

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
	public void SetPlantLevelUpScreen(SOPlant plant, int[] _statIncreases)
	{
		choosingStat = false;
		finishedLevelUp = false;

		plantToLevelUp = plant;

		statIncreases = _statIncreases;

		previousHealth = plant.defaultHealth;
        previousDamage = plant.damage;
        previousDefense = plant.defense;
        previousCritChance = plant.critChance;

        healthTextStat.text = "HEALTH: " + plant.defaultHealth;
		damageTextStat.text = "DAMAGE: " + plant.damage;
		defenseTextStat.text = "DEFENSE: " + plant.defense;
		critChanceTextStat.text = "CRIT CHANCE: " + plant.critChance;

		healthTextStatIncrease.text = "+ " + statIncreases[0];
		damageTextStatIncrease.text = "+ " + statIncreases[1];
		defenseTextStatIncrease.text = "+ " + statIncreases[2];
		critChanceTextStatIncrease.text = "+ " + statIncreases[3];

		leveledUpText.text = plant.unitName.ToUpper() + " LEVELED UP TO LEVEL " + plant.level;

		healthTextStatIncrease.gameObject.SetActive(true);
		damageTextStatIncrease.gameObject.SetActive(true);
		defenseTextStatIncrease.gameObject.SetActive(true);
		critChanceTextStatIncrease.gameObject.SetActive(true);

		popupText.text = "PRESS SPACE TO CONTINUE...";
		popupText.gameObject.SetActive(true);

		hasIncreasedStats = false;

		GameManager.OpenOverlay(levelUpParent);
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

			int maxStat;
			if (plantToLevelUp == null)
				maxStat = playerStats.maxHealth;
			else
				maxStat = plantToLevelUp.defaultHealth;

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
			if (plantToLevelUp == null)
				maxStat = playerStats.damage;
			else
				maxStat = plantToLevelUp.damage;

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
			if (plantToLevelUp == null)
				maxStat = playerStats.defense;
			else
				maxStat = plantToLevelUp.defense;

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
			if (plantToLevelUp == null)
				maxStat = playerStats.critChance;
			else
				maxStat = plantToLevelUp.critChance;

				if (previousCritChance >= maxStat) break;
			

			yield return new WaitForSecondsRealtime(0.1f);
		}

		if (choosingStat || finishedLevelUp) yield break;

		yield return new WaitForSecondsRealtime(1);

		selectionIndex = 0;
		healthTextStatIncrease.gameObject.SetActive(true);
		damageTextStatIncrease.gameObject.SetActive(false);
		defenseTextStatIncrease.gameObject.SetActive(false);
		critChanceTextStatIncrease.gameObject.SetActive(false);

		popupText.text = "CHOOSE STAT BONUS...";
		popupText.gameObject.SetActive(true);

		chosenBonusStat = healthTextStatIncrease;

		choosingStat = true;
		StartCoroutine(RotateChosenStatNumbers());
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && !hasIncreasedStats)
		{
			if (finishedLevelUp)
			{
				bool succesfulyLevelUp = playerStats.GetComponent<PlayerLevel>().TryToLevelUp();

				if (succesfulyLevelUp == false)
                {
					if (plantToLevelUp == null)
					{
						bool plantLeveledUp = Inventory.instance.equippedPlantItem.plantSO.GiveXP(BattleSystem.instance.xpGiven);

						if (plantLeveledUp) return;
					}
                    else
                    {
						bool plantLeveledUpAgain = Inventory.instance.equippedPlantItem.plantSO.TryToLevelUp();

						if (plantLeveledUpAgain) return;
					}

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

		if (choosingStat) ChooseStat();
	}

	void ChooseStat()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			StopAllCoroutines();
			StartCoroutine(ConfirmChosenStat());
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

	IEnumerator RotateChosenStatNumbers()
	{
		if (!choosingStat) yield break;

		yield return new WaitForSecondsRealtime(0.05f);
		if (rotationIndex == 4)
		{
			chosenBonusStat.text = "+ " + 1;
			rotationIndex = 1;
		}
		else
		{
			rotationIndex++;
			chosenBonusStat.text = "+ " + rotationIndex;
		}

		if (choosingStat) StartCoroutine(RotateChosenStatNumbers());
	}

	void HighlightStat()
	{
		chosenBonusStat.gameObject.SetActive(false);

		switch (selectionIndex)
		{
			case (0):
				chosenBonusStat = healthTextStatIncrease;
				break;
			case (1):
				chosenBonusStat = damageTextStatIncrease;
				break;
			case (2):
				chosenBonusStat = defenseTextStatIncrease;
				break;
			case (3):
				chosenBonusStat = critChanceTextStatIncrease;
				break;
		}

		chosenBonusStat.gameObject.SetActive(true);
	}
	IEnumerator ConfirmChosenStat()
	{
		choosingStat = false;

		popupText.gameObject.SetActive(false);

		switch (selectionIndex)
		{
			case (0):
				statIncreases[0] = Random.Range(1, 5);
				if (plantToLevelUp == null)
					playerStats.IncreaseHealthFromChosenLevelUpStat(statIncreases[0]);
				else
					plantToLevelUp.IncreaseHealthFromChosenLevelUpStat(statIncreases[0]);
				healthTextStatIncrease.text = "+ " + statIncreases[0];
				yield return new WaitForSecondsRealtime(0.5f);
				StartCoroutine(ApplyStatIncreaseToHealthStat());
				break;
			case (1):
				statIncreases[1] = Random.Range(1, 5);
				if (plantToLevelUp == null)
					playerStats.IncreaseDamageFromChosenLevelUpStat(statIncreases[1]);
				else
					plantToLevelUp.IncreaseDamageFromChosenLevelUpStat(statIncreases[1]);
				damageTextStatIncrease.text = "+ " + statIncreases[1];
				yield return new WaitForSecondsRealtime(0.5f);
				StartCoroutine(ApplyStatIncreaseToDamageStat());
				break;
			case (2):
				statIncreases[2] = Random.Range(1, 5);
				if (plantToLevelUp == null)
					playerStats.IncreaseDefenseFromChosenLevelUpStat(statIncreases[2]);
				else
					plantToLevelUp.IncreaseDefenseFromChosenLevelUpStat(statIncreases[2]);
				defenseTextStatIncrease.text = "+ " + statIncreases[2];
				yield return new WaitForSecondsRealtime(0.5f);
				StartCoroutine(ApplyStatIncreaseToDefenseStat());
				break;
			case (3):
				statIncreases[3] = Random.Range(1, 5);
				if (plantToLevelUp == null)
                    playerStats.IncreaseCritChanceFromChosenLevelUpStat(statIncreases[3]);
                else
                    plantToLevelUp.IncreaseCritChanceFromChosenLevelUpStat(statIncreases[3]);
				critChanceTextStatIncrease.text = "+ " + statIncreases[3];
				yield return new WaitForSecondsRealtime(0.5f);
				StartCoroutine(ApplyStatIncreaseToCritChanceStat());
				break;
		}
		finishedLevelUp = true;
		hasIncreasedStats = false;
		popupText.text = "PRESS SPACE TO CONTINUE...";
		popupText.gameObject.SetActive(true);
	}
}
