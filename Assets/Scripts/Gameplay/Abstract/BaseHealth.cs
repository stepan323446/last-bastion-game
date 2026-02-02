using System;
using UnityEngine;
using UnityEngine.Events;
public abstract class BaseHealth : MonoBehaviour
{
    [SerializeField] public bool _isImmortal = false;
    
    public UnityAction<float> OnDamaged;
    public UnityAction<float> OnHealed;
    public UnityAction OnDied;
    public UnityAction<float> OnHealthChanged;
    
    public abstract float MaxHealth { get; protected set; }
    public abstract float CurrentHealth { get; protected set; }

    public bool IsDead
    {
        get => CurrentHealth <= 0;
    }

    public void Heal(float healAmount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + healAmount, 0f, MaxHealth);
        OnHealed?.Invoke(CurrentHealth);
        OnHealthChanged?.Invoke(healAmount);
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;
        
        if (_isImmortal)
            damage = 0;
        
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0f, MaxHealth);
        
        OnDamaged?.Invoke(damage);
        OnHealthChanged?.Invoke(damage * -1);

        if(IsDead) HandleDeath();
    }

    public void Kill()
    {
        TakeDamage(CurrentHealth);
    }

    void HandleDeath()
    {
        OnDied?.Invoke();
    }
}