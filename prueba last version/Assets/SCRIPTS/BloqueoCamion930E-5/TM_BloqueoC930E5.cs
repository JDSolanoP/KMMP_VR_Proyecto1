using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TM_BloqueoC930E5 : Lista_Tareas_Controller
{
    public int auxContacto;//para activavionXR u otros
    public int contactoIntAux;//exclusivo de DetectorObj
    public string NombreAuxAudio;
    public float tiempoEsperaAux;
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
    [Header("***Verificacion_Pedal***")]
    public GameObject btn_Pedal;
    public GameObject[] Pedales;
    [Header("***acumulador_auxiliar***")]
    public GameObject[] escalera;
    public GameObject[] acumuladorAux;
    [Header("***acumulador_auxiliar***")]
    public GameObject[] Override;
    [Header("***Gabiente de Potencia***")]
    public GameObject btn_AbrirGabinetePotencia;
    public GameObject[] Nodo;
    [Header("*****ElementosPropioDeCamión*****")]
    public bool si_PuertaCabinaCerrada;
    public bool si_PJEnCabina;
    [Header("*****ValoresVoltimetro*****")]
    public GameObject[] NodosVoltimetro;
    public bool[] si_voltimetroAgarrado;
    public int quien_primero_agarre = 2;//0:Izquierda;1:Derecha;2:Ninguno
    public Vector3 NodoIzqPos0;
    public Vector3 NodoIzqRot0;
    public Vector3 NodoDerPos0;
    public Vector3 NodoDerRot0;
    public override void Start()
    {
        base.Start();
        if (si_login == true)
        {
            PJ.transform.position = posPJ[0].position;
        }

        StartCoroutine(ListaTareas(TareaActual));
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
                    aSource.PlayMusica(aSource.MusicaSonidos[0].nombre, 0.75f, true);
                    if (!EnPruebas)
                    {
                        CamionAnim.SetActive(true);
                        CamionC930.SetActive(false);

                        yield return new WaitForSecondsRealtime(47f);//**********************fin de animacion de entrada a camion********
                        FadeOut();
                        yield return new WaitForSecondsRealtime(2f);
                        PJ.transform.position = posPJ[1].position;
                        CamionAnim.SetActive(false);
                        FadeIn();
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
                    //**********************************************************************
                    cajaBloqueoTapaRot0 = Items[15].transform.localEulerAngles;//tapa de caja de bloqueo*************17-06-25
                    for (int i = 0; i < LucesLEDCaja.Length; i++)
                    {
                        LucesLEDCaja[i].SetActive(false);
                    }
                    LucesLEDCaja[0].SetActive(true);
                    LucesLEDCaja[2].SetActive(true);
                    LucesLEDCaja[4].SetActive(true);
                    LucesLEDCaja[6].SetActive(true);
                    //***********************Preparativos pra bloqueo de camion***************
                    llaveArranque[0].SetActive(false);
                    TimonRot0 = Timon[1].transform.localEulerAngles;

                    Tablero_Indicaciones[0].SetActive(true);//panel de bienvenido
                    /*   //audioManager de bienvenida

                    yield return new WaitForSeconds(0.5f);
                    manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                    manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                    */
                    //aSource.MusicaVol(0.75f);//**************************************Sonido Musica Inicial*************
                    aSource.FxVol(1);

                    //yield return new WaitForSecondsRealtime(23f);
                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    yield return new WaitForSecondsRealtime(5f);

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
                    Tablero_Indicaciones[5].SetActive(true);//P2
                    Items[0].SetActive(true);//candadoAmarillo refe
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
                    Tablero_Indicaciones[8].SetActive(true);
                    llaveArranque[0].SetActive(true);
                    Timon[0].SetActive(true);
                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 4://gabinete frenos
                    Tablero_Indicaciones[10].SetActive(true);
                    Tablero_Indicaciones[8].SetActive(false);
                    Muros[2].SetActive(false);
                    V_NV1[0].SetActive(true);
                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 5://verificacion´pedal
                    Tablero_Indicaciones[10].SetActive(false);
                    Tablero_Indicaciones[12].SetActive(true);
                    btn_Pedal.SetActive(true);

                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 6://liberacion acumulador auxiliar
                    Tablero_Indicaciones[12].SetActive(false);
                    Tablero_Indicaciones[14].SetActive(true);
                    escalera[0].SetActive(true);

                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 7://verificacion override
                    Tablero_Indicaciones[12].SetActive(true);


                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 8://verificacion con voltimetro
                    Tablero_Indicaciones[12].SetActive(true);


                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 9://conclusiones
                    Tablero_Indicaciones[12].SetActive(true);


                    while (AudioManager.aSource.IsPlayingVoz() == true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case 10://final
                    Tablero_Indicaciones[12].SetActive(true);


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
                        Tablero_Indicaciones[2].SetActive(true);
                        Tablero_Indicaciones[1].SetActive(false);//panelP1
                        Tablero_Indicaciones[0].SetActive(false);//panel bienvenida
                        StartCoroutine(TiempoEsperaTarea(0));
                        aSource.goFx(aSource.FxSonidos[21].nombre);
                        aSource.goFx(aSource.FxSonidos[23].nombre);
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
                        Tablero_Indicaciones[2].SetActive(true);
                        Tablero_Indicaciones[1].SetActive(false);//panelP1
                        Tablero_Indicaciones[0].SetActive(false);//panel bienvenida
                        StartCoroutine(TiempoEsperaTarea(0));
                        aSource.goFx(aSource.FxSonidos[21].nombre);
                        aSource.goFx(aSource.FxSonidos[23].nombre);
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
                    Items[7].SetActive(true);//llave ta obj
                    Items[8].SetActive(false);//llave TA mesh
                }
                break;
            case 4://contacto llave con caja
                if (contacto_confirmado[confirmarcontacto] == true && si_LlaveEnMano == false)
                {
                    Debug.Log("llave en caja confirmarcontacto " + confirmarcontacto + " : auxcontacto=" + auxContacto);
                    Items[7].GetComponent<XRGrabInteractable>().enabled = false;
                    Items[9].SetActive(true);//candado personal refe -> pl
                }
                break;
            case 5://caja bloqueo cerrada
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    if (contacto_confirmado[4] == true)
                    {
                        auxContacto = 7;
                        objRGBDActived(false);
                        Items[7].GetComponent<XRGrabInteractable>().enabled = false;
                        Items[9].SetActive(true);//candado personal refe -> pl
                        Items[17].GetComponent<XRGrabInteractable>().enabled = false;
                        Items[18].transform.localEulerAngles.Set(cajaBloqueoTapaRot0.x, cajaBloqueoTapaRot0.y, cajaBloqueoTapaRot0.z);
                    }
                }
                break;
            case 6://colocar Candado Rojo
                if (contacto_confirmado[confirmarcontacto] == true)
                {
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
                            aSource.MusicaVol(0.75f);
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
                        aSource.MusicaVol(0.75f);
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
                    if (TareaActual < 2)
                    {
                        Tablero_Indicaciones[7].SetActive(true);
                        aSource.goFx("Fallo");
                        aSource.goFx("Locu_Fallo");
                    }
                    else { Muros[1].SetActive(false); }
                }
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
                        if (nPalancasOff == 3)
                        {
                            StartCoroutine(TiempoEsperaTarea(1));
                            aSource.goFx(aSource.FxSonidos[21].nombre);
                            aSource.goFx(aSource.FxSonidos[23].nombre);
                            Tablero_Indicaciones[4].SetActive(true);
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
                        if (nPalancasOff == 3)
                        {
                            StartCoroutine(TiempoEsperaTarea(1));
                            aSource.goFx(aSource.FxSonidos[21].nombre);
                            aSource.goFx(aSource.FxSonidos[23].nombre);
                            Tablero_Indicaciones[4].SetActive(true);
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
                        if (nPalancasOff == 3)
                        {
                            StartCoroutine(TiempoEsperaTarea(1));
                            aSource.goFx(aSource.FxSonidos[21].nombre);
                            aSource.goFx(aSource.FxSonidos[23].nombre);
                            Tablero_Indicaciones[4].SetActive(true);
                        }
                        break;
                }
                break;
            case 1:
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
                StartCoroutine(TiempoEsperaTarea(2));
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
                AccionPedal();
                    break;
            case 8://boton reinicio
                IrEscenaAsincron(0);
                break;
            case 9://boton SALIR
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
    public IEnumerator TiempoEsperaAudio(float t)//Agregar tiempo***********Agregado el 26-05-25***************************************
    {
        string nAudioAux = NombreAuxAudio;
        int auxTarea = TareaActual;
        if (EnPruebas)
        {
            yield return new WaitForSeconds(t + 1);
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
                    yield return new WaitForSeconds(5);
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
        yield return new WaitForSeconds(tiempoEsperaAux + 1);
        Debug.Log("Se espero por la tarea " + tarea + "- tiempoEsperaAux : " + tiempoEsperaAux);
        if (TareaActual == tarea)
        {
            Debug.Log("Se completo la tarea " + tarea + "- dentro del tiempo tiempoEsperaAux : " + tiempoEsperaAux);
            TareaCompletada(tarea);
        }
    }
    public void OnOffBoxCollider(bool onOff)//ACTivar y DEactivar boxcollider//requiere dar AUXCONTACTO***03-06-25**************
    {
        Items[auxContacto].GetComponent<BoxCollider>().enabled = onOff;
    }
    public void objRGBDActived(bool si)//cuando agarra el perno
    {
        Debug.Log("ITEM " + (auxContacto) + "agarrado de activo el toggles de Rigidbody en funcion PernosRGBDActived");

        //Items[n].transform.SetParent(PuntoDeRecepccionPernos.transform);
        Items[auxContacto].GetComponent<BoxCollider>().enabled = si;
        Items[auxContacto].GetComponent<Rigidbody>().isKinematic = !si;
        Items[auxContacto].GetComponent<Rigidbody>().useGravity = si;
        si_LlaveEnMano = si;
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
            Tablero_Indicaciones[9].SetActive(true);
            StartCoroutine(TiempoEsperaTarea(3));//completa tarea 3
        }
    }
    public void VerificacionTimonNoArranque()
    {
        si_timon_agarrado = true;
        Timon[0].SetActive(false);//refe
        Debug.Log("timon agarrado");
        if (si_verificacionGiro[0] == false || si_verificacionGiro[1] == false)
        {
            StartCoroutine(VerificacionGiroTimon());
        }

    }
    public IEnumerator VerificacionGiroTimon()//********************03-07-25
    {
        Debug.Log("iniciando verificion timon-> ROTLOCAL : " + Timon[1].transform.localRotation.z + " " + Timon[1].transform.localEulerAngles.z);
        while (TareaActual == 3)
        {
            if (Timon[1].transform.localEulerAngles.z >= TimonRotDer && si_verificacionGiro[1] == false)//el mayor 206
            {
                Debug.Log("giro timonde timon lado der : " + si_verificacionGiro[1]);
                aSource.goFx("4Puerta_Corrediza_Alto");
                si_verificacionGiro[1] = true;
            }
            if (Timon[1].transform.localEulerAngles.z <= TimonRotIzq && si_verificacionGiro[0] == false)//el menor 173
            {
                Debug.Log("giro timonde timon lado izq : " + si_verificacionGiro[0]);
                aSource.goFx("4Puerta_Corrediza_Alto");
                si_verificacionGiro[0] = true;
            }
            if (si_verificacionGiro[0] == true && si_verificacionGiro[1] == true)
            {
                nVerificionesArranque[1] = true;
                Debug.Log("saliendo de bucle por verificacion completada de ambos giros");
            }
            if (si_timon_agarrado == false)
            {
                Debug.Log("saliendo de bucle por soltar timon");
                break;
            }

            if (nVerificionesArranque[0] == true && nVerificionesArranque[1] == true)
            {
                Debug.Log("saliendo de bucle por verificacion de no arranque completada");
                aSource.goFx("Bien");
                Tablero_Indicaciones[9].SetActive(true);
                StartCoroutine(TiempoEsperaTarea(3));//completa tarea 3
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
            StartCoroutine(GiroNV1());
        }
        else
        {
            StartCoroutine(GiroNV2());
        }
    }
    public IEnumerator GiroNV1()
    {
        if (si_Valvula_Liberada[0] == false)
        {
            V_NV1[3].SetActive(false);//MESH
            V_NV1[0].SetActive(false);
            V_NV1[1].SetActive(true);//ANIM1
            yield return new WaitForSecondsRealtime(10);
            si_Valvula_Liberada[0] = true;
            yield return new WaitForSecondsRealtime(2);

            V_NV1[0].SetActive(true);
        }
        else
        {
            V_NV1[0].SetActive(false);//REFE
            V_NV1[1].SetActive(false);//ANIM1
            V_NV1[2].SetActive(true);//ANIM2
            yield return new WaitForSecondsRealtime(10);
            //si_Valvula_Liberada[0] = true;
            yield return new WaitForSecondsRealtime(2);
            if (si_Valvula_Liberada[1] == false)
            {
                V_NV2[0].SetActive(true);
            }
            if (si_Valvula_Liberada[0] == true && si_Valvula_Liberada[1] == true)
            {
                aSource.goFx("Bien");
                aSource.goFx("Locu_Bien");
                //yield return new WaitForSecondsRealtime(2);
                Tablero_Indicaciones[11].SetActive(true);
                StartCoroutine(TiempoEsperaTarea(4));
            }
        }
    }
    public IEnumerator GiroNV2()
    {
        if (si_Valvula_Liberada[1] == false)
        {
            V_NV2[3].SetActive(false);//MESH
            V_NV2[0].SetActive(false);
            V_NV2[1].SetActive(true);//ANIM1
            yield return new WaitForSecondsRealtime(10);
            si_Valvula_Liberada[1] = true;
            yield return new WaitForSecondsRealtime(2);
            V_NV2[0].SetActive(true);
        }
        else
        {
            V_NV2[0].SetActive(false);//REFE
            V_NV2[1].SetActive(false);//ANIM1
            V_NV2[2].SetActive(true);//ANIM2
            yield return new WaitForSecondsRealtime(10);
            //si_Valvula_Liberada[0] = true;
            yield return new WaitForSecondsRealtime(2);
            if (si_Valvula_Liberada[0] == false)
            {
                V_NV1[0].SetActive(true);
            }
            if (si_Valvula_Liberada[0] == true && si_Valvula_Liberada[1] == true)
            {
                aSource.goFx("Bien");
                aSource.goFx("Locu_Bien");
                //yield return new WaitForSecondsRealtime(2);
                Tablero_Indicaciones[11].SetActive(true);
                StartCoroutine(TiempoEsperaTarea(4));
            }
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
        yield return new WaitForSecondsRealtime(6f);
        Pedales[1].SetActive(false);
        Pedales[0].SetActive(true);
        aSource.goFx("Bien");
        aSource.goFx("Locu_Bien");
        Tablero_Indicaciones[11].SetActive(false);
        Tablero_Indicaciones[13].SetActive(true);
        StartCoroutine(TiempoEsperaTarea(5));
    }
    //************************FUNCIONES DE VOLTIMETRO*******17-06-25**************
    public void LlevarVoltimetro(bool si_der)//si se agarro con alguna mano****16.06-25
    {
        if (si_voltimetroAgarrado[0] == false&& si_voltimetroAgarrado[1] == false)//primera ocacion
        {
            if (si_der==false)
            {
                quien_primero_agarre=0;//indica si padre = izq
                Debug.Log("SE AGARRO PRIMERO: IZQUIERDO, DERECHO ES HIJO");
                si_voltimetroAgarrado[0] = true;
                NodosVoltimetro[1].transform.SetParent(NodosVoltimetro[0].transform);
                NodosVoltimetro[1].transform.localPosition.Set(NodoDerPos0.x, NodoDerPos0.y, NodoDerPos0.z);
                NodosVoltimetro[1].transform.localEulerAngles.Set(NodoDerRot0.x, NodoDerRot0.y, NodoDerRot0.z);
                //NodosVoltimetro[1].GetComponent<BoxCollider>().enabled = true;
                NodosVoltimetro[1].GetComponent<Rigidbody>().isKinematic = true;
                NodosVoltimetro[1].GetComponent<Rigidbody>().useGravity = false;
                NodosVoltimetro[1].GetComponent<Return_Pos0>().enabled = false;
            }
            else
            {
                Debug.Log("SE AGARRO PRIMERO: DERECHO, IZQUIERDO ES HIJO");
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
            }
        }
        else
        {
            if (si_der==false)
            {
                Debug.Log("SE AGARRO DESPUES: IZQUIERDO");
                si_voltimetroAgarrado[0] = true;
                NodosVoltimetro[0].GetComponent<Rigidbody>().isKinematic = true;
                NodosVoltimetro[0].GetComponent<Rigidbody>().useGravity = false;
                NodosVoltimetro[0].GetComponent<Return_Pos0>().enabled = false;
            }
            else {
                Debug.Log("SE AGARRO DESPUES: DERECHO");
                si_voltimetroAgarrado[1] = true;
                NodosVoltimetro[1].GetComponent<Rigidbody>().isKinematic = true;
                NodosVoltimetro[1].GetComponent<Rigidbody>().useGravity = false;
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
                StartCoroutine(ReUbicacionEnJerarquia(false));//17*6*25
                
                //NodosVoltimetro[1].transform.SetParent(NodosVoltimetro[0].transform);
                
                //NodosVoltimetro[0].GetComponent<BoxCollider>().enabled = !si_voltimetroAgarrado[0];
                NodosVoltimetro[0].GetComponent<Rigidbody>().isKinematic = true;
                NodosVoltimetro[0].GetComponent<Rigidbody>().useGravity = false;
                if (quien_primero_agarre== 0) 
                {
                    Debug.Log("SE AGARRO PRIMERO: iZQUIERDA Y SE SOLTO IZQUIERDA");
                }
                if (quien_primero_agarre == 1)
                {
                    Debug.Log("SE AGARRO PRIMERO: derecho Y SE SOLTO izquierdo");
                }
            }
            else
            {
                si_voltimetroAgarrado[1] = false;
                StartCoroutine(ReUbicacionEnJerarquia(true));//17*6*25
                //NodosVoltimetro[0].transform.SetParent(NodosVoltimetro[1].transform);
                
                //NodosVoltimetro[1].GetComponent<BoxCollider>().enabled = !si_voltimetroAgarrado[0];
                NodosVoltimetro[1].GetComponent<Rigidbody>().isKinematic = true;
                NodosVoltimetro[1].GetComponent<Rigidbody>().useGravity = false;
                if (quien_primero_agarre == 0)
                {
                    Debug.Log("SE AGARRO PRIMERO: iZQUIERDA Y SE SOLTO derecho");
                }
                if (quien_primero_agarre == 1)
                {
                    Debug.Log("SE AGARRO PRIMERO: derecho Y SE SOLTO derecho");
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
                quien_primero_agarre = 2;
                Debug.Log("SE SOLTO ambos");
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
            }
        }
    }
    public void siPickUpVoltimetro(bool siPickUp)//verifica si se agarro con alguna mano
    {
        si_voltimetroAgarrado[0]=siPickUp;
    }
    IEnumerator ReUbicacionEnJerarquia(bool si_der)
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

}
