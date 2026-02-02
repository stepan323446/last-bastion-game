using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class PlayerData
{
    [SerializeField] float _maxHealth = 100;
    [SerializeField] float _currentHealth = 100;
    
    public float MaxHealth
    {
        get => _maxHealth; 
        set => _maxHealth = Mathf.Clamp(value, 0, value);
    }
    
    public bool IsDead => _currentHealth <= 0;

    public float CurrentHealth
    {
        get => _currentHealth; 
        set => _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
    }
}