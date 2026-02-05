using UnityEngine;

public class DetectorTapaAbierta : MonoBehaviour
{
    public GameObject Button3DInspeccionar;
    public GameObject Button3DGuardar;
    public GameObject Refe;//19.01
    public bool si_tapa_abierta=false;

    [SerializeField] private Collider TapaCollider;

    private bool _buttonWasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        //if (_buttonWasActivated) return;
        if (other == TapaCollider) {
            Button3DInspeccionar.SetActive(true);

            if (si_tapa_abierta == false)
            {
                //Debug.Log(si_tapa_abierta + ":tapa abierta desde collider- tapa refe:" + Refe.activeInHierarchy);
                Refe.SetActive(false);
                Debug.Log(si_tapa_abierta + ":tapa abierta desde collider- tapa refe:" + Refe.activeInHierarchy);
                si_tapa_abierta = true;
            }
        }
        Debug.Log("Tapa_refe=" + Refe.activeInHierarchy);
        //si_tapa_abierta = true;

    }

    private void OnTriggerExit(Collider other)
    {
        //if (_buttonWasActivated) return;
        if (other == TapaCollider) Button3DInspeccionar.SetActive(false);
        //if (other == TapaCollider) Refe.SetActive(true);
    }

    public void SetButtonWasActivated(bool state) => _buttonWasActivated = state;
}
