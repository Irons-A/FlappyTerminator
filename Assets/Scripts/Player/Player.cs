using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerAttacker))]
[RequireComponent(typeof(PlayerCollisionsHandler))]
public class Player : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerCollisionsHandler _handler;

    public event Action GameOver;

    private void Awake()
    {
        _handler = GetComponent<PlayerCollisionsHandler>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    private void ProcessCollision()
    {
        GameOver?.Invoke();
    }

    public void Reset()
    {
        _playerMover.Reset();
    }
}
