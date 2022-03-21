using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantInfoBox : MonoBehaviour
{
	[SerializeField] TMP_Text plantName;
	[SerializeField] ProgressBar plantHealthBar;

	public void SetInfoBox(SOPlant plantSO)
	{
		plantName.text = plantSO.unitName.ToUpper();
		plantHealthBar.maximum = plantSO.defaultHealth;
		plantHealthBar.current = plantSO.currentHealth;

		gameObject.SetActive(true);
	}
}
