using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WaypointAI : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 22f;
    public float steerSpeed = 90f;
    public float waypointRadius = 6f;

    int idx = 0;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (waypoints == null || waypoints.Length == 0)
            Debug.LogWarning("Waypoints not set on WaypointAI.");
    }

    void FixedUpdate()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        Transform target = waypoints[idx];
        Vector3 toTarget = (target.position - transform.position);
        Vector3 dir = toTarget.normalized;

        // Move forward and steer towards target
        Vector3 desiredVel = transform.forward * speed;
        Vector3 vel = rb.velocity;
        Vector3 force = (desiredVel - vel) * 2f; // dampened acceleration
        rb.AddForce(force, ForceMode.Acceleration);

        // Simple steering
        float angle = Vector3.SignedAngle(transform.forward, dir, Vector3.up);
        float turn = Mathf.Clamp(angle, -steerSpeed * Time.fixedDeltaTime, steerSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));

        if (toTarget.magnitude < waypointRadius) idx = (idx + 1) % waypoints.Length;
    }
}
