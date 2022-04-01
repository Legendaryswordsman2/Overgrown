using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bed : MonoBehaviour
{
	[SerializeField] KeyCode sleepKey = KeyCode.E;

	[SerializeField] Vector3 playerWakeUpPosition;

	[SerializeField] GameObject sleepIcon;

	[Space]

	[Tooltip("The time until the player can sleep again in minutes")]
	[SerializeField] int timeUntilCanSleepAgain = 5;
	[SerializeField] string cantSleepText = "YOURE NOT TIRED RIGHT NOW";

	static DateTime lastTimeSlept;
	public static bool playerhasSleptBefore = false;

	bool isInRange;

	TextPopup popupText;

	private void Awake()
	{
		popupText = Inventory.instance.textPopup;
	}

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
		if (playerhasSleptBefore)
		{
			TimeSpan timeSpan = DateTime.Now - lastTimeSlept;
			Debug.Log((int)timeSpan.TotalMinutes + " minute(s), " + timeSpan.Seconds + " seconds since last sleep");

			//Debug.Log(timeSpan.Minutes);

			if (timeSpan.TotalMinutes < timeUntilCanSleepAgain)
			{
				popupText.SetPopup(cantSleepText, 1, true);
				return;
			}
		}

		lastTimeSlept = DateTime.Now;
		playerhasSleptBefore = true;

		GameManager gameManager = GameManager.instance;
		gameManager.player.GetComponent<PlayerStats>().Sleep();
		gameManager.inventory.ResetPlantsHealth();
		StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("CrossFade Start", "CrossFade", "Home (Inside)", playerWakeUpPosition));
	}
}
