using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

public enum SaveSubLocation
    { Temp, MainSave}

public static class SaveSystem
{
    public static string savesFolder = Application.persistentDataPath + "/Saves";

    // Save Locations
    public static string saveOneLocation = savesFolder + "/SaveOne";
    public static string saveTwoLocation = savesFolder + "/SaveTwo";
    public static string saveThreeLocation = savesFolder + "/SaveThree";

    public static string currentSaveLocation { get; set; } = saveOneLocation + "/";

    public static SaveSubLocation saveSubLocation = SaveSubLocation.MainSave;

    public static void SaveFile<T>(string filePath, string fileName, T objectToWrite) // FilePath and FileName are two different variables because the filePath is used to test if the folder the file will be in exists
    {
        filePath = currentSaveLocation + saveSubLocation + filePath;
		if (!Directory.Exists(filePath))
		{
        Directory.CreateDirectory(filePath);
        Debug.Log("File or folder does not exist, Creating directory: " + filePath);
		}
        using (Stream stream = File.Open(filePath + fileName + ".data", FileMode.Create))
        {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, objectToWrite);
        }
    }

    public static T LoadFile<T>(string filePath)
    {
        filePath = currentSaveLocation + saveSubLocation + filePath + ".data";
        if (!File.Exists(filePath))
        {
            return default(T);
        }
        using (Stream stream = File.Open(filePath, FileMode.Open))
        {
            var binaryFormatter = new BinaryFormatter();
            Debug.Log(filePath);
            return (T)binaryFormatter.Deserialize(stream);
        }
    }
    public static T LoadFileInMainSave<T>(string filePath)
    {
        filePath = currentSaveLocation + "MainSave" + filePath + ".data";
        if (!File.Exists(filePath))
        {
            return default(T);
        }
        using (Stream stream = File.Open(filePath, FileMode.Open))
        {
            var binaryFormatter = new BinaryFormatter();
            return (T)binaryFormatter.Deserialize(stream);
        }
    }

    public static void CopyFolder(string sourceFolder, string destFolder)
    {
        if (!Directory.Exists(destFolder))
            Directory.CreateDirectory(destFolder);
		else
		{
            Directory.Delete(destFolder, true);
            Directory.CreateDirectory(destFolder);
		}
        string[] files = Directory.GetFiles(sourceFolder);
        foreach (string file in files)
        {
            string name = Path.GetFileName(file);
            string dest = Path.Combine(destFolder, name);
            File.Copy(file, dest);
        }
        string[] folders = Directory.GetDirectories(sourceFolder);
        foreach (string folder in folders)
        {
            string name = Path.GetFileName(folder);
            string dest = Path.Combine(destFolder, name);
            CopyFolder(folder, dest);
        }
    }

    public static void CreateAppID()
	{
        var path = "steam_appid.txt";
        var appID = "480";
        if (!File.Exists(path))
            File.Create(path);

        using StreamWriter sw = new StreamWriter(path);
        sw.Write(appID);
    }

    public static T LoadFileInSpecificSave<T>(int saveLocationIndex, string filePath)
	{
		switch (saveLocationIndex)
		{
            case (1):
                filePath = saveOneLocation +  "/" + saveSubLocation + "/" + filePath;
                break;

            case (2):
                filePath = saveTwoLocation + "/" + saveSubLocation + "/" + filePath;
                break;

            case (3):
                filePath = saveThreeLocation + "/" + saveSubLocation + "/" + filePath;
                break;

            default:
                Debug.LogError("Not a valid SaveLocationIndex");
                break;

        }

        if (!File.Exists(filePath))
        {
            return default(T);
        }
        using (Stream stream = File.Open(filePath, FileMode.Open))
        {
            var binaryFormatter = new BinaryFormatter();
            return (T)binaryFormatter.Deserialize(stream);
        }
    }
}
