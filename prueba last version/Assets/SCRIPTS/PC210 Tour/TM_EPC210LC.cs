using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TM_EPC210LC : Lista_Tareas_Controller
{
    [Header("PROPIAS DEL MODULO")]
    public GameObject usuario;
    public Transform[] pos;
    public GameObject[] muro; 
    public GameObject[] BtnContinue;
    public int actualPos = 0;
    public int aux;

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
        {
            case 0:////ARO_07

                for (int i = 0; i < Tablero_Indicaciones.Length; i++)
                {
                    Tablero_Indicaciones[i].SetActive(false);
                }
                /*for (int i = 0; i < aros_indicadores.Length; i++)
                {
                    Flechas[i].SetActive(false);
                    aros_indicadores[i].SetActive(false);
                }*/
                for (int i = 0; i < BtnContinue.Length; i++)
                {
                    BtnContinue[i].SetActive(false);
                }
                yield return new WaitForSecondsRealtime(0.5f);

                Tablero_Indicaciones[0].SetActive(true);//panel de bienvenido
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
                break;
        }
    }
    public void activacionXR(int contacto) //contacto con manos
    {
        switch (contacto)
        {
            case 0://boton continue a siguiente area
                
                StartCoroutine(Transporte());
                break;
        }
    }
    public IEnumerator Transporte()
    {
        int nMuro =0;
        FadeOutIn(1,1,1);
        yield return new WaitForSecondsRealtime(1f);
        switch (actualPos)
        {
            case 1://epps a maquina
                nMuro = 0;
                break;
            case 3://horometro1
                nMuro = 1;
                break;
        }
        muro[nMuro].SetActive(false);
        muro[actualPos+1].SetActive(true);
        usuario.transform.position = pos[actualPos].position;
        actualPos++;
    }
}
