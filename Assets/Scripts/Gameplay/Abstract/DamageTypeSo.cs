using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Type", menuName = "Gameplay/Damage Type")]
public class DamageTypeSo : ScriptableObject
{
    public string typeName;
    public Sprite icon;
}