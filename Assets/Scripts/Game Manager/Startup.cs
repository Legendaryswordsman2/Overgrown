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
            PlayerXPData xpDataSaveOne = SaveSystem.LoadFileInSpecificSave<PlayerXPData>(1, "/Player/PlayerLevel&XP.json");

            saveSlotOne.GetChild(0).GetComponent<TMP_Text>().text = "LV: " + xpDataSaveOne.playerLevel.ToString();
            saveSlotOne.GetChild(1).GetComponent<ProgressBar>().max = xpDataSaveOne.xpToLevelUp;
            saveSlotOne.GetChild(1).GetComponent<ProgressBar>().current = xpDataSaveOne.xp;

            deleteSaveButtonsParent.GetChild(0).gameObject.SetActive(true);
        }

        if (File.Exists(SaveSystem.saveTwoLocation + "/MainSave/Player/PlayerLevel&XP.json"))
        {
            PlayerXPData xpDataSaveTwo = SaveSystem.LoadFileInSpecificSave<PlayerXPData>(2, "/Player/PlayerLevel&XP.json");

            saveSlotTwo.GetChild(0).GetComponent<TMP_Text>().text = "LV: " + xpDataSaveTwo.playerLevel.ToString();
            saveSlotTwo.GetChild(1).GetComponent<ProgressBar>().max = xpDataSaveTwo.xpToLevelUp;
            saveSlotTwo.GetChild(1).GetComponent<ProgressBar>().current = xpDataSaveTwo.xp;

            deleteSaveButtonsParent.GetChild(1).gameObject.SetActive(true);
        }

        if (File.Exists(SaveSystem.saveThreeLocation + "/MainSave/Player/PlayerLevel&XP.json"))
        {
            PlayerXPData xpDataSaveThree = SaveSystem.LoadFileInSpecificSave<PlayerXPData>(3, "/Player/PlayerLevel&XP.json");

            saveSlotThree.GetChild(0).GetComponent<TMP_Text>().text = "LV: " + xpDataSaveThree.playerLevel.ToString();
            saveSlotThree.GetChild(1).GetComponent<ProgressBar>().max = xpDataSaveThree.xpToLevelUp;
            saveSlotThree.GetChild(1).GetComponent<ProgressBar>().current = xpDataSaveThree.xp;

            deleteSaveButtonsParent.GetChild(2).gameObject.SetActive(true);
        }
	}
}
