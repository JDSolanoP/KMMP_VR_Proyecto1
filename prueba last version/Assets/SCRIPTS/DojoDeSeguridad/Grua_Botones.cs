using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grua_Botones : MonoBehaviour
{
    // Start is called before the first frame update
    public Control_Grua_Puente ctrl;

    public GameObject[] dedos;
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
        if (other.name == "L_Indice" || other.name == "D_Indice"||"L_Pulgar"==other.name||other.name=="D_Pulgar")
        {
            for (int i = 0; i < dedos.Length; i++)
            {
                if (other.name != dedos[i].name)
                {
                    dedos[i].SetActive(false);
                }
            }
            ctrl.btn_controlgrua[nB] = true;
            ctrl.on_press = true;
            ctrl.press(nB);//en uso
            //Debug.Log("boton detecta " + other.name + " entrando");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "L_Indice" || other.name == "D_Indice" || "L_Pulgar" == other.name || other.name == "D_Pulgar")//***Modificado el 31-07-24*** se agrego la opcion de presionar con los pulgares
        {
            for (int i = 0; i < dedos.Length; i++)
            {
                dedos[i].SetActive(true);
            }
            AudioManager.aSource.goFx("Boton");
            ctrl.btn_controlgrua[nB] = false;
            ctrl.on_press = false;
            switch (nB) 
            {
                case 0:
                    AudioManager.aSource.altoFxLoop("Sirena_grua");
                    AudioManager.aSource.goFx("Corneta_freno");
                    break;
                case 1:
                    AudioManager.aSource.altoFxLoop("Sirena_grua");
                    AudioManager.aSource.goFx("Corneta_freno");
                    break;
                case 2:
                    AudioManager.aSource.altoFxLoop("Rieles_grua");
                    AudioManager.aSource.goFx("Corneta_freno");
                    //StartCoroutine(GruaMove(true));
                    break;
                case 3:
                    AudioManager.aSource.altoFxLoop("Rieles_grua");
                    AudioManager.aSource.goFx("Corneta_freno");
                    //StartCoroutine(GruaMove(false));
                    break;
                case 4:
                    AudioManager.aSource.altoFxLoop("Ascenso_grua");
                    break;
                case 5:
                    AudioManager.aSource.altoFxLoop("Ascenso_grua");
                    break;
                case 7:
                    AudioManager.aSource.altoFxLoop("Corneta_aviso");
                    break;
            }
            //dejar_Pres();
            
            //Debug.Log("boton detecta " + other.name + " saliendo");
        }
    }

}
 
