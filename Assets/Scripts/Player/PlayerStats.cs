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

	[field: SerializeField] public LevelSystem playerLevelSystem { get; private set; }

	[field: Header("Money"), Range(0, 1000000)]
	[field: ReadOnlyInspector, SerializeField] public int money { get; private set; } = 0;

	[Header("Equipped Items")]
	[ReadOnlyInspector] public MeleeWeapon meleeWeapon;
	[ReadOnlyInspector] public Armor armor;


	[Header("Stats Refernces")]
	[SerializeField] ProgressBar playerHealthBar;
	[SerializeField] TMP_Text playerHealthText;
	[SerializeField] TMP_Text moneyText;
	[SerializeField] TMP_Text shopMoneyText;
	[field: SerializeField] public PlayerLevelUpScreen playerLevelUpScreen { get; private set; }

	[Space]

	[SerializeField] CharacterInfoCard playerInfoCard;

	SaveManager saveManager;
	BattleSystem battleSystem;

	private void Awake()
	{
		instance = this;

		playerLevelSystem = new LevelSystem(CharacterToLevelUp.Player);

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

			playerUnit.SetStats(this);
				
			return;
		}

		playerInfoCard.SetInfoCard(this);

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

        money = statsData.money;

		if (BattleSystem.instance != null) return;

        if (money == 0)
        {
            moneyText.text = "$0";
            shopMoneyText.text = "$0";
        }
        else
        {
            moneyText.text = "$" + money.ToString("#,#");
            shopMoneyText.text = "$" + money.ToString("#,#");
        }
    }

	public void IncreaseStatsFromLevelUp(int[] statIncreases)
	{
		playerLevelUpScreen.SetLevelUpScreen(new LevelUpStats(maxHealth, damage, defense, critChance, statIncreases, playerLevelSystem.level), CharacterToLevelUp.Player);

		maxHealth += statIncreases[0];
		currentHealth += statIncreases[0];
		damage += statIncreases[1];
		defense += statIncreases[2];
		critChance += statIncreases[3];

		if (BattleSystem.instance != null) return;

		playerInfoCard.SetInfoCard(this);

		playerHealthBar.max = maxHealth;
		playerHealthBar.current = currentHealth;
	}
	public void IncreaseHealthFromChosenLevelUpStat(int amount)
	{
		maxHealth += amount;
		currentHealth += amount;

		if (BattleSystem.instance != null) return;

		playerInfoCard.SetInfoCard(this);

		playerHealthBar.max = maxHealth;
		playerHealthBar.current = currentHealth;
	}
	public void IncreaseDamageFromChosenLevelUpStat(int amount)
	{
		damage += amount;

		if (BattleSystem.instance != null) return;

		playerInfoCard.SetInfoCard(this);
	}
	public void IncreaseDefenseFromChosenLevelUpStat(int amount)
	{
		defense += amount;

		if (BattleSystem.instance != null) return;

		playerInfoCard.SetInfoCard(this);
	}
	public void IncreaseCritChanceFromChosenLevelUpStat(int amount)
	{
		critChance += amount;

		if (BattleSystem.instance != null) return;

		playerInfoCard.SetInfoCard(this);
	}
	public void Sleep()
	{
		currentHealth = maxHealth;
	}
	public bool GiveMoney(int amount)
	{
		if (money + amount > 1000000) return false;

		money += amount;
		if (moneyText != null)
			moneyText.text = "$" + money.ToString("#,#");
		if (shopMoneyText != null)
			shopMoneyText.text = "$" + money.ToString("#,#");

		return true;
	}
	public void TakeMoney(int amount)
	{
		money -= amount;
		moneyText.text = "$" + money.ToString("#,#");
		shopMoneyText.text = "$" + money.ToString("#,#");
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
		playerInfoCard.SetInfoCard(this);
	}
	public void RefreshDefenseStat()
	{
		playerInfoCard.SetInfoCard(this);
	}
}
