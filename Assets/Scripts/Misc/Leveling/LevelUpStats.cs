using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpStats
{
    // Old Stats
    public int previousHealth;
    public int previousDamage;
    public int previousDefense;
    public int previousCritChance;

    public int newLevel;

    public int[] statIncreases;

    // New Stats
    public int newHealth;
    public int newDamage;
    public int newDefense;
    public int newCritChance;

    public LevelUpStats(int _previousHealth, int _previousDamage, int _previousDefense, int _previousCritChance, int[] _statIncreases, int _newLevel)
    {
        previousHealth = _previousHealth;
        previousDamage = _previousDamage;
        previousDefense = _previousDefense;
        previousCritChance = _previousCritChance;

        newLevel = _newLevel;

        statIncreases = _statIncreases;

        newHealth = previousHealth + statIncreases[0];
        newDamage = previousDamage + statIncreases[1];
        newDefense = previousDefense + statIncreases[2];
        newCritChance = previousCritChance + statIncreases[3];
    }
}
