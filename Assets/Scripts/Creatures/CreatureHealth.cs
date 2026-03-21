using UnityEngine;

public class CreatureHealth : MonoBehaviour
{
    protected float MaxHealth;
    protected float CurrentHealth;
    protected bool IsDeath;

    public virtual void CalculateDamage(float damage, DamageType damageType, Transform hit, Transform weaponTransfor)
    {
        
    }
}
