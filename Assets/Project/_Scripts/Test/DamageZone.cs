using Project._Scripts.Gameplay._Shared;
using Project._Scripts.Gameplay._Shared.Abstract;
using UnityEngine;

namespace Project._Scripts.Test
{
    public class DamageZone : MonoBehaviour
    {
        [SerializeField] float _damage;
        [SerializeField] DamageTypeSo _damageType;
        [SerializeField] bool _is_healing;

        private void OnTriggerEnter2D(Collider2D other)
        {
            BaseHealth _health = other.GetComponent<BaseHealth>();
            if (_health != null)
            {
                if(_is_healing)
                    _health.Heal(_damage);
                else
                    _health.TakeDamage(_damage, _damageType);
            }
        }
    }

}