using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
	[SerializeField] KeyCode sleepKey = KeyCode.E;

	[SerializeField] Vector3 playerWakeUpPosition;

	[SerializeField] GameObject sleepIcon;

	bool isInRange;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isInRange = true;
			sleepIcon.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isInRange = false;
			sleepIcon.SetActive(false);
		}
	}

	private void Update()
	{
		if (isInRange && Input.GetKeyDown(sleepKey))
			Sleep();
	}

	void Sleep()
	{
		Debug.Log("Slept");
		GameManager gameManager = GameManager.instance;
		gameManager.player.GetComponent<PlayerStats>().Sleep();
		gameManager.inventory.ResetPlantsHealth();
		StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("CrossFade Start", "CrossFade", "Home (Inside)", playerWakeUpPosition));
	}
}
