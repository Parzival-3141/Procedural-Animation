using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Animancer.Units;

public class SurveyorWheel : MonoBehaviour
{
    //  stride = distance btwn stepN and stepN + 2
    //  step = distance between footsteps
    public float StrideLength { get; set; } = 2.1f;

    //  Circumference == distance per revolution
    //  if each stride == 1/2 revolution
    //  then circumference == strideLength * 2
    public float Circumference => StrideLength * 2f;
    public float Radius => Circumference / (Mathf.PI * 2f);

    [SerializeField] private Transform wheelCenter;

    public void Rotate(float angle)
    {
#pragma warning disable CS0618 // Type or member is obsolete
        wheelCenter.RotateAround(wheelCenter.right, angle * Mathf.Deg2Rad / 2f);
#pragma warning restore CS0618 // Type or member is obsolete
    }

    public float DistanceTravelled(float angle)
    {
        return Circumference * (angle / 360f);
    }

    public float AngleFromDistanceTravelled(float distance)
    {
        return (distance * 360) / Circumference;
    }

    #region Visuals
    private Vector3 PointOnDiscEdge(Vector3 normal) => PointOnDisc(normal, Radius);
    private Vector3 PointOnDisc(Vector3 normal, float distance) 
    { 
        return wheelCenter.position + normal * distance; 
    }

    private void OnDrawGizmos()
    {
        if (!wheelCenter) return;

        wheelCenter.localPosition = Vector3.up * Radius;

        Handles.DrawWireDisc(wheelCenter.position, wheelCenter.right, Radius, 0.5f);

        void DrawOrthoLine(Vector3 normal) => Handles.DrawLine(PointOnDiscEdge(normal), PointOnDiscEdge(-normal), 1.5f);

        DrawOrthoLine(wheelCenter.up);
        DrawOrthoLine(wheelCenter.forward);

        var diagUpRight   = (wheelCenter.up  + wheelCenter.forward).normalized;
        var diagUpLeft    = (wheelCenter.up  - wheelCenter.forward).normalized;
        var diagDownRight = (-wheelCenter.up + wheelCenter.forward).normalized;
        var diagDownLeft  = (-wheelCenter.up - wheelCenter.forward).normalized;

        void DrawDiagonalLine(Vector3 normal) => Handles.DrawLine(PointOnDiscEdge(normal), PointOnDiscEdge(normal * 0.5f), 1.5f);

        DrawDiagonalLine(diagUpRight);
        DrawDiagonalLine(diagUpLeft);
        DrawDiagonalLine(diagDownRight);
        DrawDiagonalLine(diagDownLeft);


        Handles.color = Color.red;
        Handles.DrawLine(transform.position, transform.position - transform.up * 0.25f, 3f);

    }
    #endregion
}