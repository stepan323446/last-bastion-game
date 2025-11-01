using System;
using Project._Scripts.Gameplay._Shared;
using Project._Scripts.Gameplay._Shared.Abstract;
using UnityEngine;

namespace Project._Scripts.Gameplay.Player
{
    public class PlayerHealth : BaseHealth
    {
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
        }

        public float GetRatio() => CurrentHealth / MaxHealth;
        public bool IsCritical() => GetRatio() <= _criticalHealthRatio;

        private void DiedHandler(DamageTypeSo damageType)
        {
            GameEvents.OnPlayerDied?.Invoke(damageType);
        }

        private void DamageHandler(float damage, DamageTypeSo damageType)
        {
            GameEvents.OnPlayerDamaged?.Invoke(damage, damageType);
        }

        private void HealedHandler(float healAmount)
        {
            GameEvents.OnPlayerHealed?.Invoke(healAmount);
        }
    }
}
