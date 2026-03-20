using UnityEngine;
using Zenject;

public class PlayerShoot : MonoBehaviour
{
    [Inject] private BulletsPool _bulletsPool;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private WeaponInfo _weaponInfo;
    [SerializeField] private ParticleSystem _muzzlePs;
    private PlayerAnimator _playerAnimator;
    private float _currentCooldown;
    private bool _isWaitingAnimationEvent;

    public bool CanStartShoot() => _currentCooldown <= 0f && !_isWaitingAnimationEvent;

    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        if (_currentCooldown > 0f)
        {
            _currentCooldown -= Time.deltaTime;
        }
    }

    public void BeginShoot()
    {
        if (!CanStartShoot()) return;
        _playerAnimator.AnimatorSetTrigger(AnimatorStrings.Shoot);
    }

    public void Fire()
    {
        _currentCooldown = (_weaponInfo.FireRate > 0f) ? 1f / _weaponInfo.FireRate : 0f;
        // _muzzlePs.Play();

        var go = _bulletsPool.GetBullet(_weaponInfo.BulletType);

        go.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);

        var bullet = go.GetComponent<Bullet>();
        bullet.Setup(_bulletsPool, _weaponInfo);
    }
}
