using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
	public static PlayerStats instance;

	[field: Header("Starting Stats")]
	[field: SerializeField] public int maxHealth { get; private set; } = 100;
	[field: ReadOnlyInspector, SerializeField] public int currentHealth { get; set; } = 0;
	[field: SerializeField] public int damage { get; private set; } = 10;
	[field: SerializeField] public int defense { get; private set; } = 0;
	[field: SerializeField] public int critChance { get; private set; } = 0;

	[Header("Equipped Items")]
	[ReadOnlyInspector] public MeleeWeapon meleeWeapon;
	[ReadOnlyInspector] public Armor armor;


	[Header("Stats Refernces")]
	[SerializeField] ProgressBar playerHealthBar;
	[SerializeField] TMP_Text playerHealthText;
	[field: SerializeField] public PlayerLevelUpScreen playerLevelUpScreen { get; private set; }

	[Space]

	[SerializeField] TMP_Text healthTextStat;
	[SerializeField] TMP_Text meleeDamageTextStat;
	[SerializeField] TMP_Text defenseTextStat;
	[SerializeField] TMP_Text critChanceTextStat;

	SaveManager saveManager;
	BattleSystem battleSystem;

	private void Awake()
	{
		instance = this;

		saveManager = SaveManager.instance;

		saveManager.OnSavingGame += SaveManager_OnSavingGame;
		saveManager.OnLoadingGame += SaveManager_OnLoadingGame;
	}

	private void Start()
	{
		if (BattleSystem.instance != null) 
		{
			battleSystem = BattleSystem.instance;
			PlayerUnit playerUnit = battleSystem.playerUnit;

			playerUnit.maxHealth = maxHealth;
			playerUnit.currentHealth = currentHealth;

			if (meleeWeapon != null)
				playerUnit.damage = damage + meleeWeapon.meleeDamageModifier;
			else
				playerUnit.damage = damage;

			if (armor != null)
				playerUnit.defense = defense + armor.defenseModifier;
			else
				playerUnit.defense = defense;

			playerUnit.critChance = critChance;

			playerUnit.playerHUD.SetHUD(playerUnit);
				
			return;
		}

		healthTextStat.text += " " + maxHealth;

		if (meleeWeapon != null)
			meleeDamageTextStat.text = "DAMAGE: " + damage + " + " + meleeWeapon.meleeDamageModifier;
		else
			meleeDamageTextStat.text = "DAMAGE: " + damage;

		if (armor != null)
			defenseTextStat.text = "DEFENSE: " + defense + " + " + armor.defenseModifier;
		else
			defenseTextStat.text = "DEFENSE: " + defense;

		critChanceTextStat.text += " " + critChance;

		playerHealthBar.max = maxHealth;
		playerHealthBar.current = currentHealth;

		playerHealthText.text = currentHealth + " / " + maxHealth;
	}

	private void SaveManager_OnSavingGame(object sender, System.EventArgs e)
	{
		if (SceneManager.GetActiveScene().name == "Turn Based Combat")
		{
			currentHealth = BattleSystem.instance.playerUnit.currentHealth;
		}

		var saveData = new PlayerStatsSaveData(this);
		SaveSystem.SaveFile("/Player", "/PlayerStats", saveData);
	}

	private void SaveManager_OnLoadingGame(object sender, System.EventArgs e)
	{
		PlayerStatsSaveData statsData = SaveSystem.LoadFile<PlayerStatsSaveData>("/Player/PlayerStats");
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
		playerLevelUpScreen.SetPlayerLevelUpScreen(this, statIncreases);

		maxHealth += statIncreases[0];
		currentHealth += statIncreases[0];
		damage += statIncreases[1];
		defense += statIncreases[2];
		critChance += statIncreases[3];

		if (BattleSystem.instance != null) return;

		healthTextStat.text += " " + maxHealth;

		if (meleeWeapon != null)
			meleeDamageTextStat.text = "DAMAGE: " + damage + " + " + meleeWeapon.meleeDamageModifier;
		else
			meleeDamageTextStat.text = "DAMAGE: " + damage;

		if (armor != null)
			defenseTextStat.text = "DEFENSE: " + defense + " + " + armor.defenseModifier;
		else
			defenseTextStat.text = "DEFENSE: " + defense;

		critChanceTextStat.text += " " + critChance;

		playerHealthBar.max = maxHealth;
		playerHealthBar.current = currentHealth;
	}
	public void IncreaseHealthFromChosenLevelUpStat(int amount)
	{
		maxHealth += amount;
		currentHealth += amount;

		if (BattleSystem.instance != null) return;

		healthTextStat.text += " " + maxHealth;

		playerHealthBar.max = maxHealth;
		playerHealthBar.current = currentHealth;
	}
	public void IncreaseDamageFromChosenLevelUpStat(int amount)
	{
		damage += amount;

		if (BattleSystem.instance != null) return;

		if (meleeWeapon != null)
			meleeDamageTextStat.text = "DAMAGE: " + damage + " + " + meleeWeapon.meleeDamageModifier;
		else
			meleeDamageTextStat.text = "DAMAGE: " + damage;
	}
	public void IncreaseDefenseFromChosenLevelUpStat(int amount)
	{
		defense += amount;

		if (BattleSystem.instance != null) return;

		if (armor != null)
			defenseTextStat.text = "DEFENSE: " + defense + " + " + armor.defenseModifier;
		else
			defenseTextStat.text = "DEFENSE: " + defense;
	}
	public void IncreaseCritChanceFromChosenLevelUpStat(int amount)
	{
		critChance += amount;

		if (BattleSystem.instance != null) return;

		critChanceTextStat.text += " " + critChance;
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

		playerHealthBar.current = currentHealth;
		playerHealthText.text = currentHealth + " / " + maxHealth;

		return true;
	}

	public void EquipMeleeWeapon(MeleeWeapon _meleeWeapon)
	{
		if (meleeWeapon != null)
			meleeWeapon.Unequip();

		meleeWeapon = _meleeWeapon;
	}

	public void UnequipMeleeWeapon()
	{
		meleeWeapon.Unequip();
		meleeWeapon = null;
	}

	public void EquipArmor(Armor _armor)
	{
		if (armor != null)
			armor.Unequip();

		armor = _armor;
	}

	public void UnequipArmor()
	{
		armor.Unequip();
		armor = null;
	}


	public void RefreshMeleeDamageStat()
	{
		if (meleeWeapon != null)
			meleeDamageTextStat.text = "DAMAGE: " + damage + " + " + meleeWeapon.meleeDamageModifier;
		else
			meleeDamageTextStat.text = "DAMAGE: " + damage;
	}
	public void RefreshDefenseStat()
	{
		if (armor != null)
			defenseTextStat.text = "DEFENSE: " + defense + " + " + armor.defenseModifier;
		else
			defenseTextStat.text = "DEFENSE: " + defense;
	}
}
