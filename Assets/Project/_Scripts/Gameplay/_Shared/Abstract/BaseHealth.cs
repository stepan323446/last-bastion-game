using Project._Scripts.Gameplay._Shared;
using UnityEngine;
using UnityEngine.Events;

namespace Project._Scripts.Gameplay._Shared.Abstract
{
    public abstract class BaseHealth : MonoBehaviour
    {
        [SerializeField] protected bool _isImmortal = false;
    
        public UnityAction<float, DamageTypeSo> OnDamaged;
        public UnityAction<float> OnHealed;
        public UnityAction<DamageTypeSo> OnDied;
    
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
        }

        public void TakeDamage(float damage, DamageTypeSo damageSource = null)
        {
            if (IsDead) return;
        
            if (_isImmortal)
                damage = 0;

            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0f, MaxHealth);

            OnDamaged?.Invoke(damage, damageSource);

            if(IsDead) HandleDeath();
        }

        public void Kill()
        {
            TakeDamage(CurrentHealth, null);
        }

        void HandleDeath(DamageTypeSo damageSource = null)
        {
            OnDied?.Invoke(damageSource);
        }
    }
}

