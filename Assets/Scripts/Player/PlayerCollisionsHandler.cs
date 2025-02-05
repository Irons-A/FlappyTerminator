using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollisionsHandler : MonoBehaviour
{
    public event Action CollisionDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Projectile damager) && damager.IsAlly == false 
            || other.TryGetComponent(out Enemy _))
        {
            CollisionDetected?.Invoke();
        }
    }
}
