using UnityEngine;
using Utils;

namespace Rosen
{
    public class AnimationControl : MonoBehaviour
    {
        public PlayerMovement pMovement;
        public Transform model;
        public Transform tilter;
        [Tooltip("Center of Mass")] public Transform COM;

        public float velocityTurnMaxDelta = 4f;
        public float accelTiltMult = 2f;
        public float accelTiltSpeed = 2f;
    

        private void Update()
        {
            Vector3 horizontalVelocity = pMovement.HorizontalVelocity();
            Vector3 horizontalAccel = pMovement.HorizontalAcceleration();
            Vector3 accelRotAxis = Vector3.Cross(-horizontalAccel.normalized, Vector3.up);

            Debug.DrawRay(model.position + Vector3.up * 1.5f, horizontalVelocity, Color.blue);
            Debug.DrawRay(model.position + Vector3.up * 1.4f, horizontalAccel, Color.red);
            Debug.DrawRay(model.position + Vector3.up * 1.3f, accelRotAxis, Color.magenta);
            Debug.DrawRay(model.position + Vector3.up * 1.2f, GetFacing(), Color.green);

            FaceVelocity(horizontalVelocity);
            AccelerationTilt(horizontalAccel, accelRotAxis);
        }

        public Vector3 GetFacing()
        {
            return Vector3.ProjectOnPlane(model.forward, Vector3.up).normalized;
        }

        private void FaceVelocity(Vector3 horizontalVelocity)
        {
            float velocityAngle = horizontalVelocity.magnitude >= 0.001f
                ? Vector3.SignedAngle(GetFacing(), horizontalVelocity.normalized, Vector3.up)
                : 0f;

            var velocityRot = Quaternion.AngleAxis(Mathf.Clamp(velocityAngle, -velocityTurnMaxDelta, velocityTurnMaxDelta), Vector3.up);
            model.localRotation *= velocityRot;

            DevGUI.Label($"FaceVelocityRot: {velocityRot.eulerAngles} ({velocityAngle})", "Animation");
        }

        private void AccelerationTilt(Vector3 horizontalAccel, Vector3 accelRotAxis)
        {
            //@Note: Perhaps this can be abstracted into a generic tilt, so anything can tilt the character additively.
            //  Tilt into acceleration
            var accelTiltRot = Quaternion.AngleAxis(horizontalAccel.magnitude * accelTiltMult, accelRotAxis);
            tilter.rotation = Quaternion.Lerp(tilter.rotation, accelTiltRot, accelTiltSpeed * Time.deltaTime);

            //  Rotate around COM
            var rotatedCOMDelta = tilter.up * (COM.position - tilter.position).magnitude;
            tilter.position += (COM.position - tilter.position) - rotatedCOMDelta;

            DevGUI.Label($"AccelTiltRot: {accelTiltRot.eulerAngles} ({horizontalAccel.magnitude * accelTiltMult})", "Animation");
        }
    }
}
