using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
	[Header("Starting Stats")]
	public int maxHealth = 100;
	public int defense = 0;
	public int damage = 10;
	public int critChance = 0;

	[Header("Stats Refernces")]
	[SerializeField] TMP_Text healthText;
	[SerializeField] TMP_Text defenseText;
	[SerializeField] TMP_Text damageText;
	[SerializeField] TMP_Text critChanceText;

	SaveManager saveManager;

	private void Awake()
	{
		saveManager = SaveManager.instance;

		saveManager.OnSavingGame += SaveManager_OnSavingGame;
		saveManager.OnLoadingGame += SaveManager_OnLoadingGame;
	}

	private void Start()
	{
		healthText.text += " " + maxHealth;
		defenseText.text += " " + defense;
		damageText.text += " " + damage;
		critChanceText.text += " " + critChance;
	}

	private void SaveManager_OnSavingGame(object sender, System.EventArgs e)
	{
		var saveData = new PlayerStatsSaveData(this);
		SaveSystem.SaveFile("/Player", "/PlayerStats.json", saveData);
	}

	private void SaveManager_OnLoadingGame(object sender, System.EventArgs e)
	{
		PlayerStatsSaveData statsData = SaveSystem.LoadFile<PlayerStatsSaveData>("/Player/PlayerStats.json");
		if (statsData == null) return;

		maxHealth = statsData.maxHealth;
		defense = statsData.defense;
		damage = statsData.damage;
		critChance = statsData.critChance;
	}
}
