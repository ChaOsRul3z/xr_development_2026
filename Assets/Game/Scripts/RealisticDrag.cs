using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RealisticDrag : MonoBehaviour
{
    [Header("Object Properties")]
    [Tooltip("0.47 for sphere, 1.05 for cube")]
    public float dragCoefficient = 0.47f; 
    [Tooltip("Cross-sectional area in square meters")]
    public float crossSectionalArea = 0.1f; 

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Disable Unity's built-in drag to use our math
        rb.linearDamping = 0;
        rb.angularDamping = 0.05f;
    }

    void FixedUpdate()
    {
        // If the manager doesn't exist or we are in a vacuum, do nothing
        if (DynamicsManager.Instance == null || DynamicsManager.Instance.isVacuum) return;

        // Formula: Fd = 1/2 * rho * v^2 * Cd * A
        float rho = DynamicsManager.Instance.airDensity;
        float v = rb.linearVelocity.magnitude;
        float Cd = dragCoefficient;
        float A = crossSectionalArea;

        if (v > 0.01f) 
        {
            float dragMagnitude = 0.5f * rho * (v * v) * Cd * A;
            Vector3 dragForce = -rb.linearVelocity.normalized * dragMagnitude;
            
            rb.AddForce(dragForce);
        }
    }
}
