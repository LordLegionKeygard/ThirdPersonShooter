using UnityEngine;

[CreateAssetMenu(menuName = "ThirdPersonShooter/Info/WeaponInfo")]
public class WeaponInfo : ScriptableObject
{
    [Header("Main")]
    public BulletEnum BulletType;

    [Header("Fire")]
    public float FireRate; // выстрелов в секунду
    public float BulletSpeed; // скорость снаряда
    public float LifeTime; // время жизни
}
