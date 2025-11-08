using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ProjectileConfig projectileConfig;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int poolSize = 16;

    private ObjectPool<Projectile> _pool;
    private Projectile _projectilePrefab;

    private void Awake()
    {
        if(projectileConfig == null)
            Debug.LogError("ProjectileConfig missing");

        if(projectileConfig.prefab == null)
            Debug.LogError("Projectile prefab missing in config");

        _projectilePrefab = projectileConfig.prefab.GetComponent<Projectile>();
        _pool = new ObjectPool<Projectile>(_projectilePrefab, poolSize);
    }

    public void Fire(Vector3 direction, GameObject owner = null)
    {
        var p = _pool.Get();
        p.SetPool(_pool);
        p.Init(spawnPoint.position, direction, projectileConfig);
    }
}
