using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class MeleeWeapon : Weapon
{
    [SerializeField] float damage = 10;
    [SerializeField] float animationSpeed = 1;
    [SerializeField] bool isSlashAnimation = false;
    
    private Animator animator;
    private bool isAttacking = false;
    
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;

    private string animationAttackName
    {
        get
        {
            if (isSlashAnimation)
                return "slash_attack";

            return "attack";
        }
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        
        animator.speed = animationSpeed;
    }

    public void StartAnimationAttack()
    {
        isAttacking = true;
    }

    public void EndAnimationAttack()
    {
        isAttacking = false;
        DisplayWeapon(false);
    }
    
    public override void Attack()
    {
        if (!isAttacking)
        {
            DisplayWeapon(true);
            animator.SetTrigger(animationAttackName);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(!isAttacking) return;

        HealthNPC health = other.GetComponent<HealthNPC>();
        if (health == null) return;
        health.TakeDamage(damage);
    }

    private void DisplayWeapon(bool isActive)
    {
        spriteRenderer.enabled = isActive;
        collider2D.enabled = isActive;
    }
}
