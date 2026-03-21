using UnityEngine;

public class CreatureHealth : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public bool IsDeath;

    public virtual void CalculateDamage(float damage, DamageType damageType, Transform hit, Transform weaponTransfor)
    {
        
    }
}
