using UnityEngine;
using UnityEngine.Serialization;

namespace Project._Scripts.Gameplay.Player.Data
{
    [System.Serializable]
    public class PlayerData
    {
        [SerializeField] float _maxHealth;
        [SerializeField] float _currentHealth;
        [SerializeField] float _maxStamina;
        [SerializeField] float _stamina;

        public float MaxHealth
        {
            get => _maxHealth; 
            set => _maxHealth = Mathf.Clamp(value, 0, value);
        }

        public float CurrentHealth
        {
            get => _currentHealth; 
            set => _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
        }

        public float MaxStamina
        {
            get => _maxStamina;
            set => _maxStamina = Mathf.Clamp(value, 0, value);
        }

        public float CurrentStamina
        {
            get => _stamina;
            set => _stamina = Mathf.Clamp(value, 0, MaxStamina);
        }
    }
}
