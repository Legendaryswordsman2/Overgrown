using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;

public class TitleManager : MonoBehaviour
{
	[SerializeField] GameObject mainMenu;
	[SerializeField] GameObject optionsMenu;
	[SerializeField] GameObject selectSaveMenu;

	[Space]

	[SerializeField] Startup startupScript;
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			mainMenu.SetActive(true);
			optionsMenu.SetActive(false);
			selectSaveMenu.SetActive(false);
		}
	}
	public void DeleteSaveSlot(int saveSlotIndex)
	{
		if (saveSlotIndex == 1 && Directory.Exists(SaveSystem.saveOneLocation + "/MainSave"))
		{
			Directory.Delete(SaveSystem.saveOneLocation + "/MainSave", true);
			startupScript.saveSlotOne.GetChild(0).GetComponent<TMP_Text>().text = "LV: 1";
			var levelProgressBar = startupScript.saveSlotOne.GetChild(1).GetComponent<ProgressBar>();
			levelProgressBar.max = 100;
			levelProgressBar.current = 0;
		}

		if (saveSlotIndex == 2 && Directory.Exists(SaveSystem.saveTwoLocation + "/MainSave"))
		{
			Directory.Delete(SaveSystem.saveTwoLocation + "/MainSave", true);
			startupScript.saveSlotTwo.GetChild(0).GetComponent<TMP_Text>().text = "LV: 1";
			var levelProgressBar = startupScript.saveSlotTwo.GetChild(1).GetComponent<ProgressBar>();
			levelProgressBar.max = 100;
			levelProgressBar.current = 0;
		}

		if (saveSlotIndex == 3 && Directory.Exists(SaveSystem.saveThreeLocation + "/MainSave"))
		{
			Directory.Delete(SaveSystem.saveThreeLocation + "/MainSave", true);
			startupScript.saveSlotThree.GetChild(0).GetComponent<TMP_Text>().text = "LV: 1";
			var levelProgressBar = startupScript.saveSlotThree.GetChild(1).GetComponent<ProgressBar>();
			levelProgressBar.max = 100;
			levelProgressBar.current = 0;
		}

	}
}
