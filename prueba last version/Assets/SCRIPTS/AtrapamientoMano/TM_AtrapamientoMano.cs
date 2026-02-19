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
    public GameObject[] Epps;
    public GameObject[] manosXR;
    public Material[] manosXRMaterial;//19.02
    public int totalEpps;
    public GameObject[] BtnContinue;
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
                for (int i = 0; i < Tablero_Indicaciones.Length; i++)
                {
                    Tablero_Indicaciones[i].SetActive(false);
                }
                yield return new WaitForSecondsRealtime(0.5f);
                //manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                //manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                aSource.MusicaVol(0.15f);//**************************************Sonido Musica Inicial*************
                //aSource.FxVol(1);
                yield return new WaitForSecondsRealtime(2f);
                //Tablero_Indicaciones[16].SetActive(true);
                yield return new WaitForSecondsRealtime(1f);
                
                //Debug.Log("Se esta reproduciendo audio");
                //BtnContinue[0].SetActive(true);//iniciar
                yield return new WaitForSecondsRealtime(0.5f);
                Tablero_Indicaciones[0].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se tiene todos los EPPS
            case 1:
                Tablero_Indicaciones[1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se tiene todos los EPPS
            case 2:
                Tablero_Indicaciones[1].SetActive(false);
                Tablero_Indicaciones[2].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se tiene todos los EPPS
            case 3:
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se tiene todos los EPPS
                break;
            case 4:
                break;
        }
    }
    public void EppPuesto(int numeroEpp)
    {
        if (numeroEpp == 0)
        {
            Debug.Log("Cambiar textura guante");
            manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[0];//19.01
            manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
            //guantesComplementos[0].SetActive(true);
            //guantesComplementos[1].SetActive(true);
        }
        Epps[numeroEpp].SetActive(false);
        ActivarEvento(0);
    }
    private void ActivarEvento(int NumeroEvento)
    {
        switch (NumeroEvento)
        {
            
            case 0:
                totalEpps++;
                if (totalEpps==6) {
                    Debug.Log("Se agararon todos los EPPs");
                    TareaCompletada(1);
                    aSource.goFx("Bien");
                    aSource.goFx("Locu_Bien");
                }
                break;
        }
    }
    public void ActivarXR(int NumeroEvento)
    {
        switch (NumeroEvento)
        {
            case 0:
                //BtnContinue[0].SetActive(false);
                Tablero_Indicaciones[0].SetActive(false);
                TareaCompletada(0);
                break;
        }
    }
}
