using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Project._Scripts.UI.Abstract
{
    public abstract class HealthBar : MonoBehaviour
    {
        [Header("UI Objects")]
        [SerializeField] Slider _healthBar;
        [SerializeField] Slider _delayedHealthBar;
        [SerializeField] private float _delayDelayedHealth = 1f;
        [SerializeField] private float _speedDelayedHealth = 0.5f;
    
        protected abstract float _MaxHealth { get; }
        protected abstract float _CurrentHealth { get; }
        private bool _startedDelayedBar = false;

        protected void Start()
        {
            UpdateHealthBar();
            ResetDelayedHealthBar();
        }

        protected void HealthChangedHandler(float healthChangedAmount)
        {
            UpdateHealthBar();

            if (!_startedDelayedBar)
            {
                if (healthChangedAmount < 0)
                {
                    StartCoroutine(DelayedHealthBarCoroutine());
                }
                else if(healthChangedAmount > 0)
                {
                    ResetDelayedHealthBar();
                }   
            }
        }

        protected virtual void UpdateHealthBar()
        {
            _healthBar.value = _CurrentHealth / _MaxHealth;
        }

        private IEnumerator DelayedHealthBarCoroutine()
        {
            _startedDelayedBar = true;
            yield return new WaitForSeconds(_delayDelayedHealth);

            while (_delayedHealthBar.value > _healthBar.value)
            {
                _delayedHealthBar.value -= Time.deltaTime * _speedDelayedHealth;
                yield return null;
            }
            ResetDelayedHealthBar();
            
            _startedDelayedBar = false;
        }

        private void ResetDelayedHealthBar() => _delayedHealthBar.value = _healthBar.value;
    }
}
