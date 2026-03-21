using UnityEngine;
using Zenject;

public class PlayerShoot : MonoBehaviour
{
    [Inject] private BulletsPool _bulletsPool;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ParticleSystem _muzzlePs;
    [SerializeField] private LayerMask _shootLayer;
    private PlayerDamage _playerDamage;
    private PlayerEquipment _playerEquipment;
    private PlayerAnimator _playerAnimator;
    private PlayerMovement _playerMovement;
    private Camera _camera;
    private float _currentCooldown;
    private bool _isShootPressed;
    private bool _isShootAnimationPlaying;
    private WeaponInfo _weaponInfo;

    public bool CanStartShoot() => _currentCooldown <= 0f && !_isShootAnimationPlaying;

    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerEquipment = GetComponent<PlayerEquipment>();
        _playerDamage = GetComponent<PlayerDamage>();

        _camera = Camera.main;
    }

    private void Update()
    {
        if (_currentCooldown > 0f)
        {
            _currentCooldown -= Time.deltaTime;
        }
    }

    public void PrepareShoot()
    {
        if (!CanStartShoot()) return;

        _weaponInfo = _playerEquipment.GetCurrentEquipmentWeapon();

        _isShootAnimationPlaying = true;
        _currentCooldown = (_weaponInfo.FireRate > 0f) ? 1f / _weaponInfo.FireRate : 0f;
        _playerMovement.SetMovementBlocked(true);
        _playerAnimator.AnimatorSetTrigger(AnimatorStrings.Shoot);
    }

    public void Fire()
    {
        _muzzlePs.Play();
        Vector3 shootDirection = GetShootDirection();
        Quaternion bulletRotation = Quaternion.LookRotation(shootDirection, Vector3.up);
        var go = _bulletsPool.GetBullet(_weaponInfo.BulletType, _firePoint.position, bulletRotation);

        var bullet = go.GetComponent<Bullet>();
        bullet.Setup(_bulletsPool, _weaponInfo, _playerDamage.GetDamage());
    }

    private Vector3 GetShootDirection()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 targetPoint = ray.origin + ray.direction * 1000f;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, _shootLayer, QueryTriggerInteraction.Ignore))
        {
            targetPoint = hit.point;
        }

        Vector3 shootDirection = (targetPoint - _firePoint.position).normalized;

        if (shootDirection.sqrMagnitude <= Mathf.Epsilon)
        {
            return _firePoint.forward;
        }

        return shootDirection;
    }

    public void SetShootMovementLock(bool isLocked)
    {
        _playerMovement.SetMovementBlocked(isLocked);
    }

    public void SetShootPressed(bool state)
    {
        _isShootPressed = state;

        if (state)
        {
            _playerMovement.SetMovementBlocked(true);
        }
    }

    public void OnShootAnimationEnd()
    {
        _isShootAnimationPlaying = false;

        if (_isShootPressed)
        {
            return;
        }

        _playerMovement.SetMovementBlocked(false);
    }
}
