using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthNPC))]
public class EnemyUIHealth : HealthBar
{
    protected override float _MaxHealth { get => _healthNPC.MaxHealth; }
    protected override float _CurrentHealth { get => _healthNPC.CurrentHealth; }

    HealthNPC _healthNPC;

    protected void Awake()
    {
        _healthNPC = GetComponent<HealthNPC>();
        _healthNPC.OnHealthChanged += HealthChangedHandler;
        _healthNPC.OnDied += HideUI;
    }

    private void HideUI()
    {
        _healthBar.gameObject.SetActive(false);
        _delayedHealthBar.gameObject.SetActive(false);
    }
}
