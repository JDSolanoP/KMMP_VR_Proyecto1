using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarOBJ : MonoBehaviour
{
    public TM_Megado megado;
    public GameObject ObjetoReferencia;//objeto con el que interactua
    public int TareaPorCumplir;
    // Start is called before the first frame update

    // Update is called once per frame
    public void OnTriggerStay(Collider col)
    {
        //Debug.Log("verificando contacto");
        if (col.name == ObjetoReferencia.name)
        {
            //Debug.Log("verificando contacto de entrada  CON "+ ObjetoReferencia.name);
            megado.contactoPinza = true;
            //megado.verificarPosPinza(TareaPorCumplir);
        }
    }
    public void OnTriggerExit(Collider col)
    {
        if(col.name== ObjetoReferencia.name)
        {
            //Debug.Log("verificando contacto de salida CON " + ObjetoReferencia.name);
            megado.contactoPinza = false;
        }
        
    }
}
