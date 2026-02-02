using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StupidMeleeAgent : MeleeEnemy
{
    [Header("Stupid Agent")]
    [SerializeField] private float _changePosInterval = 5f;
    [SerializeField] private float _waitOnPosition = 1f;
    [SerializeField] private float _randomRadiusWalk = 3f;
    [SerializeField] private float _randomRadiusAngle = 20f;
    [SerializeField] private float _speed = 3f;

    private Vector2 _targetPoint;
    private float _changePosTimer;
    private float _waitTimer;
    private float _attackTimer;
    private Rigidbody2D _rb;
    private static readonly float[] Directions =
    {
        0f,
        90f,
        180f,
        270f
    };
    
    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _targetPoint = GetRandomPointByDirections();
        OnAttacked += HandleDelayAttack;
    }
    
    private Vector2 GetRandomPointByDirections()
    {
        float baseAngle = Directions[Random.Range(0, Directions.Length)];

        // Random offset
        float randomOffset = Random.Range(-_randomRadiusAngle, _randomRadiusAngle);

        float angle = (baseAngle + randomOffset) * Mathf.Deg2Rad;

        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        return (Vector2)transform.position + dir * _randomRadiusWalk;
    }

    private void HandleDelayAttack()
    {
        _attackTimer = 1f;
    }
    
    protected void FixedUpdate()
    {
        if(PauseController.IsGamePaused) return;
        
        _attackTimer -= Time.fixedDeltaTime;
        if (_attackTimer <= 0)
        {
            Attack();
            _attackTimer = 0;
        }
        
        if (_changePosTimer > 0f)
        {
            _changePosTimer -= Time.fixedDeltaTime;
            MoveTo(_targetPoint);
            
            if (Vector2.Distance(transform.position, _targetPoint) < 0.2f)
            {
                _changePosTimer = 0f;
            }
        }
        else
        {
            _waitTimer -= Time.fixedDeltaTime;

            if (_waitTimer <= 0f)
            {
                _targetPoint = GetRandomPointByDirections();
                _changePosTimer = _changePosInterval;
                _waitTimer = _waitOnPosition;
            }
        }
    }
    protected void MoveTo(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;
        Vector2 directionNormalized = direction.normalized;
        Vector2 newPos = _rb.position + directionNormalized * _speed * Time.fixedDeltaTime;
        
        _rb.MovePosition(newPos);
    }


    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _randomRadiusWalk);

        if (_targetPoint != null)
        {
            Gizmos.DrawSphere(_targetPoint, 0.1f);
        }
    }
}
