using System;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    [SerializeField] private string TakeDamageSound;
    [SerializeField] private string HealSound;
    [Tooltip("Health ratio at which the critical health vignette starts appearing")]
    [SerializeField] private float _criticalHealthRatio = 0.3f;

    public override float MaxHealth
    {
        get => DataManager.Instance.playerData.MaxHealth;
        protected set => DataManager.Instance.playerData.MaxHealth = value;
    }

    public override float CurrentHealth
    {
        get => DataManager.Instance.playerData.CurrentHealth;
        protected set => DataManager.Instance.playerData.CurrentHealth = value;
    }

    public void Start()
    {
        OnDied += DiedHandler;
        OnHealed += HealedHandler;
        OnDamaged += DamageHandler;
        OnHealthChanged += HealthChangedHandler;
    }
    private void OnDestroy()
    {
        OnDied -= DiedHandler;
        OnHealed -= HealedHandler;
        OnDamaged -= DamageHandler;
        OnHealthChanged -= HealthChangedHandler;
    }

    public float GetRatio() => CurrentHealth / MaxHealth;
    public bool IsCritical() => GetRatio() <= _criticalHealthRatio;

    private void DiedHandler()
    {
        GameEvents.OnPlayerDied?.Invoke();
    }

    private void DamageHandler(float damage)
    {
        GameEvents.OnPlayerDamaged?.Invoke(damage);
        SoundEffectManager.Play(TakeDamageSound);
    }

    private void HealedHandler(float healAmount)
    {
        GameEvents.OnPlayerHealed?.Invoke(healAmount);
        SoundEffectManager.Play(HealSound);
    }

    private void HealthChangedHandler(float healthChangedAmount)
    {
        GameEvents.OnHealthChanged?.Invoke(healthChangedAmount);
    }
}