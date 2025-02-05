using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileObjectPool : MonoBehaviour
{
    [SerializeField] private Projectile _prefab;

    private Queue<Projectile> _pool;
    private List<Projectile> _allProjectiles = new List<Projectile>();

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
            _allProjectiles.Add(projectile);

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
        _pool.Clear();

        foreach (Projectile projectile in _allProjectiles)
        {
            _pool.Enqueue(projectile);
            projectile.gameObject.SetActive(false);
        }
    }
}
