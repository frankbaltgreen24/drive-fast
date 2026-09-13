using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform[] checkpoints;
    public Transform player;
    int lastCheckpoint = 0;

    void Start()
    {
        if (player == null) Debug.LogError("RespawnManager: player not assigned.");
    }

    public void SetCheckpoint(int idx)
    {
        if (checkpoints == null || checkpoints.Length == 0) return;
        lastCheckpoint = Mathf.Clamp(idx, 0, checkpoints.Length - 1);
    }

    public void Respawn()
    {
        if (player == null || checkpoints == null || checkpoints.Length == 0) return;
        Transform cp = checkpoints[lastCheckpoint];
        Rigidbody prb = player.GetComponent<Rigidbody>();
        player.position = cp.position + Vector3.up * 1.2f;
        player.rotation = cp.rotation;
        if (prb != null)
        {
            prb.velocity = Vector3.zero;
            prb.angularVelocity = Vector3.zero;
        }

        // reset wanted if player is respawned after capture
        var ws = player.GetComponent<WantedSystem>();
        if (ws != null) ws.ResetWanted();
    }
}
