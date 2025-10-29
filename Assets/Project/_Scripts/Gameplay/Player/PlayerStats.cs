using UnityEngine;

namespace Project._Scripts.Gameplay.Player
{
    public class PlayerStats : Singleton<PlayerStats>
    {
        [Header("Stamina")]
        [SerializeField] float staminaRegenerationRate = 0.3f;
        [SerializeField] float stamina;
        public bool staminaRegenEnabled = true;

        public float MaxStamina
        {
            get => DataManager.Instance.playerData.maxStamina;
            private set => DataManager.Instance.playerData.maxStamina = value;
        }

        public float Stamina
        {
            get => stamina;
            set => stamina = Mathf.Clamp(value, 0f, MaxStamina);
        }

        private void Start()
        {
            Stamina = 100f;
        }

        private void Update()
        {
            if(staminaRegenEnabled)
                Stamina += staminaRegenerationRate * Time.deltaTime;
        }
        public bool CheckStaminaCost(float staminaCost) => Stamina >= staminaCost;
    }
}

