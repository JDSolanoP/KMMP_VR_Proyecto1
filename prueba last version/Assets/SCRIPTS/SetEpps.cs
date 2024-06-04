using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEpps : MonoBehaviour//S_apps
{
    public enum TipoXr
    {
        Cabeza,Manos,Cuerpo,Pies,
    };
    public TipoXr tx;
    public bool enXR_Manos;

    public Material mat;
    
    public void Activar_EnXRManos(bool si_activo)
    {
        enXR_Manos = si_activo;
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ReactivarEnMano());
        }
    }
    IEnumerator ReactivarEnMano()
    {
        yield return new WaitForSeconds(1);
        enXR_Manos = false;
    }
}
