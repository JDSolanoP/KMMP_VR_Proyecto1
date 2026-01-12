using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ToggleChildMeshRenderers))]
public class ToggleChildMeshRenderersEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ToggleChildMeshRenderers tool = (ToggleChildMeshRenderers)target;

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Encender todos los MeshRenderer")) tool.EnableAll();

        if (GUILayout.Button("Apagar todos los MeshRenderer")) tool.DisableAll();

        GUILayout.EndHorizontal();
    }
}