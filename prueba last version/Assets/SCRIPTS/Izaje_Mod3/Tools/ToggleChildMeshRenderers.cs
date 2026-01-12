using UnityEngine;

public class ToggleChildMeshRenderers : MonoBehaviour
{
    public void EnableAll()
    {
        SetAll(true);
    }

    public void DisableAll()
    {
        SetAll(false);
    }

    private void SetAll(bool value)
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>(true);

        foreach (var renderer in renderers)
        {
            renderer.enabled = value;
        }
    }
}