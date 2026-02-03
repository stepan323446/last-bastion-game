using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthNPC))]
[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] protected float damage = 10;
    [Space]
    [SerializeField] private string _takeDamageSound;
    [SerializeField] private string _deathSound;
    [Space]
    [SerializeField] private float _deathSecondsDelay;
    [Space]
    [SerializeField] private string _deathAnimatorVar = "died";

    [Space] [SerializeField] private GameObject _dropPrefab;
    
    protected HealthNPC _healthNPC;
    protected Animator animator;
    
    float _coroutineDamageColorTimer = 0.3f;
    Color _damageColor = new Color(1, 0.3f, 0.3f);
    SpriteRenderer _spriteRenderer;
    Coroutine _damageCoroutine;
    protected Transform _playerTransform;
    
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _healthNPC = GetComponent<HealthNPC>();
        _healthNPC.OnDied += DiedHandle;
        _healthNPC.OnDamaged += DamageHandler;
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }
    private void DamageHandler(float damage)
    {
        SoundEffectManager.Play(_takeDamageSound);
        PlayDamageEffect();
    }
    private void DiedHandle()
    {
        SoundEffectManager.Play(_deathSound);
        PlayDeathEffect();
        DataManager.Instance.playerData.KilledEnemiesCount++;
    }

    private void PlayDamageEffect()
    {
        if(_damageCoroutine != null)
            StopCoroutine(_damageCoroutine);
        
        _damageCoroutine = StartCoroutine(DamageEffectCoroutine());
    }

    private void PlayDeathEffect()
    {
        StartCoroutine(DeathEffectCoroutine());
    }
    private void DropItem()
    {
        if(_dropPrefab == null) return;
        
        GameObject droppedItem = Instantiate(_dropPrefab, transform.position, Quaternion.identity);
        droppedItem.GetComponent<BounceEffect>().StartBounce();
    }

    private IEnumerator DeathEffectCoroutine()
    {
        if(!string.IsNullOrEmpty(_deathAnimatorVar))
            animator.SetBool(_deathAnimatorVar, true);
        else
            _spriteRenderer.enabled = false;
        DropItem();
            
        yield return new WaitForSeconds(_deathSecondsDelay);
        Destroy(gameObject);
    }
    private IEnumerator DamageEffectCoroutine()
    {
        Color startColor = _spriteRenderer.color;
        float t = 0f;
        float halfDuration = _coroutineDamageColorTimer * 0.5f;
        
        while (t < 1f)
        {
            t += Time.deltaTime / halfDuration;
            _spriteRenderer.color = Color.Lerp(startColor, _damageColor, t);
            yield return null;
        }
        startColor = _spriteRenderer.color;
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / halfDuration;
            _spriteRenderer.color = Color.Lerp(startColor, Color.white, t);
            yield return null;
        }
        _damageCoroutine = null;
        _spriteRenderer.color = Color.white;
    }
}