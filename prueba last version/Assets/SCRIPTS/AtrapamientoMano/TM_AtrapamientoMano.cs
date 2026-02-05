using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_AtrapamientoMano : Lista_Tareas_Controller
{
    public GameObject manoMesh;
    public GameObject manoRefe;
    public GameObject manoUsuario;
    public GameObject PistolaObj;
    public GameObject PistolaRefe;
    public GameObject PistolaMesh;
    
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
            case 0:// AQUI, llegada delante del dojo
                yield return new WaitForSecondsRealtime(0.5f);
                //manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                //manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                //aSource.MusicaVol(0.5f);//**************************************Sonido Musica Inicial*************
                //aSource.FxVol(1);
                yield return new WaitForSecondsRealtime(2f);
                Tablero_Indicaciones[16].SetActive(true);
                aSource.goFx(aSource.FxSonidos[0].nombre, 0.5f, true, false);
                aSource.goFx(aSource.FxSonidos[1].nombre, 0.2f, true, false);
                aSource.goFx(aSource.FxSonidos[2].nombre, 0.2f, true, false);
                yield return new WaitForSecondsRealtime(1f);

                //Debug.Log("Se esta reproduciendo audio");

                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se tiene todos los EPPS
        }
    }
}
