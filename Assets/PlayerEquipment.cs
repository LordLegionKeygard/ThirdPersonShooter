using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private WeaponInfo _weaponInfo;
    public WeaponInfo GetCurrentEquipmentWeapon() => _weaponInfo;
}
