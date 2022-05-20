using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerOrPlant { Player, Plant }

[System.Serializable]
public class LevelSystem
{

    PlayerOrPlant playerOrPlant;

    [Header("Default Stats")]

    [SerializeField] int defaultNeededXpToLevelUp = 100;
    [SerializeField] int defaultNeededXpIncreaseOnLevelUp = 100, defaultNeededXpIncreaseIncreaseOnLevelUp = 20;

    [Space]


    [Header("Level & XP")]
    [ReadOnlyInspector] public int level = 1;
    [ReadOnlyInspector] public int xp = 0, xpToLevelUp = 100, neededXpIncreaseOnLevelUp = 100, neededXpIncreaseIncreaseOnLevelUp = 20;


    public LevelSystem(PlayerOrPlant _playerOrPlant) // The equivalent of a start function
    {
        playerOrPlant = _playerOrPlant;

        SaveManager saveManager = SaveManager.instance;

        saveManager.OnSavingGame += SaveManager_OnSavingGame;
        saveManager.OnLoadingGame += SaveManager_OnLoadingGame;
    }
    private void SaveManager_OnSavingGame(object sender, System.EventArgs e)
    {
        var saveData = new LevelSystemSaveData(this);

        switch (playerOrPlant)
        {
            case PlayerOrPlant.Player:
                SaveSystem.SaveFile("/Player/Characters", "/PlayerLevel", saveData);
                break;
            case PlayerOrPlant.Plant:
                SaveSystem.SaveFile("/Player/Characters", "/PlantLevel", saveData);
                break;
 
        }
    }
    private void SaveManager_OnLoadingGame(object sender, System.EventArgs e)
    {
        LevelSystemSaveData saveData;

        switch (playerOrPlant)
        {
            case PlayerOrPlant.Player:
                saveData = SaveSystem.LoadFile<LevelSystemSaveData>("/Player/Characters/PlayerLevel");
                break;
            case PlayerOrPlant.Plant:
                saveData = SaveSystem.LoadFile<LevelSystemSaveData>("/Player/Characters/PlantLevel");
                break;

            default:
                return;
        }

        if (saveData == null) 
        {
            level = 1;
            xp = 0;
            xpToLevelUp = defaultNeededXpToLevelUp;
            neededXpIncreaseOnLevelUp = defaultNeededXpIncreaseOnLevelUp ;
            neededXpIncreaseIncreaseOnLevelUp = defaultNeededXpIncreaseIncreaseOnLevelUp;
            return;
        }

        level = saveData.level;
        xp = saveData.xp;
        xpToLevelUp = saveData.xpToLevelUp;
        neededXpIncreaseOnLevelUp = saveData.neededXpIncreaseOnLevelUp;
        neededXpIncreaseIncreaseOnLevelUp = saveData.neededXpIncreaseIncreaseOnLevelUp;
    }

    public bool GiveXp(int xpAmount)
    {
        xp += xpAmount;

        //if (levelProgressBar != null)
        //{
        //    levelText.text = "LV: " + playerLevel;
        //    levelProgressBar.current = xp;

        //}

        if (xp >= xpToLevelUp)
        {
            LevelUp();
            return true;
        }
        else
            return false;
    }
    void LevelUp()
    {
        level++;

        //if (levelText != null) levelText.text = "LV: " + level;

        xp -= xpToLevelUp;

        if (xp < 0) xp = 0;

        xpToLevelUp += neededXpIncreaseOnLevelUp;
        neededXpIncreaseOnLevelUp += neededXpIncreaseIncreaseOnLevelUp;

        //if (levelProgressBar != null)
        //{
        //    levelProgressBar.max = xpToLevelUp;
        //    levelProgressBar.current = xp;
        //}

        int[] statIncreases = new int[4];

        for (int i = 0; i < statIncreases.Length; i++)
        {
            statIncreases[i] = Random.Range(1, 5);
        }

        PlayerStats.instance.IncreaseStatsFromLevelUp(statIncreases);
    }

    public bool TryToLevelUp()
    {
        if (xp >= xpToLevelUp)
        {
            LevelUp();
            return true;
        }
        else
            return false;
    }
    public void RefreshValues()
    {
        //if (levelProgressBar != null)
        //{
        //    levelProgressBar.max = xpToLevelUp;
        //    levelProgressBar.current = xp;
        //    levelText.text = "LV: " + playerLevel;
        //}
    }
}
