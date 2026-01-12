using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ObjectToggleWindow : EditorWindow
{
    private List<GameObject> objects = new List<GameObject>();
    private Vector2 scroll;

    [MenuItem("Window/ToolsG/Toggle Objects")]
    public static void OpenWindow()
    {
        GetWindow<ObjectToggleWindow>("Toggle Objects");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Objetos de la escena", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // Botón para agregar un slot nuevo
        if (GUILayout.Button("+ Agregar objeto"))
        {
            objects.Add(null);
        }

        EditorGUILayout.Space();

        scroll = EditorGUILayout.BeginScrollView(scroll);

        for (int i = 0; i < objects.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            objects[i] = (GameObject)EditorGUILayout.ObjectField(objects[i], typeof(GameObject), true);

            if (objects[i] != null)
            {
                bool isActive = objects[i].activeSelf;
                bool newState = EditorGUILayout.Toggle(isActive, GUILayout.Width(20));

                if (newState != isActive)
                {
                    Undo.RecordObject(objects[i], "Toggle GameObject");
                    objects[i].SetActive(newState);
                    EditorUtility.SetDirty(objects[i]);
                }
            }

            // Botón para quitar el slot
            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                objects.RemoveAt(i);
                i--;
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Encender todos"))
        {
            foreach (var obj in objects)
            {
                if (obj != null)
                    obj.SetActive(true);
            }
        }

        if (GUILayout.Button("Apagar todos"))
        {
            foreach (var obj in objects)
            {
                if (obj != null)
                    obj.SetActive(false);
            }
        }

        EditorGUILayout.EndHorizontal();
    }
}
