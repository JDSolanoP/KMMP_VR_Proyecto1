using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.XR.Interaction.Toolkit;
//using UnityEditorInternal;
using UnityEditor.Animations;

public class TM_DojoSeguridad : Lista_Tareas_Controller
{
    public AccionPuertaDojo aP;
    public GameObject[] Tablero_Indicaciones;
    public GameObject[] murosConos;
    [Header("Modulo 0")]
    public GameObject[] guantesComplementos;
    public GameObject[] manosXR;
    public Material[] manosXRMaterial;
    public GameObject ambientacion;
    public GameObject[] detectorPos;
    [Header("Modulo 1")]
    public GameObject[] Epps;
    public bool correctaTareaMEpps1 = false;
    public int nEpps=0;
    public int TotalEpps = 0;
    public bool ya_interior=false;
    [Header ("Modulo 2")]
    public AudioSource ad;
    public AudioClip aClip;
    public GameObject[] M_1;
    public bool Si_Mandil = false;
    public bool Si_Careta = false;
    public bool correctaTareaM2=false;
    public bool[] contacto_confirmado;
    public float tiempoEspera;//para esperar el tiempo antes de activacion de otro evento
    public float tTemp=0;//recorrido del tiempo
    public bool amoladoraOn=false;
    [Header("Modulo 3")]
    public GameObject[] ObjetosReferencias;
    public bool CtrlGruaPick = false;
    //public AccionPuenteGrua apGrua;
    public int BotonGruaPresionado;
    [Header("Modulo 4")]
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
                //Tablero_Indicaciones[0].SetActive(true);
                //audioManager de bienvenida
                yield return new WaitForSeconds(0.1f);

                //Debug.Log("Se esta reproduciendo audio");

                while (audioManager.aSource.isPlaying == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se tiene todos los EPPS
            case 1:// AQUI, esta dentro de la instancia intermedia
                
                //audioManager de bienvenida
                yield return new WaitForSeconds(0.1f);

                //Debug.Log("Se esta reproduciendo audio");

                while (audioManager.aSource.isPlaying == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se sale del area intermedia
            case 2:// empieza la amoladora
                Tablero_Indicaciones[0].SetActive(true);
                //ya_interior = false;
                //Debug.Log("Se esta reproduciendo audio");

                while (audioManager.aSource.isPlaying == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//Se hizo correctamente el ejercicio de amoladora

            case 3:// Da pase a la siguiente area
                M_1[2].SetActive(true);
                M_1[5].SetActive(true);
                M_1[2].GetComponent<Collider>().enabled = true;
                M_1[5].GetComponent<Collider>().enabled = true;
                //murosConos[0].SetActive(false);
                //Tablero_Indicaciones[3].SetActive(true);
                //Debug.Log("Se esta reproduciendo audio");
                while (audioManager.aSource.isPlaying == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 4:// Da pase a la siguiente area PUENTE GRUA
                murosConos[0].SetActive(false);
                Tablero_Indicaciones[6].SetActive(true);
                ObjetosReferencias[0].SetActive(true);
                //Debug.Log("Se esta reproduciendo audio");
                while (audioManager.aSource.isPlaying == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;//Fin puente grua
            case 5:// Da pase a la siguiente area PISTOLA NEUMATICA
                murosConos[2].SetActive(false);
                Tablero_Indicaciones[10].SetActive(true);
                RefeinterruptorCompresora.SetActive(true);
                conexIWPCalbePos0=ObjConexionIWPCable.transform.localPosition;
                conexIWPCalbeRot0 = ObjConexionIWPCable.transform.localEulerAngles;
                //Tablero_Indicaciones[6].SetActive(true);
                //ObjetosReferencias[0].SetActive(true);
                //Debug.Log("Se esta reproduciendo audio");
                while (audioManager.aSource.isPlaying == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;//Fin de modulo de PISTOLA NEUMATICA, CUANDO SE DESCONECTA CORRECTAMENTE LA PISTOLA DEL COMPRESOR DE AIRE
            case 6://INICIO DE MOD5 Conclusiones
                murosConos[3].SetActive(false);
                Tablero_Indicaciones[13].SetActive(true);
                Tablero_Indicaciones[14].SetActive(true);
                //ObjetosReferencias[0].SetActive(true);
                //Debug.Log("Se esta reproduciendo audio");
                while (audioManager.aSource.isPlaying == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 7:// Da p
                murosConos[3].SetActive(false);
                //Tablero_Indicaciones[6].SetActive(true);
                //ObjetosReferencias[0].SetActive(true);
                //Debug.Log("Se esta reproduciendo audio");
                while (audioManager.aSource.isPlaying == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
        }
    }
    public void EncenderAmoladora()
    {
        ad.Play();
    }
    public void ApagarAmoladora()
    {
        ad.Stop();
    }
    public void verificarContacto(int confirmarContacto)//*******realiza acciones cuando2 objetos interactuan usan
    {
        switch (confirmarContacto)
        {
            case 1://verifica al pj en el area de la 1era puerta
                    if (contacto_confirmado[confirmarContacto] == true)
                    {
                        if (ya_interior == false)
                        {
                            if (TareaActual == 0)
                            {
                                aP.aperturaDojo(true);
                            }
                        }
                        else
                        {
                            if (correctaTareaM2 == true)
                            {
                                aP.aperturaDojo(false);
                            }
                        }
                    }
                    else
                    {
                        if (ya_interior == true)
                        {
                            aP.cerrandoDojo(true);
                        }
                        if (TareaActual>=0&&TareaActual<2)
                        {
                            if (ya_interior == false)
                            {
                                aP.cerrandoDojo(true);
                            }
                        }
                        if (TareaActual >= 2)
                        {
                            if (TareaActual >= 1)
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
                    ambientacion.SetActive(false);
                    if (correctaTareaMEpps1 == false)
                    {
                        Tablero_Indicaciones[3].SetActive(true);
                    }
                    else
                    {
                        if (ya_interior == true)
                        {
                            aP.aperturaDojo(false);
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
                    Tablero_Indicaciones[7].SetActive(true);
                    ObjetosReferencias[7].SetActive(true);
                    //if()
                }
                break;
            case 5://si coloca la carga en el lugar adecuado
                if (contacto_confirmado[confirmarContacto] == true)
                {
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
            case 6:
                if(contacto_confirmado[confirmarContacto] == true)
                {
                    ObjetosReferencias[2].SetActive(false);
                    ObjetosReferencias[3].SetActive(false);
                    ObjetosReferencias[5].SetActive(false);
                    ObjetosReferencias[4].SetActive(true);//si lo deja en bahia fin
                    ObjetosReferencias[7].SetActive(false);
                    TareaCompletada(TareaActual);
                }
                break;
            case 7://control remoto
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    ObjetosReferencias[2].SetActive(false);//REFE1
                    ObjetosReferencias[3].SetActive(false);//DESACTIVA EL CONTROL
                    ObjetosReferencias[5].SetActive(false);//REFE2
                    ObjetosReferencias[6].SetActive(true);//SI LO DEJA EN BAHIA INICIO
                    ObjetosReferencias[7].SetActive(false);
                    TareaCompletada(TareaActual);
                }
                break;
            case 8://Pernos000 y adaptador
                if (contacto_confirmado[confirmarContacto]== true&&PernoEnDado==false)
                {
                    Debug.Log("contacto perno "+(confirmarContacto-8)+"en funcion verificar contacto");
                    StartCoroutine(cronometro(confirmarContacto-7));
                }
                break;
            case 9://Pernos001 y adaptador
                if (contacto_confirmado[confirmarContacto] == true && PernoEnDado == false)
                {
                    Debug.Log("contacto perno " + (confirmarContacto - 8) + "en funcion verificar contacto");
                    StartCoroutine(cronometro(confirmarContacto - 7));
                }
                break;
            case 10://Pernos002 y adaptador
                if (contacto_confirmado[confirmarContacto] == true && PernoEnDado == false)
                {
                    Debug.Log("contacto perno " + (confirmarContacto - 8) + "en funcion verificar contacto");
                    StartCoroutine(cronometro(confirmarContacto - 7));
                }
                break;
            case 11 ://Pernos003 y adaptador
                if (contacto_confirmado[confirmarContacto] == true && PernoEnDado == false)
                {
                    Debug.Log("contacto perno " + (confirmarContacto - 8) + "en funcion verificar contacto");
                    StartCoroutine(cronometro(confirmarContacto - 7));
                }
                break;
        case 12 ://Contacto con el Cajon de pernos
                if (PernoEnDado == false) 
                {
                    nPernosSacados = 0;
                    if (contacto_confirmado[confirmarContacto] == true)
                    {
                        Debug.Log("contacto perno " + (confirmarContacto) + "en funcion verificar contacto COLOCADO en el cajon");
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
                        }
                    }
                    else
                    {
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
                    IWP_Refe.SetActive(false);
                    IWP_Mesh.SetActive(true);
                    IWP_OBJ.SetActive(false);
                    TareaCompletada(5);
                }
                break;


        }
    }
    //***********MODULO Herramientas con tiempo de accion*****************
    IEnumerator cronometro(int contactoV)
    {
        switch (contactoV)//verifica contacto en el codigo detectorObjObj
        {
            case 0://***** PARA AMOLADORA******************************************
                if (contacto_confirmado[contactoV] == true)//verifica contacto en el codigo detectorObjObj
                {
                    while (contacto_confirmado[contactoV] == true && amoladoraOn==true)
                    {
                        yield return new WaitForSeconds(0.25f);
                        if (amoladoraOn == false)
                        {
                            M_1[3].SetActive(false);//desactivar particulas
                            tTemp = 0;
                            break;
                        }
                        tTemp += 0.25f;
                        if (tiempoEspera <= tTemp)//cumplido el tiempo de espera
                        {
                            if (correctaTareaM2 == true)//si se puso los lentes 
                            {
                                Tablero_Indicaciones[0].SetActive(false);
                                if (Tablero_Indicaciones[1].activeInHierarchy==true|| Tablero_Indicaciones[5].activeInHierarchy == true)
                                {
                                    Tablero_Indicaciones[1].SetActive(false);
                                    Tablero_Indicaciones[5].SetActive(false);
                                }
                                Tablero_Indicaciones[2].SetActive(true);//pantalla de bien hecho
                                M_1[0].SetActive(false);//desactivar la barra de metal
                                M_1[3].SetActive(false);//desactivar particulas
                                TareaCompletada(2);//*********************************************Completar TAREA 2*****************
                            }
                            else
                            {
                                if (Si_Careta==false)
                                {
                                    Tablero_Indicaciones[1].SetActive(true);
                                    M_1[2].GetComponent<Collider>().enabled = false;
                                    M_1[2].SetActive(true);//activar careta de referencia
                                }
                                else
                                {
                                    if (Si_Mandil == false)
                                    {
                                        Tablero_Indicaciones[5].SetActive(true);
                                        M_1[5].GetComponent<Collider>().enabled = false;
                                        M_1[5].SetActive(true);//activar lentes de referencia
                                    }
                                }
                                M_1[0].SetActive(false);//desactivar la barra de metal
                                M_1[3].SetActive(false);//desactivar particulas
                            }
                            tTemp = 0;
                            break;
                        }

                    }

                }
                else
                {
                    yield return new WaitForSeconds(0.001f);
                    tTemp = 0;
                }
                    break;
            case 1://*********PARA IMPACT WRENCH GUN****************************Perno000// implementado el 10-07-2024
                if (contacto_confirmado[contactoV+7] == true)//verifica contacto en el codigo detectorObjObj
                {
                    while (contacto_confirmado[contactoV+7] == true && iwg.si_presionando == true)
                    {
                        yield return new WaitForSeconds(0.25f);
                        //Debug.Log("sacando perno "+(contactoV-1)+" tiempoxPernoActual = " + PernosTiempo[contactoV-1]);
                        if (iwg.si_presionando == false)
                        {
                            break;
                        }
                        PernosTiempo[contactoV-1] += 0.25f;
                        if (tiempoEspera <= PernosTiempo[contactoV -1])//cumplido el tiempo de espera
                        {
                            //Debug.Log("Perno" + (contactoV - 1) + "sacado en funcion Cronometro");
                            PernosMesh[contactoV-1].SetActive(false);
                            PernosRefe[contactoV-1].SetActive(false);
                            PernosGrab[contactoV-1].SetActive(true);
                            PernosGrab[contactoV-1].transform.SetParent(PuntoPernoEnMaquina.transform);
                            PernosGrab[contactoV - 1].transform.localPosition = LocalPosPernoSpawn;
                            PernosGrab[contactoV - 1].transform.localEulerAngles = LocalRotPernoSpawn;
                            PernoEnDado = true;
                        }
                    }
                }
                break;
            case 2://*********PARA IMPACT WRENCH GUN****************************Perno001
                if (contacto_confirmado[contactoV + 7] == true)//verifica contacto en el codigo detectorObjObj
                {
                    while (contacto_confirmado[contactoV + 7] == true && iwg.si_presionando == true)
                    {
                        yield return new WaitForSeconds(0.25f);
                        if (iwg.si_presionando == false)
                        {
                            break;
                        }
                        PernosTiempo[contactoV-1] += 0.25f;
                        if (tiempoEspera <= PernosTiempo[contactoV-1])//cumplido el tiempo de espera
                        {
                            //Debug.Log("Perno" + (contactoV - 1) + "sacado en funcion Cronometro");
                            PernosMesh[contactoV - 1].SetActive(false);
                            PernosRefe[contactoV - 1].SetActive(false);
                            PernosGrab[contactoV - 1].SetActive(true);
                            PernosGrab[contactoV - 1].transform.SetParent(PuntoPernoEnMaquina.transform);
                            PernosGrab[contactoV - 1].transform.localPosition = LocalPosPernoSpawn;
                            PernosGrab[contactoV - 1].transform.localEulerAngles = LocalRotPernoSpawn;
                            PernoEnDado = true;
                            //Debug.Log("Perno" + (contactoV - 1) + "sacado rot incial : "+ PernosGrab[contactoV - 1].transform.localEulerAngles.x+" " +PernosGrab[contactoV - 1].transform.localEulerAngles.y+" " +PernosGrab[contactoV - 1].transform.localEulerAngles.z);
                        }
                    }
                }
                break;
            case 3://*********PARA IMPACT WRENCH GUN****************************Perno002
                if (contacto_confirmado[contactoV + 7] == true)//verifica contacto en el codigo detectorObjObj
                {
                    while (contacto_confirmado[contactoV + 7] == true && iwg.si_presionando == true)
                    {
                        yield return new WaitForSeconds(0.25f);
                        if (iwg.si_presionando == false)
                        {
                            break;
                        }
                        PernosTiempo[contactoV-1] += 0.25f;
                        if (tiempoEspera <= PernosTiempo[contactoV-1])//cumplido el tiempo de espera
                        {
                            //Debug.Log("Perno" + (contactoV - 1) + "sacado en funcion Cronometro");
                            PernosMesh[contactoV - 1].SetActive(false);
                            PernosRefe[contactoV - 1].SetActive(false);
                            PernosGrab[contactoV - 1].SetActive(true);
                            PernosGrab[contactoV - 1].transform.SetParent(PuntoPernoEnMaquina.transform);
                            PernosGrab[contactoV - 1].transform.localPosition = LocalPosPernoSpawn;
                            PernosGrab[contactoV - 1].transform.localEulerAngles = LocalRotPernoSpawn;
                            PernoEnDado = true;
                        }
                    }
                }
                break;
            case 4://*********PARA IMPACT WRENCH GUN****************************Perno003
                if (contacto_confirmado[contactoV + 7] == true)//verifica contacto en el codigo detectorObjObj
                {
                    while (contacto_confirmado[contactoV + 7] == true && iwg.si_presionando == true)
                    {
                        yield return new WaitForSeconds(0.25f);
                        if (iwg.si_presionando == false)
                        {
                            break;
                        }
                        PernosTiempo[contactoV-1] += 0.25f;
                        if (TiempoPernos <= PernosTiempo[contactoV-1])//cumplido el tiempo de espera
                        {
                            //Debug.Log("Perno" + (contactoV - 1) + "sacado en funcion Cronometro");
                            PernosMesh[contactoV - 1].SetActive(false);
                            PernosRefe[contactoV - 1].SetActive(false);
                            PernosGrab[contactoV - 1].SetActive(true);
                            PernosGrab[contactoV - 1].transform.SetParent(PuntoPernoEnMaquina.transform);
                            PernosGrab[contactoV - 1].transform.localPosition = LocalPosPernoSpawn;
                            PernosGrab[contactoV - 1].transform.localEulerAngles = LocalRotPernoSpawn;
                            PernoEnDado = true;
                        }
                    }
                }
                break;
        }
    }
    //***********MODULO EPPS*****************
    public void EppPuesto(int nE)
    {
       if (nE == 0)
        {
            manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[0];
            manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[0];
            guantesComplementos[0].SetActive(true);
            guantesComplementos[1].SetActive(true);
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
                Debug.Log("apagar sonido de amoladora");
                M_1[3].SetActive(false);//desactivar particulas
                break;
            case -1:
                amoladoraOn = true;
                M_1[0].SetActive(true);
                //ad.play prender sierra
                Debug.Log("activar sonido de amoladora");
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
                Debug.Log("sonido de equipo");
                break;
            case 1:
                TotalEpps++;
                if (TotalEpps == 5)
                {
                    Tablero_Indicaciones[3].SetActive(false);
                    correctaTareaMEpps1 = true;
                }
                break;
            case 2:
                Si_Mandil = true;
                M_1[3].SetActive(false);//desactivar chispas
                M_1[4].SetActive(false);//desActivar mandil
                M_1[5].SetActive(false);//desActivar mandil refe
                M_1[0].SetActive(true);//Activar la barra de metal
                if (Si_Careta == true)
                {
                    correctaTareaM2 = true;
                }
                Debug.Log("sonido de equipo");
                break;
            case 3://devolver careta
                if (TareaActual == 3)
                {
                    M_1[2].SetActive(false);
                    Si_Careta = false;
                    M_1[1].SetActive(true);
                    if (Si_Mandil == false)
                    {
                        TareaCompletada(3);
                    }
                }
                break;
            case 4://devolver mandil
                if (TareaActual == 3)
                {
                    M_1[5].SetActive(false);//desactivar refe
                    Si_Mandil = false;
                    M_1[4].SetActive(true);//activar mandil devuelto
                    if (Si_Careta == false)
                    {
                        TareaCompletada(3);
                    }
                }
                break;
            case 5://****************encender el compresor***********10-07-24**************
                if (TareaActual == 5)
                {
                    if (nPernosSacados < TotalPernos)
                    {
                        iwg.MaquinaON_OFF(true);
                        RefeinterruptorCompresora.SetActive(false);
                        interruptorBTN_Compress.transform.localEulerAngles = new Vector3(0, 0, rotEncendido);//debe ser angulo -65
                    }
                    else
                    {
                        iwg.MaquinaON_OFF(false);
                        RefeinterruptorCompresora.SetActive(false);
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
    public void BotoncontrolGruaPress(int btn)//se coloca en el boton para saber que boton se presiono
    {
        Debug.Log("boton presionado "+btn);
        BotonGruaPresionado = btn;
    }
    public void PickUpActivarMuro(int pick)
    {
        murosConos[pick].SetActive(true);
    }
    //********************************MODULO 4 IMPACTGUN PERNOS******************************
    public void PernosColliderActived(int nPerno,bool si_activado)//cuando mano cerca
    {
        PernosGrab[nPerno].GetComponent<BoxCollider>().enabled =si_activado;
        PernosGrab[nPerno].GetComponent<CapsuleCollider>().enabled = si_activado;
    }
    public void pernosRGBDActived(int nPerno)//cuando agarra el perno
        {
        Debug.Log("Perno" + (nPerno) + "agarrado de activo el toggles de Rigidbody en funcion PernosRGBDActived");
        PernosGrab[nPerno].transform.SetParent(PuntoDeRecepccionPernos.transform);
        PernosGrab[nPerno].GetComponent<Rigidbody>().isKinematic = false;
        PernosGrab[nPerno].GetComponent<Rigidbody>().useGravity = true;
        PernoEnDado = false;
        PernoEnMano[nPerno] = true;
    }
    public void verificarNombrePerno(string nomPer, bool si_entrando)//creado el 16-07-24
    {
        for(int i = 0; i < verificacionNPernos.Length; i++)
        {
            if (nomPer == PernosGrab[i].name)
            {
                verificacionNPernos[i] = si_entrando;

                if (si_entrando == true)
                {
                    PernosGrab[i].GetComponent<One_Hand_PickUp>().enabled = false;//desactiva este escript paa que no puede ser agarrado denuevo//creado 17/-07-24

                }
                else
                {
                    PernosGrab[i].GetComponent<One_Hand_PickUp>().enabled = true;//desactiva este escript paa que no puede ser agarrado denuevo//creado 17/-07-24
                }
                break;
            }
            else
            {
                Debug.Log("Nombre "+nomPer+" no coincide con PernosGrab["+i+"]");
            }
        }
    }
    public void animLatigueoIWG(bool si_descargada)//cuando se detecta la desconexion de la manguera y la pistola
    {
        //CablePuntoFin.transform.SetParent(ObjJerarFinCable.transform);
        if (iwg.Cargada == false)
        {
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
            Debug.Log("Desconexion incorrecta");
        }
        
    }
    IEnumerator AnimLatigueoFull()
    {
        for (int i = 0; i < ComponentAnimLatigueo.Length; i++)
        {
            ComponentAnimLatigueo[i].SetActive(true);
        }
        Debug.Log("animacion realizando el latigueo");
        yield return new WaitForSeconds(4f);
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
        PernoEnMano[NpEnMano] = true;
    }
    public void Si_PernoSoltado(int NpSoltado)
    {
        PernoEnMano[NpSoltado] = false;
    }
}

