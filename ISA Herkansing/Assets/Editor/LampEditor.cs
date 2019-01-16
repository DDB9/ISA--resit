using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LampEditor : EditorWindow
{
    bool spotLight = false;

    [MenuItem("Window/Lamp Editor")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LampEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Create a lamp", EditorStyles.boldLabel);

        GUILayout.Space(10f);

        GUILayout.Label("Lamp type:");
        spotLight = EditorGUILayout.Toggle("Spot Light", spotLight);

        GUILayout.Space(10f);

        if (GUILayout.Button("Create Lamp"))
        {
            GameObject go = new GameObject("New Light");
            Light lt = go.AddComponent<Light>();
            if (spotLight) lt.type = LightType.Spot;

            if (GameObject.Find("Lights") != null) lt.transform.parent = GameObject.Find("Lights").transform;
        }
    }
}
