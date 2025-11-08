using UnityEngine;

public interface IProjectile
{
    void Init(Vector3 pos, Vector3 dir, ProjectileConfig config, GameObject owner = null);
    void ResetProjectile();
}
