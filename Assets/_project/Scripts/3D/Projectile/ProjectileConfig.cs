using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Projectile",order = 0)]
public class ProjectileConfig : ScriptableObject
{
    [field: SerializeField] public GameObject prefab { get; private set; }
    [field: SerializeField] public float speed { get; private set; }
    [field: SerializeField] public int maxBounces { get; private set; }
    [field: SerializeField] public float lifeTime { get; private set; }
    [field: SerializeField] public float restitution { get; private set; }
    [field: SerializeField] public LayerMask hitMask { get; private set; }
    [field: SerializeField] public bool useGravity { get; private set; }
    [field: SerializeField] public Vector3 gravity { get; private set; }
}
