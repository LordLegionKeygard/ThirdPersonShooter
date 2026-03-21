using System;
using System.IO;
using UnityEngine;

public class SaveGameDataWriter
{
    public string SaveDataDirectoryPath = "";
    private string _dataSaveFileName => "Save.txt";

    public SaveGameDataWriter(string saveDataDirectoryPath)
    {
        SaveDataDirectoryPath = saveDataDirectoryPath;
    }

    public SaveData LoadHangarDataFromJson()
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, _dataSaveFileName);

        SaveData loadedSaveData = null;

        if (File.Exists(savePath))
        {
            try
            {
                string saveDataToLoad = "";

                using (FileStream stream = new(savePath, FileMode.Open))
                {
                    using StreamReader reader = new(stream);
                    saveDataToLoad = reader.ReadToEnd();
                }
                loadedSaveData = JsonUtility.FromJson<SaveData>(saveDataToLoad);
            }
            catch (Exception exception)
            {
                Debug.LogWarning(exception.Message);
            }
        }
        else
        {
            Debug.Log("Save file does not exist");
        }
        return loadedSaveData;
    }

    public void WriteHangarDataToSaveFile(SaveData saveData)
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, _dataSaveFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            // Debug.Log("Save Path " + savePath);

            string dataToStore = JsonUtility.ToJson(saveData, true);

            using FileStream stream = new(savePath, FileMode.Create);
            using StreamWriter writer = new(stream);
            writer.Write(dataToStore);
        }
        catch (Exception exception)
        {
            Debug.LogError("Error while trying to save data, game could not be saved - " + exception);
        }
    }

    public void DeleteSaveFile()
    {
        File.Delete(Path.Combine(SaveDataDirectoryPath, _dataSaveFileName));
    }

    public bool CheckIfSaveFileExists()
    {
        if (File.Exists(Path.Combine(SaveDataDirectoryPath, _dataSaveFileName))) return true;

        else return false;
    }
}