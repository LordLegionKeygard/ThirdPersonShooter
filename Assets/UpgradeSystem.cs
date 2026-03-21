using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private int _currentHealthLevel;
    [SerializeField] private int _currentSpeedLevel;
    [SerializeField] private int _currentDamageLevel;
    public int GetCurrentHealthLevel() => _currentHealthLevel;
    public int GetCurrentSpeedLevel() => _currentSpeedLevel;
    public int GetCurrentDamageLevel() => _currentDamageLevel;

    public void LoadUpgrades(SaveData saveData)
    {
        _currentHealthLevel = saveData.HealthLevel;
        _currentSpeedLevel = saveData.SpeedLevel;
        _currentDamageLevel = saveData.DamageLevel;
    }
}
