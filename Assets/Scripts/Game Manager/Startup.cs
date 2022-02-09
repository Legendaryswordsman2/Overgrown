using UnityEngine;
using System.IO;
using TMPro;

public class Startup : MonoBehaviour
{
    [SerializeField]
    Transform saveSlotOne, saveSlotTwo, saveSlotThree;
    private void Awake()
    {
        //SaveSystem.CreateSaveSlotSubFolders();
        InitializeSaveSlots();
    }
    void InitializeSaveSlots()
	{
        //Directory.CreateDirectory(SaveSystem.savesFolder + "/SaveOne");
        //Directory.CreateDirectory(SaveSystem.savesFolder + "/SaveTwo");
        //Directory.CreateDirectory(SaveSystem.savesFolder + "/SaveThree");

        SaveSystem.saveSubLocation = SaveSubLocation.MainSave;

        if(File.Exists(SaveSystem.saveOneLocation + "/MainSave/Player/PlayerLevel&XP.json"))
        {
            PlayerXPData xpDataSaveOne = SaveSystem.LoadFile<PlayerXPData>("/Player/PlayerLevel&XP.json");

            saveSlotOne.GetChild(0).GetComponent<TMP_Text>().text = "LV: " + xpDataSaveOne.playerLevel.ToString();
            saveSlotOne.GetChild(1).GetComponent<ProgressBar>().maximum = xpDataSaveOne.xpToLevelUp;
            saveSlotOne.GetChild(1).GetComponent<ProgressBar>().current = xpDataSaveOne.xp;
        }

        if (File.Exists(SaveSystem.saveTwoLocation + "/MainSave/Player/PlayerLevel&XP.json"))
        {
            PlayerXPData xpDataSaveTwo = SaveSystem.LoadFile<PlayerXPData>("/Player/PlayerLevel&XP.json");

            saveSlotTwo.GetChild(0).GetComponent<TMP_Text>().text = "LV: " + xpDataSaveTwo.playerLevel.ToString();
            saveSlotTwo.GetChild(1).GetComponent<ProgressBar>().maximum = xpDataSaveTwo.xpToLevelUp;
            saveSlotTwo.GetChild(1).GetComponent<ProgressBar>().current = xpDataSaveTwo.xp;
        }

        if (File.Exists(SaveSystem.saveThreeLocation + "/MainSave/Player/PlayerLevel&XP.json"))
        {
            PlayerXPData xpDataSaveThree = SaveSystem.LoadFile<PlayerXPData>("/Player/PlayerLevel&XP.json");

            saveSlotThree.GetChild(0).GetComponent<TMP_Text>().text = "LV: " + xpDataSaveThree.playerLevel.ToString();
            saveSlotThree.GetChild(1).GetComponent<ProgressBar>().maximum = xpDataSaveThree.xpToLevelUp;
            saveSlotThree.GetChild(1).GetComponent<ProgressBar>().current = xpDataSaveThree.xp;
        }
	}
}
