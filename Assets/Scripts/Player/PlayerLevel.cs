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

    [Header("Money"), Range(0, 1000000)]
     [ReadOnlyInspector] public int money = 0;

    [Header("References")]
    [SerializeField] TMP_Text levelText;
    [SerializeField] ProgressBar levelProgressBar;
    [SerializeField] TMP_Text MoneyText;

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

    public void GiveMoney(int amount)
	{
        money += amount;
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
            levelProgressBar.maximum = xpToLevelUp;
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
            levelProgressBar.maximum = xpToLevelUp;
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
            levelProgressBar.maximum = xpToLevelUp;
            levelProgressBar.current = xp;

            string tempMoney = money.ToString();
            //string.Format(("Score: {0:#,#}", score));
            MoneyText.text = "$" + money.ToString("#,#");
		}
    }
}
