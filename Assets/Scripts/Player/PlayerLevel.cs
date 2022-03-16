using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    SaveManager saveManager;

    [Header("Level & XP")]
    public int playerLevel = 1;
    public int xp = 0, xpToLevelUp = 100, xpIncreaseOnLevelUp = 100, xpIncreaseIncreaseOnLevelUp = 20;

    [Header("References")]
    [SerializeField] TMP_Text levelText;
    [SerializeField] ProgressBar levelProgressBar;

    private void Awake()
	{
        saveManager = SaveManager.instance;

        saveManager.OnSavingGame += SaveManager_OnSavingGame;
        saveManager.OnLoadingGame += SaveManager_OnLoadingGame;
    }
    public void GiveXp(int xpAmount)
    {
        xp += xpAmount;
        levelText.text = "LV: " + playerLevel;
        levelProgressBar.current = xp;
        if (xp >= xpToLevelUp)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        playerLevel++;

        Debug.Log("You Leveled up to level " + playerLevel);
        levelText.text = "LV: " + playerLevel;

        xp -= xpToLevelUp;
        xpToLevelUp += xpIncreaseOnLevelUp;
        xpIncreaseOnLevelUp += xpIncreaseIncreaseOnLevelUp;

        levelProgressBar.maximum = xpToLevelUp;
        levelProgressBar.current = xp;

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
        levelProgressBar.maximum = xpToLevelUp;
        levelProgressBar.current = xp;
        levelText.text = "LV: " + playerLevel;
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

    }
}
