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
	[SerializeField, ReadOnlyInspector] int timeRemaining;

	[SerializeField, ReadOnlyInspector] int currentGrowthStageIndex = 0;

	[SerializeField] int[] stageIntervals;

	public void StartGrowing()
	{
		plant.sprite = chosenGrowingPlant.plantGrowthStages[0];
		currentGrowthStageIndex++;

		totalGrowTime = chosenGrowingPlant.plantGrowTime;

		stageGrowthTime = totalGrowTime / (float)chosenGrowingPlant.plantGrowthStages.Length;

		progressBar.maximum = totalGrowTime;

		plantIcon.SetActive(false);

		progressBar.gameObject.SetActive(true);

		timeRemaining = chosenGrowingPlant.plantGrowTime;

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
		timeRemaining--;
		progressBar.current = timeRemaining;

		if (timeRemaining > 0)
		{
		StartCoroutine(Timer());
		}
	}
}
