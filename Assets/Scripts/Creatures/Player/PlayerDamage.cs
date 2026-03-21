using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private float _damage;
    public float GetDamage() => _damage;
    public void SetupDamage(float damage)
    {
        _damage = damage;
    }
}
