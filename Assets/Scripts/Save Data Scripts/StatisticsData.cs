using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatisticsData
{
	public int playerKills;
	public int plantsPlanted;
	public int playerDeaths;

    public StatisticsData(Statistics stats)
    {
        playerKills = stats.playerKills;
        plantsPlanted = stats.plantsPlanted;
        playerDeaths = stats.playerDeaths;
    }
}
