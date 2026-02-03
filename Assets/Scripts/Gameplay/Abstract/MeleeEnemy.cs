using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class MeleeEnemy : Enemy
{
    enum AttackZoneSide
    {
        RIGHTLEFT,
        UPDOWN
    }
    
    [Header("Attack Options")]
    [SerializeField] private float _delayBeforeAttack = 0.5f;
    [SerializeField] private float _attackDuration = 1f;
    [SerializeField] private Vector2 _attackSize = new Vector2(1.5f, 1.5f);
    [SerializeField] private float _attackDistance = 1f;
    [SerializeField] private string _attackAnimatorVar = "attacking";
    [SerializeField] private LayerMask _playerLayer;

    private Vector2 _dynamicAttackSize;
    private Vector2 _attackOffset = Vector2.zero;
    private Coroutine _attackCoroutine;
    protected UnityAction OnAttacked;
    public bool IsAttacking { get => _attackCoroutine != null; }
    private Vector2 AttackOriginPosition
    {
        get => (Vector2)transform.position + _attackOffset;
    }

    protected virtual void Awake()
    {
        _dynamicAttackSize = _attackSize;
        _attackOffset = Vector2.zero;
    }

    protected void SetDirectionAttackZone(Vector2 direction)
    {
        if (direction == Vector2.left)
        {
            _dynamicAttackSize = new Vector2(_attackSize.y, _attackSize.x);
        }
        else if (direction == Vector2.right)
        {
            _dynamicAttackSize = new Vector2(_attackSize.y, _attackSize.x);
        }
        else if (direction == Vector2.up)
        {
            _dynamicAttackSize = _attackSize;
        }
        else if (direction == Vector2.down)
        {
            _dynamicAttackSize = _attackSize;
        }
        else
        {
            Debug.LogWarning("Invalid attack direction");
            return;
        }

        _attackOffset = direction * _attackDistance;
    }

    protected void Attack()
    {
        if(_attackCoroutine == null)
            _attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    protected virtual void Hit()
    {
        Collider2D hit = Physics2D.OverlapBox(AttackOriginPosition, _dynamicAttackSize, 0f, _playerLayer);
        if (hit != null)
        {
            hit.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            OnAttacked?.Invoke();
        }
    }
    
    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(_delayBeforeAttack);
        if(!string.IsNullOrEmpty(_attackAnimatorVar))
            animator.SetBool(_attackAnimatorVar, true);
        
        Hit();
        
        yield return new WaitForSeconds(_attackDuration);
        if(!string.IsNullOrEmpty(_attackAnimatorVar))
            animator.SetBool(_attackAnimatorVar, false);
        
        _attackCoroutine = null;
    }
    protected virtual void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) 
        {
            Vector2 size = _attackSize;
            Vector2 offset = Vector2.down * _attackDistance;

            Vector3 center = (Vector3)transform.position + (Vector3)offset;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, size);
        }
        else
        {
            Vector3 center = (Vector3)AttackOriginPosition;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, _dynamicAttackSize);
        }
    }
}
