using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRender : MonoBehaviour
{
    [SerializeField] private ProjectileConfig config;
    [SerializeField] private Transform origin;
    [SerializeField] private int maxPoints = 64;
    [SerializeField] private float maxDistance = 200f;

    private LineRenderer _lr;
    private IRicochetSolver _ricochetSolver;
    private RicochetData _richochetData;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        _ricochetSolver = new RicochetSolver();
        _richochetData = new RicochetData();
    }

    public void Show(Vector3 startPos, Vector3 direction)
    {
        if (config == null)
            return;

        _richochetData.UpdateValues(startPos, direction, maxDistance,
            config.maxBounces, config.hitMask, config.speed,
            config.useGravity ? config.gravity : Vector3.zero,
            config.useGravity, config.restitution, maxPoints);

        var points = _ricochetSolver.CalculatePath(_richochetData);
            
        _lr.positionCount = points.Count;
        _lr.SetPositions(points.ToArray());
        _lr.enabled = true;
    }

    public void Hide()
    {
        _lr.enabled = false;
    }
}
