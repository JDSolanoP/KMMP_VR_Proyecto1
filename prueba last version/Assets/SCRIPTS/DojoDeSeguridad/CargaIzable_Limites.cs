using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaIzable_Limites : MonoBehaviour
{
    public Control_Grua_Puente cgp;
    public int nBloqueo;
    public bool BloqueaActivo;
    public GameObject carga;
    public GameObject ContactoNombre;
    public string auxNombreContacto;
    public string TriggerContact;

    void Start()
    {
        //carga = this.gameObject;
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
                cgp.bloqueo[nBloqueo] = true;
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
                cgp.bloqueo[nBloqueo] = false;
                //Debug.Log("Se detecto desbloqueo en " + nBloqueo + " con " + ContactoNombre.name);
            }
            else { 
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
            cgp.bloqueo[nBloqueo] = true;
            ContactoNombre = other.gameObject;
            auxNombreContacto = ContactoNombre.name;
           // Debug.Log("Se detecto bloqueo en " + nBloqueo + " con " + ContactoNombre.name + "en el ontriggerStay");
        }
        else
        {
            TriggerContact = other.name;
        }
    }
}
