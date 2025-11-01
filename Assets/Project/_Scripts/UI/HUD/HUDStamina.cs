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
            get => DataManager.Instance.playerData.CurrentStamina;
        }

        private float MaxStamina
        {
            get => DataManager.Instance.playerData.MaxStamina;
        }
    
        void Update()
        {
            _staminaBar.value = CurrentStamina / MaxStamina;
        }
    }
}
