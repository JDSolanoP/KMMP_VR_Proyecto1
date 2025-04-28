using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_INSPECCIONCAMION : Lista_Tareas_Controller
{
    [Header("Elementos")]
    public GameObject[] Muros;
    public GameObject[] Fechas;
    public override void Start()
    {
        base.Start();
        StartCoroutine(ListaTareas(TareaActual));
    }
    public override void TareaCompletada(int TareaSiguiente)
    {
        base.TareaCompletada(TareaSiguiente);
        StartCoroutine(ListaTareas(TareaActual));
    }
    IEnumerator ListaTareas(int tarea)
    {
        switch (tarea)
        {//Agregar notaciones de tareas en cada caso
            case 0:// AQUI, Colocando todos los componentes
                for (int i = 0; i < Tablero_Indicaciones.Length; i++)
                {
                    Tablero_Indicaciones[i].SetActive(false);
                }
                Tablero_Indicaciones[0].SetActive(true);//panel de conformidad
                /*   //audioManager de bienvenida

                yield return new WaitForSeconds(0.5f);
                manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                */

                aSource.PlayMusica(aSource.MusicaSonidos[0].nombre, 0.75f, true);
                //aSource.MusicaVol(0.75f);//**************************************Sonido Musica Inicial*************
                aSource.FxVol(1);
                
                //yield return new WaitForSeconds(1f);
                //yield return new WaitForSecondsRealtime(23f);

                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                yield return new WaitForSecondsRealtime(23f);
                //Locucion para panel intro de P1
                
                break;
            
        }
    }
    //**********************VERIFICAR CONTACTO OBJETO A OBJETO****************************************
}
