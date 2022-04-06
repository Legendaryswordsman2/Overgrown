using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatisticsData
{
    public int battleWins;
    public int numberOfTimesSlept;

    public StatisticsData()
    {
        battleWins = Statistics.battleWins;
        numberOfTimesSlept = Statistics.numberOfTimesSlept;
    }
}
