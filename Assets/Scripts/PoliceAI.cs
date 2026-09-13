using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PoliceAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;
    public float patrolSpeed = 14f;
    public float pursuitSpeed = 30f;
    public float detectionRadius = 45f;
    public float chaseLoseDistance = 100f;
    public float fieldOfView = 110f; // degrees
    public float engageTimeToWant = 0.8f; // seconds in sight to increase wanted

    Rigidbody rb;
    int idx = 0;
    enum State { Patrol, Spotting, Pursuit, Returning }
    State state = State.Patrol;
    float spottingTimer = 0f;

    WantedSystem playerWanted;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (player == null) Debug.LogError("PoliceAI: player not assigned.");
        playerWanted = player.GetComponent<WantedSystem>();
    }

    void FixedUpdate()
    {
        if (player == null) return;
        float dist = Vector3.Distance(transform.position, player.position);
        bool canSee = CanSeePlayer();

        switch (state)
        {
            case State.Patrol:
                PatrolStep();
                if (canSee && dist <= detectionRadius)
                {
                    state = State.Spotting;
                    spottingTimer = 0f;
                }
                break;
            case State.Spotting:
                PatrolStep();
                if (canSee && dist <= detectionRadius)
                {
                    spottingTimer += Time.fixedDeltaTime;
                    if (spottingTimer >= engageTimeToWant)
                    {
                        // begin pursuit
                        playerWanted?.IncreaseWanted(1);
                        state = State.Pursuit;
                    }
                }
                else
                {
                    state = State.Patrol;
                }
                break;
            case State.Pursuit:
                PursueStep();
                if (dist > chaseLoseDistance)
                {
                    state = State.Returning;
                }
                break;
            case State.Returning:
                // go back to nearest patrol point
                ReturnToPatrol();
                if (canSee && dist <= detectionRadius)
                {
                    state = State.Pursuit;
                }
                break;
        }
    }

    void PatrolStep()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;
        Transform target = patrolPoints[idx];
        MoveTowards(target.position, patrolSpeed);
        if (Vector3.Distance(transform.position, target.position) < 6f) idx = (idx + 1) % patrolPoints.Length;
    }

    void PursueStep()
    {
        MoveTowards(player.position, pursuitSpeed);
    }

    void ReturnToPatrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;
        Transform target = patrolPoints[idx];
        MoveTowards(target.position, patrolSpeed);
        if (Vector3.Distance(transform.position, target.position) < 6f) state = State.Patrol;
    }

    void MoveTowards(Vector3 targetPos, float moveSpeed)
    {
        Vector3 dir = (targetPos - transform.position).normalized;
        Vector3 desired = transform.forward * moveSpeed;
        Vector3 force = (desired - rb.velocity) * 2f;
        rb.AddForce(force, ForceMode.Acceleration);

        float angle = Vector3.SignedAngle(transform.forward, dir, Vector3.up);
        float turn = Mathf.Clamp(angle, -160f * Time.fixedDeltaTime, 160f * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));
    }

    bool CanSeePlayer()
    {
        Vector3 toPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, toPlayer);
        if (angle > fieldOfView * 0.5f) return false;
        Ray ray = new Ray(transform.position + Vector3.up * 1.2f, toPlayer.normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, detectionRadius))
        {
            if (hit.transform == player || hit.transform.IsChildOf(player))
                return true;
        }
        return false;
    }
}
