using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;

    private Queue<Enemy> _pool;
    private List<Enemy> _allEnemies = new List<Enemy>();

    private void Awake()
    {
        _pool = new Queue<Enemy>();
    }

    private void OnEnable()
    {
        foreach (Enemy enemy in _pool)
        {
            enemy.IsDestroyed += ReturnObject;
        }
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in _pool)
        {
            enemy.IsDestroyed -= ReturnObject;
        }
    }

    public Enemy GetObject()
    {
        if (_pool.Count == 0)
        {
            Enemy enemy = Instantiate(_prefab);
            enemy.IsDestroyed += ReturnObject;
            _allEnemies.Add(enemy);

            return enemy;
        }

        return _pool.Dequeue();
    }

    public void ReturnObject(Enemy enemy)
    {
        _pool.Enqueue(enemy);
        enemy.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _pool.Clear();

        foreach (Enemy enemy in _allEnemies)
        {
            _pool.Enqueue(enemy);
            enemy.gameObject.SetActive(false);
        }
    }
}
