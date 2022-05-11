using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grow : MonoBehaviour
{
	[Header("References")]
	public GameObject plantIcon;
	public GameObject discardIcon;
	[SerializeField] SpriteRenderer plant;
	[SerializeField] ProgressBar progressBar;

	[field: Space]

	[field: SerializeField] public SOGrowingPlant chosenGrowingPlant { get; private set; }

	// Growing
	[field: SerializeField] public int totalGrowTime { get; private set; }
	[field: SerializeField] public float stageGrowthTime { get; private set; }
	[field: SerializeField, ReadOnlyInspector] public int elapsedTime { get; private set; } = 0;

	[field: SerializeField, ReadOnlyInspector] public int currentGrowthStageIndex { get; private set; } = 0;

	[field: SerializeField] public float[] stageIntervals { get; private set; }

	private void Awake()
	{
		SaveManager.instance.OnSavingGame += SaveManager_OnSavingGame;
		SaveManager.instance.OnLoadingGame += SaveManager_OnLoadingGame;
	}
	public void StartGrowing(SOGrowingPlant plantToGrow)
	{
		chosenGrowingPlant = plantToGrow;

		plant.sprite = chosenGrowingPlant.plantGrowthStages[0];
		currentGrowthStageIndex++;

		totalGrowTime = chosenGrowingPlant.plantGrowTime;
		stageGrowthTime = totalGrowTime / (float)(chosenGrowingPlant.plantGrowthStages.Length - 1);

		stageIntervals = new float[chosenGrowingPlant.plantGrowthStages.Length - 1];
		for (int i = 0; i < chosenGrowingPlant.plantGrowthStages.Length - 1; i++)
		{
			if (i == 0) stageIntervals[i] = stageGrowthTime;
			else stageIntervals[i] = stageIntervals[i - 1] + stageGrowthTime;
		}

		progressBar.max = totalGrowTime;
		progressBar.current = 0;

		plantIcon.SetActive(false);

		progressBar.gameObject.SetActive(true);

		StartCoroutine(Timer());
	}

    void GoToNextGrowthStage()
	{
		plant.sprite = chosenGrowingPlant.plantGrowthStages[currentGrowthStageIndex];
		currentGrowthStageIndex++;
	}
	IEnumerator Timer()
	{
		yield return new WaitForSeconds(1);
		elapsedTime++;
		progressBar.current = elapsedTime;

		if (stageIntervals[currentGrowthStageIndex - 1] <= elapsedTime)
		{
			GoToNextGrowthStage();
		}

		if (elapsedTime < totalGrowTime)
		{
		StartCoroutine(Timer());
		}
	}
	private void SaveManager_OnSavingGame(object sender, System.EventArgs e)
	{
		if (chosenGrowingPlant == null) return;

		SaveSystem.SaveFile("/Misc/CurrentlyGrowingPlants", "/GrowingPlant1", new GrowSaveData(this));
	}
	private void SaveManager_OnLoadingGame(object sender, System.EventArgs e)
	{
		GrowSaveData growingPlantSaveData = SaveSystem.LoadFile<GrowSaveData>("/Misc/CurrentlyGrowingPlants/GrowingPlant1");

		if (growingPlantSaveData == null) return;

		chosenGrowingPlant = GameManager.instance.Database.GetGrowingPlant(growingPlantSaveData.chosenGrowingPlantID);

		totalGrowTime = growingPlantSaveData.totalGrowTime;
		stageGrowthTime = growingPlantSaveData.stageGrowthTime;
		elapsedTime = growingPlantSaveData.elapsedTime;

		currentGrowthStageIndex = growingPlantSaveData.currentGrowthStageIndex;

		stageIntervals = growingPlantSaveData.stageIntervals;

		StartGrowingFromLastPoint();
	}

	void StartGrowingFromLastPoint()
	{
		plant.sprite = chosenGrowingPlant.plantGrowthStages[currentGrowthStageIndex - 1];

		progressBar.max = totalGrowTime;
		progressBar.current = elapsedTime;

		progressBar.gameObject.SetActive(true);

		StartCoroutine(Timer());
	}
}
