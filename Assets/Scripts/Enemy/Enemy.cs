using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyCollisionsHandler))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _attackSpeed = 1.5f;
    [SerializeField] private float _projectileSpeed = 2f;

    public event Action<Enemy> IsDestroyed;
    public event Action ScoreAdded;

    private Coroutine _attackRoutine;
    private EnemyCollisionsHandler _selfCollisionsHandler;
    private ProjectileObjectPool _projectilePool;

    private void Awake()
    {
        _selfCollisionsHandler = GetComponent<EnemyCollisionsHandler>();
    }

    private void OnEnable()
    {
        _selfCollisionsHandler.CollisionDetected += ProcessCollision;

        if (_attackRoutine != null)
        {
            StopCoroutine(_attackRoutine);
        }

        _attackRoutine = StartCoroutine(AttackRoutine());
    }

    private void OnDisable()
    {
        _selfCollisionsHandler.CollisionDetected -= ProcessCollision;
    }

    public void SetProjectilePool(ProjectileObjectPool pool)
    {
        _projectilePool = pool;
    }

    private void ProcessCollision(bool isDestroyedByPlayer)
    {
        if (isDestroyedByPlayer)
        {
            ScoreAdded?.Invoke();
        }

        IsDestroyed?.Invoke(this);
    }

    private IEnumerator AttackRoutine()
    {
        WaitForSeconds attackSpeed = new WaitForSeconds(_attackSpeed);

        while (enabled)
        {
            yield return attackSpeed;

            Projectile projectile = _projectilePool.GetObject();

            projectile.gameObject.SetActive(true);
            projectile.SetParameters(false, _projectileSpeed);
            projectile.transform.position = _firePoint.transform.position;
            projectile.transform.rotation = transform.rotation;
        }
    }
}
