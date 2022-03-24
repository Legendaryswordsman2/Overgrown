using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
	[Header("Starting Stats")]
	public int maxHealth = 100;
	[ReadOnlyInspector] public int currentHealth = 0;
	public int defense = 0;
	public int meleeDamage = 10;
	public int rangedDamage = 10;
	public int critChance = 0;

	[Header("Stat Modifiers")]
	//public int maxHealthModifier;
	[ReadOnlyInspector] public int defenseModifier;
	[ReadOnlyInspector] public int meleeDamageModifier;
	[ReadOnlyInspector] public int rangedDamageModifier;


	[Header("Stats Refernces")]
	[SerializeField] ProgressBar playerHealthBar;
	[SerializeField] TMP_Text currentHealthText;
	[SerializeField] TMP_Text maxHealthText;

	[Space]

	[SerializeField] TMP_Text healthTextStat;
	[SerializeField] TMP_Text defenseTextStat;
	[SerializeField] TMP_Text damageTextStat;
	[SerializeField] TMP_Text critChanceTextStat;

	SaveManager saveManager;

	private void Awake()
	{
		saveManager = SaveManager.instance;

		saveManager.OnSavingGame += SaveManager_OnSavingGame;
		saveManager.OnLoadingGame += SaveManager_OnLoadingGame;
	}

	private void Start()
	{
		if (healthTextStat == null) return;

		healthTextStat.text += " " + maxHealth;
		defenseTextStat.text += " " + defense;
		damageTextStat.text += " " + meleeDamage;
		critChanceTextStat.text += " " + critChance;

		playerHealthBar.maximum = maxHealth;
		playerHealthBar.current = currentHealth;

		currentHealthText.text = currentHealth.ToString();
		maxHealthText.text = "       / " + maxHealth.ToString();
	}

	private void SaveManager_OnSavingGame(object sender, System.EventArgs e)
	{
		var saveData = new PlayerStatsSaveData(this);
		SaveSystem.SaveFile("/Player", "/PlayerStats.json", saveData);
	}

	private void SaveManager_OnLoadingGame(object sender, System.EventArgs e)
	{
		PlayerStatsSaveData statsData = SaveSystem.LoadFile<PlayerStatsSaveData>("/Player/PlayerStats.json");
		if (statsData == null)
		{
			currentHealth = maxHealth;
			return;
		}


		maxHealth = statsData.maxHealth;
		currentHealth = statsData.currentHealth;
		defense = statsData.defense;
		meleeDamage = statsData.meleeDamage;
		rangedDamage = statsData.rangedDamage;
		critChance = statsData.critChance;

		// Modifiers
		//defenseModifier = statsData.defenseModifier;
		//meleeDamageModifier = statsData.meleeDamageModifier;
		//rangedDamageModifier = statsData.rangedDamageModifier;
	}
}
