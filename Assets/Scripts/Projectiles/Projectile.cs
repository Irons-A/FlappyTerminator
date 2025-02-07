using System;
using UnityEngine;

[RequireComponent(typeof(ProjectileCollisionsHandler))]
public class Projectile : MonoBehaviour
{
    [field: SerializeField] public bool IsAlly { get; private set; }
    [SerializeField] private float _movementSpeed = 0;
    
    private ProjectileCollisionsHandler _selfCollisionsHandler;

    public event Action<Projectile> IsDestroyed;

    private void Awake()
    {
        _selfCollisionsHandler = GetComponent<ProjectileCollisionsHandler>();
    }

    private void OnEnable()
    {
        _selfCollisionsHandler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _selfCollisionsHandler.CollisionDetected -= ProcessCollision;
    }

    private void Update()
    {
        transform.position += transform.right * _movementSpeed * Time.deltaTime;
    }

    public void SetParameters(bool affiliation, float speed)
    {
        IsAlly = affiliation;
        _movementSpeed = speed;
    }

    private void ProcessCollision()
    {
        IsDestroyed?.Invoke(this);
    }
}
