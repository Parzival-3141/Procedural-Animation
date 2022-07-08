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

        //  Credits to kybernetik
        //  https://kybernetik.com.au/cs-unity/docs/component-referencing/extension-methods
        /// <summary>
        /// Used to automatically cache component references.
        /// <para>Particularly useful in <c>MonoBehaviour.OnValidate()</c>, since it improves runtime performance.</para>
        /// </summary>
        public static void GetComponent<T>(this GameObject gameObject, ref T component) where T : class
        {
            if (component == null)
                component = gameObject.GetComponent<T>();
        }
    }
}
