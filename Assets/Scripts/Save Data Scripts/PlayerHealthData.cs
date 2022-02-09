using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerHealthData
{
    public int playerHealth;
    public PlayerHealthData(PlayerHealth playerHealthData)
    {
        playerHealth = playerHealthData.maxHealth;
    }
}
