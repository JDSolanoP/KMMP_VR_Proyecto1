using UnityEngine;

public class SwitchLucesEnCabina : MonoBehaviour
{
    [SerializeField] private Horometro horometro;
    [SerializeField] private GameObject lucesDeExcavadora;
    [SerializeField] private Transform targetRotation;
    private Quaternion _initialLocalRotation;
    private bool _isPhysicalSwitchOn = false;

    private void Start()
    {
        _initialLocalRotation = transform.localRotation;
    }
    public void EncenderLuces()
    {
        AudioManager.aSource.goFx("Boton");
        EstablecerRotacionSwitchOn();
        if (!horometro.GetIsPowerOn()) return;
        EstablecerLuces(true);
    }

    public void ApagarLuces()
    {
        AudioManager.aSource.goFx("Boton");
        EstablecerRotacionSwitchOff();
        EstablecerLuces(false);
    }

    public void ForzarApagarLuces()
    {
        EstablecerLuces(false);
    }
    public void ForzarEncenderLuces()
    {
        EstablecerLuces(true);
    }
    private void EstablecerLuces(bool state)
    {
        lucesDeExcavadora.SetActive(state);
    } 

    private void EstablecerRotacionSwitchOff()
    {
        transform.localRotation = _initialLocalRotation;
        _isPhysicalSwitchOn = false;
    }

    private void EstablecerRotacionSwitchOn()
    {
        transform.localRotation = targetRotation.localRotation;
        _isPhysicalSwitchOn = true;
    }

    public bool GetIsPhysicalSwitchOn() => _isPhysicalSwitchOn;
}