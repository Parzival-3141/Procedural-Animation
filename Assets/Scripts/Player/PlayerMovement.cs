using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Rosen
{
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

        private void OnValidate()
        {
            gameObject.GetComponent(ref rb);
        }

        private void Update()
        { 
            DevGUI.Label($"Velocity: {rb.velocity} ({rb.velocity.magnitude})", "Movement");
            DevGUI.Label($"Acceleration: {HorizontalAcceleration()} ({HorizontalAcceleration().magnitude})", "Movement");
        }

        private void FixedUpdate()
        {
            var moveDir = GetMoveDirection();

            float speed = Input.GetKey(KeyCode.LeftShift) ? maxSpeed * 0.25f : maxSpeed;

            var decel = -deceleration * HorizontalVelocity();
            var moveSpeed = (speed - HorizontalVelocity().magnitude) * acceleration + decel.magnitude; //  cancel out deceleration while moving

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
       
    }
}

