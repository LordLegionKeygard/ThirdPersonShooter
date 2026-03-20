using UnityEngine;
using Zenject;

public class PlayerShoot : MonoBehaviour
{
    [Inject] private BulletsPool _bulletsPool;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private WeaponInfo _weaponInfo;
    [SerializeField] private ParticleSystem _muzzlePs;
    private float _currentCooldown;
    public bool CanShoot() => _currentCooldown <= 0;

    private void Update()
    {
        if (_currentCooldown > 0f)
        {
            _currentCooldown -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if (!CanShoot()) return;

        _currentCooldown = (_weaponInfo.FireRate > 0f) ? 1f / _weaponInfo.FireRate : 0f;

        Fire();
    }

    public void Fire()
    {
        // _muzzlePs.Play();

        var go = _bulletsPool.GetBullet(_weaponInfo.BulletType);

        go.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);

        var bullet = go.GetComponent<Bullet>();
        bullet.Setup(_bulletsPool, _weaponInfo);
    }
}
