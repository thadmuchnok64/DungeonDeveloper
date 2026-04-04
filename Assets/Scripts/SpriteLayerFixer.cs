using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Layerfix))]
public class SpriteLayerFixer : Editor
{
    public override void OnInspectorGUI() {
        EditorGUILayout.LabelField("pizza");
        Layerfix lay = (Layerfix)target;
        if (GUILayout.Button("Fix Layers")) {
            lay.dothegoddamnthing();
        }
    }
}
