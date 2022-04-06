using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	[SerializeField] GameObject mainMenu;
	[SerializeField] GameObject optionsMenu;
	[SerializeField] GameObject selectSaveMenu;
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			mainMenu.SetActive(true);
			optionsMenu.SetActive(false);
			selectSaveMenu.SetActive(false);
		}
	}
}
