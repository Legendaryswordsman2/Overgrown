using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Statistics
{
	public static int battleWins { get; private set; }
	public static int numberOfTimesSlept { get; private set; }
	public static void Initialize()
	{
		SaveManager.instance.OnSavingGame += SaveManager_OnSavingGame;
		SaveManager.instance.OnLoadingGame += SaveManager_OnLoadingGame;
	}
	private static void SaveManager_OnSavingGame(object sender, System.EventArgs e)
	{
		SaveSystem.SaveFile("/Player", "/Statistics.json", new StatisticsData());
	}
	private static void SaveManager_OnLoadingGame(object sender, System.EventArgs e)
	{
		StatisticsData stats = SaveSystem.LoadFile<StatisticsData>("/Player/Statistics.json");

		if (stats == null) return;

		battleWins = stats.battleWins;
		numberOfTimesSlept = stats.numberOfTimesSlept;
	}

	public static void IncreaseBattleWins()
	{
		battleWins++;
	}
	public static void IncreaseNumberOfTimesSlept()
	{
		numberOfTimesSlept++;
	}

}
