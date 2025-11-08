using UnityEngine;

public class ShootingController2D : MonoBehaviour
{
    [SerializeField] private Weapon2D weapon;
    [SerializeField] private TrajectoryRender2D trajectoryRender;
    [SerializeField] private Transform aimTransform;

    private void Update()
    {
        Vector2 shootDir = aimTransform.right;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon.Fire(shootDir);
            trajectoryRender.Hide();
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            trajectoryRender.Show(aimTransform.position, shootDir);
        }
    }
}
