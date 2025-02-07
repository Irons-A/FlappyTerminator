using System;
using UnityEngine;

public class EnemyCollisionsHandler : MonoBehaviour
{
    public event Action<bool> CollisionDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _) || other.TryGetComponent(out ObjectRemover _))
        {
            CollisionDetected?.Invoke(false);
        }
        else if (other.TryGetComponent(out Projectile damager) && damager.IsAlly == true)
        {
            CollisionDetected?.Invoke(true);
        }
    }
}
