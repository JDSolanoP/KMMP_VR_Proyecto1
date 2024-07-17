using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grua_Botones : MonoBehaviour
{
    // Start is called before the first frame update
    public Control_Grua_Puente ctrl;

    //public GameObject r_hand;
    //public GameObject l_hand;
    public int nB;
    public void onPress(int nboton)
    {
        ctrl.on_press = true;
        ctrl.press(nboton);
    }
    public void OnPressExit(int nboton)
    {
        ctrl.on_press = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "L_Indice" || other.name == "D_Indice")
        {
            ctrl.btn_controlgrua[nB] = true;
            ctrl.on_press = true;
            ctrl.press(nB);//en uso
            
            
            //Debug.Log("boton detecta " + other.name + " entrando");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "L_Indice" || other.name == "D_Indice")
        {
            ctrl.btn_controlgrua[nB] = false;
            ctrl.on_press = false;
            //dejar_Pres();
            
            //Debug.Log("boton detecta " + other.name + " saliendo");
        }
    }
    public void dejar_Pres()
    {
        ctrl.Invoke("No_press", 0);
        Debug.Log("no presiona");
    }
}
 
