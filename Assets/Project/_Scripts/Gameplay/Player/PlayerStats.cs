using UnityEngine;

namespace Project._Scripts.Gameplay.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Stamina")]
        [SerializeField] float staminaRegenerationRate = 0.3f;
        public bool staminaRegenEnabled = true;

        public float MaxStamina
        {
            get => DataManager.Instance.playerData.MaxStamina;
            private set => DataManager.Instance.playerData.MaxStamina = value;
        }

        public float Stamina
        {
            get => DataManager.Instance.playerData.CurrentStamina;
            set => DataManager.Instance.playerData.CurrentStamina = value;
        }

        private void Update()
        {
            if(staminaRegenEnabled)
                Stamina += staminaRegenerationRate * Time.deltaTime;
        }
        public bool CheckStaminaCost(float staminaCost) => Stamina >= staminaCost;
    }
}

