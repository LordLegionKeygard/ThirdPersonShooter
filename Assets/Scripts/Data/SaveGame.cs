using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public SaveLoad SaveLoad;

    [Header("SaveDataWriter")]
    private SaveGameDataWriter _saveGameDataWriter;
    public SaveGameDataWriter GetHangarSaveGameDataWriter() => _saveGameDataWriter;

    [Header("CurrentCommandCenterData")]
    public SaveData SaveData;

    private void Awake()
    {
        _saveGameDataWriter = new SaveGameDataWriter(Application.persistentDataPath);
    }

    private void Start()
    {
        CheckSaveData();
    }

    private void CheckSaveData()
    {
        if (!_saveGameDataWriter.CheckIfSaveFileExists())
        {
            NewData();
        }
        else
        {
            LoadDataFromJson();
            SaveLoad?.LoadGameData(ref SaveData);
        }
    }

    public void NewData()
    {
        SaveData = new SaveData
        {
            Score = 0,
            HealthLevel = 0,
            SpeedLevel = 0,
            DamageLevel = 0,
        };

        _saveGameDataWriter.WriteHangarDataToSaveFile(SaveData);

        SaveLoad.LoadGameData(ref SaveData);
    }

    public void SaveDataToJson()
    {
        _saveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        SaveLoad.SaveData(ref SaveData);
        _saveGameDataWriter.WriteHangarDataToSaveFile(SaveData);
    }

    public void LoadDataFromJson()
    {
        _saveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        SaveData = _saveGameDataWriter.LoadHangarDataFromJson();
    }
}
