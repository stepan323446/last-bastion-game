using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project._Scripts.UI.HUD
{
    public class HUDHealth : MonoBehaviour
    {
        [Header("UI Objects")]
        [SerializeField] Slider _healthBar;
        [SerializeField] TextMeshProUGUI _healthText;
    
        private float _MaxHealth { get => DataManager.Instance.playerData.maxHealth; }
        private float _CurrentHealth { get => DataManager.Instance.playerData.currentHealth; }
    
        void Update()
        {
            _healthBar.value = _CurrentHealth / _MaxHealth;
            _healthText.text = (int)_CurrentHealth + "/" + (int)_MaxHealth;
        }
    }
}
