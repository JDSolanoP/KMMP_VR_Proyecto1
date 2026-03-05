using UnityEngine;

public class DetectorManoEnPolea : MonoBehaviour
{
    [SerializeField] private GameObject LeftHandPalm;
    [SerializeField] private GameObject LeftHandMesh;
    [SerializeField] private SkinnedMeshRenderer RealLeftHandSkinnedMeshRenderer;
    [SerializeField] private Material InvisibleMaterial;
    [SerializeField] private MeshRenderer LeftHandMeshRenderer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LeftHandPalm)
        {
            LeftHandMesh.SetActive(true);
            LeftHandMeshRenderer.enabled = false;
            RealLeftHandSkinnedMeshRenderer.material = InvisibleMaterial;
        }
    }
}
