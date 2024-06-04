using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarBotonPresionado : MonoBehaviour
{
    public TM_Megado megado;
    public int nbtn;
    public void btn_Press_In(int nbtn)
    {
        megado.manoContacto = true;
        megado.activarBTN(nbtn);
    }
    public void btn_Press_Out(int nbtn)
    {
        megado.manoContacto=false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "L_Indice" || other.name == "D_Indice")
        {
            megado.manoContacto = true;
            megado.activarBTN(nbtn);
            Debug.Log("boton detecta " + other.name + ", activado");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "L_Indice" || other.name == "D_Indice")
        {
            megado.manoContacto = false;
            
            //dejar_Presionar();
            Debug.Log("boton detecta " + other.name + ", activado");
        }
    }
    public void dejar_Presionar()
    {
        //megado.invoke("No_Pressing",0);
        Debug.Log("dejar de presionar");
    }
}
