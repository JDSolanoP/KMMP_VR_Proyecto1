using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarOBJ : MonoBehaviour
{
    public TM_Megado megado;
    public GameObject ObjetoReferencia;//objeto con el que interactua
    public int contacto_Aux;
    // Start is called before the first frame update

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.name != ObjetoReferencia.name)
        {
            megado.contacto_confirmado[contacto_Aux]=false;
            megado.contacto_aux=contacto_Aux;
            Debug.Log(contacto_Aux+" Malo desde el OBJ "+ObjetoReferencia.name);
        }
        else
        {
            megado.contacto_confirmado[contacto_Aux] = true;
            megado.contacto_aux = contacto_Aux;
            Debug.Log(contacto_Aux + " bueno desde el OBJ " + ObjetoReferencia.name);
        }
    }
    public void OnTriggerStay(Collider col)
    {
        //Debug.Log("verificando contacto");
        /*if (col.name == ObjetoReferencia.name)
        {
            //megado.contacto_confirmado[contacto_Aux] = true;
            //Debug.Log("verificando contacto de entrada  CON "+ ObjetoReferencia.name);
            megado.contactoPinza = true;
            //megado.verificarPosPinza(TareaPorCumplir);
        }*/
        megado.contactoPinza = true;

    }
    public void OnTriggerExit(Collider col)
    {
        if(col.name== ObjetoReferencia.name)
        {
            //Debug.Log("verificando contacto de salida CON " + ObjetoReferencia.name);
            megado.contactoPinza = false;
        }
        megado.contactoPinza = false;
    }
}
