using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaIzable_ColViento : MonoBehaviour
{//creado el 13-01-25 para deteccion de collider en caso de viento
    public GruaGanchoRot GGR;
    public int nBloqueo;
    public bool BloqueaActivo;
    public int nBloqueAdyaIzq;
    public int nBloqueAdyaDer;
    public int nBloqueOpuesto;
    public GameObject carga;
    public GameObject ContactoNombre;
    public string auxNombreContacto;
    public string TriggerContact;
    public bool siSaliendo=false;

    void Start()
    {
        nBloqueAdyaDer = nBloqueo + 1;
        if (nBloqueAdyaDer > 7) nBloqueAdyaDer = 0;
        nBloqueAdyaIzq = nBloqueo -1;
        if (nBloqueAdyaIzq <0) nBloqueAdyaIzq = 7;
    }
    public void OnTriggerEnter(Collider other)
    {
        ContactoNombre = other.gameObject;
        //Debug.Log("Se detecto bloqueo en " + nBloqueo);
        if (other.name != carga.name)
        {
            if (other.isTrigger == false)
            {
                AudioManager.aSource.goFx("4Puerta_Corrediza_Alto");//******************************************audio carga**************************
                BloqueaActivo = true;
                GGR.siBloqueo[nBloqueo] = true;
                ContactoNombre = other.gameObject;
                auxNombreContacto = ContactoNombre.name;
                //Debug.Log("Se detecto bloqueo en " + nBloqueo + " con " + ContactoNombre);
            }

        }
    }
    void OnTriggerExit(Collider other)
    {
        //ContactoNombre = other.name;
        // Debug.Log("Se detecto bloqueo en " + nBloqueo);
        if (other.name != carga.name)
        {
            OnTriggerStay(other);
            if (ContactoNombre.name == auxNombreContacto)
            {
                BloqueaActivo = false;
                GGR.siBloqueo[nBloqueo] = false;
                //Debug.Log("Se detecto desbloqueo en " + nBloqueo + " con " + ContactoNombre.name);
                //ContactoNombre = null;
            }
            else
            {
                OnTriggerEnter(other);
            }
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (auxNombreContacto == null)
        {

        }
        if (other.isTrigger == false)
        {
            BloqueaActivo = true;
            GGR.siBloqueo[nBloqueo] = true;
            ContactoNombre = other.gameObject;
            auxNombreContacto = ContactoNombre.name;
            // Debug.Log("Se detecto bloqueo en " + nBloqueo + " con " + ContactoNombre.name + "en el ontriggerStay");
        }
        else
        {
            TriggerContact = other.name;
        }
    }

    private void OnDisable()
    {
        ForceUnlock();
    }

    public void ForceUnlock()
    {
        BloqueaActivo = false;
        GGR.siBloqueo[nBloqueo] = false;
    }
}
