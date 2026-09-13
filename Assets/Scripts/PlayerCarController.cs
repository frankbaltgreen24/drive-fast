using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCarController : MonoBehaviour
{
    [Header("Performance")]
    public float maxSpeed = 50f;           // meters per second (~180 km/h)
    public float acceleration = 80f;       // force multiplier
    public float brakeForce = 160f;
    public float turnSpeed = 120f;         // degrees per second at low speed
    public float grip = 0.98f;             // lateral velocity multiplier (0..1) - higher = grippier
    public float handbrakeGrip = 0.6f;     // grip factor while handbrake
    public float nitroBoost = 1.5f;        // speed multiplier while nitro active
    public float nitroDuration = 2.5f;
    public float nitroCooldown = 6f;

    Rigidbody rb;
    float nitroTimer = 0f;
    float nitroCdTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.down * 0.5f; // lower center for stability
        rb.mass = 1400f;
        rb.drag = 0.1f;
        rb.angularDrag = 0.5f;
    }

    void Update()
    {
        // Nitro input
        if (Input.GetKeyDown(KeyCode.LeftShift) && nitroCdTimer <= 0f)
        {
            nitroTimer = nitroDuration;
            nitroCdTimer = nitroCooldown + nitroDuration;
        }

        if (nitroCdTimer > 0f) nitroCdTimer -= Time.deltaTime;
        if (nitroTimer > 0f) nitroTimer -= Time.deltaTime;

        // Respawn
        if (Input.GetKeyDown(KeyCode.R))
        {
            var mgr = FindObjectOfType<RespawnManager>();
            if (mgr != null) mgr.Respawn();
        }
    }

    void FixedUpdate()
    {
        float throttleInput = Input.GetAxis("Vertical");   // W/S or gamepad RT/ LT
        float steerInput = Input.GetAxis("Horizontal");    // A/D or gamepad left stick
        bool handbrake = Input.GetKey(KeyCode.Space);

        float throttle = Mathf.Clamp01(throttleInput);
        float brake = Mathf.Clamp01(-throttleInput);

        // Nitro modifies max speed
        float currentMax = maxSpeed * (nitroTimer > 0f ? nitroBoost : 1f);

        // Forward force
        Vector3 forwardVel = Vector3.Project(rb.velocity, transform.forward);
        float forwardSpeed = forwardVel.magnitude;
        if (throttle > 0.01f)
        {
            if (forwardSpeed < currentMax)
                rb.AddForce(transform.forward * throttle * acceleration, ForceMode.Acceleration);
        }
        else if (brake > 0.01f)
        {
            rb.AddForce(-transform.forward * brake * brakeForce, ForceMode.Acceleration);
        }
        else
        {
            // Natural drag
            rb.velocity *= 0.999f;
        }

        // Clamp top speed
        if (rb.velocity.magnitude > currentMax)
            rb.velocity = rb.velocity.normalized * currentMax;

        // Steering scales down at high speed for stability
        float steerEffect = Mathf.Lerp(turnSpeed, turnSpeed * 0.35f, rb.velocity.magnitude / currentMax);
        float turn = steerInput * steerEffect * Time.fixedDeltaTime;
        if (rb.velocity.magnitude > 0.5f)
        {
            Quaternion deltaRot = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * deltaRot);
        }

        // Grip-first lateral damping (keeps car planted unless player intentionally drifts)
        Vector3 localVel = transform.InverseTransformDirection(rb.velocity);
        float targetGrip = handbrake ? handbrakeGrip : grip;
        localVel.x *= targetGrip;
        rb.velocity = transform.TransformDirection(localVel);
    }
}
