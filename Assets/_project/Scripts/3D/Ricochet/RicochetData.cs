using UnityEngine;

public class RicochetData
{
    public Vector3 Origin { get; set; }
    public Vector3 Direction { get; set; }
    public float MaxDistance { get; set; }
    public int MaxBounces { get; set; }
    public LayerMask HitMask { get; set; }
    public float InitialSpeed { get; set; }
    public Vector3 Gravity { get; set; }
    public bool UseGravity { get; set; }
    public float Restitution { get; set; }
    public int MaxSegments { get; set; }

    public RicochetData()
    {
        
    }
    public RicochetData(Vector3 origin, Vector3 direction, float maxDistance, int maxBounces,
        LayerMask hitMask, float initialSpeed, Vector3 gravity, bool useGravity,
        float restitution, int maxSegments = 64)
    {
        Origin = origin;
        Direction = direction;
        MaxDistance = maxDistance;
        MaxBounces = maxBounces;
        HitMask = hitMask;
        InitialSpeed = initialSpeed;
        Gravity = gravity;
        UseGravity = useGravity;
        Restitution = restitution;
        MaxSegments = maxSegments;
    }

    public void UpdateValues(Vector3 origin, Vector3 direction, float maxDistance, int maxBounces,
        LayerMask hitMask, float initialSpeed, Vector3 gravity, bool useGravity,
        float restitution, int maxSegments = 64)
    {
        Origin = origin;
        Direction = direction;
        MaxDistance = maxDistance;
        MaxBounces = maxBounces;
        HitMask = hitMask;
        InitialSpeed = initialSpeed;
        Gravity = gravity;
        UseGravity = useGravity;
        Restitution = restitution;
        MaxSegments = maxSegments;
    }
}
