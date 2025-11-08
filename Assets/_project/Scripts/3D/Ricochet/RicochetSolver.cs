using System.Collections.Generic;
using UnityEngine;

public class RicochetSolver : IRicochetSolver
{
    public List<Vector3> CalculatePath(RicochetData ricochetData)
    {
        var points = new List<Vector3>() { ricochetData.Origin };

        Vector3 pos = ricochetData.Origin;
        Vector3 dir = ricochetData.Direction.normalized;
        Vector3 velocity = dir * ricochetData.InitialSpeed;

        float distanceLeft = ricochetData.MaxDistance;
        int bounces = 0;

        for (int seg = 0; seg < ricochetData.MaxSegments && distanceLeft > 0f; seg++)
        {
            var timeStep = Time.fixedDeltaTime;
            Vector3 nextPos = pos + velocity * timeStep;

            if (ricochetData.UseGravity)
            {
                velocity += ricochetData.Gravity * timeStep;
                nextPos = pos + velocity * timeStep;
            }

            float segmentDistance = Vector3.Distance(pos, nextPos);

            if (Physics.Raycast(pos, dir, out RaycastHit hit, segmentDistance, ricochetData.HitMask))
            {
                points.Add(hit.point);
                bounces++;

                if (bounces > ricochetData.MaxBounces)
                {
                    break;
                }

                dir = Vector3.Reflect(dir, hit.normal).normalized;
                velocity = Vector3.Reflect(velocity, hit.normal) * ricochetData.Restitution;

                pos = hit.point + dir * 0.01f;
                distanceLeft -= hit.distance;
            }
            else
            {
                pos = nextPos;
                points.Add(pos);
                distanceLeft -= segmentDistance;

                if (velocity.magnitude > 0.01f)
                    dir = velocity.normalized;
            }
        }

        return points;
    }
}