using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActivityCard))]
[CanEditMultipleObjects]
public class ActivityCardInspector : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Editor"))
        {
            CustomEditorOpener.OpenActivityCardEditor();
        }
    }
}
