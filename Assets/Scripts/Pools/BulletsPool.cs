using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bulletPrefabs;
    private int _poolSize = 5;
    private Dictionary<BulletEnum, Queue<GameObject>> _bulletPools;

    private void Start()
    {
        _bulletPools = new Dictionary<BulletEnum, Queue<GameObject>>();

        for (int i = 0; i < _bulletPrefabs.Count; i++)
        {
            BulletEnum bulletType = (BulletEnum)i;
            Queue<GameObject> pool = new();

            for (int j = 0; j < _poolSize; j++)
            {
                GameObject bullet = Instantiate(_bulletPrefabs[i], transform);
                bullet.SetActive(false);
                pool.Enqueue(bullet);
            }

            _bulletPools[bulletType] = pool;
        }
    }

    public GameObject GetBullet(BulletEnum bulletType)
    {
        return GetBullet(bulletType, Vector3.zero, Quaternion.identity);
    }

    public GameObject GetBullet(BulletEnum bulletType, Vector3 position, Quaternion rotation)
    {
        if (_bulletPools.ContainsKey(bulletType))
        {
            Queue<GameObject> pool = _bulletPools[bulletType];

            if (pool.Count > 0)
            {
                GameObject bullet = pool.Dequeue();
                bullet.transform.SetPositionAndRotation(position, rotation);
                bullet.SetActive(true);
                return bullet;
            }
            else
            {
                GameObject bullet = Instantiate(_bulletPrefabs[(int)bulletType], transform);
                bullet.transform.SetPositionAndRotation(position, rotation);
                bullet.SetActive(true);
                return bullet;
            }
        }

        return null;
    }

    public void ReturnBullet(BulletEnum bulletType, GameObject bullet)
    {
        bullet.SetActive(false);
        if (_bulletPools.ContainsKey(bulletType))
        {
            _bulletPools[bulletType].Enqueue(bullet);
        }
    }
}
