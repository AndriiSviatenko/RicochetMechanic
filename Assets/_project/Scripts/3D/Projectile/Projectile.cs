using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour, IProjectile
{
    private Rigidbody _rb;
    private ProjectileConfig _config;
    private GameObject _owner;
    private int _bounces = 0;
    private float _spawnTime;
    private Vector3 _direction;
    private ProjectileConfig _cfg;

    private ObjectPool<Projectile> _pool;

    public void SetPool(ObjectPool<Projectile> pool) => _pool = pool;

    private void Awake() =>
        _rb = GetComponent<Rigidbody>();

    private void OnEnable() =>
        _spawnTime = Time.time;

    private void Update()
    {
        if (_config != null && Time.time - _spawnTime > _config.lifeTime)
            ReturnToPool();
    }

    public void Init(Vector3 position, Vector3 direction, ProjectileConfig cfg, GameObject owner = null)
    {
        transform.position = position;
        transform.forward = direction.normalized;
        _config = cfg;
        _owner = owner;
        _bounces = 0;
        _rb.useGravity = cfg.useGravity;
        gameObject.SetActive(true);
        _direction = direction;
        _cfg = cfg;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _direction.normalized * _config.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _owner) return;

        var contact = collision.contacts[0];

        _bounces++;

        if (_bounces > _config.maxBounces)
        {
            return;
        }

        Vector3 reflect = Vector3.Reflect(_direction, contact.normal).normalized;
        float newSpeed = _rb.linearVelocity.magnitude * _config.restitution;

        _direction = reflect;
        _rb.linearVelocity = reflect * newSpeed;

        transform.position = contact.point + reflect * 0.1f;
        transform.forward = reflect;
    }

    private void ReturnToPool()
    {
        if (_pool != null)
        {
            _pool.Return(this);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetProjectile()
    {
        _config = null;
        _owner = null;
        _rb.linearVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
