using System.Collections.Generic;
using UnityEngine;

public interface IRicochetSolver2D
{
    List<Vector2> CalculatePath(RicochetData ricochetData);
}

