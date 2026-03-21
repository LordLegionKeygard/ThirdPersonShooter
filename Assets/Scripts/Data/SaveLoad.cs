using UnityEngine;
using Zenject;

public class SaveLoad : MonoBehaviour
{
    [Inject] private readonly SaveGame _saveGame;
    [SerializeField] private UpgradeSystem _upgradeSystem;
    

    private void Awake()
    {
        _saveGame.SaveLoad = this;
    }

    public void SaveData(ref SaveData currentSaveData)
    {
        currentSaveData.HealthLevel = _upgradeSystem.GetCurrentHealthLevel();
        currentSaveData.SpeedLevel = _upgradeSystem.GetCurrentSpeedLevel();
        currentSaveData.DamageLevel = _upgradeSystem.GetCurrentDamageLevel();
    }

    public void LoadGameData(ref SaveData currentSaveData)
    {
        _upgradeSystem.LoadUpgrades(currentSaveData);
    }
}
