using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlantStats : MonoBehaviour
{
	//public static PlayerStats instance;

	[field: Header("Starting Stats")]
	[field: SerializeField] public int maxHealth { get; private set; } = 100;
	[field: ReadOnlyInspector, SerializeField] public int currentHealth { get; set; } = 0;
	[field: SerializeField] public int damage { get; private set; } = 10;
	[field: SerializeField] public int defense { get; private set; } = 0;
	[field: SerializeField] public int critChance { get; private set; } = 0;

	[field: SerializeField] public LevelSystem plantLevelSystem { get; private set; }
	[field: SerializeField] public PlayerLevelUpScreen playerLevelUpScreen { get; private set; }

	[Space]

	[SerializeField] CharacterInfoCard plantInfoCard;

	SaveManager saveManager;
	BattleSystem battleSystem;

	private void Awake()
	{
		plantLevelSystem = new LevelSystem(PlayerOrPlant.Plant);

		saveManager = SaveManager.instance;

		saveManager.OnSavingGame += SaveManager_OnSavingGame;
		saveManager.OnLoadingGame += SaveManager_OnLoadingGame;
	}

	private void Start()
	{
        //if (BattleSystem.instance != null)
        //{
        //    battleSystem = BattleSystem.instance;
        //    PlayerPlantUnit plantUnit = battleSystem.playerPlantUnit;

        //    playerUnit.maxHealth = maxHealth;
        //    playerUnit.currentHealth = currentHealth;

        //    playerUnit.damage = damage;

        //    playerUnit.defense = defense;

        //    playerUnit.critChance = critChance;

        //    playerUnit.playerHUD.SetHUD(playerUnit);

        //    return;
        //}

		plantInfoCard.SetInfoCard(this);
	}

	private void SaveManager_OnSavingGame(object sender, System.EventArgs e)
	{
		if (SceneManager.GetActiveScene().name == "Turn Based Combat")
		{
			currentHealth = BattleSystem.instance.playerUnit.currentHealth;
		}

		var saveData = new PlantStatsSaveData(this);
		SaveSystem.SaveFile("/Player/Characters", "/PlayerStats", saveData);
	}

	private void SaveManager_OnLoadingGame(object sender, System.EventArgs e)
	{
		PlayerStatsSaveData statsData = SaveSystem.LoadFile<PlayerStatsSaveData>("/Player/Characters/PlayerStats");
		if (statsData == null)
		{
			currentHealth = maxHealth;
			return;
		}


		maxHealth = statsData.maxHealth;
		currentHealth = statsData.currentHealth;
		defense = statsData.defense;
		damage = statsData.damage;
		critChance = statsData.critChance;
	}

	public void IncreaseStatsFromLevelUp(int[] statIncreases)
	{
		//playerLevelUpScreen.SetPlayerLevelUpScreen(this, statIncreases);

		maxHealth += statIncreases[0];
		currentHealth += statIncreases[0];
		damage += statIncreases[1];
		defense += statIncreases[2];
		critChance += statIncreases[3];

		if (BattleSystem.instance != null) return;

		plantInfoCard.SetInfoCard(this);
	}
	public void IncreaseHealthFromChosenLevelUpStat(int amount)
	{
		maxHealth += amount;
		currentHealth += amount;

		if (BattleSystem.instance != null) return;

		plantInfoCard.SetInfoCard(this);
	}
	public void IncreaseDamageFromChosenLevelUpStat(int amount)
	{
		damage += amount;

		if (BattleSystem.instance != null) return;

		plantInfoCard.SetInfoCard(this);
	}
	public void IncreaseDefenseFromChosenLevelUpStat(int amount)
	{
		defense += amount;

		if (BattleSystem.instance != null) return;

		plantInfoCard.SetInfoCard(this);
	}
	public void IncreaseCritChanceFromChosenLevelUpStat(int amount)
	{
		critChance += amount;

		if (BattleSystem.instance != null) return;

		plantInfoCard.SetInfoCard(this);
	}
	public void Sleep()
	{
		currentHealth = maxHealth;
	}

	public bool Heal(int amount)
	{
		if (currentHealth >= maxHealth) return false;

		if (currentHealth + amount >= maxHealth)
			currentHealth = maxHealth;
		else
			currentHealth += amount;

		return true;
	}
}