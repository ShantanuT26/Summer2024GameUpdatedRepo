using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DoorTrigger))]
public class LabelHandler : Editor
{
    private static GUIStyle labelStyle;

    private void OnEnable()
    {
        labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.MiddleCenter;
    }

    private void OnSceneGUI()
    {
        DoorTrigger door = (DoorTrigger)target;

        Handles.BeginGUI();
        Handles.Label(door.transform.position + new Vector3(0f, 4f, 0f), door.currentDoorPosition.ToString(), labelStyle);
        Handles.EndGUI();
    }
}
