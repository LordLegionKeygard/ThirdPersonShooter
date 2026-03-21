using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : CreatureHealth
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _healthBarBack;
    private CreatureTakeDamageVFX _creatureTakeDamageVFX;
    private float _oldMaxHealth;

    private void Awake()
    {
        _creatureTakeDamageVFX = GetComponent<CreatureTakeDamageVFX>();
    }
    public void CalculateHealth(bool dataLoad)
    {
        _healthBar.maxValue = MaxHealth;
        _healthBarBack.maxValue = MaxHealth;

        if (dataLoad || CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
            _healthBar.value = CurrentHealth;
            _healthBarBack.value = CurrentHealth;
        }
        else if (!dataLoad)
        {
            var currentHealthPercent = CurrentHealth / _oldMaxHealth;
            var newCurrentHealth = MaxHealth * currentHealthPercent;
            CurrentHealth = newCurrentHealth;
            _healthBar.value = CurrentHealth;
            _healthBarBack.value = CurrentHealth;
        }
        _oldMaxHealth = MaxHealth;
    }

    public override void CalculateDamage(float damage, DamageType damageType, Transform hit, Transform weaponTransform)
    {
        if (IsDeath) return;

        switch (damageType)
        {
            case DamageType.PhysDamage:
                TakeDamage(damage);
                _creatureTakeDamageVFX.SpawnTakeDamageVFX(hit, weaponTransform);
                break;
        }
    }

    private void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        _healthBar.value = CurrentHealth;
        _healthBarBack.DOValue(CurrentHealth, 1f);
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (CurrentHealth <= 0 && !IsDeath)
        {
            IsDeath = true;
        }
    }
}

[Serializable]
public enum DamageType
{
    PhysDamage = 0,
}
