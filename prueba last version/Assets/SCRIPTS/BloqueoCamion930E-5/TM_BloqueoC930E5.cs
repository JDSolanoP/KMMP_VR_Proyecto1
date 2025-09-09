using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Newtonsoft.Json.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TM_BloqueoC930E5 : Lista_Tareas_Controller
{
    [Header("************Propios de Módulo***********")]
    public GameObject ModuloEvaluacion;
    public int auxContacto;//para activavionXR u otros
    public int contactoIntAux;//exclusivo de DetectorObj
    public string NombreAuxAudio;
    public float tiempoEsperaAux;
    public bool FullSonidos=true;//********************************Agregado 02-09-25************************
    [Header("***ElementosEscenario***")]
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
    public bool si_voltimetro_en_mano=false;
    public TMP_Text txt_panelVolti;

        [Header("Extras")]
    public GameObject UI_btn_Continuar_Panel;
    public GameObject UI_btn_Salir_Panel;
    public GameObject UI_btn_Reiniciar_Panel;
    public GameObject UI_btn_Menu_Panel;

    
    public override void Start()
    {
        base.Start();
        if (si_login == true)
        {
            PJ.transform.position = posPJ[0].position;
        }
        if (si_ModuloEvaluación == false)
        {
            StartCoroutine(ListaTareas(TareaActual));
        }
    }
    public override void TareaCompletada(int TareaSiguiente)
    {
        base.TareaCompletada(TareaSiguiente);
        StartCoroutine(ListaTareas(TareaActual));
    }
    IEnumerator ListaTareas(int tarea)
    {

        if (si_login == true)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        else {
            switch (tarea)
            {//Agregar notaciones de tareas en cada caso
                case 0:// AQUI, Colocando todos los componentes
                    for (int i = 0; i < NPC.Length; i++)
                    {
                        if (i < 3)
                        {
                            NPC[i].SetActive(false);
                        }
                        else
                        {
                            NPC[i].SetActive(true);
                        }
                    }
                    /*for (int i = 0; i < aros_indicadores.Length; i++)
                    {
                        Flechas[i].SetActive(false);
                        aros_indicadores[i].SetActive(false);
                    }
                    CamionC930.SetActive(false);
                    /*for (int i = 0; i < BtnContinue.Length; i++)
                    {
                        BtnContinue[i].SetActive(false);
                    }*/
                    aSource.PlayMusica(aSource.MusicaSonidos[0].nombre, 0.6f, true);
                    if (!EnPruebas)
                    {
                        aSource.altoFx("Locu_Lobby");//*****************Agregado 02-09-25************ naudiofx40
                        CamionAnim.SetActive(true);
                        CamionC930.SetActive(false);
                        StartCoroutine(CoroutineAnimSonidoEntradaCamion());
                        aSource.goFx("BloqueoCamion_EntradaAnim");
                        yield return new WaitForSecondsRealtime(47f);//**********************fin de animacion de entrada a camion********
                        FadeOut(3f);
                        yield return new WaitForSecondsRealtime(5f);
                        PJ.transform.position = posPJ[1].position;
                        CamionAnim.SetActive(false);
                        FadeIn(3f);
                    }
                    CamionC930.SetActive(true);
                    Muros[0].SetActive(false);
                    yield return new WaitForSecondsRealtime(2f);
                    //*****************Ubicacion de palancas de caja de bloqueo*************
                    /*rotPalancas = new float[3];
                    rotPalancas[0] = Palancas[0].transform.localEulerAngles.x;// captura de Rotacion
                    rotPalancas[1] = Palancas[2].transform.localEulerAngles.x;
                    rotPalancas[2] = Palancas[4].transform.localEulerAngles.x;*/
                    Palancas[0].transform.localEulerAngles = new Vector3(Palancas[1].transform.localEulerAngles.x, Palancas[1].transform.localEulerAngles.y, Palancas[1].transform.localEulerAngles.z);//colocacion de rot de prendido
                    Palancas[2].transform.localEulerAngles = new Vector3(Palancas[2].transform.localEulerAngles.x, Palancas[3].transform.localEulerAngles.y, rotPalancas[1]);
                    Palancas[4].transform.localEulerAngles = new Vector3(Palancas[3].transform.localEulerAngles.x, Palancas[5].transform.localEulerAngles.y, rotPalancas[2]);
                    Palancas[1].SetActive(false);
                    Palancas[3].SetActive(false);
                    Palancas[5].SetActive(false);

                    btn_Pedal.SetActive(false);
                    //*****CAptura de posiciones de los nodos del voltimetro*****16-06-25**********
                    
                    NodosVoltimetro[0].transform.SetParent(NodosVoltimetro[1].transform);
                    NodoIzqPos0 = NodosVoltimetro[0].transform.localPosition;
                    NodoIzqRot0 = NodosVoltimetro[0].transform.localEulerAngles;
                    NodosVoltimetro[0].transform.SetParent(NodosVoltimetro[2].transform);
                    NodosVoltimetro[1].transform.SetParent(NodosVoltimetro[0].transform);
                    NodoDerPos0 = NodosVoltimetro[1].transform.localPosition;
                    NodoDerRot0 = NodosVoltimetro[1].transform.localEulerAngles;
                    NodosVoltimetro[1].transform.SetParent(NodosVoltimetro[2].transform);
                    RotPuertaGPontencia[0] = PuertasGPotencia[0].transform.localEulerAngles.z;//rot0,izq
                    RotPuertaGPontencia[2] = PuertasGPotencia[1].transform.localEulerAngles.z;//rot0 der
                    txt_panelVolti.text = "";
                    ItemsVolti[14].SetActive(false);
                    //**********************************************************************
                    cajaBloqueoTapaRot0 = Items[18].transform.localEulerAngles;//tapa de caja de bloqueo*************17-06-25
                    cajaBloqueoTapaPos0 = Items[18].transform.localPosition;    
                    for (int i = 0; i < LucesLEDCaja.Length; i++)
                    {
                        LucesLEDCaja[i].SetActive(false);
                    }
                    LucesLEDCaja[0].SetActive(true);
                    LucesLEDCaja[2].SetActive(true);
                    LucesLEDCaja[4].SetActive(true);
                    LucesLEDCaja[6].SetActive(true);
                    //***********************Preparativos para bloqueo de camion***************
                    llaveArranque[0].SetActive(false);
                    TimonRot0 = Timon[1].transform.localEulerAngles;
                    
                    Tablero_Indicaciones[0].SetActive(true);//panel de bienvenido
                    /*   //audioManager de bienvenida

                    yield return new WaitForSeconds(0.5f);
                    manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                    manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                    */
                    //aSource.MusicaVol(0.75f);//**************************************Sonido Musica Inicial*************
                    aSource.FxVol(0.6f);
                    //yield return new WaitForSecondsRealtime(3f);
                    
                    aSource.goFx("Locu_intro");
                    if (FullSonidos == true)//********************************Agregado 02-09-25************************
                    {
                        yield return new WaitForSecondsRealtime(20f);//********************
                        aSource.goFx("Locu_Parte1");
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

                    Tacos[2].SetActive(true);
                    Tacos[3].SetActive(true);
                    Tablero_Indicaciones[1].SetActive(true);//Locucion para panel intro de P1
                                                            //Flechas[0].SetActive(true);
                                                            //aros_indicadores[0].SetActive(true);
                    break;
                case 1://Cierre de palancas
                    Tablero_Indicaciones[3].SetActive(true);//P2
                    Palancas[1].SetActive(true);
                    Palancas[3].SetActive(true);

                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 2://LOTO
                    Tablero_Indicaciones[5].SetActive(true);//P3
                    Tablero_Indicaciones[3].SetActive(false);//P2
                    Tablero_Indicaciones[0].SetActive(false);//PBIENVENIDA
                    Tablero_Indicaciones[1].SetActive(false);//PBIENVENIDA
                    Tablero_Indicaciones[2].SetActive(false);//PBIENVENIDA
                    Items[0].SetActive(true);//candadoAmarillo refe
                    Items[18].transform.localEulerAngles = new Vector3(0, 0, 0);
                    Items[18].transform.localPosition = new Vector3(cajaBloqueoTapaPos0.x, cajaBloqueoTapaPos0.y, cajaBloqueoTapaPos0.z);
                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 3://verificacion de no arranque
                    if (Tablero_Indicaciones[7].activeInHierarchy == true)
                    {
                        Tablero_Indicaciones[7].SetActive(false);//Panel error de subida
                    }
                    Tablero_Indicaciones[3].SetActive(false);
                    Tablero_Indicaciones[5].SetActive(false);
                    Tablero_Indicaciones[8].SetActive(true);
                    llaveArranque[0].SetActive(true);
                    Timon[0].SetActive(true);
                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 4://gabinete frenos
                    Tablero_Indicaciones[6].SetActive(false);
                    Tablero_Indicaciones[10].SetActive(true);
                    Tablero_Indicaciones[8].SetActive(false);
                    Muros[2].SetActive(false);
                    V_NV1[0].SetActive(true);
                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 5://verificacion pedal
                    Tablero_Indicaciones[9].SetActive(false);
                    Muros[3].SetActive(true);//muro que evita pedal
                    Tablero_Indicaciones[10].SetActive(false);
                    Tablero_Indicaciones[12].SetActive(true);
                    btn_Pedal.SetActive(true);

                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 6://liberacion acumulador auxiliar
                    Muros[3].SetActive(false);//muro que evita pedal
                    Tablero_Indicaciones[12].SetActive(false);
                    Tablero_Indicaciones[14].SetActive(true);
                    acumuladorAux[0].SetActive(true);
                    escalera[4].SetActive(false);//mesh
                    escalera[0].SetActive(true);//refe
                    escalera[2].SetActive(true);//obj
                    tareaHecha = false;
                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 7://verificacion override
                    Tablero_Indicaciones[16].SetActive(true);
                    Tablero_Indicaciones[14].SetActive(false);

                    tareaHecha=false;
                    Override[0].SetActive(true);
                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 8://verificacion con voltimetro
                    ItemsVolti[14].SetActive(true);
                    Tablero_Indicaciones[18].SetActive(true);
                    Tablero_Indicaciones[16].SetActive(false);
                    Tablero_Indicaciones[22].SetActive(true);
                    auxContacto = 0;
                    contactoIntAux = 0;
                    yield return new WaitForSecondsRealtime(5);
                    btn_AbrirGabinetePotencia.SetActive(true);

                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 9://conclusiones
                    UI_btn_Continuar_Panel.SetActive(false);
                    Tablero_Indicaciones[19].SetActive(false);
                    Tablero_Indicaciones[20].SetActive(true);
                    yield return new WaitForSecondsRealtime(12f);
                    UI_btn_Continuar_Panel.SetActive(true);
                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 10://final
                    Muros[5].SetActive(false);//muro que evita bajar escaleras diagonales
                    aSource.goFx("fanfarrias");
                    aSource.goFx("aplausos");
                    UI_btn_Reiniciar_Panel.SetActive(false);
                    UI_btn_Salir_Panel.SetActive(false);    
                    Tablero_Indicaciones[21].SetActive(true);
                    Tablero_Indicaciones[20].SetActive(false);
                    yield return new WaitForSecondsRealtime(5);
                    UI_btn_Reiniciar_Panel.SetActive(true);
                    yield return new WaitForSecondsRealtime(5);
                    UI_btn_Salir_Panel.SetActive(true);
                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
            }
        }
    }
    //**********************VERIFICAR CONTACTO OBJETO A OBJETO****************************************
    public void verificarContacto(int confirmarcontacto)
    {
        switch (confirmarcontacto)
        {
            case 0://contactoTacoGRab00
                Debug.Log("confirmarcontacto : 0 auxcontacto=" + auxContacto);
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    Tacos[0].SetActive(false);
                    Tacos[contactoIntAux + 2].SetActive(false);
                    Tacos[contactoIntAux + 4].SetActive(true);
                    aSource.goFx("4Puerta_Corrediza_Alto");
                    if (Tacos[0].activeInHierarchy == false && Tacos[1].activeInHierarchy == false)
                    {
                        Tacos[6].SetActive(true);
                        Tacos[7].SetActive(true);
                        if (si_ModuloEvaluación == false)//*********************************************04-09-25*************
                        {
                            Tablero_Indicaciones[2].SetActive(true);
                            Tablero_Indicaciones[1].SetActive(false);//panelP1
                            Tablero_Indicaciones[0].SetActive(false);//panel bienvenida
                        }
                        StartCoroutine(TiempoEsperaTarea(0,6,43));//************************************************COMPLETA TAREA 0
                        aSource.goFx(aSource.FxSonidos[21].nombre);
                        aSource.goFx(aSource.FxSonidos[23].nombre);
                        if (ModuloEvaluacion != null)//*********************************************04-09-25*************
                        {
                            ModuloEvaluacion.GetComponent<EV_Bloqueo_C390E5>().Si_TareasHecha[0]=true;
                        }
                    }
                }
                break;
            case 1://contactoTacograb01
                Debug.Log("confirmarcontacto 1 : auxcontacto=" + auxContacto);
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    Tacos[1].SetActive(false);
                    Tacos[contactoIntAux + 2].SetActive(false);
                    Tacos[contactoIntAux + 4].SetActive(true);
                    aSource.goFx("4Puerta_Corrediza_Alto");
                    if (Tacos[0].activeInHierarchy == false && Tacos[1].activeInHierarchy == false)
                    {
                        Tacos[6].SetActive(true);
                        Tacos[7].SetActive(true);
                        if (si_ModuloEvaluación == false)//*********************************************04-09-25*************
                        {
                            Tablero_Indicaciones[2].SetActive(true);
                            Tablero_Indicaciones[1].SetActive(false);//panelP1
                            Tablero_Indicaciones[0].SetActive(false);//panel bienvenida
                        }
                        StartCoroutine(TiempoEsperaTarea(0, 6, 43));//************************************************COMPLETA TAREA 0
                        aSource.goFx(aSource.FxSonidos[21].nombre);
                        aSource.goFx(aSource.FxSonidos[23].nombre);
                        if (ModuloEvaluacion != null)//*********************************************04-09-25*************
                        {
                            ModuloEvaluacion.GetComponent<EV_Bloqueo_C390E5>().Si_TareasHecha[1] = true;
                        }
                    }
                }
                break;
            case 2://candado amarillo obj->caobj
                Debug.Log("confirmarcontacto 2 colocar candado amarillo");
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    Items[0].SetActive(false);//refe ca
                    Items[1].SetActive(false);//caobj
                    Items[2].SetActive(true);//CAMesh
                    Items[3].SetActive(true);//refe tarjeta amarillo->ta
                    Items[8].SetActive(true);//llave TA mesh
                }
                break;
            case 3://colocar ta
                Debug.Log("tarjeta amarilla confirmarcontacto 3 : auxcontacto=" + auxContacto);
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    Items[3].SetActive(false);//refe ta
                    Items[4].SetActive(false);//taobj
                    Items[5].SetActive(true);//TAMesh
                    Items[6].SetActive(true);//refe llave
                    Items[7].SetActive(true);//llave ca obj
                    Items[8].SetActive(false);//llave TA mesh
                }
                break;
            case 4://detector contacto llave con caja
                if (contacto_confirmado[confirmarcontacto] == true && si_LlaveEnMano == false)
                {
                    aSource.goFx("Perno_Contacto");
                    Items[7].GetComponent<XRGrabInteractable>().enabled = false;
                    Items[7].GetComponent<BoxCollider>().enabled = false;
                    //Items[9].SetActive(true);//candado personal refe -> pl
                    Debug.Log("*****llave en caja confirmarcontacto " + confirmarcontacto + " : auxcontacto=" + auxContacto+" box collider:"+Items[7].GetComponent<BoxCollider>().enabled);
                }
                break;
            case 5://caja bloqueo cerrada
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    if (contacto_confirmado[4] == true)
                    {
                        Items[18].transform.localEulerAngles = new Vector3(0, 0, 0);
                        Items[18].transform.localPosition = new Vector3(cajaBloqueoTapaPos0.x, cajaBloqueoTapaPos0.y, cajaBloqueoTapaPos0.z);
                        auxContacto = 7;
                        objRGBDActived(false);
                        Items[7].GetComponent<XRGrabInteractable>().enabled = false;
                        Items[7].GetComponent<BoxCollider>().enabled = false;
                        Items[9].SetActive(true);//candado personal refe -> pl
                        Items[17].GetComponent<XRGrabInteractable>().enabled = false;
                        Items[18].transform.localEulerAngles.Set(0, 0, 0);
                        Debug.Log("Caja cerrada*************************************************************");
                    }
                }
                break;
            case 6://colocar Candado Rojo
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    Items[7].SetActive(false);//llave CA
                    Items[9].SetActive(false);//cr refe
                    Items[10].SetActive(false);// cr obj
                    Items[11].SetActive(true);//cr mesh
                    Items[12].SetActive(true);//tp refe
                    Items[16].SetActive(true);//tp refe
                }
                break;
            case 7://colocar tarjeta personal TP
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    Items[12].SetActive(false);//ta refe
                    Items[13].SetActive(false);//ta obj
                    Items[14].SetActive(true);//ta mesh
                    Items[15].SetActive(true);//llave candadopersonal refe

                }
                break;
            case 8://sonido Gabinete de Frenos***********03-07-25
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    if (TareaActual == 4 && Tablero_Indicaciones[11].activeInHierarchy==false)
                    {
                        Muros[2].SetActive(true);
                        Debug.Log("Muro de Frenos activado");
                    }
                    else
                    {
                        Muros[2].SetActive(false);
                        Debug.Log("Muro de Frenos desactivado");
                    }
                    if (/*si_PuertaCabinaCerrada == false &&*/ TareaActual > 0)//cerrando
                    {
                        Debug.Log(confirmarcontacto + " auxcontacto=" + auxContacto + " cerrando");
                        /*perillaPuertaCabinaOBJ[0].GetComponent<Transform>().Equals(pos0PerillasPCabina[0]);
                        perillaPuertaCabinaOBJ[1].GetComponent<Transform>().Equals(pos0PerillasPCabina[1]);*/
                        aSource.goFx("PuertaCabinaCerrando");
                        //si_PuertaCabinaCerrada = true;
                        //aSource.MusicaVol(0.1f);
                    }
                }
                else
                {
                    if (TareaActual == 4)
                    {
                        Muros[2].SetActive(true);
                        Debug.Log("Muro de Frenos activado");
                    }
                    else
                    {
                        Muros[2].SetActive(false);
                        Debug.Log("Muro de Frenos desactivado");
                    }
                }
                break;
            case 9://escalera obj en refe
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    escalera[0].SetActive(false);//refe1
                    escalera[2].transform.localPosition = new Vector3(escalera[0].transform.localPosition.x, escalera[0].transform.localPosition.y, escalera[0].transform.localPosition.z);
                    escalera[2].transform.localEulerAngles = new Vector3(escalera[0].transform.localEulerAngles.x, escalera[0].transform.localEulerAngles.y, escalera[0].transform.localEulerAngles.z);
                    escalera[1].SetActive(true);//subible
                    escalera[2].SetActive(false);//obj
                    Tablero_Indicaciones[13].SetActive(false);
                    aSource.goFx("4Puerta_Corrediza_Alto");
                }
                break;
            case 10://detecta pj en el suelo
                if (tareaHecha)
                {
                    escalera[2].transform.SetParent(escalera[1].transform);
                    escalera[2].transform.localPosition = new Vector3(0, 0, 0);
                    escalera[2].transform.localEulerAngles = new Vector3(0, 0, 0);
                    escalera[2].GetComponent<Rigidbody>().isKinematic = true;
                    escalera[2].GetComponent<Rigidbody>().useGravity = false;
                    escalera[2].transform.SetParent(CamionC930.transform);
                    if (contacto_confirmado[confirmarcontacto] == true)
                    {
                        escalera[2].SetActive(false);//escalera obj
                        escalera[5].SetActive(false);//escalera refe agarre

                    }
                    else
                    {
                        escalera[2].SetActive(true);
                        escalera[5].SetActive(true);//escalera refe agarre
                    }
                }
                break;
            case 11://colocar la escalera en su sitio
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    escalera[3].SetActive(false);//refe2
                    escalera[4].SetActive(true);//mesh
                    escalera[2].SetActive(false);//obj
                    aSource.goFx("4Puerta_Corrediza_Alto");
                    aSource.goFx("Bien");
                    aSource.goFx("Locu_Bien");
                    Tablero_Indicaciones[15].SetActive(true);//panel de victoria acu aux
                    StartCoroutine(TiempoEsperaTarea(6, 6, 43));//*******************************************************************fin de acumulador aux TAREA 6
                }
                break;
            case 12://refe volti
                if (contacto_confirmado[confirmarcontacto] == true/*&&si_voltimetro_en_mano==false*/)//*******21-07-25
                {
                    ItemsVolti[12].SetActive(false);//mesh izq
                    ItemsVolti[13].SetActive(false);//mesh der
                    ItemsVolti[3].SetActive(false);//refe volti
                    ItemsVolti[4].SetActive(false);//obj volti
                    ItemsVolti[0].SetActive(false);//refe caja
                    ItemsVolti[1].SetActive(true);//caja cerrada
                    ItemsVolti[2].SetActive(false);//caja abierta
                    
                    
                    ItemsVolti[6].SetActive(true);//manija refe izq
                    ItemsVolti[7].SetActive(true);//manija refe izq
                    ItemsVolti[15].SetActive(true);//detector conclusiones
                }
                break;
            case 13://sonido Puerta Cabina
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    if (si_PuertaCabinaCerrada == false && TareaActual > 0)//cerrando
                    {
                        Debug.Log(confirmarcontacto + " auxcontacto=" + auxContacto + " cerrando");
                        /*perillaPuertaCabinaOBJ[0].GetComponent<Transform>().Equals(pos0PerillasPCabina[0]);
                        perillaPuertaCabinaOBJ[1].GetComponent<Transform>().Equals(pos0PerillasPCabina[1]);*/
                        aSource.goFx("PuertaCabinaCerrando");
                        si_PuertaCabinaCerrada = true;
                        if (si_PJEnCabina == true)
                        {
                            aSource.MusicaVol(0.1f);
                        }
                        else
                        {
                            aSource.MusicaVol(0.6f);
                        }
                    }
                }
                else
                {
                    if (si_PuertaCabinaCerrada == true && TareaActual > 0)//abriendo
                    {
                        /*perillaPuertaCabinaOBJ[0].GetComponent<Transform>().Equals(pos0PerillasPCabina[0]);
                        perillaPuertaCabinaOBJ[1].GetComponent<Transform>().Equals(pos0PerillasPCabina[1]);*/
                        Debug.Log(confirmarcontacto + " auxcontacto=" + auxContacto + " Abriendo");

                        si_PuertaCabinaCerrada = false;
                        aSource.goFx("PuertaCabinaAbriendo");
                        aSource.MusicaVol(0.6f);
                    }
                }
                break;
            case 14://Si Pj dentro de cabina
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    si_PJEnCabina = true;
                }
                else
                {
                    si_PJEnCabina = false;
                }
                break;
            case 15://detector de subida a cabina
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    if (TareaActual < 3 && Tablero_Indicaciones[6].activeInHierarchy==false)
                    {
                        Tablero_Indicaciones[7].SetActive(true);
                        aSource.goFx("Fallo");
                        aSource.goFx("Locu_Fallo");
                    }
                    else { Muros[1].SetActive(false);
                        Tablero_Indicaciones[7].SetActive(false);
                    }
                }
                break;
            case 16://nodo izq
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    
                    valorNodo[0] = contactoIntAux;
                    NodosContactoIzq[contactoIntAux] = true;
                    Debug.Log("izquierda en valorNodo[0] : " + contactoIntAux+ "NodosContactoIzq["+contactoIntAux+"]="+ NodosContactoIzq[contactoIntAux]);
                    if (contacto_confirmado[17] == true && Tablero_Indicaciones[19].activeInHierarchy == false)
                    {
                        si_circuitoCompleto=true;
                        Debug.Log("Verificado en IZQ; si_circuitoCompleto=" + si_circuitoCompleto);
                        verificacionNodo(contactoIntAux);
                    }
                    /*else
                    {
                        txt_panelVolti.text = "";//si solo se muestra cuando se hace contacto
                        NodosContactoIzq[contactoIntAux] = false;
                        si_circuitoCompleto = false;
                    }*/
                    /*Debug.Log(auxContacto +"= auxcontacto - nodo izq en : " + contactoIntAux);
                    auxContacto += contactoIntAux;
                    if (contactoIntAux == 0 || contactoIntAux == 4)
                    {
                        Debug.Log(auxContacto + "= auxcontacto - nodo izq en : " + contactoIntAux + " negativo");
                        si_OrdenPositivo = false;
                    }
                    if (contacto_confirmado[17] == true&&auxContacto>4)
                    {
                        Debug.Log(auxContacto + "= auxcontacto - nodo izq en : " + contactoIntAux + " negativo, circuito completo");
                        si_circuitoCompleto = true;
                        verificacionNodo(auxContacto);
                    }
                } else
                {
                    Debug.Log(auxContacto + "= auxcontacto - nodo izq fuera de : " + contactoIntAux);
                    si_circuitoCompleto = false;
                    auxContacto -= contactoIntAux;
                    Debug.Log(auxContacto + "= auxcontacto -  nodo izq fuera de : " + contactoIntAux);*/
                }
                else
                {
                    NodosContactoIzq[contactoIntAux] = false;
                    si_circuitoCompleto = false;
                    Debug.Log("Salio de IZQ; si_circuitoCompleto=" + si_circuitoCompleto);
                }
                if (si_circuitoCompleto == false)
                {
                    txt_panelVolti.text = "";
                }
                break;
            case 17://nodo der//********************015*17*25
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    valorNodo[1] = contactoIntAux;
                    /*auxContacto += contactoIntAux;
                    if (contactoIntAux == 0 || contactoIntAux == 4)
                    {
                        Debug.Log(auxContacto + "= auxcontacto - nodo derecho en : " + contactoIntAux+ " positivo");
                        si_OrdenPositivo = true;
                    }
                    if (contacto_confirmado[16] == true && auxContacto > 4)
                    {
                        Debug.Log(auxContacto + "= auxcontacto - nodo derecho en : " + contactoIntAux + " positivo, circuito completo" );
                        si_circuitoCompleto = true;
                        verificacionNodo(auxContacto);
                    }
                }
                else {
                    Debug.Log(auxContacto + "= auxcontacto - nodo derecho fuera de : " + contactoIntAux);
                    si_circuitoCompleto = false;
                    auxContacto -= contactoIntAux;
                    Debug.Log(auxContacto + "= auxcontacto -  nodo derecho fuera de : " + contactoIntAux);*/
                    NodosContactoDer[contactoIntAux] = true;
                    Debug.Log("derecha en valorNodo[1] : " + contactoIntAux + "NodosContactoDer[" + contactoIntAux + "]=" + NodosContactoDer[contactoIntAux]);
                    if (contacto_confirmado[16] == true&& Tablero_Indicaciones[19].activeInHierarchy==false)
                    {
                        si_circuitoCompleto = true;
                        Debug.Log("Verificado en Der; si_circuitoCompleto=" + si_circuitoCompleto);
                        verificacionNodo(contactoIntAux);
                    }
                    /*else
                    {
                        txt_panelVolti.text = "";//si solo se muestra cuando se hace contacto
                        NodosContactoDer[contactoIntAux] = false;
                        si_circuitoCompleto = false;
                        Debug.Log("Salio de Der; si_circuitoCompleto=" + si_circuitoCompleto);
                    }
                    Debug.Log(auxContacto +"= auxcontacto - nodo izq en : " + contactoIntAux);
                    auxContacto += contactoIntAux;
                    if (contactoIntAux == 0 || contactoIntAux == 4)
                    {
                        Debug.Log(auxContacto + "= auxcontacto - nodo izq en : " + contactoIntAux + " negativo");
                        si_OrdenPositivo = false;
                    }
                    if (contacto_confirmado[17] == true&&auxContacto>4)
                    {
                        Debug.Log(auxContacto + "= auxcontacto - nodo izq en : " + contactoIntAux + " negativo, circuito completo");
                        si_circuitoCompleto = true;
                        verificacionNodo(auxContacto);
                    }
                } else
                {
                    Debug.Log(auxContacto + "= auxcontacto - nodo izq fuera de : " + contactoIntAux);
                    si_circuitoCompleto = false;
                    auxContacto -= contactoIntAux;
                    Debug.Log(auxContacto + "= auxcontacto -  nodo izq fuera de : " + contactoIntAux);*/
                }
                else
                {
                    NodosContactoDer[contactoIntAux] = false;
                    si_circuitoCompleto = false;
                    Debug.Log("Salio de Der; si_circuitoCompleto=" + si_circuitoCompleto);
                }
                if (si_circuitoCompleto == false)
                {
                    txt_panelVolti.text = "";
                }
                break;
                case 18:
                TareaCompletada(8);//******************************************************fin de TAREA 8
                ItemsVolti[15].SetActive(false);//detector conclusiones
                break;
        }
    }
    public void activacionXR(int contacto) //contacto con manos
    {
        switch (contacto)
        {
            case 0://verifica 
                switch (auxContacto)
                {
                    case 0://propel
                        Debug.Log("Auxcontacto:" + auxContacto + " contacto:" + contacto + " rotActual:" + Palancas[auxContacto].transform.localEulerAngles.x);
                        Palancas[2 * auxContacto + 1].SetActive(false);
                        Palancas[auxContacto].transform.localEulerAngles = new Vector3(rotPalancas[auxContacto], Palancas[auxContacto].transform.localEulerAngles.y, Palancas[auxContacto].transform.localEulerAngles.z);
                        Debug.Log("Auxcontacto:" + auxContacto + " contacto:" + contacto + " rotActual:" + Palancas[auxContacto].transform.localEulerAngles.x);
                        LucesLEDCaja[auxContacto].SetActive(false);
                        LucesLEDCaja[auxContacto + 1].SetActive(true);
                        if (nPalancasOff == 0)
                        {
                            StartCoroutine(ActivarObjxTiempo(Palancas[5]));
                            StartCoroutine(DeactivarObjxTiempo(LucesLEDCaja[6]));
                        }
                        nPalancasOff++;
                        if (ModuloEvaluacion != null)//*********************************************04-09-25*************
                        {
                            ModuloEvaluacion.GetComponent<EV_Bloqueo_C390E5>().Si_TareasHecha[2] = true;//************************EV_Bloqueo.Si_tareaHecha[2]
                        }
                        if (nPalancasOff == 3)
                        {
                            StartCoroutine(TiempoEsperaTarea(1, 5, 44));//***********************************************************COMPLETA TAREA 1
                            aSource.goFx(aSource.FxSonidos[21].nombre);
                            aSource.goFx(aSource.FxSonidos[23].nombre);
                            if (si_ModuloEvaluación == false)//*********************************************04-09-25*************
                            {
                                Tablero_Indicaciones[4].SetActive(true);
                            }
                        }
                        break;
                    case 1://starter
                        Debug.Log("Auxcontacto:" + auxContacto + " contacto:" + contacto + " rotActual:" + Palancas[2].transform.localEulerAngles.x);
                        Palancas[2 * auxContacto + 1].SetActive(false);
                        Palancas[2].transform.localEulerAngles = new Vector3(0, 0, 0);
                        Debug.Log("Auxcontacto:" + auxContacto + " contacto:" + contacto + " rotActual:" + Palancas[2].transform.localEulerAngles.x);
                        LucesLEDCaja[2 * auxContacto].SetActive(false);
                        LucesLEDCaja[2 * auxContacto + 1].SetActive(true);
                        if (nPalancasOff == 0)
                        {
                            StartCoroutine(ActivarObjxTiempo(Palancas[5]));
                            StartCoroutine(DeactivarObjxTiempo(LucesLEDCaja[6]));
                        }
                        nPalancasOff++;
                        if (ModuloEvaluacion != null)//*********************************************04-09-25*************
                        {
                            ModuloEvaluacion.GetComponent<EV_Bloqueo_C390E5>().Si_TareasHecha[3] = true;//************************EV_Bloqueo.Si_tareaHecha[3]
                        }
                        
                        if (nPalancasOff == 3)
                        {
                            StartCoroutine(TiempoEsperaTarea(1, 5, 44));//***********************************************************COMPLETA TAREA 1
                            aSource.goFx(aSource.FxSonidos[21].nombre);
                            aSource.goFx(aSource.FxSonidos[23].nombre);
                            if (si_ModuloEvaluación == false)//*********************************************04-09-25*************
                            {
                                Tablero_Indicaciones[4].SetActive(true);
                            }
                            else
                            {
                                Items[0].SetActive(true);
                            }
                        }
                        break;
                    case 2://Master
                        Debug.Log("Auxcontacto:" + auxContacto + " contacto:" + contacto + " rotActual:" + Palancas[4].transform.localEulerAngles.x);
                        Palancas[2 * auxContacto + 1].SetActive(false);
                        Palancas[4].transform.localEulerAngles = new Vector3(0, 0, 0);
                        Debug.Log("Auxcontacto:" + auxContacto + " contacto:" + contacto + " rotActual:" + Palancas[4].transform.localEulerAngles.x);
                        LucesLEDCaja[2 * auxContacto].SetActive(false);
                        LucesLEDCaja[2 * auxContacto + 1].SetActive(true);
                        LucesLEDCaja[1].SetActive(false);
                        LucesLEDCaja[3].SetActive(false);
                        nPalancasOff++;
                        if (ModuloEvaluacion != null)//*********************************************04-09-25*************
                        {
                            ModuloEvaluacion.GetComponent<EV_Bloqueo_C390E5>().Si_TareasHecha[2] = true;
                        }
                        if (nPalancasOff == 3)
                        {
                            StartCoroutine(TiempoEsperaTarea(1, 5, 44));//***********************************************************COMPLETA TAREA 1
                            aSource.goFx(aSource.FxSonidos[21].nombre);
                            aSource.goFx(aSource.FxSonidos[23].nombre);
                            Tablero_Indicaciones[4].SetActive(true);
                        }
                        break;
                }
                break;
            case 1:
                aSource.goFx("Agarrar");
                Items[8].SetActive(false);//llave TA mesh
                Items[6].SetActive(false);//refe llave
                break;
            case 2:
                Items[15].SetActive(false);//llave candado personal refe
                Items[16].SetActive(false);//llave candado personal mesh
                Tablero_Indicaciones[6].SetActive(true);
                Tablero_Indicaciones[4].SetActive(false);
                Tablero_Indicaciones[5].SetActive(false);
                aSource.goFx("Bien");
                aSource.goFx("Locu_Bien");
                StartCoroutine(TiempoEsperaTarea(2, 6, 43));//*********************************************************************fin de tarea 2
                break;
            case 3:
                activarLlaveArranque();
                break;
            case 4://girando NV1
                GiroFrenoAnim(true);
                break;
            case 5://girando NV2
                GiroFrenoAnim(false);
                break;
            case 6:
                Muros[3].SetActive(false);
                AccionPedal();
                
                break;
            case 7://
                if (tareaHecha == true)
                {
                    //escalera[2].GetComponent<Transform>().position = new Vector3(escalera[0].transform.position.x, escalera[0].transform.position.y, escalera[0].transform.position.z);
                    escalera[1].SetActive(false);//subible
                }
                break;
            case 8://activacion animacion acu aux
                if (tareaHecha == false)
                {
                    StartCoroutine(AcuAux());
                }
                else
                {
                    acumuladorAux[0].SetActive(false);
                    aSource.goFx("Bien");
                    aSource.goFx("Locu_Bien");
                    escalera[3].SetActive(true);//refe 2
                }

                break;
            case 9://para agarra la escalera despues de tarea
                if (tareaHecha == true && escalera[1].activeInHierarchy == true)
                {
                    escalera[1].SetActive(false);//subible
                    escalera[2].GetComponent<Rigidbody>().isKinematic = false;//obje
                    escalera[2].GetComponent<Rigidbody>().useGravity = true;//obj
                }
                break;
            case 10://override
                if (tareaHecha == false)
                {
                    AccionOverride();
                }
                else
                {
                    aSource.goFx("Bien");
                    aSource.goFx("Locu_Bien");
                    Override[0].SetActive(false);//refe
                    Tablero_Indicaciones[17].SetActive(true);
                    StartCoroutine(TiempoEsperaTarea(7,5,47));//***********************************fin de tarea7
                }
                break;
            case 11://abrir gabinete
                StartCoroutine(animAbrirGPotencia());
                Muros[4].SetActive(true);//muro que evita bajar escaleras diagonales
                    break;
            case 12://abrir caja
                ItemsVolti[1].SetActive(false);//case cerrado
                ItemsVolti[2].SetActive(true);//case abierto
                ItemsVolti[4].SetActive(true);//voltimetro obj
                ItemsVolti[8].SetActive(true);//nodo condesandor A0
                ItemsVolti[9].SetActive(true);//nodo condesandor A1
                ItemsVolti[10].SetActive(true);//nodo condesandor B0
                ItemsVolti[11].SetActive(true);//nodo condesandor B1
                
                ItemsVolti[8].GetComponent<MeshRenderer>().enabled=true;//nodo condesandor A0
                //ItemsVolti[9].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor A1
                ItemsVolti[10].GetComponent<MeshRenderer>().enabled = true;//nodo condesandor b0
                //ItemsVolti[11].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor b1
                
                aSource.goFx("Bien");
                break;
            case 13://puerta izq g potencia
                PuertasGPotencia[0].transform.localEulerAngles = new Vector3(0, 0, RotPuertaGPontencia[0]);
                ItemsVolti[6].SetActive(false);//manija refe izq
                aSource.goFx("Bien");
                if (ItemsVolti[7].activeInHierarchy == false)
                {
                    ItemsVolti[15].SetActive(true);//detector conclusiones
                    Muros[5].SetActive(true);//muro que evita bajar escaleras diagonales
                    aSource.goFx("Locu_Bien");
                    Muros[4].SetActive(false);//muro que evita bajar escaleras diagonales
                }
                break;
            case 14://puerta der g potencia
                PuertasGPotencia[1].transform.localEulerAngles = new Vector3(0, 0, RotPuertaGPontencia[2]);
                ItemsVolti[7].SetActive(false);//manija refe izq
                aSource.goFx("Bien");
                if (ItemsVolti[6].activeInHierarchy == false)
                {
                    ItemsVolti[15].SetActive(true);//detector conclusiones
                    Muros[5].SetActive(true);//muro que evita bajar escaleras diagonales
                    aSource.goFx("Locu_Bien");
                    Muros[4].SetActive(false);//muro que evita bajar escaleras diagonales
                }
                break;
            case 15:
                if (TareaActual == 4 && Tablero_Indicaciones[11].activeInHierarchy == false)
                {
                    Muros[2].SetActive(true);
                }
                else
                {
                    Muros[2].SetActive(false);
                }
                    
                break;
            case 17://bt continuar
                TareaCompletada(9);
                break;
            case 18://boton reinicio
                IrEscenaAsincron(0);
                Debug.Log("reinicio");
                break;
            case 19://boton SALIR
                Debug.Log("salir");
                Application.Quit();
                break;
        }
    }
    public void setAuxContacto(int n)
    {
        auxContacto = n;
    }
    public IEnumerator ActivarObjxTiempo(GameObject obj)
    {
        Debug.Log("en 5s Activando obj =" + obj.name);
        yield return new WaitForSecondsRealtime(5);
        obj.SetActive(true);
    }
    public IEnumerator DeactivarObjxTiempo(GameObject obj)
    {
        Debug.Log("en 5s Deactivando obj =" + obj.name);
        yield return new WaitForSecondsRealtime(5);
        obj.SetActive(false);
    }
    public void activarItem(bool on)
    {
        Flechas[8].SetActive(on);
    }
    public void Verificar_llaveEnMano(bool si_) 
    {
        si_LlaveEnMano=si_;
    }
    public void Verificar_VoltiEnMano(bool si_)
    {
        si_voltimetro_en_mano = si_;
    }
    public IEnumerator TiempoXAudioXTarea(float t)//Agregar tiempo***********Agregado el 26-05-25***************************************
    {
        yield return new WaitForSeconds(1);
    }
    public IEnumerator TiempoEsperaAudio(float t)//Agregar tiempo***********Agregado el 26-05-25***************************************
    {
        string nAudioAux = NombreAuxAudio;
        int auxTarea = TareaActual;
        if (EnPruebas)
        {
            yield return new WaitForSecondsRealtime(t + 1);
            Debug.Log("Se espero " + t + "segundos - AuxAudio : " + NombreAuxAudio);
            while (auxTarea == TareaActual)
            {
                if (NombreAuxAudio != nAudioAux)
                {
                    aSource.goFx(NombreAuxAudio);
                    Debug.Log("Se espero y se reprodujo el audio " + NombreAuxAudio + " por seguir en la tarea auxiliar : " + auxTarea);
                    break;
                }
                else
                {
                    yield return new WaitForSecondsRealtime(5);
                    if (auxTarea != TareaActual)
                    {
                        NombreAuxAudio = "";
                        break;
                    }
                    aSource.goFx(NombreAuxAudio);
                }
                yield return new WaitForSeconds(10);
            }
            NombreAuxAudio = "";
        }
        else
        {
            aSource.goFx(NombreAuxAudio);
        }
    }
    public IEnumerator TiempoEsperaTarea(int tarea)//*******************************************Agregado el 26-05-25***************************************
    {
        if (si_ModuloEvaluación == false)
        {
            yield return new WaitForSecondsRealtime(tiempoEsperaAux + 1);
            Debug.Log("Se espero por la tarea " + tarea + "- tiempoEsperaAux : " + tiempoEsperaAux);
            if (TareaActual == tarea)
            {
                Debug.Log("Se completo la tarea " + tarea + "- dentro del tiempo tiempoEsperaAux : " + tiempoEsperaAux);
                TareaCompletada(tarea);
            }
        }
        else
        {
            ModuloEvaluacion.GetComponent<EV_Bloqueo_C390E5>().AccionTarea(tarea, 0, 0);
        }
    }
    public IEnumerator TiempoEsperaTarea(int tarea,float tiempo,int nAudio)//*******************************************Agregado el 26-05-25***************************************
    {
        if (si_ModuloEvaluación == false)
        {
            if (FullSonidos == true)
            {//para audio "Locu_Bien"
                yield return new WaitForSecondsRealtime(1.5f);//*********CAMBIADO PARA BLOQUEO EN INGLES***********1-09-25******************
            }
            else
            {
                yield return new WaitForSeconds(0);//*********CAMBIADO PARA BLOQUEO EN INGLES***********1-09-25******************
            }
            //yield return new WaitForSeconds(0);//*********CAMBIADO PARA BLOQUEO EN INGLES***********1-09-25******************
            aSource.goFx(aSource.FxSonidos[nAudio].nombre);
            yield return new WaitForSecondsRealtime(tiempo);

            Debug.Log("Se espero por la tarea " + tarea + "- tiempoEspera : " + tiempo);
            if (TareaActual == tarea)
            {
                Debug.Log("Se completo la tarea " + tarea + "- dentro del tiempo tiempoEspera : " + tiempo);
                TareaCompletada(tarea);
            }
        }
        else 
        {
            if (ModuloEvaluacion != null)
            {
                ModuloEvaluacion.GetComponent<EV_Bloqueo_C390E5>().AccionTarea(tarea, tiempo, nAudio);
            }
        }
    }
    public void OnOffBoxCollider(bool onOff)//ACTivar y DEactivar boxcollider//requiere dar AUXCONTACTO***03-06-25**************
    {
        Items[auxContacto].GetComponent<BoxCollider>().enabled = onOff;
    }
    /*public void reposTapaLockbox(bool si_pos0)
    {
        Items[17].GetComponent<BoxCollider>().isTrigger = !si_pos0;
        Items[18].GetComponent<Rigidbody>().isKinematic = !si_pos0;
        Items[18].GetComponent<Rigidbody>().useGravity = si_pos0;
        if (Items[18].transform.localEulerAngles.z < 0||Items[18].transform.localEulerAngles.x < 0)
        {
            Items[18].transform.localEulerAngles = new Vector3(0, 0, 0);
            Items[18].transform.localPosition = new Vector3(cajaBloqueoTapaPos0.x, cajaBloqueoTapaPos0.y,cajaBloqueoTapaPos0.z);
            Items[17].GetComponent<BoxCollider>().isTrigger = true;
            Items[18].GetComponent<Rigidbody>().isKinematic = false;
            Items[18].GetComponent<Rigidbody>().useGravity = true;
        }
        
    }*/
    public void objRGBDActived(bool si)//cuando agarra el llave
    {
        Debug.Log("ITEM " + (auxContacto) + "agarrado de activo el toggles de Rigidbody en funcion PernosRGBDActived");
        
        //Items[n].transform.SetParent(PuntoDeRecepccionPernos.transform);
        Items[auxContacto].GetComponent<BoxCollider>().enabled = si;
        Items[auxContacto].GetComponent<Rigidbody>().isKinematic = !si;
        Items[auxContacto].GetComponent<Rigidbody>().useGravity = si;
        //si_LlaveEnMano = !si;
    }
    public void llaveRGBDActived(bool si_)//***********19--08-25***********
    {
        Debug.Log("ITEM llave agarrado de activo el toggles de Rigidbody en funcion PernosRGBDActived");
            Items[7].GetComponent<BoxCollider>().enabled = true;
            Items[7].GetComponent<Rigidbody>().isKinematic = false;
            Items[7].GetComponent<Rigidbody>().useGravity = true;
        if (si_ == true)
        {
            Debug.Log("ITEM llave agarrado de activo el toggles de Rigidbody en funcion PernosRGBDActived");
        }
        else
        {
            Debug.Log("ITEM llave SOLTADO de activo el toggles de Rigidbody en funcion PernosRGBDActived");
        }
        si_LlaveEnMano = si_;
    }
    //**************************FUNCIONES DE TACOS************************************22-07-25
    public void OnFisicasOBJEscaleraObj(bool si_)
    {
        Items[19].GetComponent<Rigidbody>().isKinematic = !si_;//para escalera 3 peldaños
        Items[19].GetComponent <Rigidbody>().useGravity = si_;
    }
    public void OffFisicasOBJ(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.GetComponent<Rigidbody>().useGravity = false;
    }
    public void activeTriggerTacoIZQ(bool si_)
    {
        Tacos[0].GetComponent<BoxCollider>().isTrigger = si_;
    }
    public void activeTriggerTacoDer(bool si_)
    {
        Tacos[1].GetComponent<BoxCollider>().isTrigger = si_;
    }
    //*************************FUNCIONES DE VERIFICACION DE NO ARRANQUE*****TIMON Y LLAVE DE ARRANQUE******17-06-25********
    public void activarLlaveArranque()
    {
        llaveArranque[0].SetActive(false);
        llaveArranque[1].SetActive(false);
        StartCoroutine(AnimacionLlaveArranque());
    }
    public IEnumerator AnimacionLlaveArranque()
    {
        llaveArranque[2].SetActive(true);
        yield return new WaitForSeconds(3);
        nVerificionesArranque[0] = true;//llave completada
        if (nVerificionesArranque[0] == true && nVerificionesArranque[1] == true)
        {
            aSource.goFx("Bien");
            aSource.goFx("Locu_Bien");
            Tablero_Indicaciones[9].SetActive(true);
            StartCoroutine(TiempoEsperaTarea(3,6,45));//*************************************************************************completa tarea 3
        }
    }
    public void VerificacionTimonNoArranque()
    {
        si_timon_agarrado = true;
        Timon[0].SetActive(false);//refe
        Debug.Log("timon agarrado");
        StartCoroutine(VerificacionGiroTimon());
    }
    public IEnumerator VerificacionGiroTimon()//********************03-07-25***09-07-25**comprobacion pendiente
    {
        Debug.Log("iniciando verificion timon-> ROTLOCAL : " + Timon[1].transform.localRotation.z + " " + Timon[1].transform.localEulerAngles.z);
        while (TareaActual >= 3)
        {
            if (Timon[1].transform.localEulerAngles.z >= TimonRotDer )//el mayor 206
            {
                Debug.Log("giro timonde timon lado der : " + si_verificacionGiro[1]);
                aSource.goFx("4Puerta_Corrediza_Alto");
                if(si_verificacionGiro[1] == false)
                {
                    si_verificacionGiro[1] = true;
                }
            }
            if (Timon[1].transform.localEulerAngles.z <= TimonRotIzq)//el menor 173
            {
                Debug.Log("giro timonde timon lado izq : " + si_verificacionGiro[0]);
                aSource.goFx("4Puerta_Corrediza_Alto");
                if (si_verificacionGiro[0] == false)
                {
                    si_verificacionGiro[0] = true;
                }
            }
            if (si_verificacionGiro[0] == true && si_verificacionGiro[1] == true&& nVerificionesArranque[1] == false)
                {
                    nVerificionesArranque[1] = true;
                    aSource.goFx("Bien");
                    Debug.Log("saliendo de bucle por verificacion completada de ambos giros");
                }
            if (nVerificionesArranque[0] == true && nVerificionesArranque[1] == true && Tablero_Indicaciones[6].activeInHierarchy==true)
            {
                Debug.Log("saliendo de bucle por verificacion de no arranque completada");
                aSource.goFx("Bien");
                aSource.goFx("Locu_Bien");
                Tablero_Indicaciones[6].SetActive(false);
                Tablero_Indicaciones[9].SetActive(true);
                StartCoroutine(TiempoEsperaTarea(3,6,45));//************************************************completa tarea 3
                break;
            }
            if (si_timon_agarrado == false)
            {
                Debug.Log("saliendo de bucle por soltar timon");
                break;
            }
            yield return new WaitForSecondsRealtime(0.2f);
            Debug.Log("iniciando verificon timon-> ROTLOCAL : " + Timon[1].transform.localRotation.z + " " + Timon[1].transform.localEulerAngles.z);
            TimonRot0 = Timon[1].transform.localEulerAngles;
        }
    }
    public void si_Timon_verificado()//en el select exit
    {
        Debug.Log("deteniendo verificion timon");
        si_timon_agarrado = false;
        Timon[0].SetActive(true);
        if (si_verificacionGiro[0] == true && si_verificacionGiro[1] == true)
        {
            Timon[0].SetActive(false);
        }
    }
    //************************FUNCIONES DE GABINETE DE FRENOS*********************
    public void GiroFrenoAnim(bool si_NV1)
    {
        if (si_NV1)
        {
            if (si_Valvula_Liberada[0] == true && si_Valvula_Cerrada[0] == true)
            {
                if(si_Valvula_Liberada[1] == false && si_Valvula_Cerrada[1] == false)
                {
                    V_NV1[0].SetActive(false);//por si se desea evaluar este error
                    V_NV2[0].SetActive(true);
                }
                if (si_Valvula_Liberada[0] == true && si_Valvula_Liberada[1] == true && si_Valvula_Cerrada[0] == true && si_Valvula_Cerrada[1] == true)
                {
                    aSource.goFx("Bien");
                    aSource.goFx("Locu_Bien");
                    //yield return new WaitForSecondsRealtime(2);
                    Tablero_Indicaciones[11].SetActive(true);
                    V_NV1[0].SetActive(false);
                    StartCoroutine(TiempoEsperaTarea(4,6,43));//*********************************************fin de tarea 4
                }
            }
            else
            {
                StartCoroutine(GiroNV1());
            }
        }
        else
        {//*******09-07-25*********** cambio para verificar cerrado y ajustado de valvulas********
            if (si_Valvula_Liberada[1] == true && si_Valvula_Cerrada[1] == true)
            {
                if (si_Valvula_Liberada[0] == false && si_Valvula_Cerrada[0] == false)
                {
                    V_NV2[0].SetActive(false);//por si se desea evaluar este error
                    V_NV1[0].SetActive(true);
                }
                if (si_Valvula_Liberada[0] == true && si_Valvula_Liberada[1] == true && si_Valvula_Cerrada[0] == true && si_Valvula_Cerrada[1] == true)
                {
                    aSource.goFx("Bien");
                    aSource.goFx("Locu_Bien");
                    //yield return new WaitForSecondsRealtime(2);
                    V_NV2[0].SetActive(false);
                    Tablero_Indicaciones[11].SetActive(true);
                    if (contacto_confirmado[8] == true)
                    {
                        Muros[2].SetActive(false);
                    }
                    StartCoroutine(TiempoEsperaTarea(4,5,48));//*********************************************fin de tarea 4
                }
            }
            else
            {
                StartCoroutine(GiroNV2());
            }
        }
    }
    public IEnumerator GiroNV1()
    {
        if (si_Valvula_Liberada[0] == false)
        {
            V_NV1[3].SetActive(false);//MESH
            V_NV1[0].SetActive(false);
            V_NV1[1].SetActive(true);//ANIM1
            aSource.goFx("Liberacion_Acu", 1f, false, false);
            
            yield return new WaitForSecondsRealtime(10);
            si_Valvula_Liberada[0] = true;
            si_Valvula_Cerrada[0] = false;
            yield return new WaitForSecondsRealtime(.5f);
            V_NV1[0].SetActive(true);
        }
        else
        {
            V_NV1[0].SetActive(false);//REFE
            V_NV1[1].SetActive(false);//ANIM1
            V_NV1[2].SetActive(true);//ANIM2
            yield return new WaitForSecondsRealtime(10);
            si_Valvula_Cerrada[0] = true;
            yield return new WaitForSecondsRealtime(.5f);
            V_NV1[0].SetActive(true);
            /*if (si_Valvula_Liberada[1] == false)
            {
                V_NV2[0].SetActive(true);
            }
            if (si_Valvula_Liberada[0] == true && si_Valvula_Liberada[1] == true && si_Valvula_Cerrada[0] == true && si_Valvula_Cerrada[1]==true)
            {
                aSource.goFx("Bien");
                aSource.goFx("Locu_Bien");
                //yield return new WaitForSecondsRealtime(2);
                Tablero_Indicaciones[11].SetActive(true);
                StartCoroutine(TiempoEsperaTarea(4));
            }*/
        }
    }
    public IEnumerator GiroNV2()
    {
        if (si_Valvula_Liberada[1] == false)
        {
            V_NV2[3].SetActive(false);//MESH
            V_NV2[0].SetActive(false);
            V_NV2[1].SetActive(true);//ANIM1
            aSource.goFx("Liberacion_Acu",1f,false,false);
            yield return new WaitForSecondsRealtime(10);
            si_Valvula_Liberada[1] = true;
            si_Valvula_Cerrada[1] = false;
            yield return new WaitForSecondsRealtime(1);
            V_NV2[0].SetActive(true);
        }
        else
        {
            V_NV2[0].SetActive(false);//REFE
            V_NV2[1].SetActive(false);//ANIM1
            V_NV2[2].SetActive(true);//ANIM2
            yield return new WaitForSecondsRealtime(10);
            si_Valvula_Cerrada[1] = true;
            yield return new WaitForSecondsRealtime(1);
            
            V_NV2[0].SetActive(true);
        }
    }
    //************************FUNCIONES DE VERIFICACION DE PEDAL*******03-07-25**************
    public void AccionPedal()
    {
        aSource.goFx("boton");
        btn_Pedal.SetActive(false);
        StartCoroutine(pedalAnim());
    }
    public IEnumerator pedalAnim()
    {
        Pedales[0].SetActive(false);
        Pedales[1].SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        aSource.goFx("pedal");
        yield return new WaitForSecondsRealtime(2.5f);
        aSource.goFx("pedal");
        yield return new WaitForSecondsRealtime(3f);
        Pedales[1].SetActive(false);
        Pedales[0].SetActive(true);
        aSource.goFx("Bien");
        aSource.goFx("Locu_Bien");
        Tablero_Indicaciones[11].SetActive(false);
        Tablero_Indicaciones[13].SetActive(true);
        StartCoroutine(TiempoEsperaTarea(5,6,43));//**********************************************************************COMPLETA TAREA 5
    }
    //************************FUNCIONES DE Acumulador Auxiliar*******07-07-25***************************
    public IEnumerator AcuAux()
    {
        if (si_AcuAux_Liberado == false)
        {
            aSource.goFx("Liberacion_Acu_Aux");
            acumuladorAux[3].SetActive(false);//MESH
            acumuladorAux[0].SetActive(false);
            acumuladorAux[1].SetActive(true);//ANIM1
            yield return new WaitForSecondsRealtime(10);
            si_AcuAux_Liberado = true;
            //yield return new WaitForSecondsRealtime(.5f);
            acumuladorAux[0].SetActive(true);
        }
        else
        {
            acumuladorAux[0].SetActive(false);//REFE
            acumuladorAux[1].SetActive(false);//ANIM1
            acumuladorAux[2].SetActive(true);//ANIM1
            yield return new WaitForSecondsRealtime(10);
            //si_Valvula_Liberada[0] = true;
            //yield return new WaitForSecondsRealtime(1);
            tareaHecha=true;
            acumuladorAux[0].SetActive(true);//REFE

            //yield return new WaitForSecondsRealtime(2);
            //Tablero_Indicaciones[11].SetActive(true);
            //StartCoroutine(TiempoEsperaTarea(4));
        }
    }
    //************************FUNCIONES DE Override*******09-07-25***********************
    public void AccionOverride()
    {
        StartCoroutine(OverrideAnim());
    }
    public IEnumerator OverrideAnim()
    {
        if (si_Override_Liberado == false)
        {
            Override[0].transform.SetParent(Override[1].transform);
            Override[3].SetActive(false);//MESH
            Override[0].SetActive(false);
            Override[1].SetActive(true);//ANIM1
            yield return new WaitForSecondsRealtime(2.5f);
            si_Override_Liberado = true;
            tareaHecha = false;
            yield return new WaitForSecondsRealtime(1);
            Override[0].SetActive(true);
        }
        else
        {
            Override[0].transform.SetParent(Override[2].transform);
            Override[0].SetActive(false);//REFE
            Override[1].SetActive(false);//ANIM1
            Override[2].SetActive(true);//ANIM1
            yield return new WaitForSecondsRealtime(2.5f);
            Override[2].SetActive(false);//ANIM1
            Override[3].SetActive(true);//ANIM1
            Override[0].transform.SetParent(Override[3].transform);
            tareaHecha = true;
            yield return new WaitForSecondsRealtime(1);
            Override[0].SetActive(true);//REFE
        }
    }
    //************************FUNCIONES DE VOLTIMETRO*******17-06-25**************
    public IEnumerator animAbrirGPotencia()
    {
        btn_AbrirGabinetePotencia.SetActive(false);
        StartCoroutine(FadeOutIn(2f, 5f, 2f));
        yield return new WaitForSecondsRealtime(2f);
        aSource.goFx("Pistola_neumatica_Accion_cerrada");
        yield return new WaitForSecondsRealtime(1f);
        aSource.goFx("Pistola_neumatica_Accion_cerrada");
        yield return new WaitForSecondsRealtime(1f);
        aSource.goFx("Pistola_neumatica_Accion_cerrada");
        PuertasGPotencia[0].transform.localEulerAngles = new Vector3(0, 0, RotPuertaGPontencia[1]);
        PuertasGPotencia[1].transform.localEulerAngles = new Vector3(0, 0, RotPuertaGPontencia[3]);
        ItemsVolti[0].SetActive(true);
    }
    public void LlevarVoltimetro(bool si_der)//si se agarro con alguna mano****16.06-25
    {
        
        si_voltimetro_en_mano = true;
        if (si_voltimetroAgarrado[0] == false&& si_voltimetroAgarrado[1] == false)//primera ocacion
        {
            aSource.goFx("Agarrar");
            if (si_der==false)
            {
                quien_primero_agarre=0;//indica si padre = izq
                //Debug.Log("SE AGARRO PRIMERO: IZQUIERDO, DERECHO ES HIJO");
                si_voltimetroAgarrado[0] = true;
                NodosVoltimetro[1].transform.SetParent(NodosVoltimetro[0].transform);
                NodosVoltimetro[1].transform.localPosition.Set(NodoDerPos0.x, NodoDerPos0.y, NodoDerPos0.z);
                NodosVoltimetro[1].transform.localEulerAngles.Set(NodoDerRot0.x, NodoDerRot0.y, NodoDerRot0.z);
                //NodosVoltimetro[1].GetComponent<BoxCollider>().enabled = true;
                NodosVoltimetro[1].GetComponent<Rigidbody>().isKinematic = true;
                NodosVoltimetro[1].GetComponent<Rigidbody>().useGravity = false;
                NodosVoltimetro[1].GetComponent<Return_Pos0>().enabled = false;
                StartCoroutine(ReUbicacionEnJerarquia(true));//02*09*25

            }
            else
            {
                //Debug.Log("SE AGARRO PRIMERO: DERECHO, IZQUIERDO ES HIJO");
                quien_primero_agarre = 1;//indica si padre = der
                si_voltimetroAgarrado[1] = true;
                NodosVoltimetro[0].transform.SetParent(NodosVoltimetro[1].transform);
                NodosVoltimetro[0].transform.localPosition.Set(NodoIzqPos0.x, NodoIzqPos0.y, NodoIzqPos0.z);
                NodosVoltimetro[0].transform.localEulerAngles.Set(NodoIzqRot0.x, NodoIzqRot0.y, NodoIzqRot0.z);
                //NodosVoltimetro[0].transform.localPosition = new Vector3(NodoIzqPos0.x, NodoIzqPos0.y, NodoIzqPos0.z);
                //NodosVoltimetro[0].transform.localEulerAngles = new Vector3(NodoIzqRot0.x, NodoIzqRot0.y, NodoIzqRot0.z);
                //NodosVoltimetro[0].GetComponent<BoxCollider>().enabled = true;
                NodosVoltimetro[0].GetComponent<Rigidbody>().isKinematic = true;
                NodosVoltimetro[0].GetComponent<Rigidbody>().useGravity = false;
                NodosVoltimetro[0].GetComponent<Return_Pos0>().enabled = false;
                StartCoroutine(ReUbicacionEnJerarquia(false));//02*09*25
            }
        }
        else
        {
            if (si_der==false)
            {
                //Debug.Log("SE AGARRO DESPUES: IZQUIERDO");
                si_voltimetroAgarrado[0] = true;
                NodosVoltimetro[0].GetComponent<Rigidbody>().useGravity = false;
                NodosVoltimetro[0].GetComponent<Rigidbody>().isKinematic = true;
                
                NodosVoltimetro[0].GetComponent<Return_Pos0>().enabled = false;
            }
            else {
                //Debug.Log("SE AGARRO DESPUES: DERECHO");

                si_voltimetroAgarrado[1] = true;
                NodosVoltimetro[1].GetComponent<Rigidbody>().useGravity = false;
                NodosVoltimetro[1].GetComponent<Rigidbody>().isKinematic = true;
                
                NodosVoltimetro[1].GetComponent<Return_Pos0>().enabled = false;
            }
        }
    }
    public void soltarVoltimetro(bool si_der)//16-06-25
    {
        //falta veriricar si se sostiene ambos y se suelta el primero
        if (si_voltimetroAgarrado[0] == true && si_voltimetroAgarrado[1] == true)
        {
            if (si_der==false)
            {
                si_voltimetroAgarrado[0] = false;
                NodosVoltimetro[0].GetComponent<Rigidbody>().isKinematic = true;
                NodosVoltimetro[0].GetComponent<Rigidbody>().useGravity = false;
                StartCoroutine(ReUbicacionEnJerarquia(false));//17*6*25
                /*
                //NodosVoltimetro[1].transform.SetParent(NodosVoltimetro[0].transform);
                
                //NodosVoltimetro[0].GetComponent<BoxCollider>().enabled = !si_voltimetroAgarrado[0];
                
                if (quien_primero_agarre== 0) 
                {
                    //Debug.Log("SE AGARRO PRIMERO: iZQUIERDA Y SE SOLTO IZQUIERDA");
                }
                if (quien_primero_agarre == 1)
                {
                    //Debug.Log("SE AGARRO PRIMERO: derecho Y SE SOLTO izquierdo");
                }*/
            }
            else
            {
                si_voltimetroAgarrado[1] = false;
                NodosVoltimetro[1].GetComponent<Rigidbody>().isKinematic = true;
                NodosVoltimetro[1].GetComponent<Rigidbody>().useGravity = false;
                StartCoroutine(ReUbicacionEnJerarquia(true));//17*6*25
                //NodosVoltimetro[0].transform.SetParent(NodosVoltimetro[1].transform);
                
                //NodosVoltimetro[1].GetComponent<BoxCollider>().enabled = !si_voltimetroAgarrado[0];
                
                if (quien_primero_agarre == 0)
                {
                    //Debug.Log("SE AGARRO PRIMERO: iZQUIERDA Y SE SOLTO derecho");
                }
                if (quien_primero_agarre == 1)
                {
                    //Debug.Log("SE AGARRO PRIMERO: derecho Y SE SOLTO derecho");
                }
            }
        }
        else
        {
            if (si_der == false)
            {
                si_voltimetroAgarrado[0] = false;
            }
            else { si_voltimetroAgarrado[1] = false; 
            }
            if (si_voltimetroAgarrado[0] == false && si_voltimetroAgarrado[1] == false)
            {
                si_voltimetro_en_mano = false;
                quien_primero_agarre = 2;
                //Debug.Log("SE SOLTO ambos");
                si_voltimetroAgarrado[0] = false;
                si_voltimetroAgarrado[1] = false;
                NodosVoltimetro[0].transform.SetParent(NodosVoltimetro[2].transform);
                NodosVoltimetro[1].transform.SetParent(NodosVoltimetro[2].transform);
                //NodosVoltimetro[1].GetComponent<BoxCollider>().enabled = !si_voltimetroAgarrado[0];
                NodosVoltimetro[1].GetComponent<Rigidbody>().isKinematic = false;
                NodosVoltimetro[1].GetComponent<Rigidbody>().useGravity = true;
                //NodosVoltimetro[0].GetComponent<BoxCollider>().enabled = !si_voltimetroAgarrado[0];
                NodosVoltimetro[0].GetComponent<Rigidbody>().isKinematic = false;
                NodosVoltimetro[0].GetComponent<Rigidbody>().useGravity = true;
                NodosVoltimetro[0].GetComponent<Return_Pos0>().enabled = true;
                NodosVoltimetro[1].GetComponent<Return_Pos0>().enabled = true;
                auxContacto = 0;
                contactoIntAux = 0;
                for(int i = 0; i < NodosContactoDer.Length; i++)
                {
                    NodosContactoDer[i]=false;
                    NodosContactoIzq[i] = false;
                    txt_panelVolti.text = "";//si solo se muestra cuando se hace contacto
                }
                Debug.Log(auxContacto+" : aux-contactointaux : "+contactoIntAux);
            }
        }
    }
    public void siPickUpVoltimetro(bool siPickUp)//verifica si se agarro con alguna mano
    {
        si_voltimetroAgarrado[0]=siPickUp;
    }
    public void verificacionNodo(int cont)
    {
        
        Debug.Log(valorNodo[0] + "-> izquierda ; derecha -> " + valorNodo[1]);
        //*************version 1*****************
        if (NodosContactoDer[0] == true && NodosContactoIzq[2]==true&&
            si_contactoNodos[0] == false && si_contactoNodos[1] == false&& 
            si_contactoNodos[2] == false && si_contactoNodos[3] == false)//1
        {
            txt_panelVolti.text = "0.0 v";
            ItemsVolti[10].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor B0
            ItemsVolti[11].GetComponent<MeshRenderer>().enabled = true;//nodo condesandor B1
            si_contactoNodos[0]=true;
            Debug.Log("circuito[0] verificado +");
            aSource.goFx("Bien");
        }
        if (NodosContactoDer[0] == true && NodosContactoIzq[3] == true &&
            si_contactoNodos[0] == true && si_contactoNodos[1] == false &&
            si_contactoNodos[2] == false && si_contactoNodos[3] == false)//2
        {
            txt_panelVolti.text = "0.0 v";
            //ItemsVolti[8].SetActive(false);//nodo condesandor A0
            ItemsVolti[11].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor B1
            ItemsVolti[8].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor A0
            ItemsVolti[9].GetComponent<MeshRenderer>().enabled = true;//nodo condesandor A1
            ItemsVolti[10].GetComponent<MeshRenderer>().enabled = true;//nodo condesandor B0
            //ItemsVolti[11].SetActive(false);//nodo condesandor B1
            si_contactoNodos[1] = true;
            Debug.Log("circuito[1] verificado +");
            aSource.goFx("Bien");
        }
        if (NodosContactoDer[1] == true && NodosContactoIzq[2] == true &&
            si_contactoNodos[0] == true && si_contactoNodos[1] == true && 
            si_contactoNodos[2] == false && si_contactoNodos[3] == false)//3
        {
            txt_panelVolti.text = "0.0 v";
            /*ItemsVolti[8].SetActive(false);//nodo condesandor A0
            ItemsVolti[9].SetActive(true);//nodo condesandor A1
            ItemsVolti[10].SetActive(false);//nodo condesandor B0
            ItemsVolti[11].SetActive(true);//nodo condesandor B1*/
            ItemsVolti[10].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor B0
            ItemsVolti[11].GetComponent<MeshRenderer>().enabled = true;//nodo condesandor B1
            si_contactoNodos[2] = true;
            Debug.Log("circuito[2] verificado +");
            aSource.goFx("Bien");
        }

        if (NodosContactoDer[1] == true && NodosContactoIzq[3] == true &&
            si_contactoNodos[0] == true && si_contactoNodos[1] == true &&
            si_contactoNodos[2] == true && si_contactoNodos[3] == false)
        {
            txt_panelVolti.text = "0.0 v";
            /*ItemsVolti[8].SetActive(false);//nodo condesandor A0
            ItemsVolti[9].SetActive(false);//nodo condesandor A1
            ItemsVolti[10].SetActive(false);//nodo condesandor B0
            ItemsVolti[11].SetActive(false);//nodo condesandor B1*/
            ItemsVolti[9].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor A1
            ItemsVolti[11].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor B0
            Debug.Log("circuito[3] verificado +");
            si_contactoNodos[3] = true;
            aSource.goFx("Bien");
        }
        /////////*************version2********************
        if (NodosContactoDer[2] == true && NodosContactoIzq[0] == true 
            && si_contactoNodos[0] == false && si_contactoNodos[1] == false 
            && si_contactoNodos[2] == false && si_contactoNodos[3] == false)//1
        {
            txt_panelVolti.text = "0.0 v";
            /*ItemsVolti[8].SetActive(true);//nodo condesandor A0
            ItemsVolti[9].SetActive(false);//nodo condesandor A1
            ItemsVolti[10].SetActive(false);//nodo condesandor B0
            ItemsVolti[11].SetActive(true);//nodo condesandor B1*/
            ItemsVolti[10].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor B0
            ItemsVolti[11].GetComponent<MeshRenderer>().enabled = true;//nodo condesandor B1
            si_contactoNodos[0] = true;
            Debug.Log("circuito[0] verificado -");

            aSource.goFx("Bien");
        }
        if (NodosContactoDer[3] == true && NodosContactoIzq[0] == true 
            && si_contactoNodos[0] == true && si_contactoNodos[1] == false 
            && si_contactoNodos[2] == false && si_contactoNodos[3] == false)//2
        {
            txt_panelVolti.text = "0.0 v";
            /*ItemsVolti[8].SetActive(false);//nodo condesandor A0
            ItemsVolti[9].SetActive(true);//nodo condesandor A1
            ItemsVolti[10].SetActive(true);//nodo condesandor B0
            ItemsVolti[11].SetActive(false);//nodo condesandor B1*/
            ItemsVolti[11].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor B1
            ItemsVolti[8].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor A0
            ItemsVolti[9].GetComponent<MeshRenderer>().enabled = true;//nodo condesandor A1
            ItemsVolti[10].GetComponent<MeshRenderer>().enabled = true;//nodo condesandor B0
            si_contactoNodos[1] = true;
            Debug.Log("circuito[2] verificado -");
            aSource.goFx("Bien");
        }
        if (NodosContactoDer[2] == true && NodosContactoIzq[1] == true && 
            si_contactoNodos[0] == true && si_contactoNodos[1] == true && 
            si_contactoNodos[2] == false && si_contactoNodos[3] == false)
        {
            txt_panelVolti.text = "0.0 v";
            /*ItemsVolti[8].SetActive(false);//nodo condesandor A0
            ItemsVolti[9].SetActive(true);//nodo condesandor A1
            ItemsVolti[10].SetActive(false);//nodo condesandor B0
            ItemsVolti[11].SetActive(true);//nodo condesandor B1*/
            ItemsVolti[10].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor B0
            ItemsVolti[11].GetComponent<MeshRenderer>().enabled = true;//nodo condesandor B1
            si_contactoNodos[2] = true;
            Debug.Log("circuito[1] verificado -");
            aSource.goFx("Bien");
        }

        if (NodosContactoDer[3] == true && NodosContactoIzq[1] == true &&
            si_contactoNodos[0] == true && si_contactoNodos[1] == true &&
            si_contactoNodos[2] == true && si_contactoNodos[3] == false)
        {
            txt_panelVolti.text = "0.0 v";
            /*ItemsVolti[8].SetActive(false);//nodo condesandor A0
            ItemsVolti[9].SetActive(false);//nodo condesandor A1
            ItemsVolti[10].SetActive(false);//nodo condesandor B0
            ItemsVolti[11].SetActive(false);//nodo condesandor B1*/
            ItemsVolti[9].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor A1
            ItemsVolti[11].GetComponent<MeshRenderer>().enabled = false;//nodo condesandor B1
            Debug.Log("circuito[3] verificado -");
            si_contactoNodos[3] = true;
            aSource.goFx("Bien");
        }
        /*switch (cont)
        {//****A*B***0*4
            //*C*D***6*7
            case 6://AC
                si_contactoNodos[0] = true;
                if (si_OrdenPositivo)
                {
                    Debug.Log("Circuito de valor positivo");
                }
                else
                {
                    Debug.Log("Circuito de valor negativo");
                }
                break;
            case 7://AD
                si_contactoNodos[1] = true;
                if (si_OrdenPositivo)
                {
                    Debug.Log("Circuito de valor positivo");
                }
                else
                {
                    Debug.Log("Circuito de valor negativo");
                }
                break;
            case 10://BC
                si_contactoNodos[2] = true;
                if (si_OrdenPositivo)
                {
                    Debug.Log("Circuito de valor positivo");
                }
                else
                {
                    Debug.Log("Circuito de valor negativo");
                }
                break;
            case 11://BD
                si_contactoNodos[3] = true;
                if (si_OrdenPositivo)
                {
                    Debug.Log("Circuito de valor positivo");
                }
                else
                {
                    Debug.Log("Circuito de valor negativo");
                }
                break;
        }*/
        if (si_contactoNodos[0]&& si_contactoNodos[1] && si_contactoNodos[2] && si_contactoNodos[3] )
        {
            txt_panelVolti.text = "0.0 v";
            //aSource.goFx("Bien");
            aSource.goFx("Locu_Bien");
            Debug.Log("Todos los Circuitos verificados");
            Tablero_Indicaciones[17].SetActive(false);
            Tablero_Indicaciones[18].SetActive(false);
            Tablero_Indicaciones[19].SetActive(true);
            ItemsVolti[3].SetActive(true);
            StartCoroutine(CoroutineAnimSonidoFinVolti());
            //StartCoroutine(TiempoEsperaTarea(8));//fin de verificacion de voltimetro
        }
    }
    //***********************FIN DE FUNCION VOLTIMETRO*********************
    public IEnumerator ReUbicacionEnJerarquia(bool si_der)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (si_der== false)
        {
            NodosVoltimetro[0].transform.SetParent(NodosVoltimetro[1].transform);
            Debug.Log(NodosVoltimetro[0].name + " Se reubico en " + NodosVoltimetro[1]);
            NodosVoltimetro[0].transform.localPosition = new Vector3(-.13f, 0, 0);
            NodosVoltimetro[0].transform.localEulerAngles = new Vector3(0, 0, 0);
            if (quien_primero_agarre == 1)
            {
                NodosVoltimetro[0].transform.localPosition.Set(.13f,0,0);
                NodosVoltimetro[0].transform.localEulerAngles.Set(0,0,0);
            }
            else
            {
                NodosVoltimetro[0].transform.localPosition.Set(-.13f, 0, 0);
                NodosVoltimetro[0].transform.localEulerAngles.Set(0, 0, 0);
            }
        }
        else
        {
            NodosVoltimetro[1].transform.SetParent(NodosVoltimetro[0].transform);
            Debug.Log(NodosVoltimetro[1].name + " Se reubico en " + NodosVoltimetro[0].name);
            if (quien_primero_agarre == 0)
            {
                NodosVoltimetro[1].transform.localPosition = new Vector3(-.13f, 0, 0);
                NodosVoltimetro[1].transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else {
                NodosVoltimetro[1].transform.localPosition = new Vector3(.13f, 0, 0);
                NodosVoltimetro[1].transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
    public IEnumerator CoroutineAnimSonidoEntradaCamion()
    {
        yield return new WaitForSecondsRealtime(49f);
        aSource.goFx("Camion_Escalera_Short");
    }
    public IEnumerator CoroutineAnimSonidoFinVolti()
    {
        yield return new WaitForSecondsRealtime(2f);
        aSource.goFx("Locu_vic_volti");
    }
}
