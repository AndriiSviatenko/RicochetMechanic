using System.Collections.Generic;
using UnityEngine;

public class RicochetSolver2D : IRicochetSolver2D
{
    public List<Vector2> CalculatePath(RicochetData ricochetData)
    {
        List<Vector2> points = new List<Vector2> { ricochetData.Origin };
        Vector2 pos = ricochetData.Origin;
        Vector2 vel = ricochetData.Direction.normalized * ricochetData.InitialSpeed;

        var step = Time.fixedDeltaTime;
        int bounce = 0;

        while (points.Count < ricochetData.MaxSegments && bounce <= ricochetData.MaxBounces)
        {
            Vector2 next = pos + vel * step;

            RaycastHit2D hit = Physics2D.Linecast(pos, next, ricochetData.HitMask);
            if (hit.collider != null)
            {
                points.Add(hit.point);
                bounce++;

                if (bounce > ricochetData.MaxBounces)
                    break;

                vel = Vector2.Reflect(vel, hit.normal) * ricochetData.Restitution;
                pos = hit.point + hit.normal * 0.1f;
            }
            else
            {
                pos = next;
            }

            points.Add(pos);
        }

        return points;
    }
}
