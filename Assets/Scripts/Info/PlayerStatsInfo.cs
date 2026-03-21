using UnityEngine;

[CreateAssetMenu(menuName = "ThirdPersonShooter/Info/PlayerStatsInfo")]
public class PlayerStatsInfo : ScriptableObject
{
    [Header("Base")]
    public float BaseHealth;
    public float BaseSpeed;
    public float BaseDamage;

    [Header("UpgradeFactor")]
    public float UpgradeHealthFactor;
    public float UpgradeSpeedFactor;
    public float UpgradeDamageFactor;
}