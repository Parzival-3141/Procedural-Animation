using UnityEngine;

public class SpinnyTest : MonoBehaviour
{
    public Vector3 velocity;
    public float circumference = 1f;
    public Rigidbody rb;
    public float error;

    private void Start()
    {
        rb.maxAngularVelocity = 9999f;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position + Vector3.up * Radius(), velocity, Color.blue);
        Debug.DrawRay(transform.position + Vector3.up * Radius(), AngularVelocityRadians(velocity), Color.red);

        if (rb.position.magnitude > 50f)
            rb.position = Vector3.up * rb.position.y;

        transform.localScale = Vector3.one * Radius() * 2f;
    }

    private void FixedUpdate()
    {
        rb.angularVelocity = AngularVelocityRadians(velocity);
        error = (rb.velocity - velocity).magnitude;
    }


    public float Radius() => circumference / (2 * Mathf.PI);

    public Vector3 AngularVelocityRadians(Vector3 linearVelocity)
    {
        var length = /*Mathf.Rad2Deg **/ (linearVelocity.magnitude / Radius());
        var axis = Vector3.Cross(Vector3.up, linearVelocity.normalized);
        return axis * length;
    }

    public Vector3 AngularVelocityDegrees(Vector3 linearVelocity)
    {
        var length = Mathf.Rad2Deg * (linearVelocity.magnitude / Radius());
        var axis = Vector3.Cross(Vector3.up, linearVelocity.normalized);
        return axis * length;
    }
}
