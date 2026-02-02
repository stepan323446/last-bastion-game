using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNPC : BaseHealth
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth = 100;
    
    public override float MaxHealth { get => _maxHealth; protected set => _maxHealth = value; }

    public override float CurrentHealth
    {
        get => _currentHealth;
        protected set
        {
            _currentHealth = value;
        }
    }
    
    
}
