using System;
using UnityEngine;

public class ProjectileCollisionsHandler : MonoBehaviour
{
    private Projectile core;

    public event Action CollisionDetected;

    private void Awake()
    {
        core = GetComponent<Projectile>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ObjectRemover _) || other.TryGetComponent(out Player _) && core.IsAlly == false 
            || other.TryGetComponent(out Enemy _) && core.IsAlly == true)
        {
            CollisionDetected?.Invoke();
        }
    }
}
