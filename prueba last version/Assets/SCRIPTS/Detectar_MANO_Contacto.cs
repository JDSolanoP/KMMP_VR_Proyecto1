using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detectar_MANO_Contacto : MonoBehaviour
{
    public TM_Megado megado;
    [Header("Izq=0 - Der=1")]
    public GameObject[] ManoContacto;//objeto con el que interactua
    public int TareaPorCumplir;
    public bool CualquierMano = true;
    public bool verificarAccion = false;
    public bool usarObjReferencia = false;
    public int nTareaAsignada;
    
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if (CualquierMano == true)
        {
            if (verificarAccion == false&&TareaPorCumplir==megado.TareaActual)
            {
                if (other.name == ManoContacto[0].name || other.name == ManoContacto[1].name)

                //if (other.name == ManoContacto[0].name) { megado.ContactoManoIzq = true; }
                //if (other.name == ManoContacto[1].name) { megado.ContactoManoDer = true; }

                {
                    Debug.Log("solo 1 pulsacion");
                    megado.si_realizar_accion[0] = true;
                    verificarUnaAccion();
                    //megado.si_realizar_accion[0]=true;
                    //megado.TareaCompletada(TareaPorCumplir);
                    this.gameObject.SetActive(false);
                }
            }else  
            {
                if (other.name == ManoContacto[0].name || other.name == ManoContacto[1].name)
                {
                    Debug.Log("detectado : " + other.name);
                    megado.manoContacto = true;
                    if (usarObjReferencia == true && megado.si_realizar_accion[1] == false)
                    {
                        megado.si_realizar_accion[nTareaAsignada] = true;
                        Debug.Log("cumpliento activacion de accion por mano -> tarea "+nTareaAsignada);
                        //megado.accionConfirmadaPorContactoMano(nTareaAsignada);
                        /*if (nTareaAsignada == 1 )
                        {
                            this.gameObject.SetActive(false);
                        }*/
                        /*if(this.gameObject.name == ObjReferencia.name)
                        {
                            megado.si_realizar_accion[] = true;
                        }*/
                    }
                }
            }
        /*else
        {
            if (other.name == ManoContacto[0].name) 
            {
                if (other.name == ManoContacto[0].name) { megado.ContactoManoIzq = true; }
            }
            else
            {
                if (other.name == ManoContacto[1].name)
                {
                    if (other.name == ManoContacto[0].name) { megado.ContactoManoIzq = true; }
                }
            }*/
           
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (verificarAccion == true)
        {
            if (other.name == ManoContacto[0].name || other.name == ManoContacto[1].name)
            {
                megado.manoContacto = false;
            }
        }
    }
    public void verificarUnaAccion()
    {
        verificarAccion = true;
        Debug.Log("cumplir 1 vez");
        megado.TareaCompletada(TareaPorCumplir);
        Destroy(this);
            
    }
     
}
