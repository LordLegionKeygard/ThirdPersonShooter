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

    [Header("Damage")]
    public float Damage;
    public float DamageFactor; // доп урон которое получает оружие, за каждое улучшение

    [Header("VFX")]
    public GameObject ExplosionPrefab;
}
