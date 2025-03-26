using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.XR.Interaction.Toolkit;

using System.Runtime.CompilerServices;

//using UnityEditorInternal;
//using UnityEditor.Animations;

public class TM_DojoSeguridad : Lista_Tareas_Controller
{
    //public AudioManager aSource;
    [Header("Dojo_Seguridad")]
    public AccionPuertaDojo aP;
    //public GameObject[] Tablero_Indicaciones;
    public GameObject[] murosConos;
    public GameObject[] DetectorSgtM;
    public int contadorDetectorSgtMod=0;
    string NombreAuxAudio;
    float tiempoEsperaAux;
    [Header("Modulo 0")]
    bool llamada_apertura=false;
    public GameObject[] guantesComplementos;
    public GameObject[] manosXR;
    public Material[] manosXRMaterial;
    public GameObject ambientacion;
    public GameObject[] detectorPos;
    [Header("Modulo 1")]
    public GameObject[] Epps;
    public GameObject Flechasgrupo2;
    public bool correctaTareaMEpps1 = false;
    public int nEpps=0;
    public int TotalEpps = 0;
    public bool ya_interior=false;
    [Header ("Modulo 2")]
    public GameObject[] M_1;
    public bool Si_Mandil = false;
    public bool Si_Careta = false;
    public bool correctaTareaM2=false;
    //public bool[] contacto_confirmado;
    public float tiempoEspera;//para esperar el tiempo antes de activacion de otro evento
    public float tTemp=0;//recorrido del tiempo
    public bool amoladoraOn=false;
    bool amolando = false;
    bool tareaCorrectaAmoladora = false;
    [Header("Modulo 3")]
    public GameObject[] ObjetosReferencias;
    public bool si_corneta_Presionada;
    public int numNoAvisoCorneta;
    public bool CtrlGruaPick = false;
    //public AccionPuenteGrua apGrua;
    public int BotonGruaPresionado;
    [Header("Modulo 4")]
    public bool Boquilla_ContactoRefe=false;
    public GameObject interruptorBTN_Compress;
    public float rotEncendido;
    public GameObject RefeinterruptorCompresora;
    public int TotalPernos;
    public int nPernosSacados;
    public GameObject ObjRefePernosConjunto;
    public GameObject[] PernosRefe;//imagen d referencia en verde
    public GameObject[] PernosGrab;//objeto agarrable
    public GameObject[] PernosMesh;//imagen de muestra inicial
    public float[] PernosTiempo;
    public float TiempoPernos;
    public ImpactWrenchGunAccion iwg;
    public GameObject PuntoPernoEnMaquina;
    public Vector3 LocalPosPernoSpawn;
    public Vector3 LocalRotPernoSpawn;
    public GameObject PuntoDeRecepccionPernos;
    public bool[] verificacionNPernos;
    public GameObject[] ComponentAnimLatigueo;//cable aparte solo activado para la animacion de latigueo
    public GameObject CableAnim;//cable para la animacion.
    public GameObject ObjJerarFinCable;//obj estacionario donde se ubicara el cable despues de desconectarlo
    public GameObject ObjConexionIWPCable;//boquilla de cable con pistola
    public GameObject CableCorrecto;//Cable que se usa siempre
    public GameObject ObjConexionIWPCableRefe;//capsula indicadora
    public GameObject ObjPuntoUbic_Pistola;//Punto en donde reubicar el punto reubicar la conexion
    public GameObject ObjConectorSiCorrecto;//Mesh si se realiza correctamente la desconexion
    public bool PistolaDescargada=false;
    public Vector3 conexIWPCalbePos0;
    public Vector3 conexIWPCalbeRot0;
    public bool ConectorMPPickedUp=false;
    public bool PernoEnDado=false;
    public bool[] PernoEnMano;
    public GameObject IWP_Refe;
    public GameObject IWP_Mesh;
    public GameObject IWP_OBJ;
    public GameObject ParticulasExpl;
    public GameObject Flecha_Indi;
    bool si_algun_pernoEnMano = false;
    [Header("Extras")]
    public GameObject UI_btn_Continuar_Panel;
    public GameObject UI_btn_Salir_Panel;
    public GameObject UI_btn_Reiniciar_Panel;
    public GameObject UI_btn_Menu_Panel;


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
                manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                //aSource.MusicaVol(0.5f);//**************************************Sonido Musica Inicial*************
                //aSource.FxVol(1);
                /*for(int i = 0; i < Tablero_Indicaciones.Length; i++)
                {
                    Tablero_Indicaciones[i].SetActive(false);
                }*/
                yield return new WaitForSecondsRealtime(2f);
                Tablero_Indicaciones[16].SetActive(true);
                aSource.goFx(aSource.FxSonidos[0].nombre, 0.5f, true, false);
                aSource.goFx(aSource.FxSonidos[1].nombre, 0.2f, true, false);
                aSource.goFx(aSource.FxSonidos[2].nombre, 0.2f, true, false);
                yield return new WaitForSecondsRealtime(1f);
                
                //Debug.Log("Se esta reproduciendo audio");

                while (AudioManager.aSource.IsPlayingVoz()==true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se tiene todos los EPPS
            case 1:// AQUI, esta dentro de la instancia intermedia

                yield return new WaitForSeconds(.1f);
                Tablero_Indicaciones[16].SetActive(false);
                //Debug.Log("Se esta reproduciendo audio");

                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se sale del area intermedia
            case 2:// empieza la amoladora
                Tablero_Indicaciones[0].SetActive(true);
                
                //ya_interior = false;
                //Debug.Log("Se esta reproduciendo audio");

                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//Se hizo correctamente el ejercicio de amoladora

            case 3:// devolver careta y mandil
                Tablero_Indicaciones[0].SetActive(false);
                //aSource.goFx("Bien");
                //aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                M_1[2].SetActive(true);
                M_1[5].SetActive(true);
                M_1[2].GetComponent<Collider>().enabled = true;
                M_1[5].GetComponent<Collider>().enabled = true;
                //murosConos[0].SetActive(false);
                //Tablero_Indicaciones[3].SetActive(true);
                //Debug.Log("Se esta reproduciendo audio");
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 4:// Da pase a la siguiente area PUENTE GRUA
                //aSource.goFx("Bien");
                //aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                aSource.altoFx("SiguienteModulo");
                Tablero_Indicaciones[6].SetActive(true);
                Tablero_Indicaciones[15].SetActive(true);
                ObjetosReferencias[0].SetActive(true);
                //Debug.Log("Se esta reproduciendo audio");
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;//Fin puente grua
            case 5:// Da pase a la siguiente area PISTOLA NEUMATICA
                TM_Lobby.lb.AgregarNota(2, TM_Lobby.lb.auxNotas[2]);
                iwg.DetectorPerno.SetActive(false);
                //aSource.goFx("Bien");
                //aSource.goFx("Devolver_Epps");//********************AGREGADO EL 27-08-24********************////////////
                aSource.altoFx("SiguienteModulo");
                Tablero_Indicaciones[15].SetActive(false);
                Tablero_Indicaciones[10].SetActive(true);
                RefeinterruptorCompresora.SetActive(true);
                Flecha_Indi.SetActive(true);
                conexIWPCalbePos0=ObjConexionIWPCable.transform.localPosition;
                conexIWPCalbeRot0 = ObjConexionIWPCable.transform.localEulerAngles;
                //Tablero_Indicaciones[6].SetActive(true);
                //ObjetosReferencias[0].SetActive(true);
                //Debug.Log("Se esta reproduciendo audio");
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;//Fin de modulo de PISTOLA NEUMATICA, CUANDO SE DESCONECTA CORRECTAMENTE LA PISTOLA DEL COMPRESOR DE AIRE
            case 6://INICIO DE MOD5 Conclusiones
                //aSource.goFx("Bien");
                //aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                aSource.altoFx("SiguienteModulo");
                Tablero_Indicaciones[10].SetActive(false);
                Tablero_Indicaciones[13].SetActive(true);
                yield return new WaitForSeconds(12);
                UI_btn_Continuar_Panel.SetActive(true);

                //ObjetosReferencias[0].SetActive(true);
                //Debug.Log("Se esta reproduciendo audio");
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;
            case 7:// Finde conclusiones
                TM_Lobby.lb.moverPanelFinal();
                //aSource.VocesSourceCanal[aSource.VozCanalActual].Stop();
                UI_btn_Reiniciar_Panel.SetActive(false);
                UI_btn_Salir_Panel.SetActive(false);
                aSource.goFx("Aplausos");
                aSource.goFx("fanfarrias");
                Tablero_Indicaciones[13].SetActive(false);
                Tablero_Indicaciones[14].SetActive(true);
                murosConos[4].SetActive(false);
                yield return new WaitForSeconds(8f);
                UI_btn_Reiniciar_Panel.SetActive(true);
                yield return new WaitForSeconds(4f);
                UI_btn_Salir_Panel.SetActive(true);
                //Tablero_Indicaciones[6].SetActive(true);
                //ObjetosReferencias[0].SetActive(true);
                //Debug.Log("Se esta reproduciendo audio");
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;
        }
    }
    public void verificarContacto(int confirmarContacto)//*******realiza acciones cuando2 objetos interactuan usan
    {
        switch (confirmarContacto)
        {
            case 1://verifica al pj en el area de la 1era puerta
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (AudioManager.aSource.isVozPlay == false && aP.permite_anim == true)
                    {
                        Debug.Log("voz apagada");
                        if (ya_interior == false && llamada_apertura==false)
                        {
                            llamada_apertura = true;
                            if (TareaActual == 0)
                            {
                                // aSource.PlayFx(aSource.FxSonidos[4].nombre);
                                aSource.PlayMusica(aSource.MusicaSonidos[0].nombre, 0.9f, true);
                                aP.aperturaDojo(true);
                            }
                        }
                    }
                }else
                    {
                        if (ya_interior == true)
                        {
                        aP.cerrandoDojo(true);
                        
                        //aSource.goFx();
                        aSource.FxVolPropio(aSource.FxSonidos[0].nombre, 0.25f);//****************************************Sonidos para ambientacion***********************
                        aSource.FxVolPropio(aSource.FxSonidos[1].nombre, 0.25f);
                        aSource.FxVolPropio(aSource.FxSonidos[2].nombre, 0.25f);
                        }
                        if (TareaActual<2)
                        {
                        llamada_apertura = false;
                            if (ya_interior == false)
                            {

                            aSource.MusicaVol(1f);//**************************************Sonido Musica Inicial*************KRCP***
                            aP.cerrandoDojo(true);
                            }
                        }
                        if (TareaActual >= 2)
                        {

                        if (TareaActual >= 1)
                            aSource.FxVol(1f);
                        aP.cerrandoDojo(false);
                        }
                    }
                
                break;
            case 0://verica contacto de amoladora con barra de hierro
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (amoladoraOn == true)
                    {
                        M_1[3].SetActive(true);//chispas de amolacion
                                               //ad.Play(); sonido de chispas y corte
                        StartCoroutine(cronometro(confirmarContacto));
                    }
                }
                else
                {
                    M_1[3].SetActive(false);//chispas de amolacion
                }
                 break;
            case 2://verifica si cuando se acerca a la 2da puerta ya cuenta con todos los epps
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    
                    //ambientacion.SetActive(false);
                    if (correctaTareaMEpps1 == false)
                    {
                        aSource.goFx("Fallo");
                        aSource.goFx("Locu_Fallo");//********************AGREGADO EL 27-08-24********************////////////
                        Tablero_Indicaciones[3].SetActive(true);
                        TM_Lobby.lb.auxNotas[0]++;
                    }
                    else
                    {
                        if (ya_interior == true)
                        {
                            aSource.goFx("Bien");
                            aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                            aSource.MusicaVol(0.9f);
                            aP.aperturaDojo(false);
                            TM_Lobby.lb.AgregarNota(0, TM_Lobby.lb.auxNotas[0]); ;//********************AGREGADO EL 24-02-25********************////////////
                        }
                        //audio de bienvenido;
                    }
                }
                else
                {
                    if (correctaTareaMEpps1 == true&&ya_interior==true)
                    {
                        aP.cerrandoDojo(false);
                        //ya_interior = false;
                        TareaCompletada(1);//**********************************************************Tarea 1 Completada************
                    }
                    Tablero_Indicaciones[3].SetActive(false);
                }
                break;
            case 3://verifica cpor medio de un collider si el pj esta en el area de epps
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (TareaActual ==0)
                    {
                        aSource.MusicaVol(0.9f);
                        TareaCompletada(0);//////////////////////////////////////////////////completando la tarea 0******************************
                        ya_interior = true;
                    }
                }
                else
                {
                    if(TareaActual == 2)
                    {
                        ya_interior = false;
                    }
                }
                break;
            case 4://cuando se haga contacto con el collider cerca al colaborador trabajando en el motor
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    aSource.goFx("Fallo");//*************************************************verificado/////07-08-24
                    aSource.goFx("Locu_Fallo");//********************AGREGADO EL 27-08-24********************////////////
                    Tablero_Indicaciones[7].SetActive(true);
                    ObjetosReferencias[7].SetActive(true);
                    TM_Lobby.lb.auxNotas[2]++;//*********************************AGREGADO EL 24-02-25********************////////////
                }
                break;
            case 5://si coloca la carga en el lugar adecuado
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    
                    aSource.goFx("Bien");//*************************************************verificado/////07-08-24
                    aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                    NombreAuxAudio = "Devolver_Control";//******************************************agregado 04-09-24************************************
                    StartCoroutine(TiempoEsperaAudio(8));
                    Tablero_Indicaciones[7].SetActive(false);
                    Tablero_Indicaciones[8].SetActive(true);
                    Tablero_Indicaciones[9].SetActive(true);
                    ObjetosReferencias[0].SetActive(false);//detector zona correcta
                    ObjetosReferencias[1].SetActive(false);//detector zona de choque
                    ObjetosReferencias[2].SetActive(true);//refe1
                    ObjetosReferencias[5].SetActive(true);//refe2
                    murosConos[1].SetActive(false);
                }
                break;
            case 6://******************************************************confirma dejar control en el punto verde indicado********////////////////////
                if(contacto_confirmado[confirmarContacto] == true)
                {
                    aSource.altoFx("Devolver_Control");
                    aSource.goFx("Soltar");//*************************************************sonido soltar control grua******************
                    ObjetosReferencias[2].SetActive(false);
                    ObjetosReferencias[3].SetActive(false);
                    ObjetosReferencias[5].SetActive(false);
                    ObjetosReferencias[4].SetActive(true);//si lo deja en bahia fin
                    ObjetosReferencias[7].SetActive(false);
                    aSource.goFx("Bien");//*************************************************verificado/////07-08-24
                    aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                    murosConos[2].SetActive(false);
                    NombreAuxAudio = "SiguienteModulo";
                    StartCoroutine(TiempoEsperaAudio(6));
                    tiempoEsperaAux = 15;
                    StartCoroutine(TiempoEsperaTarea(4));
                    contadorDetectorSgtMod = 1;
                    DetectorSgtM[1].SetActive(true);
                    //TareaCompletada(TareaActual);
                }
                break;
            case 7://control remoto
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    aSource.altoFx("Devolver_Control");
                    aSource.goFx("Soltar");
                    ObjetosReferencias[2].SetActive(false);//REFE1
                    ObjetosReferencias[3].SetActive(false);//DESACTIVA EL CONTROL
                    ObjetosReferencias[5].SetActive(false);//REFE2
                    ObjetosReferencias[6].SetActive(true);//SI LO DEJA EN BAHIA INICIO
                    ObjetosReferencias[7].SetActive(false);
                    aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                    aSource.goFx("Bien");
                    NombreAuxAudio = "SiguienteModulo";
                    murosConos[2].SetActive(false);
                    StartCoroutine(TiempoEsperaAudio(5));
                    tiempoEsperaAux = 15;
                    StartCoroutine(TiempoEsperaTarea(4));
                    contadorDetectorSgtMod = 1;
                    DetectorSgtM[1].SetActive(true);
                    //TareaCompletada(TareaActual);
                }
                break;
            case 8://Pernos000 y adaptador
                if (contacto_confirmado[confirmarContacto]== true&&PernoEnDado==false)
                {
                    //Debug.Log("contacto perno "+(confirmarContacto-8)+"en funcion verificar contacto");
                    StartCoroutine(cronometro(confirmarContacto-7));
                }
                break;
            case 9://Pernos001 y adaptador
                if (contacto_confirmado[confirmarContacto] == true && PernoEnDado == false)
                {
                    //Debug.Log("contacto perno " + (confirmarContacto - 8) + "en funcion verificar contacto");
                    StartCoroutine(cronometro(confirmarContacto - 7));
                }
                break;
            case 10://Pernos002 y adaptador
                if (contacto_confirmado[confirmarContacto] == true && PernoEnDado == false)
                {
                    //Debug.Log("contacto perno " + (confirmarContacto - 8) + "en funcion verificar contacto");
                    StartCoroutine(cronometro(confirmarContacto - 7));
                }
                break;
            case 11 ://Pernos003 y adaptador
                if (contacto_confirmado[confirmarContacto] == true && PernoEnDado == false)
                {
                    
                    //Debug.Log("contacto perno " + (confirmarContacto - 8) + "en funcion verificar contacto");
                    StartCoroutine(cronometro(confirmarContacto - 7));
                }
                break;
        case 12 ://Contacto con el Cajon de pernos
                if (contacto_confirmado[confirmarContacto] == true && PernoEnDado == false)
                {
                    si_algun_pernoEnMano = false;
                    for (int i = 0; i < PernoEnMano.Length; i++)
                        {
                            if (PernoEnMano[i] == true)
                            {
                            Debug.Log("prueba00");
                            si_algun_pernoEnMano = true;
                                break;
                            }
                        }
                    Debug.Log("prueba1");
                    if (!si_algun_pernoEnMano)
                    { 
                        Debug.Log("prueba2");

                        nPernosSacados = 0;

                        //Debug.Log("contacto perno " + (confirmarContacto) + "en funcion verificar contacto COLOCADO en el cajon");
                        for (int j = 0; j < verificacionNPernos.Length; j++)
                            {

                                if (verificacionNPernos[j] == true)
                                {
                                    nPernosSacados++;
                                }
                            }
                            if (nPernosSacados == TotalPernos)
                            {

                                RefeinterruptorCompresora.SetActive(true);
                                Flecha_Indi.SetActive(true);
                            }
                        }
                    else
                    {
                        nPernosSacados = 0;
                        for (int j = 0; j < verificacionNPernos.Length; j++)
                        {
                            
                            if (verificacionNPernos[j] == true)
                            {
                                nPernosSacados++;
                            }
                        }
                        Debug.Log("contacto perno " + (confirmarContacto) + "en funcion verificar contacto SACADO en el cajon");

                    }
                }
                break;
            case 13 ://Contacto de PuntoManglera de Pistola con el detector de manglera
                if (contacto_confirmado[confirmarContacto] == false&&ConectorMPPickedUp==true)
                {
                    if (iwg.Cargada == true)
                    {
                        Debug.Log("contacto manglera " + (confirmarContacto) + " en funcion verificar contacto desconectar de pistola de forma incorrecta");
                        animLatigueoIWG(iwg.Cargada);
                    }
                    else 
                    {
                        Debug.Log("contacto manglera " + (confirmarContacto) + " en funcion verificar contacto desconectar de pistola de forma correcta");
                        animLatigueoIWG(iwg.Cargada);//ac
                        IWP_Refe.SetActive(true);
                    }
                    
                }
                break;
                case 14 ://coloca la IWP en el cajon
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    TM_Lobby.lb.AgregarNota(3, TM_Lobby.lb.auxNotas[3]);//********************AGREGADO EL 24-02-25********************////////////
                    TM_Lobby.lb.GuardarNotasTotales();
                    IWP_Refe.SetActive(false);
                    IWP_Mesh.SetActive(true);
                    IWP_OBJ.SetActive(false);
                    murosConos[3].SetActive(false);
                    aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                    aSource.goFx("Bien");
                    NombreAuxAudio = "SiguienteModulo";
                    StartCoroutine(TiempoEsperaAudio(1));//*******************************************Agregado el 04-09-24***************************************
                    contadorDetectorSgtMod = 2;
                    DetectorSgtM[2].SetActive(true);
                    tiempoEsperaAux = 10;
                    StartCoroutine(TiempoEsperaTarea(5));

                    //TareaCompletada(5);
                }
                break;
                case 15 :
                aSource.goFx("Bien");
                TareaCompletada(6);
                break;
                case 16 ://****Contacto de detector de cambio de modulos para emezar la siguiente tarea****
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    
                    Debug.Log("se detecto contacto con el detector" + contadorDetectorSgtMod);
                    if (contadorDetectorSgtMod + 3 == TareaActual)
                    {
                        Debug.Log("siguiente tarea por detector " + contadorDetectorSgtMod);
                        TareaCompletada(contadorDetectorSgtMod + 3);
                    }
                    DetectorSgtM[contadorDetectorSgtMod].SetActive(false);

                }
                break;
                case 17 :
                IrEscenaAsincron(0);
                break;
                case 18 :
                Application.Quit();
                break;
        }
    }
    //***********MODULO Herramientas con tiempo de accion*****************
    IEnumerator cronometro(int contactoV)
    {
        switch (contactoV)//verifica contacto en el codigo detectorObjObj
        {
            case 0://***** PARA AMOLADORA******************************************
                if (contacto_confirmado[contactoV])//verifica contacto en el codigo detectorObjObj
                {
                    amolando=true;
                    
                    while (contacto_confirmado[contactoV]&& amoladoraOn&&amolando)
                    {
                        yield return new WaitForSeconds(0.5f);
                        if (!amoladoraOn)
                        {
                            M_1[3].SetActive(false);//desactivar particulas
                            tTemp = 0;
                            break;
                        }
                        tTemp += 0.5f;
                        if (tiempoEspera <= tTemp&& TareaActual==2&&tareaCorrectaAmoladora==false)//cumplido el tiempo de espera//cambiado el 07-08-24
                        {
                            if (correctaTareaM2 == true)//si se puso los lentes 
                            {
                                //amolando=false;
//                                aSource.goFx("Bien");//***************************************************07-08-24
                                Tablero_Indicaciones[0].SetActive(false);
                                if (Tablero_Indicaciones[1].activeInHierarchy==true|| Tablero_Indicaciones[5].activeInHierarchy == true)
                                {
                                    Tablero_Indicaciones[1].SetActive(false);
                                    Tablero_Indicaciones[5].SetActive(false);
                                }
                                Tablero_Indicaciones[2].SetActive(true);//pantalla de bien hecho
                                M_1[0].SetActive(false);//desactivar la barra de metal
                                M_1[3].SetActive(false);//desactivar particulas
                                aSource.goFx("Bien");
                                aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                                tareaCorrectaAmoladora=true;

                                break;
                            }
                            else
                            {
                                if (!Si_Careta)
                                {
                                    Tablero_Indicaciones[5].SetActive(false);
                                    Tablero_Indicaciones[1].SetActive(true);
                                    M_1[2].GetComponent<Collider>().enabled = false;
                                    M_1[2].SetActive(true);//activar careta de referencia
                                    TM_Lobby.lb.auxNotas[1]++;//----------------------------aumenta notas para ejercicio 1-----24-02-25
                                }
                                else
                                {
                                    if (!Si_Mandil)
                                    {
                                        Tablero_Indicaciones[1].SetActive(false);
                                        Tablero_Indicaciones[5].SetActive(true);
                                        M_1[5].GetComponent<Collider>().enabled = false;//DEACTIVA EL COLLIDER DE LA REFERENCIA A MANDIL
                                        M_1[5].SetActive(true);//activar MANDIL de referencia
                                        TM_Lobby.lb.auxNotas[1]++;//----------------------------aumenta notas para ejercicio 1-----24-02-25
                                    }
                                }
                                
                                M_1[0].SetActive(false);//desactivar la barra de metal
                                M_1[3].SetActive(false);//desactivar particulas
                                tTemp = 0;
                                if (amolando == true)
                                {
                                    amolando = false;
                                    aSource.goFx("Fallo");//******************************************************Fallos**************//////////////////////////
                                    aSource.goFx("Locu_Fallo");//***************AGREGADO EL 27-08-24**************************///////////////////////////////////////////////
                                }
                                yield return new WaitForSeconds(0.1f);
                                break;
                            }
                        }
                    }
                    if (tareaCorrectaAmoladora && TareaActual == 2)
                    {
                        tTemp = 0;
                        tiempoEsperaAux = 2;

                        StartCoroutine(TiempoEsperaTarea(2));
                        TM_Lobby.lb.AgregarNota(1, TM_Lobby.lb.auxNotas[1]);
                        //TareaCompletada(2);//*********************************************Completar TAREA 2*****************
                    }
                }
                else
                {
                    yield return new WaitForSeconds(0.001f);
                    tTemp = 0;
                }
                    break;
            case 1://*********PARA IMPACT WRENCH GUN****************************Perno000// implementado el 10-07-2024
                if (contacto_confirmado[contactoV+7] == true && PernoEnDado == false)//verifica contacto en el codigo detectorObjObj
                {
                while (contacto_confirmado[contactoV+7] == true && iwg.si_presionando == true)
                    {
                        yield return new WaitForSeconds(0.25f);
                        //Debug.Log("sacando perno "+(contactoV-1)+" tiempoxPernoActual = " + PernosTiempo[contactoV-1]);
                        if (iwg.si_presionando == false)
                        {
                            //aSource.altoFx("IWG_Rot02");
                            break;
                        }
                        PernosTiempo[0] += 0.25f;
                        PernosTiempo[2] = 0;
                        PernosTiempo[1] = 0;
                        PernosTiempo[3] = 0;
                        if (tiempoEspera <= PernosTiempo[contactoV -1])//cumplido el tiempo de espera
                        {
                            //Debug.Log("Perno" + (contactoV - 1) + "sacado en funcion Cronometro");
                            iwg.DetectorPerno.SetActive(false);
                            PernosMesh[contactoV-1].SetActive(false);
                            contacto_confirmado[contactoV + 7] = false;
                            PernosRefe[contactoV-1].SetActive(false);
                            PernosGrab[contactoV - 1].SetActive(true);
                            PernosGrab[contactoV - 1].GetComponent<Return_Pos0>().enabled = false;
                            PernosGrab[contactoV-1].transform.SetParent(PuntoPernoEnMaquina.transform);
                            PernosGrab[contactoV - 1].transform.localPosition = LocalPosPernoSpawn;
                            PernosGrab[contactoV - 1].transform.localEulerAngles = LocalRotPernoSpawn;
                            PernoEnDado = true;
                            Boquilla_ContactoRefe = true;
                            Debug.Log("sacando perno y verificando el sonido" + (contactoV - 1));
                            AudioManager.aSource.altoFxLoop("IWG_Rot01");
                            AudioManager.aSource.goFx("IWG_Rot02",1,true,true);//**************************************************      SONIDO PISTOLA NEUMATICA CONTACTO PERNO*************
                            break;
                        }
                    }
                }
                else
                {
                    Boquilla_ContactoRefe = false;
                    //aSource.altoFx("IWG_Rot02");
                }
                break;
            case 2://*********PARA IMPACT WRENCH GUN****************************Perno001
                if (contacto_confirmado[contactoV + 7] == true&&PernoEnDado==false)//verifica contacto en el codigo detectorObjObj
                {
                    if (contacto_confirmado[contactoV + 7] == true && iwg.si_presionando == true)
                    {
                        
                    }
                    while (contacto_confirmado[contactoV + 7] == true && iwg.si_presionando == true)
                    {
                        yield return new WaitForSeconds(0.25f);
                        if (iwg.si_presionando == false)
                        {
                            aSource.altoFx("IWG_Rot02");
                            break;
                        }
                        PernosTiempo[1] += 0.25f;
                        PernosTiempo[2] = 0;
                        PernosTiempo[0] = 0;
                        PernosTiempo[3] = 0;
                        if (tiempoEspera <= PernosTiempo[contactoV-1])//cumplido el tiempo de espera
                        {
                            //Debug.Log("Perno" + (contactoV - 1) + "sacado en funcion Cronometro");
                            iwg.DetectorPerno.SetActive(false);
                            PernosMesh[contactoV - 1].SetActive(false);
                            contacto_confirmado[contactoV + 7] = false;
                            PernosRefe[contactoV - 1].SetActive(false);
                            PernosGrab[contactoV - 1].SetActive(true);
                            PernosGrab[contactoV - 1].GetComponent<Return_Pos0>().enabled = false;
                            PernosGrab[contactoV - 1].transform.SetParent(PuntoPernoEnMaquina.transform);
                            PernosGrab[contactoV - 1].transform.localPosition = LocalPosPernoSpawn;
                            PernosGrab[contactoV - 1].transform.localEulerAngles = LocalRotPernoSpawn;
                            PernoEnDado = true;
                            Boquilla_ContactoRefe = true;
                            //Debug.Log("Perno" + (contactoV - 1) + "sacado rot incial : "+ PernosGrab[contactoV - 1].transform.localEulerAngles.x+" " +PernosGrab[contactoV - 1].transform.localEulerAngles.y+" " +PernosGrab[contactoV - 1].transform.localEulerAngles.z);
                            AudioManager.aSource.altoFxLoop("IWG_Rot01");
                            AudioManager.aSource.goFx("IWG_Rot02", 1, true, true);//**************************************************      SONIDO PISTOLA NEUMATICA CONTACTO PERNO*************
                        }
                    }
                }
                break;
            case 3://*********PARA IMPACT WRENCH GUN****************************Perno002
                if (contacto_confirmado[contactoV + 7] == true && PernoEnDado == false)//verifica contacto en el codigo detectorObjObj
                {
                    if (contacto_confirmado[contactoV + 7] == true && iwg.si_presionando == true)
                    {
                    
                    }
                    while (contacto_confirmado[contactoV + 7] == true && iwg.si_presionando == true)
                    {
                        yield return new WaitForSeconds(0.25f);
                        if (iwg.si_presionando == false)
                        {
                            aSource.altoFx("IWG_Rot02");
                            break;
                        }
                        PernosTiempo[2] += 0.25f;
                        PernosTiempo[1] = 0;
                        PernosTiempo[0] = 0;
                        PernosTiempo[3] = 0;


                        if (tiempoEspera <= PernosTiempo[contactoV-1])//cumplido el tiempo de espera
                        {
                            //Debug.Log("Perno" + (contactoV - 1) + "sacado en funcion Cronometro");
                            iwg.DetectorPerno.SetActive(false);
                            PernosMesh[contactoV - 1].SetActive(false);
                            contacto_confirmado[contactoV + 7] = false;
                            PernosRefe[contactoV - 1].SetActive(false);
                            PernosGrab[contactoV - 1].SetActive(true);
                            PernosGrab[contactoV - 1].GetComponent<Return_Pos0>().enabled = false;
                            PernosGrab[contactoV - 1].transform.SetParent(PuntoPernoEnMaquina.transform);
                            PernosGrab[contactoV - 1].transform.localPosition = LocalPosPernoSpawn;
                            PernosGrab[contactoV - 1].transform.localEulerAngles = LocalRotPernoSpawn;
                            PernoEnDado = true;
                            Boquilla_ContactoRefe = true;
                            AudioManager.aSource.altoFxLoop("IWG_Rot01");
                            AudioManager.aSource.goFx("IWG_Rot02", 1, true, true);//**************************************************      SONIDO PISTOLA NEUMATICA CONTACTO PERNO*************
                        }
                    }
                }
                break;
            case 4://*********PARA IMPACT WRENCH GUN****************************Perno003
                if (contacto_confirmado[contactoV + 7] == true && PernoEnDado == false)//verifica contacto en el codigo detectorObjObj
                {
                    if (contacto_confirmado[contactoV + 7] == true && iwg.si_presionando == true)
                    {
                   
                    }
                        
                    while (contacto_confirmado[contactoV + 7] == true && iwg.si_presionando == true)
                    {
                        yield return new WaitForSeconds(0.25f);
                        if (iwg.si_presionando == false)
                        {
                            aSource.altoFx("IWG_Rot02");
                            break;
                        }
                        PernosTiempo[3] += 0.25f;
                        PernosTiempo[2] = 0;
                        PernosTiempo[0] = 0;
                        PernosTiempo[1] = 0;
                        if (TiempoPernos <= PernosTiempo[contactoV-1])//cumplido el tiempo de espera
                        {

                            //Debug.Log("Perno" + (contactoV - 1) + "sacado en funcion Cronometro");
                            iwg.DetectorPerno.SetActive(false);
                            PernosMesh[contactoV - 1].SetActive(false);
                            contacto_confirmado[contactoV + 7] = false;
                            PernosRefe[contactoV - 1].SetActive(false);
                            PernosGrab[contactoV - 1].SetActive(true);
                            PernosGrab[contactoV - 1].GetComponent<Return_Pos0>().enabled = false;
                            PernosGrab[contactoV - 1].transform.SetParent(PuntoPernoEnMaquina.transform);
                            PernosGrab[contactoV - 1].transform.localPosition = LocalPosPernoSpawn;
                            PernosGrab[contactoV - 1].transform.localEulerAngles = LocalRotPernoSpawn;
                            PernoEnDado = true;
                            Boquilla_ContactoRefe = true;
                            AudioManager.aSource.altoFxLoop("IWG_Rot01");
                            AudioManager.aSource.goFx("IWG_Rot02", 1, true, true);//**************************************************      SONIDO PISTOLA NEUMATICA CONTACTO PERNO*************
                        }
                    }
                }
                break;
        }
    }
    //***********MODULO EPPS*****************
    public void EppPuesto(int nE)
    {
        
        if(!Flechasgrupo2.activeInHierarchy)
        Flechasgrupo2.SetActive(true);
        //ambientacion.SetActive(false);
        Destroy(ambientacion.gameObject);
        if (nE == 0)
        {
            manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[0];
            manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[0];
            guantesComplementos[0].SetActive(true);
            guantesComplementos[1].SetActive(true);
        }
        if (nE==1)
        {
            aSource.FxVolPropio("0Cigarras001",0.3f);
            aSource.FxVolPropio("1Cuervos_Vientos", 0.1f);
            aSource.FxVolPropio("2Cuervos_Loop", 0.1f);
        }
        Epps[nE].SetActive(false);
        ActivarEvento(1);
    }
    public void ActivarEvento(int nEvento)//Cosas que debn verse con el contacto de las manosXR o Colliders
    {
        switch (nEvento)
        {
            case -2:
                amoladoraOn = false;
                //Debug.Log("apagar sonido de amoladora");
                aSource.altoFx(aSource.FxSonidos[6].nombre);
                M_1[3].SetActive(false);//desactivar particulas
                break;
            case -1:
                amoladoraOn = true;
                M_1[0].SetActive(true);
                aSource.goFx("Amoladora",1,true,false);
                //aSource.PlayFx("Amoladora");
                //ad.play prender sierra
               // Debug.Log("activar sonido de amoladora");
                break;
            case 0://agarrar Careta
                M_1[2].SetActive(false);
                Si_Careta = true;
                M_1[1].SetActive(false);
                M_1[0].SetActive(true);//Activar la barra de metal
                if (Si_Mandil == true)
                {
                    correctaTareaM2 = true;
                }
                //Debug.Log("sonido de equipo");
                break;
            case 1:
                TotalEpps++;
                //aSource.PlayFx()
                if (TotalEpps == 5)
                {
                    Tablero_Indicaciones[3].SetActive(false);
                    correctaTareaMEpps1 = true;
                }
                break;
            case 2://agarra mandilM2
                Si_Mandil = true;
                M_1[3].SetActive(false);//desactivar chispas
                M_1[4].SetActive(false);//desActivar mandil
                M_1[5].SetActive(false);//desActivar mandil refe
                M_1[0].SetActive(true);//Activar la barra de metal
                if (Si_Careta == true)
                {
//                    aSource.goFx("Bien");
                    correctaTareaM2 = true;
                }
                //Debug.Log("sonido de equipo");
                break;
            case 3://devolver careta
                if (TareaActual == 3)
                {
                    M_1[2].SetActive(false);
                    Si_Careta = false;
                    M_1[1].GetComponent<Collider>().enabled = false;
                    M_1[1].SetActive(true);
                    
                    if (Si_Mandil == false)
                    {
                        aSource.goFx("Bien");
                        aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                        murosConos[0].SetActive(false);
                        NombreAuxAudio = "SiguienteModulo";
                        StartCoroutine(TiempoEsperaAudio(8));
                        tiempoEsperaAux = 15;
                        StartCoroutine(TiempoEsperaTarea(3));
                        contadorDetectorSgtMod = 0;
                        DetectorSgtM[0].SetActive(true);
                        //TareaCompletada(3);
                    }
                }
                break;
            case 4://devolver mandil
                if (TareaActual == 3)
                {
                    M_1[5].SetActive(false);//desactivar refe
                    M_1[4].GetComponent<Collider>().enabled = false;
                    Si_Mandil = false;
                    M_1[4].SetActive(true);//activar mandil devuelto
                    if (Si_Careta == false)
                    {
                        aSource.goFx("Bien");
                        aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                        murosConos[0].SetActive(false);
                        NombreAuxAudio = "SiguienteModulo";
                        StartCoroutine(TiempoEsperaAudio(8));
                        tiempoEsperaAux = 15;
                        StartCoroutine(TiempoEsperaTarea(3));
                        contadorDetectorSgtMod = 0;
                        DetectorSgtM[0].SetActive(true);
                        
                        //TareaCompletada(3);
                    }
                }
                break;
            case 5://****************encender el compresor***********10-07-24**************
                if (TareaActual == 5)
                {
                    if (nPernosSacados < TotalPernos)
                    {
                        murosConos[2].SetActive(true);
                        aSource.goFx("Compresor_On", 0.5f, true,true);//*****************************************************************************SONIDO COMPRESORA*******************
                        iwg.MaquinaON_OFF(true);
                        iwg.DetectorPerno.SetActive(true);
                        Flecha_Indi.SetActive(false);
                        RefeinterruptorCompresora.SetActive(false);
                        interruptorBTN_Compress.transform.localEulerAngles = new Vector3(0, 0, rotEncendido);//debe ser angulo -65
                    }
                    else
                    {
                        aSource.altoFxLoop("Compresor_On");
                        aSource.goFx("Guardar_Pistola");
                        //aSource.FxAlto("Compresor_On");
                        iwg.MaquinaON_OFF(false);
                        RefeinterruptorCompresora.SetActive(false);
                        Flecha_Indi.SetActive(false);
                        interruptorBTN_Compress.transform.localEulerAngles = new Vector3(0, 0, 0);//debe ser angulo -65
                        ObjConexionIWPCableRefe.SetActive(true);
                        ObjConexionIWPCable.GetComponent<CapsuleCollider>().enabled = true;//Agregado el 16-07-24********
                        
                    }
                }
                break;
                case 6://Activar el obj que contiene todos los pernos de referencia
                if (TareaActual == 5 && ObjRefePernosConjunto.gameObject.activeSelf==false) 
                {
                    ObjRefePernosConjunto.SetActive(true);
                }

                break;
            case 7://desactive exit
                
                break;
        }
    }
    //************MODULO PUENTE GRUA************************************************
    //***********RECEPTOR DE BOTONES DE CONTROL DE GRUA*****************************
    public void verificarAvisoCorneta()
    {
        if (si_corneta_Presionada == false)
        {
            numNoAvisoCorneta++;
        }
    }
    public void BotoncontrolGruaPress(int btn)//se coloca en el boton para saber que boton se presiono
    {
        Debug.Log("boton presionado "+btn);
        BotonGruaPresionado = btn;
    }
    public void PickUpActivarMuro(int pick)
    {
        murosConos[pick].SetActive(true);
    }
    //********************************MODULO 4 IMPACTGUN PERNOS*********************************************************************
    public void PernosColliderActived(int nPerno)//cuando mano cerca
    {
        //PernosGrab[nPerno].GetComponent<BoxCollider>().enabled = false;
        PernosGrab[nPerno].GetComponent<CapsuleCollider>().enabled = true;
    }
    public void pernosRGBDActived(int nPerno)//cuando agarra el perno
    {
            Debug.Log("Perno" + (nPerno) + "agarrado de activo el toggles de Rigidbody en funcion PernosRGBDActived");

            PernosGrab[nPerno].transform.SetParent(PuntoDeRecepccionPernos.transform);
            PernosGrab[nPerno].GetComponent<BoxCollider>().enabled = true;
            PernosGrab[nPerno].GetComponent<Rigidbody>().isKinematic = false;
            PernosGrab[nPerno].GetComponent<Rigidbody>().useGravity = true;
            PernoEnMano[nPerno] = false;
        
        
    }
    public void verificarNombrePerno(string nomPer, bool si_entrando)//creado el 16-07-24
    {
        AudioManager.aSource.goFx("Perno_Contacto");//************************************************************************************************SONIDO PARA CONTACTO CON EL PISO*************AGREGADO EL 02-08-24
        if (PernoEnDado == false)
        {
            for (int i = 0; i < verificacionNPernos.Length; i++)
            {
                if (nomPer == PernosGrab[i].name)
                {
                    verificacionNPernos[i] = si_entrando;

                    if (si_entrando == true)
                    {
                        if (PernosGrab[i].GetComponent<Rigidbody>().isKinematic == true)//agregado el 07-10-24
                        {
                            Debug.Log("verificamos de que perno " + i + " si este en caja a pesar de estar en el dado");
                            pernosRGBDActived(i);
                        }
                        else
                        {
                            PernosGrab[i].GetComponent<One_Hand_PickUp>().enabled = false;//desactiva este escript paa que no puede ser agarrado denuevo//creado 17/-07-24
                        }


                        PernosGrab[i].name = "Perno_" + i + "_EnCaja";

                    }
                    else
                    {
                        PernosGrab[i].GetComponent<One_Hand_PickUp>().enabled = true;//desactiva este escript paa que no puede ser agarrado denuevo//creado 17/-07-24
                    }
                    break;
                }
                else
                {
                    //Debug.Log("Nombre "+nomPer+" no coincide con PernosGrab["+i+"]");
                }
            }
        }
        
    }
    public void animLatigueoIWG(bool si_descargada)//cuando se detecta la desconexion de la manguera y la pistola
    {
        //CablePuntoFin.transform.SetParent(ObjJerarFinCable.transform);
        if (iwg.Cargada == false)
        {
            aSource.goFx("Bien");
            aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
            CableCorrecto.SetActive(false);
            ObjConexionIWPCable.SetActive(false);
            ObjConexionIWPCableRefe.SetActive(false);
            Tablero_Indicaciones[11].SetActive(false);//panel correcto
            Tablero_Indicaciones[12].SetActive(true);//panel correcto
            ObjConectorSiCorrecto.SetActive(true);
            Debug.Log("Desconexion correcta");
        }
        else
        {//SI NO Descarga
            aSource.goFx("ExplosionAire");////////////////////////////////////////////////////////**************************************SONIDO EXPLOSION*******************
            ObjConexionIWPCable.SetActive(false);
            CableCorrecto.SetActive(false);
            Debug.Log("animacion de latigueo");
            StartCoroutine(AnimLatigueoFull());
            Debug.Log("FIN DE animacion de latigueo");

            ObjConexionIWPCable.transform.SetParent(ObjJerarFinCable.transform);
            Debug.Log("ObjJerarFinCable="+ObjJerarFinCable.name);
            ObjConexionIWPCable.transform.localPosition = conexIWPCalbePos0;
            ObjConexionIWPCable.transform.localEulerAngles = conexIWPCalbeRot0;
            Debug.Log("ObjConexionIWPCable.transform.localPosition.x "+ ObjConexionIWPCable.transform.localPosition.x+ " y "+ ObjConexionIWPCable.transform.localPosition.y+" z "+ ObjConexionIWPCable.transform.localPosition.z);
            ObjConexionIWPCable.GetComponent<CapsuleCollider>().enabled = false;
            ObjConexionIWPCableRefe.SetActive(false);
            Tablero_Indicaciones[11].SetActive(true);//panel fallido
            aSource.goFx("Fallo");
            aSource.goFx("Locu_Fallo");//********************AGREGADO EL 27-08-24********************////////////
            Debug.Log("Desconexion incorrecta");
            TM_Lobby.lb.auxNotas[3]++;
        }
        
    }
    IEnumerator AnimLatigueoFull()
    {
        for (int i = 0; i < ComponentAnimLatigueo.Length; i++)
        {
            ComponentAnimLatigueo[i].SetActive(true);
        }
        ParticulasExpl.SetActive(true);
        Debug.Log("animacion realizando el latigueo");
        yield return new WaitForSeconds(1.5f);
        ParticulasExpl.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("FIN DE animacion de latigueo");
        for (int i = 0; i < ComponentAnimLatigueo.Length; i++)
        {
            ComponentAnimLatigueo[i].SetActive(false);
        }
        CableCorrecto.SetActive(true);
        ObjConexionIWPCable.SetActive(true);
    }
    public void ObjPickedUp(bool grabbed)
    {
        ConectorMPPickedUp= grabbed;
    }
    public void Si_PernoEnMano(int NpEnMano)
    {
        PernoEnDado=false;
        PernoEnMano[NpEnMano] = true;
        PernosGrab[NpEnMano].GetComponent<Return_Pos0>().enabled = true;
        //PernosGrab[NpEnMano].transform.SetParent(PuntoDeRecepccionPernos.transform);
    }
    public void Si_PernoSoltado(int NpSoltado)
    {
        PernoEnMano[NpSoltado] = false;
    }
    //************************************************DETALLES DE INTERFACE DE USUARIO - AGREGADO PARA LOS AUDIOS DE LOCUCION ****************************
    public IEnumerator TiempoEsperaAudio(float t)//*******************************************Agregado el 04-09-24***************************************
    {string nAudioAux=NombreAuxAudio;
        int auxTarea = TareaActual;
        if (tutorial)
        {
            yield return new WaitForSeconds(t+1);
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
                    if(auxTarea != TareaActual)
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
        
    }
    public IEnumerator TiempoEsperaTarea(int tarea)//*******************************************Agregado el 04-09-24***************************************
    {
        yield return new WaitForSeconds(tiempoEsperaAux+1);
        Debug.Log("Se espero por la tarea " + tarea + "- tiempoEsperaAux : " + tiempoEsperaAux);
        if (TareaActual == tarea)
        {
            Debug.Log("Se completo la tarea " + tarea + "- dentro del tiempo tiempoEsperaAux : " + tiempoEsperaAux);
            TareaCompletada(tarea);
        }
        
    }

}

