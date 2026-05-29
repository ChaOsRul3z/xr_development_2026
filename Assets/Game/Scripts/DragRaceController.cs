using UnityEngine;

public class DragRaceController : MonoBehaviour
{
    [SerializeField] GameObject hollow;
    [SerializeField] GameObject solid;
    [SerializeField] GameObject gate;
    [SerializeField] GameObject slope;

    Transform hollowStartPos;
    Transform solidStartPos;
    Transform gateStartPos;

    public GameObject Gate => gate;

    public void Reset()
    {
        hollow.transform.localRotation = hollowStartPos.localRotation;
        hollow.transform.localPosition = hollowStartPos.localPosition;
        solid.transform.localPosition = solidStartPos.localPosition;
        solid.transform.localRotation = solidStartPos.localRotation;
        gate.transform.localPosition = gateStartPos.localPosition;
        gate.transform.localRotation = gateStartPos.localRotation;
    }
}
