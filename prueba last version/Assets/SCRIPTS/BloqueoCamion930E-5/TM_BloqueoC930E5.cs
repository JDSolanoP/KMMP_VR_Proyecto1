using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_BloqueoC930E5 : Lista_Tareas_Controller
{
    public int auxContacto;
    [Header("ElementosEscenario")]
    public GameObject CamionAnim;
    public GameObject CamionC930;
    public GameObject[] Muros;
    public GameObject[] Flechas;
    [Header("ElementosModulo")]
    public GameObject[] Items;

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
                for (int i = 0; i < aros_indicadores.Length; i++)
                {
                    Flechas[i].SetActive(false);
                    aros_indicadores[i].SetActive(false);
                }
                CamionC930.SetActive(false);
                /*for (int i = 0; i < BtnContinue.Length; i++)
                {
                    BtnContinue[i].SetActive(false);
                }*/
                aSource.PlayMusica(aSource.MusicaSonidos[0].nombre, 0.75f, true);
                yield return new WaitForSecondsRealtime(47f);//**********************fin de animacion de entrada a camion********
                FadeOut();
                yield return new WaitForSecondsRealtime(2f);
                CamionAnim.SetActive(false);
                FadeIn();
                CamionC930.SetActive(true);    
                Muros[0].SetActive(false);
                yield return new WaitForSecondsRealtime(2f);
                Tablero_Indicaciones[0].SetActive(true);//panel de bienvenido
                /*   //audioManager de bienvenida

                yield return new WaitForSeconds(0.5f);
                manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                */

                
                //aSource.MusicaVol(0.75f);//**************************************Sonido Musica Inicial*************
                aSource.FxVol(1);

                //yield return new WaitForSeconds(1f);
                //yield return new WaitForSecondsRealtime(23f);

                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                yield return new WaitForSecondsRealtime(5f);
                Tablero_Indicaciones[1].SetActive(true);//Locucion para panel intro de P1
                //Flechas[0].SetActive(true);
                //aros_indicadores[0].SetActive(true);
                break;
        }
    }
    //**********************VERIFICAR CONTACTO OBJETO A OBJETO****************************************
    public void verificarContacto(int confirmarcontacto)
    {
        switch (confirmarcontacto)
        {
            case 0://contacto con aros, completa tareas segun aux
                
                break;
        }
    }
    public void activacionXR(int contacto) //contacto con manos
    {
        switch (contacto)
        {
            case 0://boton continue
                Debug.Log("auxcontacto=" + auxContacto);
                if (auxContacto < 8)
                {
                    Muros[auxContacto - 1].SetActive(false);
                    aros_indicadores[auxContacto].SetActive(true);
                    Flechas[auxContacto].SetActive(true);
                }

                if (TareaActual < 4)//antes de subir a cabina
                {
                    Tablero_Indicaciones[auxContacto + 1].SetActive(false);
                    if (TareaActual == 2)
                    {
                        Tablero_Indicaciones[1].SetActive(false);
                        Tablero_Indicaciones[0].SetActive(false);
                    }
                }
                else if (TareaActual == 4)
                {
                    Tablero_Indicaciones[auxContacto + 1].SetActive(false);
                    Tablero_Indicaciones[auxContacto + 2].SetActive(true);
                }
                else
                {

                    Tablero_Indicaciones[auxContacto + 2].SetActive(false);
                }
                if (auxContacto == 7)
                {
                    Muros[7].SetActive(false);
                    Tablero_Indicaciones[6].SetActive(false);
                    TareaCompletada(TareaActual);
                    Muros[8].SetActive(true);
                }
                break;
            case 1://boton reinicio
                IrEscenaAsincron(0);
                break;
            case 2://boton SALIR
                Application.Quit();
                break;
        }
    }
    public IEnumerator ActiveContinue(GameObject obj)
    {
        yield return new WaitForSecondsRealtime(5);
        obj.SetActive(true);
    }
    public void activarItem(bool on)
    {
        Flechas[8].SetActive(on);
    }
}
