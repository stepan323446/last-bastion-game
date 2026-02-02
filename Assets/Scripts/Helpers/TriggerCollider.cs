using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCollider : MonoBehaviour
{
    public UnityAction<Collider2D> OnEnter;
    public UnityEvent OnEnterEvent;

    public void OnTriggerEnter2D(Collider2D other)
    {
        OnEnter?.Invoke(other);
        OnEnterEvent?.Invoke();
    }
}
