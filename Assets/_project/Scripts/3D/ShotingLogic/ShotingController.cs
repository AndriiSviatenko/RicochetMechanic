using UnityEngine;

public class ShotingController : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private TrajectoryRender trajectoryRender;
    [SerializeField] private Transform firePoint;

    private void Update()
    {
        Vector3 shootDir = firePoint.forward;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon.Fire(shootDir);
            trajectoryRender.Hide();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            trajectoryRender.Show(firePoint.position, shootDir);
        }
    }
}
