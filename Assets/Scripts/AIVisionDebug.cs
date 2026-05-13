using UnityEngine;

public class AIVisionDebug : MonoBehaviour
{
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private float viewAngle = 30f;

    private void Update()
    {
        Vector3 origin = transform.position + Vector3.up * 1.5f;
        float halfAngle = viewAngle * 0.5f;

        Vector3 forward = transform.forward;
        Vector3 left = Quaternion.Euler(0f, -halfAngle, 0f) * forward;
        Vector3 right = Quaternion.Euler(0f, halfAngle, 0f) * forward;

        Debug.DrawRay(origin, forward * viewDistance, Color.yellow);
        Debug.DrawRay(origin, left * viewDistance, Color.yellow);
        Debug.DrawRay(origin, right * viewDistance, Color.yellow);
    }
}