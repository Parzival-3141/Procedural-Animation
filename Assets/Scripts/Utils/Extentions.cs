using System;
using UnityEngine;

namespace Utils
{
    public static class Extentions
    {
        public static void RotateAround(this Transform t, Vector3 worldPoint, Quaternion rotation)
        {
            t.position = rotation * (t.position - worldPoint) + worldPoint;
            t.rotation *= rotation;
        }

        public static void RotateAround(this Transform t, Vector3 worldPoint, Vector3 axis, float angle, out Quaternion rotation)
        {
            rotation = Quaternion.AngleAxis(angle, axis);
            t.RotateAround(worldPoint, rotation);
        }
    }
}
