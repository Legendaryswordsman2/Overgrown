using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
	[field: SerializeField, ReadOnlyInspector] public int playerKills { get; set; }
	[field: SerializeField, ReadOnlyInspector] public int plantsPlanted { get; set; }
	[field: SerializeField, ReadOnlyInspector] public int playerDeaths { get; set; }

	public void IncreasePlayerKills()
	{
		playerKills++;
	}
	public void IncreasePlantsPlanted()
	{
		plantsPlanted++;
	}
	public void IncreasePlayerDeaths()
	{
		playerDeaths++;
	}
}
