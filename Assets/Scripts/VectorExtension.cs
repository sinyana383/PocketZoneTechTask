using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{
    // There the Vector3 class extends
    public static Vector3 WithAxis(this Vector3 vector, Axis axis, float value)
    {
        return new Vector3(
            axis == Axis.X ? value : vector.x,
            axis == Axis.Y ? value : vector.y,
            axis == Axis.Z? value : vector.z
            );
    }
}

public enum Axis
{
    X, Y, Z
}
