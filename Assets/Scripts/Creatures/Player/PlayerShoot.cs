using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private BulletsPool _bulletsPool; // TODO Zenject
    [SerializeField] private Transform _firePoint;
    [SerializeField] private WeaponInfo _weaponInfo;
    [SerializeField] private ParticleSystem _muzzlePs;
    private bool _canShoot;
    private float _currentCooldown;
    public bool CanShoot() => _canShoot;

    private void Update()
    {
        if (_currentCooldown > 0f)
        {
            _currentCooldown -= Time.deltaTime;
        }
    }

    public void TryShoot()
    {
        if (_currentCooldown > 0) return;

        _currentCooldown = (_weaponInfo.FireRate > 0f) ? 1f / _weaponInfo.FireRate : 0f;

        Fire();
    }

    public void Fire()
    {
        _muzzlePs.Play();

        var go = _bulletsPool.GetBullet(_weaponInfo.BulletType);

        go.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);

        var bullet = go.GetComponent<Bullet>();
        bullet.Setup(_bulletsPool, _weaponInfo);

    }
}
