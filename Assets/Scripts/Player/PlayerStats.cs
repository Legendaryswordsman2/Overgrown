using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
	[Header("Starting Stats")]
	public int maxHealth = 100;
	[field: ReadOnlyInspector, SerializeField] public int currentHealth { get; set; } = 0;
	public int damage = 10;
	public int defense = 0;
	public int critChance = 0;

	[Header("Equipped Items")]
	[ReadOnlyInspector] public MeleeWeapon meleeWeapon;
	[ReadOnlyInspector] public Armor armor;


	[Header("Stats Refernces")]
	[SerializeField] ProgressBar playerHealthBar;
	[SerializeField] TMP_Text playerHealthText;

	[Space]

	[SerializeField] TMP_Text healthTextStat;
	[SerializeField] TMP_Text meleeDamageTextStat;
	[SerializeField] TMP_Text defenseTextStat;
	[SerializeField] TMP_Text critChanceTextStat;

	SaveManager saveManager;
	BattleSystem battleSystem;

	private void Awake()
	{
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
			meleeDamageTextStat.text = "MELEE DAMAGE: " + (damage + meleeWeapon.meleeDamageModifier);
		else
			meleeDamageTextStat.text = "MELEE DAMAGE: " + damage;

		if (armor != null)
			defenseTextStat.text = "DEFENSE: " + (defense + armor.defenseModifier);
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
		damage = statsData.meleeDamage;
		critChance = statsData.critChance;
	}

	public void Sleep()
	{
		Debug.Log("Plaher Slet");
		currentHealth = maxHealth;
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
			meleeDamageTextStat.text = "MELEE DAMAGE: " + (damage + meleeWeapon.meleeDamageModifier);
		else
			meleeDamageTextStat.text = "MELEE DAMAGE: " + damage;
	}
	public void RefreshDefenseStat()
	{
		if (armor != null)
			defenseTextStat.text = "DEFENSE: " + (defense + armor.defenseModifier);
		else
			defenseTextStat.text = "DEFENSE: " + defense;
	}
}
