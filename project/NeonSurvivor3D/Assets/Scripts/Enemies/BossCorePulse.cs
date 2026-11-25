
using UnityEngine;

public class BossCorePulse : MonoBehaviour
{
    public float pulseSpeed = 2f;
    public float pulseAmount = 0.12f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = originalScale * pulse;
    }
}