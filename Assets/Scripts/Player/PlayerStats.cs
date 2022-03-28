using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
	[Header("Starting Stats")]
	public int maxHealth = 100;
	[ReadOnlyInspector] public int currentHealth = 0;
	public int defense = 0;
	public int meleeDamage = 10;
	public int rangedDamage = 10;
	public int critChance = 0;

	//[Header("Stat Modifiers")]
	//[ReadOnlyInspector] public int defenseModifier;
	//[ReadOnlyInspector] public int meleeDamageModifier;
	//[ReadOnlyInspector] public int rangedDamageModifier;

	[Header("Stat Item Modifiers")]
	[ReadOnlyInspector] public MeleeWeapon meleeWeapon;
	[ReadOnlyInspector] public RangedWeapon rangedWeapon;
	[ReadOnlyInspector] public Armor armor;


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
				playerUnit.meleeDamage = meleeDamage + meleeWeapon.meleeDamageModifier;
			else
				playerUnit.meleeDamage = meleeDamage;

			if (rangedWeapon != null)
				playerUnit.rangedDamage = rangedDamage + rangedWeapon.rangedDamageModifier;
			else
				playerUnit.rangedDamage = rangedDamage;

			if (armor != null)
				playerUnit.defense = defense + armor.armorModifier;
			else
				playerUnit.defense = defense;

			playerUnit.critChance = critChance;

			playerUnit.playerHUD.SetHUD(playerUnit);
				
			return;
		}

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
		if(BattleSystem.instance != null)
		currentHealth = BattleSystem.instance.playerUnit.currentHealth;

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

	public void EquipRangedWeapon(RangedWeapon _rangedWeapon)
	{
		if (rangedWeapon != null)
			rangedWeapon.Unequip();

		rangedWeapon = _rangedWeapon;
	}

	public void UnequipRangedWeapon()
	{
		rangedWeapon.Unequip();
		rangedWeapon = null;
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
}
