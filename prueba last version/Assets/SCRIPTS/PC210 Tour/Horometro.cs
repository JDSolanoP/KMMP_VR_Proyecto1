using System.Collections;
using UnityEngine;

public class Horometro : MonoBehaviour
{
    [Header("Pantallas")]
    [SerializeField] private GameObject pantallaApagada;
    [SerializeField] private GameObject pantallaEncendiendo;
    [SerializeField] private GameObject pantallaInicio;
    [SerializeField] private GameObject pantallaGracias;

    [Header("Llave")]
    [SerializeField] private GameObject zonaOn;
    [SerializeField] private float tiempoDeEncendidoApagado = 2f;

    [SerializeField] private SwitchLucesEnCabina switchLucesEnCabina;

    private Coroutine _rutinaActual;
    private bool _isPowerOn;

    private void Start()
    {
        MostrarSolo(pantallaApagada);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == zonaOn)
        {
            print("Llave en la zona");
            DetenerRutina();
            _rutinaActual = StartCoroutine(RutinaEncendido());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == zonaOn)
        {
            print("Llave fuera de la zona");
            DetenerRutina();
            _rutinaActual = StartCoroutine(RutinaApagado());
        }
    }

    private IEnumerator RutinaEncendido()
    {
        MostrarSolo(pantallaEncendiendo);
        yield return new WaitForSeconds(tiempoDeEncendidoApagado);

        _isPowerOn = true;
        MostrarSolo(pantallaInicio);
        if (switchLucesEnCabina.GetIsPhysicalSwitchOn())
        {
            switchLucesEnCabina.ForzarEncenderLuces();
        }
    }

    private IEnumerator RutinaApagado()
    {
        MostrarSolo(pantallaGracias);
        yield return new WaitForSeconds(tiempoDeEncendidoApagado);

        _isPowerOn = false;
        switchLucesEnCabina.ForzarApagarLuces();
        MostrarSolo(pantallaApagada);
    }

    private void DetenerRutina()
    {
        if (_rutinaActual != null) StopCoroutine(_rutinaActual);
    }

    private void MostrarSolo(GameObject pantalla)
    {
        pantallaApagada.SetActive(false);
        pantallaInicio.SetActive(false);
        pantallaEncendiendo.SetActive(false);
        pantallaGracias.SetActive(false);

        pantalla.SetActive(true);
    }

    public bool GetIsPowerOn() => _isPowerOn;
}