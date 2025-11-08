using System;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile2D : MonoBehaviour
{
    private Rigidbody2D _rb;
    private ProjectileConfig _config;
    private GameObject _owner;
    private int _bounces;
    private float _spawnTime;

    private ObjectPool<Projectile2D> _pool;
    private Vector2 _direction;

    public void SetPool(ObjectPool<Projectile2D> pool)
    {
        _pool = pool;
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _spawnTime = Time.time;
    }
    private void Update()
    {
        if (_config != null && Time.time - _spawnTime > _config.lifeTime)
            ReturnOnPool();
    }

    public void Init(Vector2 position, Vector2 direction, ProjectileConfig cfg, GameObject owner = null)
    {
        transform.position = position;
        transform.right = direction.normalized;
        _config = cfg;
        _owner = owner;
        _bounces = 0;

        _rb.gravityScale = cfg.useGravity ? 1f : 0f;
        gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _direction.normalized * _config.speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == _owner) return;

        _bounces++;
        var contact = collision.contacts[0];

        if(_bounces > _config.maxBounces)
        {
            ReturnOnPool();
            return;
        }

        Vector2 v = _rb.linearVelocity.normalized;
        Vector2 n = contact.normal.normalized;

        Vector2 reflect = v - 2f * Vector2.Dot(v, n) * n;

        _rb.linearVelocity = reflect.normalized * _config.speed * _config.restitution;
        transform.position = contact.point + n * 0.1f;
    }


    private void ReturnOnPool()
    {
        if(_pool != null)
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
        _rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
