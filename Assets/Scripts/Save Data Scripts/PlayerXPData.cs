using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerXPData
{
    public int playerLevel;
    public int xp, xpIncreaseOnLevelUp, xpIncreaseIncreaseOnLevelUp, xpToLevelUp;

    public PlayerXPData (PlayerLevel player)
    {
        playerLevel = player.playerLevel;
        xp = player.xp;
        xpToLevelUp = player.xpToLevelUp;
        xpIncreaseOnLevelUp = player.xpIncreaseOnLevelUp;
        xpIncreaseIncreaseOnLevelUp = player.xpIncreaseIncreaseOnLevelUp;
    }
}
