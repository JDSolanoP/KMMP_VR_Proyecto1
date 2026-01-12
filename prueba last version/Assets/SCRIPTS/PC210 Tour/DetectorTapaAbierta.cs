using UnityEngine;

public class DetectorTapaAbierta : MonoBehaviour
{
    public GameObject Button3DInspeccionar;

    [SerializeField] private Collider TapaCollider;

    private bool _buttonWasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_buttonWasActivated) return;
        if (other == TapaCollider) Button3DInspeccionar.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_buttonWasActivated) return;
        if (other == TapaCollider) Button3DInspeccionar.SetActive(false);
    }

    public void SetButtonWasActivated(bool state) => _buttonWasActivated = state;
}
