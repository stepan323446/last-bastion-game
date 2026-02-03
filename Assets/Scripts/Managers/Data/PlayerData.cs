using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class PlayerData
{
    [SerializeField] float _maxHealth = 100;
    [SerializeField] float _currentHealth = 100;
    
    [SerializeField] int _killedEnemies = 0;
    [SerializeField] int _completedQuests = 0;

    public void LoadData(PlayerData playerData)
    {
        _maxHealth = playerData.MaxHealth;
        _currentHealth = playerData.CurrentHealth;
        _killedEnemies = playerData.KilledEnemiesCount;
        _completedQuests = playerData.CompletedQuestsCount;
        
        GameEvents.OnHealthChanged?.Invoke(_currentHealth);
    }
    
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
    
    public int KilledEnemiesCount
    {
        get => _killedEnemies; 
        set => _killedEnemies = Mathf.Clamp(value, 0, value);
    }
    public int CompletedQuestsCount
    {
        get => _completedQuests; 
        set => _completedQuests = Mathf.Clamp(value, 0, value);
    }
}