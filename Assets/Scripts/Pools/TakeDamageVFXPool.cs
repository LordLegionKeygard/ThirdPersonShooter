using System.Collections.Generic;
using UnityEngine;

public class TakeDamageVFXPool : MonoBehaviour
{
    [SerializeField] private List<VFXConfig> _vfxConfigs;
    private int _poolSize = 10;
    private Dictionary<DamageVFXType, List<Queue<GameObject>>> _vfxPools;

    private void Start()
    {
        _vfxPools = new Dictionary<DamageVFXType, List<Queue<GameObject>>>();

        foreach (var config in _vfxConfigs)
        {
            var pools = new List<Queue<GameObject>>();

            foreach (var variation in config.VFX)
            {
                var pool = new Queue<GameObject>();

                for (int i = 0; i < _poolSize; i++)
                {
                    GameObject vfx = Instantiate(variation, transform);
                    vfx.SetActive(false);
                    pool.Enqueue(vfx);
                }

                pools.Add(pool);
            }

            _vfxPools[config.VFXType] = pools;
        }
    }

    public GameObject GetVFX(DamageVFXType vfxType)
    {
        if (_vfxPools.ContainsKey(vfxType))
        {
            var pools = _vfxPools[vfxType];
            int randomIndex = Random.Range(0, pools.Count);
            var pool = pools[randomIndex];

            if (pool.Count > 0)
            {
                GameObject vfx = pool.Dequeue();
                vfx.SetActive(true);
                return vfx;
            }
            else
            {
                // Если пул пуст, создаем новый VFX
                GameObject variationPrefab = _vfxConfigs.Find(x => x.VFXType == vfxType).VFX[randomIndex];
                GameObject newVFX = Instantiate(variationPrefab, transform);
                newVFX.SetActive(true);

                // Добавляем новый VFX обратно в пул
                pool.Enqueue(newVFX);
                return newVFX;
            }
        }

        Debug.LogWarning($"VFX Pool does not contain VFX of type {vfxType}");
        return null;
    }

    public void ReturnVFX(DamageVFXType vfxType, GameObject vfx)
    {
        vfx.SetActive(false);

        if (_vfxPools.ContainsKey(vfxType))
        {
            var config = _vfxConfigs.Find(x => x.VFXType == vfxType);

            // Ищем индекс вариации
            int variationIndex = -1;
            for (int i = 0; i < config.VFX.Count; i++)
            {
                if (vfx.name.Contains(config.VFX[i].name))
                {
                    variationIndex = i;
                    break;
                }
            }

            if (variationIndex >= 0)
            {
                _vfxPools[vfxType][variationIndex].Enqueue(vfx);
            }
            else
            {
                Debug.LogWarning($"Returned VFX does not match any variation for type {vfxType}");
            }
        }
    }
}

[System.Serializable]
public class VFXConfig
{
    public DamageVFXType VFXType;
    public List<GameObject> VFX;
}

[System.Serializable]
public enum DamageVFXType
{
    RedBlood = 0,
    MetalSpark = 1,
}
