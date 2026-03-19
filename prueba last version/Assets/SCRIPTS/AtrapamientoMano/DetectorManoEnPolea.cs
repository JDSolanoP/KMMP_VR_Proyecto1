using UnityEngine;

public class DetectorManoEnPolea : MonoBehaviour
{
    [SerializeField] public GameObject ManoIzq_ReferenciaMalPuesta;
    [SerializeField] public GameObject LeftHandPalm;
    [SerializeField] public GameObject LeftHandMesh;
    [SerializeField] public SkinnedMeshRenderer RealLeftHandSkinnedMeshRenderer;
    [SerializeField] public MeshRenderer LeftHandMeshRenderer;

    [Header("Materials")]
    [SerializeField] public Material InvisibleMaterial;
    [SerializeField] public Material GuantesMaterial;

    public bool IsHandOnPolea = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LeftHandPalm)
        {
            LeftHandMesh.SetActive(true);
            LeftHandMeshRenderer.enabled = false;
            RealLeftHandSkinnedMeshRenderer.material = InvisibleMaterial;
            IsHandOnPolea = true;
        }
    }
}