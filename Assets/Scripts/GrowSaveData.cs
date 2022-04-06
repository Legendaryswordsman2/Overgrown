using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GrowSaveData
{
	public string chosenGrowingPlantID;

	public int totalGrowTime;
	public float stageGrowthTime;
	public int elapsedTime = 0;

	public int currentGrowthStageIndex = 0;

	public float[] stageIntervals;

	public GrowSaveData(Grow growScript)
	{
		chosenGrowingPlantID = growScript.chosenGrowingPlant.id;

		totalGrowTime = growScript.totalGrowTime;
		stageGrowthTime = growScript.stageGrowthTime;
		elapsedTime = growScript.elapsedTime;

		currentGrowthStageIndex = growScript.currentGrowthStageIndex;

		stageIntervals = growScript.stageIntervals;
	}
}
