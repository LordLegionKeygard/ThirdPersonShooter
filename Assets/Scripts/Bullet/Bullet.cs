using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask _hitMask;
    [SerializeField] private GameObject _model;
    [SerializeField] private float _destroyTimeAfterHit;
    [SerializeField] private ParticleSystem[] _particleSystems;
    private BulletsPool _pool;
    private float _lifeTime;
    private float _life;
    private bool _inited;
    private WeaponInfo _weaponInfo;
    private float _damage;
    private bool _hasHit;

    public void Setup(BulletsPool pool, WeaponInfo weaponInfo, float damage)
    {
        _pool = pool;
        _lifeTime = Mathf.Max(0.05f, weaponInfo.LifeTime);
        _life = 0f;
        _inited = true;
        _weaponInfo = weaponInfo;
        _damage = damage;
    }

    private void OnEnable()
    {
        _life = 0f;
        _hasHit = false;
        ResetParticles(true);
    }

    private void OnDisable()
    {
        ResetParticles(false);
    }

    private void Update()
    {
        if (!_inited) return;

        float deltaTime = Time.deltaTime;
        _life += deltaTime;

        Vector3 from = transform.position;
        Vector3 to = from + transform.forward * (_weaponInfo.BulletSpeed * deltaTime);

        if (Physics.Linecast(from, to, out RaycastHit hit, _hitMask, QueryTriggerInteraction.Ignore))
        {
            Hit(hit.collider, hit.point);
            return;
        }

        transform.position = to;

        if (_life >= _lifeTime) TryReturnBullet();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_inited || _hasHit || other == null)
        {
            return;
        }

        Hit(other, other.ClosestPoint(transform.position));
    }

    private void Hit(Collider hitCollider, Vector3 hitPoint)
    {
        if (_hasHit || hitCollider == null)
        {
            return;
        }

        _hasHit = true;

        CreatureHealth creatureHealth = hitCollider.GetComponentInParent<CreatureHealth>();
        if (creatureHealth != null)
        {
            creatureHealth.CalculateDamage(_damage, DamageType.PhysDamage, hitCollider.transform, transform);
        }

        transform.position = hitPoint;
        TryReturnBullet();
    }

    private void TryReturnBullet()
    {
        _inited = false;
        if (_destroyTimeAfterHit > 0)
        {
            if (_model != null) _model.SetActive(false);
            StartCoroutine(nameof(ReturnBulletCoroutine));
        }
        else
        {
            _pool.ReturnBullet(_weaponInfo.BulletType, gameObject);
        }
    }

    private void ResetParticles(bool playAfterReset)
    {
        if (_particleSystems == null) return;

        for (int i = 0; i < _particleSystems.Length; i++)
        {
            ParticleSystem particleSystem = _particleSystems[i];
            if (particleSystem == null) continue;

            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particleSystem.Clear(true);
            if (playAfterReset)
            {
                particleSystem.Play(true);
            }
        }
    }

    private IEnumerator ReturnBulletCoroutine()
    {
        float elapsedTime = 0;

        while (elapsedTime < _destroyTimeAfterHit)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _pool.ReturnBullet(_weaponInfo.BulletType, gameObject);
        if (_model != null) _model.SetActive(true);
    }
}

[System.Serializable]
public enum BulletEnum
{
    None = -1,
    ShotGunBullet = 0
}
