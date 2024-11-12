using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.XR.Interaction.Toolkit;

public class TM_IZAJE_M2 : Lista_Tareas_Controller
{
    [Header("Modulo 2 de IZAJE")]
    
    public bool[] contacto_confirmado;
    public int contactoIntAux;
    string NombreAuxAudio;
    float tiempoEsperaAux;
    public GameObject[] Tablero_Indicaciones;
    public GameObject[] guantesComplementos;
    public GameObject[] manosXR;
    public Material[] manosXRMaterial;

    [Header("ESLINGAS")]
    public Set_Eslingas[] sE;
    public GameObject[] EslingasObj;
    public GameObject[] Eslinga_Colocada_MT;
    public int nEslingaCorrecta;
    public int[] MAT_Eslinga;
    public bool[] si_UsableEslinga;//perfecto estado
    public bool Si_HayEslingaColocada=false;//verifica si hay eslinga
    public bool Si_EslingaColocadaUsable = false;//verifica si la eslinga es usable
    public bool TodoCorrecto;
    [Header("Gruas 0:5T 1:25T")]
    public GameObject[] Gruas;
    public GameObject perillaSelectGancho;
    public float AnguloPerilla;//+90 grados;
    public bool si_perilla;
    public bool perilla_correcta=true;
    [Header("Elementos Post Verificacion")]
    public GameObject[] ElementoPost; 

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

                /*   //audioManager de bienvenida

                yield return new WaitForSeconds(0.5f);
                manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];

                aSource.MusicaVol(0.5f);//**************************************Sonido Musica Inicial*************
                //aSource.FxVol(1);
                /*for (int i = 0; i < Tablero_Indicaciones.Length; i++)
                {
                    Tablero_Indicaciones[i].SetActive(false);
                }*/
                for (int i = 0; i < sE.Length; i++)//asignando caracteristicas a eslingas 
                {
                    int rnd = Random.Range(0, 8);
                    sE[i].set_Valores(rnd);
                    sE[i].Num_Material=rnd;
                }
                Saber_Eslinga_Correcta();
                colocar_Eslinga();
                yield return new WaitForSeconds(1f);

                //Debug.Log("Se esta reproduciendo audio");

                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se tiene todos los EPPS
            case 1:
                break;
        }
    }
    //**********************VERIFICAR CONTACTO OBJETO A OBJETO****************************************
    public void verificarContacto(int confirmarContacto)//*******realiza acciones cuando2 objetos interactuan usan
    {
        switch (confirmarContacto)
        {
            case 0://verifica contacto de eslinga con detector_eslinga
                if (contacto_confirmado[confirmarContacto] == true)// 15-10-24 Colocar Eslinga desde el OBJ hasta el MT
                {
                    if (sE[contactoIntAux].En_Mano == false)
                    {
                        for (int i = 0; i < EslingasObj.Length; i++)
                        {
                            Eslinga_Colocada_MT[i].SetActive(false);
                            EslingasObj[i].SetActive(true);
                        }
                        Eslinga_Colocada_MT[8].SetActive(false);
                        Eslinga_Colocada_MT[MAT_Eslinga[contactoIntAux]].SetActive(true);
                        EslingasObj[contactoIntAux].SetActive(false);
                        sE[contactoIntAux].usable = si_UsableEslinga[contactoIntAux];
                        Si_HayEslingaColocada = true;
                        Si_EslingaColocadaUsable=(MAT_Eslinga[contactoIntAux] == nEslingaCorrecta&& si_UsableEslinga[contactoIntAux])?true:false;
                        Debug.Log("Se coloco la eslinga " + contactoIntAux);
                        
                    }
                }
                VerificarTodoCorrecto();
                break;
        }
    }
    //*********************************Parte 1 Verificacion de eslinga correcta***********************
    public void Saber_Eslinga_Correcta()//recopila detalles de las eslingas
    {
        for (int i = 0; i < si_UsableEslinga.Length; i++)
        {
            MAT_Eslinga[i] = sE[i].Num_Material;
            if (sE[i].Num_Material == nEslingaCorrecta && sE[i].usable) 
            {
                si_UsableEslinga[i] = true;
            }
            else
            {//si la materia es incorrecta
                if(sE[i].usable)
                sE[i].usable = false;//reasigna valor falso
            }
        }
    }
    public void colocar_Eslinga()
    {
        bool existecorrecto=false;
        for(int i = 0; i<si_UsableEslinga.Length; i++)
        {
            if (si_UsableEslinga[i] == true)
            {
                existecorrecto = true;
                Debug.Log("Se encontro una eslinga correcta,es " + i);
                break;
            }
        }
        if (existecorrecto==false)
        {
            int rnd = Random.Range(0, 8);
            sE[rnd].set_Valores(7, 1, 1, 1,true);
            sE[rnd].Num_Material = 7;
            MAT_Eslinga[rnd] = 7;
            si_UsableEslinga[rnd] = true;
            //sE[rnd].usable = true;
            Debug.Log("No se encontro una eslinga correcta y se reasigno a " + rnd);
        }
    }
    public void Seleccion_Grua()
    {
        if (si_perilla)
        {
            si_perilla=false;
            perillaSelectGancho.transform.localEulerAngles = new Vector3(perillaSelectGancho.transform.localEulerAngles.x, 0, perillaSelectGancho.transform.localEulerAngles.z);
            Gruas[1].SetActive(false);
            Gruas[0].SetActive(true);
        }
        else
        {
            si_perilla = true;//si es true es la medida correcta
            perillaSelectGancho.transform.localEulerAngles = new Vector3(perillaSelectGancho.transform.localEulerAngles.x, AnguloPerilla, perillaSelectGancho.transform.localEulerAngles.z);
            Gruas[0].SetActive(false);
            Gruas[1].SetActive(true);
        }
        VerificarTodoCorrecto();
    }
    public void VerificarTodoCorrecto()
    {
        if (si_perilla && Si_EslingaColocadaUsable)
        {
            
            TodoCorrecto =true;
        }
        else
        {
            TodoCorrecto = false;
        }
        Debug.Log("TODO CORRECTO : " + TodoCorrecto + " - " + si_perilla + " " + Si_EslingaColocadaUsable);
    }
}
