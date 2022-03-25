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

	[SerializeField, ReadOnlyInspector] SOGrowingPlant chosenGrowingPlant;

	// Growing
	List<int> GrowthStageTimes = new List<int>();

	public void StartGrowing()
	{
		plant.sprite = chosenGrowingPlant.plantGrowthStages[0];

		//for (int i = 0; i < chosenGrowingPlant.plantGrowthStages.Length; i++)
		//{
		//	GrowthStageTimes.Add()
		//}
	}
}
