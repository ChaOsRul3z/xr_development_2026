using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DragRaceController))]
public class DragRaceUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DragRaceController controller = (DragRaceController)target;
        GateControls gateControls = controller.Gate.GetComponent<GateControls>();

        if (GUILayout.Button("Reset"))
        {
            controller.Reset();
        }

        if (GUILayout.Button("Lower Gate"))
        {
            gateControls?.Lower();
        }
    }
}
