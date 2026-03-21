using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private PlayerStatsInfo _playerStatsInfo;

    [Header("Health")]
    [SerializeField] private int _currentHealthLevel;
    [SerializeField] private PlayerHealth _playerHealth;

    [Header("Speed")]
    [SerializeField] private int _currentSpeedLevel;
    [SerializeField] private PlayerSpeed _playerSpeed;

    [Header("Damage")]
    [SerializeField] private int _currentDamageLevel;
    [SerializeField] private PlayerDamage _playerDamage;
    
    public int GetCurrentHealthLevel() => _currentHealthLevel;
    public int GetCurrentSpeedLevel() => _currentSpeedLevel;
    public int GetCurrentDamageLevel() => _currentDamageLevel;

    public void LoadUpgrades(SaveData saveData)
    {
        _currentHealthLevel = saveData.HealthLevel;
        _currentSpeedLevel = saveData.SpeedLevel;
        _currentDamageLevel = saveData.DamageLevel;

        SetupPlayerStats(true);
    }

    private void SetupPlayerStats(bool isDataLoad)
    {
        var health = _playerStatsInfo.BaseHealth + _currentHealthLevel * _playerStatsInfo.UpgradeHealthFactor;
        _playerHealth.SetupHealth(health, isDataLoad);

        var speed = _playerStatsInfo.BaseSpeed + _currentSpeedLevel * _playerStatsInfo.UpgradeSpeedFactor;
        _playerSpeed.SetupSpeed(speed);

        var damage = _playerStatsInfo.BaseDamage + _currentDamageLevel * _playerStatsInfo.UpgradeDamageFactor;
        _playerDamage.SetupDamage(damage);
    }
}
