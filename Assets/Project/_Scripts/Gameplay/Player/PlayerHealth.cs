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
            get => DataManager.Instance.playerData.maxHealth;
            protected set => DataManager.Instance.playerData.maxHealth = value;
        }

        public override float CurrentHealth
        {
            get => DataManager.Instance.playerData.currentHealth;
            protected set => DataManager.Instance.playerData.currentHealth = value;
        }
    

        public float GetRatio() => CurrentHealth / MaxHealth;
        public bool IsCritical() => GetRatio() <= _criticalHealthRatio;
    }
}
