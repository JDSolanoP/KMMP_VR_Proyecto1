using System.Collections;
using System.Collections.Generic;
using Meta.WitAi;
using TMPro;
using UnityEngine;

public class EV_Bloqueo_C390E5 : MonoBehaviour
{
    public TM_BloqueoC930E5 tm_;//***********03-09-25**********
    public int TareaActual;
    public void Awake()
    {
        /*auxContacto=tm_.auxContacto;//para activavionXR u otros
    contactoIntAux=tm_.contactoIntAux;//exclusivo de DetectorObj
    NombreAuxAudio;
    /*public float tiempoEsperaAux;
    public bool FullSonidos = true;//********************************Agregado 02-09-25************************
    [Header("ElementosEscenario")]
    public GameObject PJ;
    public Transform[] posPJ;
    public GameObject[] NPC;
    public GameObject CamionAnim;
    public GameObject CamionC930;
    public GameObject[] Muros;
    public GameObject[] Flechas;
    [Header("*****ElementosModulo*****")]
    [Header("***Tacos***")]
    public GameObject[] Tacos;
    [Header("***Caja_De_Aislamiento***")]
    public GameObject[] Palancas;//caja de bloqueo
    public GameObject[] LucesLEDCaja;
    public float[] rotPalancas;
    int nPalancasOff = 0;
    [Header("***Bloqueo_Etiquetado***")]
    public GameObject[] Items;//LOTO
    public Vector3 cajaBloqueoTapaRot0;
    public Vector3 cajaBloqueoTapaPos0;
    bool si_LlaveEnMano;
    [Header("***NoArranque***")]
    public GameObject[] llaveArranque;//prueba de desenergizado
    public GameObject[] Timon;
    public Vector3 TimonRot0;
    public float TimonRotIzq;
    public float TimonRotDer;
    public bool[] si_verificacionGiro;
    public bool[] nVerificionesArranque;
    public bool si_timon_agarrado = false;
    [Header("***Gabiente_Frenos***")]
    public bool si_PuertaGFreno_Cerrada = true;
    public GameObject[] V_NV1;
    public GameObject[] V_NV2;
    public bool[] si_Valvula_Liberada;
    public bool[] si_Valvula_Cerrada;
    [Header("***Verificacion_Pedal***")]
    public GameObject btn_Pedal;
    public GameObject[] Pedales;
    [Header("***acumulador_auxiliar***")]
    public GameObject[] escalera;
    public GameObject[] acumuladorAux;
    public bool si_AcuAux_Liberado = false;
    public bool tareaHecha = false;
    [Header("***Verificacion_Override***")]
    public bool si_Override_Liberado;
    public GameObject[] Override;
    [Header("***Gabiente de Potencia***")]
    public GameObject btn_AbrirGabinetePotencia;
    public GameObject[] Nodo;
    [Header("*****ElementosPropioDeCamión*****")]
    public bool si_PuertaCabinaCerrada;
    public bool si_PJEnCabina;
    [Header("*****ValoresVoltimetro*****")]
    public GameObject[] ItemsVolti;
    public GameObject[] NodosVoltimetro;
    public bool[] si_voltimetroAgarrado;
    public GameObject[] PuertasGPotencia;
    public float[] RotPuertaGPontencia;//0,1 : Izq 2,3: Der
    public int quien_primero_agarre = 2;//0:Izquierda;1:Derecha;2:Ninguno
    public Vector3 NodoIzqPos0;
    public Vector3 NodoIzqRot0;
    public Vector3 NodoDerPos0;
    public Vector3 NodoDerRot0;
    public bool[] NodosContactoIzq;//los nodos que hacen contacto con la izquierda ***** 15-07-25******
    public bool[] NodosContactoDer;
    [Header("0:IZQ-1DER")]
    public int[] valorNodo;
    public bool[] si_contactoNodos;//si contactos coinciden
    public bool si_OrdenPositivo;//verifica si el resultado es positivo
    public bool si_circuitoCompleto;//verifica que ambos nodos esten en contacto
    public bool si_voltimetro_en_mano = false;
    public TMP_Text txt_panelVolti;

    [Header("Extras")]
    public GameObject UI_btn_Continuar_Panel;
    public GameObject UI_btn_Salir_Panel;
    public GameObject UI_btn_Reiniciar_Panel;
    public GameObject UI_btn_Menu_Panel;*/
    }
    public void Start()
    {
        if (tm_.si_ModuloEvaluación == true)
        {
            if (tm_.si_login == true)
            {
                tm_.PJ.transform.position = tm_.posPJ[0].position;
            }
            StartCoroutine(ListaTareas(TareaActual));
        }
    }
    IEnumerator ListaTareas(int tarea)
    {
        if (tm_.si_login == true)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        else
        {
            switch (tarea)
            {//Agregar notaciones de tareas en cada caso
                case 0:// AQUI, Colocando todos los componentes
                    for (int i = 0; i < tm_.NPC.Length; i++)
                    {
                        if (i < 3)
                        {
                            tm_.NPC[i].SetActive(false);
                        }
                        else
                        {
                            tm_.NPC[i].SetActive(true);
                        }
                    }

                    tm_.aSource.PlayMusica(tm_.aSource.MusicaSonidos[0].nombre, 0.6f, true);
                    if (!tm_.EnPruebas)
                    {
                        tm_.aSource.altoFx("Locu_Lobby");//*****************Agregado 02-09-25************ naudiofx40
                        tm_.CamionAnim.SetActive(true);
                        tm_.CamionC930.SetActive(false);
                        tm_.StartCoroutine(tm_.CoroutineAnimSonidoEntradaCamion());
                        tm_.aSource.goFx("BloqueoCamion_EntradaAnim");
                        yield return new WaitForSecondsRealtime(47f);//**********************fin de animacion de entrada a camion********
                        tm_.FadeOut(3f);
                        yield return new WaitForSecondsRealtime(5f);
                        tm_.PJ.transform.position = tm_.posPJ[1].position;
                        tm_.CamionAnim.SetActive(false);
                        tm_.FadeIn(3f);
                    }
                    tm_.CamionC930.SetActive(true);
                    tm_.Muros[0].SetActive(false);
                    yield return new WaitForSecondsRealtime(2f);
                    //*****************Ubicacion de palancas de caja de bloqueo*************

                    tm_.Palancas[0].transform.localEulerAngles = new Vector3(tm_.Palancas[1].transform.localEulerAngles.x, tm_.Palancas[1].transform.localEulerAngles.y, tm_.Palancas[1].transform.localEulerAngles.z);//colocacion de rot de prendido
                    tm_.Palancas[2].transform.localEulerAngles = new Vector3(tm_.Palancas[2].transform.localEulerAngles.x, tm_.Palancas[3].transform.localEulerAngles.y, tm_.rotPalancas[1]);
                    tm_.Palancas[4].transform.localEulerAngles = new Vector3(tm_.Palancas[3].transform.localEulerAngles.x, tm_.Palancas[5].transform.localEulerAngles.y, tm_.rotPalancas[2]);
                    tm_.Palancas[1].SetActive(false);
                    tm_.Palancas[3].SetActive(false);
                    tm_.Palancas[5].SetActive(false);

                    tm_.btn_Pedal.SetActive(false);
                    //*****CAptura de posiciones de los nodos del voltimetro*****16-06-25**********

                    tm_.NodosVoltimetro[0].transform.SetParent(tm_.NodosVoltimetro[1].transform);
                    tm_.NodoIzqPos0 = tm_.NodosVoltimetro[0].transform.localPosition;
                    tm_.NodoIzqRot0 = tm_.NodosVoltimetro[0].transform.localEulerAngles;
                    tm_.NodosVoltimetro[0].transform.SetParent(tm_.NodosVoltimetro[2].transform);
                    tm_.NodosVoltimetro[1].transform.SetParent(tm_.NodosVoltimetro[0].transform);
                    tm_.NodoDerPos0 = tm_.NodosVoltimetro[1].transform.localPosition;
                    tm_.NodoDerRot0 = tm_.NodosVoltimetro[1].transform.localEulerAngles;
                    tm_.NodosVoltimetro[1].transform.SetParent(tm_.NodosVoltimetro[2].transform);
                    tm_.RotPuertaGPontencia[0] = tm_.PuertasGPotencia[0].transform.localEulerAngles.z;//rot0,izq
                    tm_.RotPuertaGPontencia[2] = tm_.PuertasGPotencia[1].transform.localEulerAngles.z;//rot0 der
                    tm_.txt_panelVolti.text = "";
                    tm_.ItemsVolti[14].SetActive(false);
                    //**********************************************************************
                    tm_.cajaBloqueoTapaRot0 = tm_.Items[18].transform.localEulerAngles;//tapa de caja de bloqueo*************17-06-25
                    tm_.cajaBloqueoTapaPos0 = tm_.Items[18].transform.localPosition;
                    for (int i = 0; i < tm_.LucesLEDCaja.Length; i++)
                    {
                        tm_.LucesLEDCaja[i].SetActive(false);
                    }
                    tm_.LucesLEDCaja[0].SetActive(true);
                    tm_.LucesLEDCaja[2].SetActive(true);
                    tm_.LucesLEDCaja[4].SetActive(true);
                    tm_.LucesLEDCaja[6].SetActive(true);
                    //***********************Preparativos para bloqueo de camion***************
                    tm_.llaveArranque[0].SetActive(false);
                    tm_.TimonRot0 = tm_.Timon[1].transform.localEulerAngles;

                    tm_.Tablero_Indicaciones[0].SetActive(true);//panel de bienvenido

                    /*manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                    manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                    */
                    //**************************************Sonido Musica Inicial*************
                    tm_.aSource.FxVol(0.6f);
                    
                    tm_.aSource.goFx("Locu_intro");
                    if (tm_.FullSonidos == true)//********************************Agregado 02-09-25************************
                    {
                        yield return new WaitForSecondsRealtime(20f);//********************
                        tm_.aSource.goFx("Locu_Parte1");
                    }
                    else
                    {
                        yield return new WaitForSecondsRealtime(5f);//********************
                    }

                    //yield return new WaitForSecondsRealtime(23f);
                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    //yield return new WaitForSecondsRealtime(5f);
                    //***********************Parte 1*****************************
                    tm_.Tablero_Indicaciones[1].SetActive(true);//Muestra informacion inicial
                    ActivacionesXTarea(0);//Tacos
                    /*tm_.Tacos[2].SetActive(true);
                    tm_.Tacos[3].SetActive(true);*/
                    
                    //***********************Parte 2*****************************
                    tm_.Palancas[1].SetActive(true);
                    tm_.Palancas[3].SetActive(true);
                    break;
            }
        }
    }
    public void ActivacionesXTarea(int t)
    {
        switch (t)
        {
            case 0:
                tm_.Tacos[2].SetActive(true);
                tm_.Tacos[3].SetActive(true);
                break;
        }
    }
    public void AccionTarea(int t,float ti,int nAudio)
    {
        switch (t)
        {
            case 0:
                break;
        }
    }
}
