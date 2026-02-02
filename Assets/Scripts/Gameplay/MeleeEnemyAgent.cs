using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAgent : MeleeEnemy
{
    [Header("Enemy Agent")]
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _stopDistance = 1.2f;
    
    [Space]
    [SerializeField] private TriggerCollider _detectTriggerCollider;
    
    private Vector2 watchDirection;
    
    private bool CanMoving
    {
        get
        {
            return _playerTransform && !PauseController.IsGamePaused && !_healthNPC.IsDead && !IsAttacking; 
        }
    }

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        _detectTriggerCollider.OnEnter += DetectPlayer; 
    }

    void FixedUpdate()
    {
        if (!CanMoving) return;
        
        Move();
    }

    private void Move()
    {
        Vector2 direction = (_playerTransform.position - transform.position);
        Vector2 directionNormalized = direction.normalized;
        float distance = direction.magnitude;
        Vector2 newPos = rb.position + directionNormalized * _speed * Time.fixedDeltaTime;

        // Animation
        watchDirection = directionNormalized;
        if (Mathf.Abs(watchDirection.x) > Mathf.Abs(watchDirection.y))
        {
            watchDirection.y = 0;
            watchDirection.x = Mathf.Sign(watchDirection.x);
        }
        else
        {
            watchDirection.x = 0;
            watchDirection.y = Mathf.Sign(watchDirection.y);
        }
        animator.SetFloat("vertical", watchDirection.x);
        animator.SetFloat("horizontal", watchDirection.y);

        if (distance > _stopDistance)
        {
            animator.SetBool("is_walking", true);
            rb.MovePosition(newPos);
        }
        else
        {
            SetDirectionAttackZone(watchDirection);
            Attack();
            animator.SetBool("is_walking", false);
        }
    }

    
    public void DetectPlayer(Collider2D collider)
    {
        PlayerMovement playerMovement = collider.GetComponent<PlayerMovement>();
        if (playerMovement == null) return;
        
        _playerTransform = playerMovement.transform;
    }
}
