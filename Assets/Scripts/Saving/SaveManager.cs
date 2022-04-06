using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveManager : MonoBehaviour
{
	public static SaveManager instance;

	public event EventHandler OnSavingGame;
	public event EventHandler OnLoadingGame;

	public bool canSave { get; set; } = true;

	private void Awake() // Set Instance
	{
		if (instance != null)
			Destroy(this);
		else
			instance = this;
	}

	private void Start() // Load Game
	{
		LoadGame();
	}

	private void OnDestroy() // Save Game
	{
		if (!canSave) return; // If the class is not allowed to save
		SaveGame(false);
	}

	public void SaveGame(bool isMainSave)
	{
		if (isMainSave)
			SaveSystem.saveSubLocation = SaveSubLocation.MainSave;
		else
		{
			SaveSystem.saveSubLocation = SaveSubLocation.Temp;
		}
		OnSavingGame?.Invoke(this, EventArgs.Empty);
		Debug.Log("Saved Game in: " + SaveSystem.saveSubLocation);

		SaveSystem.saveSubLocation = SaveSubLocation.Temp;
	}

	void LoadGame()
	{
		if(SaveSystem.saveSubLocation == SaveSubLocation.MainSave)
		{ // If the game's loading in the main save folder then create a copy of the folder, re-name it to temp, and load that
			if(Directory.Exists(SaveSystem.currentSaveLocation + "/Temp"))
			Directory.Delete(SaveSystem.currentSaveLocation + "/Temp", true);

			if(Directory.Exists(SaveSystem.currentSaveLocation + "/MainSave"))
			SaveSystem.CopyFolder(SaveSystem.currentSaveLocation + "/MainSave", SaveSystem.currentSaveLocation + "Temp");
		}
		OnLoadingGame?.Invoke(this, EventArgs.Empty);
		Debug.Log("Loaded Game in: " + SaveSystem.saveSubLocation);

		SaveSystem.saveSubLocation = SaveSubLocation.Temp;
	} 

	public void MakeTempMainSave()
	{
		SaveGame(false);
		if (Directory.Exists((SaveSystem.currentSaveLocation + "/MainSave")))
		{
		Directory.Delete(SaveSystem.currentSaveLocation + "/MainSave", true);
		}

		if (Directory.Exists((SaveSystem.currentSaveLocation + "/Temp")))
		{
		SaveSystem.CopyFolder(SaveSystem.currentSaveLocation + "Temp", SaveSystem.currentSaveLocation + "/MainSave");
		}
		Debug.Log("Copied Temp to MainSave");
	}

	public void DeleteTempSave()
	{
		if(Directory.Exists(SaveSystem.currentSaveLocation + "/Temp"))
		Directory.Delete(SaveSystem.currentSaveLocation + "/Temp", true);
		Debug.Log("Deleted temp save");
	}

	public void LeaveSceneWithoutSaving(string sceneName)
	{
		canSave = false;
		StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", sceneName));
	}

	private void OnApplicationQuit()
	{
		DeleteTempSave();
		canSave = false;
	}
}
