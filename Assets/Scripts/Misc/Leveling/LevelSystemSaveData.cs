using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSystemSaveData
{
    public int level = 1;
    public int xp = 0, xpToLevelUp = 100, neededXpIncreaseOnLevelUp = 100, neededXpIncreaseIncreaseOnLevelUp = 20;

    public LevelSystemSaveData(LevelSystem levelSystem)
    {
        level = levelSystem.level;
        xp = levelSystem.xp;
        xpToLevelUp = levelSystem.xpToLevelUp;
        neededXpIncreaseOnLevelUp = levelSystem.neededXpIncreaseOnLevelUp;
        neededXpIncreaseIncreaseOnLevelUp = levelSystem.neededXpIncreaseIncreaseOnLevelUp;
    }
}
