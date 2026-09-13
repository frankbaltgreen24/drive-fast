using UnityEngine;

public class WantedSystem : MonoBehaviour
{
    public int wantedLevel = 0;            // 0 = no pursuit, higher = more police aggression
    public float wantedDecayRate = 0.25f;  // points per second to decay when not being chased
    public int maxWanted = 5;

    float lastIncreaseTime = 0f;

    void Update()
    {
        if (Time.time - lastIncreaseTime > 1f && wantedLevel > 0)
        {
            // decay slowly
            wantedLevel = Mathf.Max(0, wantedLevel - Mathf.FloorToInt(wantedDecayRate * Time.deltaTime));
        }
    }

    public void IncreaseWanted(int amount = 1)
    {
        wantedLevel = Mathf.Clamp(wantedLevel + amount, 0, maxWanted);
        lastIncreaseTime = Time.time;
    }

    public void ResetWanted()
    {
        wantedLevel = 0;
    }
}
