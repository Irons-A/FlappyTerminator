using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed = 6f;
    [SerializeField] private ProjectileObjectPool _projectilePool;
    [SerializeField] private Transform _firePoint;

    public void Attack()
    {
        Projectile projectile = _projectilePool.GetObject();

        projectile.gameObject.SetActive(true);
        projectile.SetParameters(true, _projectileSpeed);
        projectile.transform.position = _firePoint.transform.position;
        projectile.transform.rotation = transform.rotation;
    }
}
