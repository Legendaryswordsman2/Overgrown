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

	[Space]

	[SerializeField] SOGrowingPlant chosenGrowingPlant;

	// Growing
	[SerializeField] int totalGrowTime;
	[SerializeField] float stageGrowthTime;
	[SerializeField, ReadOnlyInspector] int elapsedTime = 0;

	[SerializeField, ReadOnlyInspector] int currentGrowthStageIndex = 0;

	[SerializeField] float[] stageIntervals;

	public void StartGrowing()
	{
		plant.sprite = chosenGrowingPlant.plantGrowthStages[0];
		currentGrowthStageIndex++;

		totalGrowTime = chosenGrowingPlant.plantGrowTime;

		stageGrowthTime = totalGrowTime / (float)chosenGrowingPlant.plantGrowthStages.Length + 1;


		stageIntervals = new float[chosenGrowingPlant.plantGrowthStages.Length - 1];
		for (int i = 0; i < chosenGrowingPlant.plantGrowthStages.Length - 1; i++)
		{
			if (i == 0) stageIntervals[i] = stageGrowthTime;
			else stageIntervals[i] = stageIntervals[i - 1] + stageGrowthTime;
		}

		progressBar.max = totalGrowTime;

		plantIcon.SetActive(false);

		progressBar.gameObject.SetActive(true);

		StartCoroutine(NextGrowthStage());
		StartCoroutine(Timer());
	}

    IEnumerator NextGrowthStage()
	{
		yield return new WaitForSeconds(stageGrowthTime);
		plant.sprite = chosenGrowingPlant.plantGrowthStages[currentGrowthStageIndex];
		currentGrowthStageIndex++;

		if (currentGrowthStageIndex == chosenGrowingPlant.plantGrowthStages.Length)
		{
			Debug.Log("Finished Growing");
		}
		else
		{
			StartCoroutine(NextGrowthStage());
		}
	}
	IEnumerator Timer()
	{
		yield return new WaitForSeconds(1);
		elapsedTime++;
		progressBar.current = elapsedTime;

		if (elapsedTime < totalGrowTime)
		{
		StartCoroutine(Timer());
		}
	}
}
