using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float _spawnFrequency;
    [SerializeField] private float _enemyRotation = 180;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;
    [SerializeField] private EnemyObjectPool _enemyPool;
    [SerializeField] private ProjectileObjectPool _projectilePool;

    private Coroutine _spawnRoutine;

    private void Start()
    {
        if (_spawnRoutine != null)
        {
            StopCoroutine(EnemySpawnRoutine());
        }

        _spawnRoutine = StartCoroutine(EnemySpawnRoutine());
    }

    private IEnumerator EnemySpawnRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnFrequency);

        while (enabled)
        {
            Spawn();
            yield return delay;
        }
    }

    private void Spawn()
    {
        float spawnPositionY = Random.Range(_upperBound, _lowerBound);
        Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

        Enemy enemy = _enemyPool.GetObject();

        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPoint;
        enemy.transform.rotation = Quaternion.Euler(0, 0, _enemyRotation);
        enemy.SetProjectilePool(_projectilePool);
    }
}
