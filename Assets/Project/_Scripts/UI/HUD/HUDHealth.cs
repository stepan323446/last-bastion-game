using Project._Scripts.Gameplay;
using Project._Scripts.UI.Abstract;
using TMPro;
using UnityEngine;

namespace Project._Scripts.UI.HUD
{
    public class HUDHealth : HealthBar
    {
        [Header("UI Objects")]
        [SerializeField] TextMeshProUGUI _healthText;
    
        protected override float _MaxHealth { get => DataManager.Instance.playerData.MaxHealth; }
        protected override float _CurrentHealth { get => DataManager.Instance.playerData.CurrentHealth; }

        private void Awake()
        {
            GameEvents.OnHealthChanged += HealthChangedHandler;
        }

        protected override void UpdateHealthBar()
        {
            base.UpdateHealthBar();
            _healthText.text = (int)_CurrentHealth + "/" + (int)_MaxHealth;
        }
    }
}
