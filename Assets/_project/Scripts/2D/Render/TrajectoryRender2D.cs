using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRender2D : MonoBehaviour
{
    [SerializeField] private ProjectileConfig config;
    [SerializeField] private Transform origin;
    [SerializeField] private int maxPoints = 64;
    [SerializeField] private float maxDistance = 50f;

    private LineRenderer _lr;
    private IRicochetSolver2D _ricochetSolver;
    private RicochetData _ricochetData;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        _ricochetSolver = new RicochetSolver2D();
        _ricochetData = new();
    }

    public void Show(Vector2 startPos, Vector2 direction)
    {
        if (config == null)
        {
            return;
        }

        _ricochetData.UpdateValues(startPos, direction, maxDistance,
            config.maxBounces, config.hitMask, config.speed,
            config.useGravity ? config.gravity : Vector2.zero,
            config.useGravity, config.restitution, maxPoints);

        var points = _ricochetSolver.CalculatePath(_ricochetData);

        _lr.positionCount = points.Count;
        _lr.SetPositions(points.ConvertAll(p => (Vector3)p).ToArray());
        _lr.enabled = true;
    }

    public void Hide()
    {
        _lr.enabled = false;
    }
}
