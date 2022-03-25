using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingManager : MonoBehaviour
{
	[SerializeField, ReadOnlyInspector] GameObject plantablePot;

	GameObject plantUiMenu;

	private void Awake()
	{
		plantUiMenu = GameManager.instance.plantUiMenu;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Pot"))
		{
			plantablePot = collision.gameObject;
			plantablePot.GetComponentInParent<Grow>().plantIcon.SetActive(true);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Pot"))
		{
			plantablePot.GetComponentInParent<Grow>().plantIcon.SetActive(false);
			plantablePot = null;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && plantablePot != null)
		{
			GameManager.StopTime();
			plantUiMenu.SetActive(true);
		}
	}

	public void StartPlanting(SOGrowingPlant growingPlantSO)
	{
		plantUiMenu.SetActive(false);

		
	}
}
