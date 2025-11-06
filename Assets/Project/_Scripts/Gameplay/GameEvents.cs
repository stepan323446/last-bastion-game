using System;
using Project._Scripts.Gameplay._Shared;

namespace Project._Scripts.Gameplay
{
    public static class GameEvents
    {
        public static Action<float> OnPlayerHealed;
        public static Action<float, DamageTypeSo> OnPlayerDamaged;
        public static Action<float> OnHealthChanged;
        public static Action<DamageTypeSo> OnPlayerDied;
    }
}
