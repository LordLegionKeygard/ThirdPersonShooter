using UnityEngine;
using Zenject;
using System.Collections;

public class CreatureTakeDamageVFX : MonoBehaviour
{
    [Inject] protected readonly TakeDamageVFXPool _pool;
    [SerializeField] protected DamageVFXType _vfxType;
    private float _delay = 2;
    protected float Height;
    
    public virtual void SpawnTakeDamageVFX(Transform hit, Transform weapon)
    {
        GameObject vfx = _pool.GetVFX(_vfxType);

        vfx.transform.position = new Vector3(hit.position.x, weapon.position.y, hit.position.z);
        vfx.transform.rotation = transform.rotation * Quaternion.Euler(0, 90, 0);

        StartCoroutine(ReturnVFXAfterDelay(_vfxType, vfx));
    }

    public virtual IEnumerator ReturnVFXAfterDelay(DamageVFXType vfxType, GameObject vfx)
    {
        yield return new WaitForSeconds(_delay);
        _pool.ReturnVFX(vfxType, vfx);
    }
}
