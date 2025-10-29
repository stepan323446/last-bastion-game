using Project._Scripts.Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Project._Scripts.UI.HUD
{
    public class HUDStamina : MonoBehaviour
    {
        [Header("UI Objects")]
        [SerializeField] Slider _staminaBar;

        private float CurrentStamina
        {
            get => PlayerStats.Instance.Stamina;
        }

        private float MaxStamina
        {
            get => PlayerStats.Instance.MaxStamina;
        }
    
        void Update()
        {
            _staminaBar.value = CurrentStamina / MaxStamina;
        }
    }
}
