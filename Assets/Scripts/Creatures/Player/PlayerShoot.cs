using UnityEngine;
using Zenject;

public class PlayerShoot : MonoBehaviour
{
    [Inject] private BulletsPool _bulletsPool;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private WeaponInfo _weaponInfo;
    [SerializeField] private ParticleSystem _muzzlePs;
    private PlayerAnimator _playerAnimator;
    private float _currentCooldown;

    public bool CanStartShoot() => _currentCooldown <= 0f;

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
        _currentCooldown = (_weaponInfo.FireRate > 0f) ? 1f / _weaponInfo.FireRate : 0f;
        _playerAnimator.AnimatorSetTrigger(AnimatorStrings.Shoot);
    }

    public void Fire()
    {
        // _muzzlePs.Play();

        var go = _bulletsPool.GetBullet(_weaponInfo.BulletType);
        Vector3 shootDirection = GetShootDirection();
        Quaternion bulletRotation = Quaternion.LookRotation(shootDirection, Vector3.up);

        go.transform.SetPositionAndRotation(_firePoint.position, bulletRotation);

        var bullet = go.GetComponent<Bullet>();
        bullet.Setup(_bulletsPool, _weaponInfo);
    }

    private Vector3 GetShootDirection()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 targetPoint = ray.origin + ray.direction * 1000f;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
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
}
