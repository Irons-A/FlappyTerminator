using System;
using UnityEngine;

public class ProjectileCollisionsHandler : MonoBehaviour
{
    private Projectile _core;

    public event Action CollisionDetected;

    private void Awake()
    {
        _core = GetComponent<Projectile>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ObjectRemover _) || other.TryGetComponent(out Player _) && _core.IsAlly == false 
            || other.TryGetComponent(out Enemy _) && _core.IsAlly == true)
        {
            CollisionDetected?.Invoke();
        }
    }
}
