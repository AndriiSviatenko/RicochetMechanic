using UnityEngine;

public class Weapon2D : MonoBehaviour
{
    [SerializeField] private ProjectileConfig projectileConfig;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int poolSize = 16;

    private ObjectPool<Projectile2D> _pool;
    private Projectile2D _projectilePrefab;

    private void Awake()
    {
        if(projectileConfig == null || projectileConfig.prefab == null)
        {
            Debug.LogError("ProjectileConfig or prefab missing");
            return;
        }

        _projectilePrefab = projectileConfig.prefab.GetComponent<Projectile2D>();
        _pool = new ObjectPool<Projectile2D>(_projectilePrefab, poolSize, transform);
    }

    public void Fire(Vector2 direction, GameObject owner = null)
    {
        var p = _pool.Get();
        p.SetPool(_pool);
        p.Init(spawnPoint.position, direction, projectileConfig, owner);
    }
}
