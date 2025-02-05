using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPool : MonoBehaviour
{
    [SerializeField] private Projectile _prefab;

    private Queue<Projectile> _pool;

    private void Awake()
    {
        _pool = new Queue<Projectile>();
    }

    private void OnEnable()
    {
        foreach (Projectile projectile in _pool)
        {
            projectile.IsDestroyed += ReturnObject;
        }
    }

    private void OnDisable()
    {
        foreach (Projectile projectile in _pool)
        {
            projectile.IsDestroyed -= ReturnObject;
        }
    }

    public Projectile GetObject()
    {
        if (_pool.Count == 0)
        {
            Projectile projectile = Instantiate(_prefab);
            projectile.IsDestroyed += ReturnObject;

            return projectile;
        }

        return _pool.Dequeue();
    }

    public void ReturnObject(Projectile projectile)
    {
        _pool.Enqueue(projectile);
        projectile.gameObject.SetActive(false);
    }

    public void Reset()
    {
        foreach (Projectile projectile in _pool)
        {
            Destroy(projectile);
        }

        _pool.Clear();
    }
}
