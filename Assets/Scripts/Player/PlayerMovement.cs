using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTarget;

    public float
        acceleration,
        deceleration,
        turningAccel,
        maxSpeed;

    private Rigidbody rb;
    private Vector3 lastHorzVel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var moveDir = GetMoveDirection();

        var decel = -deceleration * HorizontalVelocity();
        var moveSpeed = (maxSpeed - HorizontalVelocity().magnitude) * acceleration + decel.magnitude; //  cancel out deceleration while moving

        rb.AddForce(moveDir * moveSpeed, ForceMode.Acceleration);
        rb.AddForce(decel, ForceMode.Acceleration);
        lastHorzVel = HorizontalVelocity();
    }

    public Vector3 GetMoveDirection()
    {
        var normal = Vector3.up;
        Vector3 forward = Vector3.ProjectOnPlane(cameraTarget.forward, normal);
        Vector3 right = Vector3.ProjectOnPlane(cameraTarget.right, normal);

        return (Input.GetAxisRaw("Horizontal") * right + Input.GetAxisRaw("Vertical") * forward).normalized;
    }


    public Vector3 HorizontalVelocity()
    {
        return new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }

    public Vector3 HorizontalAcceleration()
    {
        return HorizontalVelocity() - lastHorzVel;
    }

    public float VerticalVelocity() => rb.velocity.y;

    private void OnGUI()
    {
        GUILayout.BeginVertical("box");
        GUILayout.Label("----- Movement -----");
        GUILayout.Label($"Velocity: {rb.velocity} ({rb.velocity.magnitude})");

        GUILayout.Label($"Acceleration: {HorizontalAcceleration()} ({HorizontalAcceleration().magnitude})");
        GUILayout.Label($"Tilt: {Vector3.Cross(-HorizontalAcceleration(), Vector3.up)}");
        GUILayout.EndVertical();
    }
}
