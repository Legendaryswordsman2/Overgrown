using UnityEngine;
using System.IO;
using TMPro;
using System;

public class Startup : MonoBehaviour
{
    public Transform saveSlotOne, saveSlotTwo, saveSlotThree;

    [field: Space]
    [field: SerializeField] public Transform deleteSaveButtonsParent { get; private set; }
    private void Awake()
    {
        SaveSystem.saveSubLocation = SaveSubLocation.MainSave;
        DeleteTempSaves();
        InitializeSaveSlots();
        SetDeleteSaveButtons();
    }

	private void DeleteTempSaves()
	{
        if (Directory.Exists(SaveSystem.saveOneLocation + "/Temp"))
            Directory.Delete(SaveSystem.saveOneLocation + "/Temp", true);

        if (Directory.Exists(SaveSystem.saveTwoLocation + "/Temp"))
            Directory.Delete(SaveSystem.saveTwoLocation + "/Temp", true);

        if (Directory.Exists(SaveSystem.saveThreeLocation + "/Temp"))
            Directory.Delete(SaveSystem.saveThreeLocation + "/Temp", true);
        Debug.Log("Deleted temp saves");
    }

	void InitializeSaveSlots()
	{

        SaveSystem.saveSubLocation = SaveSubLocation.MainSave;

        if(File.Exists(SaveSystem.saveOneLocation + "/MainSave/Player/PlayerLevel&XP.json"))
        {
            LevelSystemSaveData xpDataSaveOne = SaveSystem.LoadFileInSpecificSave<LevelSystemSaveData>(1, "/Player/Characters/PlayerLevel.Data");

            saveSlotOne.GetChild(0).GetComponent<TMP_Text>().text = "LV: " + xpDataSaveOne.level;
            saveSlotOne.GetChild(1).GetComponent<ProgressBar>().max = xpDataSaveOne.xpToLevelUp;
            saveSlotOne.GetChild(1).GetComponent<ProgressBar>().current = xpDataSaveOne.xp;
        }

        if (File.Exists(SaveSystem.saveTwoLocation + "/MainSave/Player/PlayerLevel&XP.json"))
        {
            LevelSystemSaveData xpDataSaveTwo = SaveSystem.LoadFileInSpecificSave<LevelSystemSaveData>(2, "/Player/Characters/PlayerLevel.Data");

            saveSlotTwo.GetChild(0).GetComponent<TMP_Text>().text = "LV: " + xpDataSaveTwo.level;
            saveSlotTwo.GetChild(1).GetComponent<ProgressBar>().max = xpDataSaveTwo.xpToLevelUp;
            saveSlotTwo.GetChild(1).GetComponent<ProgressBar>().current = xpDataSaveTwo.xp;
        }

        if (File.Exists(SaveSystem.saveThreeLocation + "/MainSave/Player/PlayerLevel&XP.json"))
        {
            LevelSystemSaveData xpDataSaveThree = SaveSystem.LoadFileInSpecificSave<LevelSystemSaveData>(3, "/Player/Characters/PlayerLevel.Data");

            saveSlotThree.GetChild(0).GetComponent<TMP_Text>().text = "LV: " + xpDataSaveThree.level;
            saveSlotThree.GetChild(1).GetComponent<ProgressBar>().max = xpDataSaveThree.xpToLevelUp;
            saveSlotThree.GetChild(1).GetComponent<ProgressBar>().current = xpDataSaveThree.xp;
        }
	}
    void SetDeleteSaveButtons()
    {
        if(Directory.Exists(SaveSystem.saveOneLocation + "/MainSave"))
            deleteSaveButtonsParent.GetChild(0).gameObject.SetActive(true);

        if (Directory.Exists(SaveSystem.saveTwoLocation + "/MainSave"))
            deleteSaveButtonsParent.GetChild(1).gameObject.SetActive(true);

        if (Directory.Exists(SaveSystem.saveThreeLocation + "/MainSave"))
            deleteSaveButtonsParent.GetChild(2).gameObject.SetActive(true);
    }
}
