using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OverlapZone : MonoBehaviour
{
    [SerializeField] private Vector2 _overlapSize = new Vector2(1.5f, 1.5f);
    [SerializeField] private Vector2 _overlapOffset = new Vector2(0, 0);
    [SerializeField] private LayerMask _hitLayer;
    
    public UnityAction<Collider2D> OnHit;
    private Vector2 OverlapOriginPosition
    {
        get => (Vector2)transform.position + _overlapOffset;
    }

    public void Hit()
    {
        Collider2D hit = Physics2D.OverlapBox(OverlapOriginPosition, _overlapSize, 0f, _hitLayer);
        if (hit != null)
        {
            OnHit?.Invoke(hit);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(OverlapOriginPosition, _overlapSize);
    }
}
