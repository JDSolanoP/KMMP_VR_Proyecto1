using Oculus.Interaction;
using System.Collections;
using UnityEngine;

public class DetectorPistolaNeumaticaEnCaja : MonoBehaviour
{
    [Header("TareaManager")]
    [SerializeField] private TM_AtrapamientoMano TM_AtrapamientoMano;
    [SerializeField] private int NumeroDeTareaPorCompletar;

    [Header("Pistola Neumatica")]
    [SerializeField] private GameObject PistolaNeumaticaPorGuardar;
    [SerializeField] private Transform LugarDePistolaGuardada;
    [SerializeField] private GameObject PistolaNeumaticaGuardadaEnCajaVisual;

    private Grabbable _pistolaNeumaticaGrabbable;
    private Transform _pistolaNeumaticaTransform;

    private void Awake()
    {
        _pistolaNeumaticaGrabbable = PistolaNeumaticaPorGuardar.GetComponent<Grabbable>();
        _pistolaNeumaticaTransform = PistolaNeumaticaPorGuardar.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PistolaNeumaticaPorGuardar)
        {
            StartCoroutine(GuardarPistolaEnCaja());
        }
    }

    private IEnumerator GuardarPistolaEnCaja()
    {
        if (_pistolaNeumaticaGrabbable != null) _pistolaNeumaticaGrabbable.enabled = false;
        yield return new WaitForSeconds(0.1f);
        _pistolaNeumaticaTransform.SetPositionAndRotation(LugarDePistolaGuardada.position, LugarDePistolaGuardada.rotation);
        PistolaNeumaticaPorGuardar.SetActive(false);
        PistolaNeumaticaGuardadaEnCajaVisual.SetActive(true);
        gameObject.SetActive(false);
        TM_AtrapamientoMano.TareaCompletada(NumeroDeTareaPorCompletar);
    }
}