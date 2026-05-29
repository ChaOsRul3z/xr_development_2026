using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotationalInertiaSetter : MonoBehaviour
{
    public enum Distribution { Solid, Hollow }
    public Distribution massDistribution;
    public float radius = 0.5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CalculateInertia();
    }

    public void CalculateInertia()
    {
        float m = rb.mass;
        float r = radius;
        Vector3 tensor = Vector3.one;

        if (massDistribution == Distribution.Solid)
        {
            // Formula for Solid Cylinder: I = 1/2 * m * r^2
            float I = 0.5f * m * (r * r);
            tensor = new Vector3(I, I, I); 
        }
        else
        {
            // Formula for Hollow Cylinder: I = m * r^2
            float I = m * (r * r);
            tensor = new Vector3(I, I, I);
        }

        rb.inertiaTensor = tensor;
        rb.inertiaTensorRotation = Quaternion.identity;
        
        Debug.Log($"{gameObject.name} Inertia set to {massDistribution} (I={tensor.x})");
    }
}
