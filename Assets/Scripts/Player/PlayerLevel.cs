using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerLevel : MonoBehaviour
{
    SaveManager saveManager;

    [Header("Level & XP")]
    [ReadOnlyInspector] public int playerLevel = 1;
    [ReadOnlyInspector] public int xp = 0, xpToLevelUp = 100, xpIncreaseOnLevelUp = 100, xpIncreaseIncreaseOnLevelUp = 20;

    [field: Header("Money"), Range(0, 1000000)]
    [field: ReadOnlyInspector, SerializeField] public int money { get; private set; } = 0;

    [Header("References")]
    [SerializeField] TMP_Text levelText;
    [SerializeField] ProgressBar levelProgressBar;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text shopMoneyText;

    event EventHandler OnLevelUp;

    private void Awake()
	{
        saveManager = SaveManager.instance;

        saveManager.OnSavingGame += SaveManager_OnSavingGame;
        saveManager.OnLoadingGame += SaveManager_OnLoadingGame;
    }
    public void GiveXp(int xpAmount)
    {
        xp += xpAmount;

        if(levelProgressBar != null)
		{
            levelText.text = "LV: " + playerLevel;
            levelProgressBar.current = xp;

		}

        if (xp >= xpToLevelUp)
        {
            LevelUp();
        }
    }

    public bool GiveMoney(int amount)
	{
        if (money + amount > 1000000) return false;

        money += amount;
        if(moneyText != null)
        moneyText.text = "$" + money.ToString("#,#");
        if(shopMoneyText != null)
        shopMoneyText.text = "$" + money.ToString("#,#");

        return true;
    }
    public void TakeMoney(int amount)
	{
        money -= amount;
        moneyText.text = "$" + money.ToString("#,#");
        shopMoneyText.text = "$" + money.ToString("#,#");
    }
    void LevelUp()
    {
        playerLevel++;

        Debug.Log("You Leveled up to level " + playerLevel);

        if(levelText != null) levelText.text = "LV: " + playerLevel;

        xp -= xpToLevelUp;
        xpToLevelUp += xpIncreaseOnLevelUp;
        xpIncreaseOnLevelUp += xpIncreaseIncreaseOnLevelUp;

        if (levelProgressBar != null)
		{
            levelProgressBar.max = xpToLevelUp;
            levelProgressBar.current = xp;
		}

        OnLevelUp?.Invoke(this, EventArgs.Empty);

        TestIfCanLevelUpAgain();
    }
    void TestIfCanLevelUpAgain()
    {
        if (xp >= xpToLevelUp)
        {
            LevelUp();
        }
    }
    public void RefreshValues()
    {
        if (levelProgressBar != null)
        {
            levelProgressBar.max = xpToLevelUp;
            levelProgressBar.current = xp;
            levelText.text = "LV: " + playerLevel;
        }
    }

    private void SaveManager_OnSavingGame(object sender, System.EventArgs e)
    {
        PlayerXPData xpData = new PlayerXPData(this);
        SaveSystem.SaveFile("/Player", "/PlayerLevel&XP.json", xpData);
    }
    private void SaveManager_OnLoadingGame(object sender, System.EventArgs e)
    {
        PlayerXPData xpData = SaveSystem.LoadFile<PlayerXPData>("/Player/PlayerLevel&XP.json");
        if (xpData == null) return;

        playerLevel = xpData.playerLevel;
        xp = xpData.xp;
        xpToLevelUp = xpData.xpToLevelUp;
        xpIncreaseOnLevelUp = xpData.xpIncreaseOnLevelUp;
        xpIncreaseIncreaseOnLevelUp = xpData.xpIncreaseIncreaseOnLevelUp;

        money = xpData.playerMoney;

        if(levelProgressBar != null)
		{
            levelText.text = "LV: " + playerLevel;
            levelProgressBar.max = xpToLevelUp;
            levelProgressBar.current = xp;

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
    }
}
