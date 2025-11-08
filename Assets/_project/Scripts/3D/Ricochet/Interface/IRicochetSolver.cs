using System.Collections.Generic;
using UnityEngine;

public interface IRicochetSolver
{
    List<Vector3> CalculatePath(RicochetData ricochetData);
}

