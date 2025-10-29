using UnityEngine;

namespace Project._Scripts.Gameplay._Shared
{
    [CreateAssetMenu(fileName = "New Damage Type", menuName = "Gameplay/Damage Type")]
    public class DamageTypeSo : ScriptableObject
    {
        public string typeName;
        public Sprite icon;
    }
}