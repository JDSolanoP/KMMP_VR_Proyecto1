using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.XR.Interaction.Toolkit;
public class TM_IZAJE_M1 : Lista_Tareas_Controller
{
    
    public GameObject[] murosConos;
    public GameObject[] DetectorSgtM;
    public int contadorDetectorSgtMod = 0;
    string NombreAuxAudio;
    float tiempoEsperaAux;
    [Header("PARTE 0")]
    public GameObject[] guantesComplementos;
    public GameObject[] manosXR;
    public Material[] manosXRMaterial;
    
    public GameObject[] detectorPos;
    [Header("PARTE 1")]
    public GameObject[] Epps;
    public GameObject Flechasgrupo2;
    public bool correctaTareaMEpps1 = false;
    public int nEpps = 0;
    public int TotalEpps = 0;
    
    [Header("PARTE 2")]
    public Control_Grua_Puente Ctrl_Grua;
    public GameObject[] ObjetosReferencias;
    public bool si_corneta_Presionada;
    public int numNoAvisoCorneta;
    public bool CtrlGruaPick = false;
    
    public int BotonGruaPresionado;
    // Start is called before the first frame update
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
            case 0:// AQUI, en al entra del taller
                   //audioManager de bienvenida
                activarCtrlBTN(false);
                aSource.PlayMusica(aSource.MusicaSonidos[0].nombre, 1f, true);
                yield return new WaitForSeconds(0.5f);
                manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[0];
                manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[0];

                aSource.MusicaVol(1);//**************************************Sonido Musica Inicial*************
                //aSource.FxVol(1);
                for (int i = 0; i < Tablero_Indicaciones.Length; i++)//apago todos los paneles
                {
                    Tablero_Indicaciones[i].SetActive(false);
                }
                yield return new WaitForSeconds(1f);
                Tablero_Indicaciones[0].SetActive(true);//tablero de bienvenida

                //yield return new WaitForSeconds(3f);
              
                //Debug.Log("Se esta reproduciendo audio");

                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                yield return new WaitForSeconds(1f);
                TareaCompletada(0);
                break;//cuando se tiene todos los EPPS
            case 1:
                Tablero_Indicaciones[1].SetActive(true);//tablero de parte1
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se sale del area intermedia
            case 2:// AQUI, esta en la parte 2 para usar el puente grua


                yield return new WaitForSeconds(.1f);
                aSource.altoFx("SiguienteModulo");
                Tablero_Indicaciones[3].SetActive(true);
                Tablero_Indicaciones[4].SetActive(true);
                ObjetosReferencias[2].SetActive(true);
                ObjetosReferencias[3].SetActive(true);

                Tablero_Indicaciones[1].SetActive(false);
                //Debug.Log("Se esta reproduciendo audio");

                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se sale del area intermedia
            case 3://INICIO DE MOD5 Conclusiones
                //aSource.goFx("Bien");
                //aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                aSource.altoFx("Sgte_Area");
                Tablero_Indicaciones[7].SetActive(false);
                Tablero_Indicaciones[8].SetActive(false);
                Tablero_Indicaciones[9].SetActive(true);
                yield return new WaitForSeconds(14.7f);
                UI_btn_Continuar_Panel.SetActive(true);

                //ObjetosReferencias[0].SetActive(true);
                //Debug.Log("Se esta reproduciendo audio");
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 4:// Finde conclusiones
                //aSource.VocesSourceCanal[aSource.VozCanalActual].Stop();
                UI_btn_Reiniciar_Panel.SetActive(false);
                UI_btn_Salir_Panel.SetActive(false);
                aSource.goFx("Aplausos");
                aSource.goFx("fanfarrias");
                Tablero_Indicaciones[9].SetActive(false);
                Tablero_Indicaciones[10].SetActive(true);
                murosConos[3].SetActive(false);
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
            case 5:
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
            case 0://verifica al pj con todos los epps
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (correctaTareaMEpps1 == false)
                    {
                        aSource.goFx("Fallo");
                        aSource.goFx("Locu_Fallo");//********************AGREGADO EL 27-08-24********************////////////
                        Tablero_Indicaciones[2].SetActive(true);
                    }
                    else
                    {
                        if (TareaActual == 1)
                        {
                            aSource.goFx("Bien");
                            aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                            murosConos[0].SetActive(false);
                            Tablero_Indicaciones[2].SetActive(false);//fallido m0
                            tiempoEsperaAux = 2;
                            StartCoroutine(TiempoEsperaTarea(1));
                            //TareaCompletada(0);//**********************************************************Tarea 1 Completada************
                        }

                    }
                }
                else { Tablero_Indicaciones[2].SetActive(false); }
            break;
            case 1://Detectar_objeto izable si  no se presiono la corneta
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (Ctrl_Grua.corneta_presionada == true)
                    {
                        ObjetosReferencias[0].SetActive(false);////detector cornete
                    }
                    else
                    {
                        aSource.goFx("Fallo");
                        aSource.goFx("Locu_Fallo");//********************AGREGADO EL 276-08-24********************////////////
                        Tablero_Indicaciones[5].SetActive(true);
                    }
                }
                else
                {
                    Tablero_Indicaciones[5].SetActive(false);
                }
            break;
            case 2://deactivar el panel si se activo el error y se esta sobre el area desde el boton corneta
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    Ctrl_Grua.corneta_presionada = true;
                    ObjetosReferencias[0].SetActive(false);//dectector corneta
                    if (Tablero_Indicaciones[5].activeInHierarchy)
                    {
                        
                        Tablero_Indicaciones[5].SetActive(false);
                    }
                }
                break;
            case 3://activa error de caga cerca a colaborador
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    aSource.goFx("Fallo");//*************************************************verificado/////07-08-24
                    aSource.goFx("Locu_Fallo");//********************AGREGADO EL 27-08-24********************////////////
                    Tablero_Indicaciones[6].SetActive(true);
                    ObjetosReferencias[4].SetActive(true);//flechas
                }
                break;
            case 4://si coloca la carga en el lugar adecuado
                if (contacto_confirmado[confirmarContacto] == true)
                {

                    aSource.goFx("Bien");//*************************************************verificado/////07-08-24
                    aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                    NombreAuxAudio = "Devolver_Control";//******************************************agregado 04-09-24************************************
                    StartCoroutine(TiempoEsperaAudio(8));
                    Tablero_Indicaciones[6].SetActive(false);
                    Tablero_Indicaciones[8].SetActive(true);
                    Tablero_Indicaciones[7].SetActive(true);
                    ObjetosReferencias[2].SetActive(false);//detector zona correcta
                    ObjetosReferencias[1].SetActive(false);//detector zona de choque
                    ObjetosReferencias[5].SetActive(true);//refe1 controles refe
                    ObjetosReferencias[6].SetActive(true);//refe2 controles refe
                    murosConos[1].SetActive(false);
                }
                break;
            case 5://******************************************************confirma dejar control en el punto verde indicado de la bahia fin********////////////////////
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    aSource.altoFx("Devolver_Control");
                    aSource.goFx("Soltar");//*************************************************sonido soltar control grua******************
                    ObjetosReferencias[5].SetActive(false);
                    ObjetosReferencias[6].SetActive(false);
                    ObjetosReferencias[9].SetActive(false);//control grua obj
                    ObjetosReferencias[8].SetActive(true);//mesh si lo deja en bahia fin
                    ObjetosReferencias[4].SetActive(false);//DESACTIVA ADVERTENCIA DE ERROR EN CASO CARGA ESTE MAL UBICADA
                    aSource.goFx("Bien");//*************************************************verificado/////07-08-24
                    aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                    murosConos[2].SetActive(false);
                    NombreAuxAudio = "Sgte_Area";
                    StartCoroutine(TiempoEsperaAudio(8));
                    tiempoEsperaAux = 16;
                    StartCoroutine(TiempoEsperaTarea(2));
                    contadorDetectorSgtMod = 1;
                    DetectorSgtM[1].SetActive(true);
                    //TareaCompletada(TareaActual);
                }
                break;
            case 6://control remoto en la bahia inicio
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    aSource.altoFx("Devolver_Control");
                    aSource.goFx("Soltar");
                    ObjetosReferencias[5].SetActive(false);
                    ObjetosReferencias[6].SetActive(false);
                    ObjetosReferencias[9].SetActive(false);//control grua obj
                    ObjetosReferencias[7].SetActive(true);//mesh si lo deja en bahia inicio
                    ObjetosReferencias[4].SetActive(false);//DESACTIVA ADVERTENCIA DE ERROR EN CASO CARGA ESTE MAL UBICADA
                    aSource.goFx("Locu_Bien");//********************AGREGADO EL 27-08-24********************////////////
                    aSource.goFx("Bien");
                    NombreAuxAudio = "Sgte_Area";
                    murosConos[2].SetActive(false);
                    StartCoroutine(TiempoEsperaAudio(5));
                    tiempoEsperaAux = 16;
                    StartCoroutine(TiempoEsperaTarea(2));
                    contadorDetectorSgtMod = 1;
                    DetectorSgtM[1].SetActive(true);
                    //TareaCompletada(TareaActual);
                }
                break;
            case 10:
                aSource.goFx("Bien");
                TareaCompletada(3);
                break;
            case 9://****Contacto de detector de cambio de modulos para emezar la siguiente tarea****
                if (contacto_confirmado[confirmarContacto] == true)
                {

                    Debug.Log("se detecto contacto con el detector" + contadorDetectorSgtMod);
                    TareaCompletada(contadorDetectorSgtMod+1);
                    DetectorSgtM[GlobalInt[0]].SetActive(false);
                    if (GlobalInt[0] == 0) { ObjetosReferencias[10].SetActive(false); }

                }
                break;
            case 7:
                IrEscenaAsincron(0);
                break;
            case 8:
                Application.Quit();
                break;
        }
    }
    public void EppPuesto(int nE)
    {
        if (!Flechasgrupo2.activeInHierarchy)
            Flechasgrupo2.SetActive(true);
        if (nE == 0)
        {
            manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
            manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
            guantesComplementos[0].SetActive(true);
            guantesComplementos[1].SetActive(true);
        }
        if (nE == 1)
        {
            aSource.MusicaVol(0.8f);
        }
        Epps[nE].SetActive(false);
        ActivarEvento(0);
    }
    public void ActivarEvento(int nEvento)//Cosas que debn verse con el contacto de las manosXR o Colliders
    {
        switch (nEvento)
        {
            case 0:
                TotalEpps++;
                //aSource.PlayFx()
                if (TotalEpps == 5)
                {
                    Tablero_Indicaciones[2].SetActive(false);
                    correctaTareaMEpps1 = true;
                }
            break;
        }
    }
    //********************************************************Parte 2 puente grua******************************
    public void verificarAvisoCorneta()
    {
        if (si_corneta_Presionada == false)
        {
            numNoAvisoCorneta++;
        }
    }
    public void BotoncontrolGruaPress(int btn)//se coloca en el boton para saber que boton se presiono
    {
        Debug.Log("boton presionado " + btn);
        BotonGruaPresionado = btn;
    }
    public void PickUpActivarMuro(int pick)
    {
        murosConos[pick].SetActive(true);
    }
    //************************************************* FUNCIONES EXTRAS********************************************************
    public IEnumerator TiempoEsperaAudio(float t)//*******************************************Agregado el 04-09-24***************************************
    {
        string nAudioAux = NombreAuxAudio;
        int auxTarea = TareaActual;
        if (!EnPruebas)
        {
            yield return new WaitForSeconds(t);
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
                    yield return new WaitForSeconds(t);
                    if (auxTarea != TareaActual)
                    {
                        NombreAuxAudio = "";
                        break;
                    }
                    aSource.goFx(NombreAuxAudio);
                }
                yield return new WaitForSeconds(t);
            }
            NombreAuxAudio = "";
        }

    }
    public IEnumerator TiempoEsperaTarea(int tarea)//*******************************************Agregado el 04-09-24***************************************
    {
        yield return new WaitForSeconds(tiempoEsperaAux + 1);
        Debug.Log("Se espero por la tarea " + tarea + "- tiempoEsperaAux : " + tiempoEsperaAux);
        if (TareaActual == tarea)
        {
            Debug.Log("Se completo la tarea " + tarea + "- dentro del tiempo tiempoEsperaAux : " + tiempoEsperaAux);
            TareaCompletada(tarea);
        }

    }

    public void ActiveObj(GameObject obj,bool si)
    {
        obj.SetActive(si);
    }
    public void activarCtrlBTN(bool on)
    {
        if (Ctrl_Grua.BTN_CTRL.Length != 0)
        {
            for (int i=0;i< Ctrl_Grua.BTN_CTRL.Length; i++) 
            {
                ActiveObj(Ctrl_Grua.BTN_CTRL[i], on);
            }
        }
    }
    public void ApagarSonidoGrua()
    {
        aSource.altoFx("Sirena_grua");
        aSource.altoFx("Rieles_grua");
        aSource.altoFx("Corneta_aviso");
        aSource.altoFx("Ascenso_grua");
    }
}

