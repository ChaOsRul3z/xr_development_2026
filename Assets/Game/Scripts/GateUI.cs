using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GateControls))]
public class GateUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GateControls controller = (GateControls)target;

        if (GUILayout.Button("Lower Gate"))
        {
            controller.Lower();
        }
    }
}
