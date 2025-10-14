using UnityEngine;

public class PhotoController : MonoBehaviour
{
    [SerializeField] private GameObject photoNode = null;
    [SerializeField] private GameObject XRPlayer = null;

    private void Start()
    {
        photoNode.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == XRPlayer.name)
        {
            photoNode.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == XRPlayer.name)
        {
            photoNode.SetActive(false);
        }
    }
}