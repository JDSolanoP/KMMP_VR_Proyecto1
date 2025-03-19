using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.XR.Interaction.Toolkit;
//using System;
using System.Collections;
using Unity.VisualScripting;
public class TM_IZAJE_M2 : Lista_Tareas_Controller
{
    [Header("Modulo 2 de IZAJE")]

    public GameObject CtrlTotalBtn;
    public GameObject[] Muros;
    public int contactoIntAux;
    public int contactoIntAux2;
    string NombreAuxAudio;
    float tiempoEsperaAux;
    /*
     public GameObject[] guantesComplementos;
     public GameObject[] manosXR;
     public Material[] manosXRMaterial;*/

    [Header("ESLINGAS")]
    public Set_Eslingas[] sE;
    public GameObject[] EslingasObj;
    public Set_Eslingas[] sERefe;
    public GameObject[] EslingasRefe;
    public GameObject[] Eslinga_Colocada_MT;
    public int nEslingaCorrecta;
    public int[] MAT_Eslinga;
    public int numEslingaColocada;
    public bool[] si_UsableEslinga;//perfecto estado
    public bool Si_HayEslingaColocada = false;//verifica si hay eslinga
    public bool Si_EslingaColocadaUsable = false;//verifica si la eslinga es usable
    public bool TodoCorrecto = false;
    public bool[] EslingaContactoRefe;//Para verificar si la eslinga esta en el locker
    public bool[] EslingaEnMano;
    public Vector3 Pos0EslingaCorrectoPostIzaje;
    [Header("Gruas 0:5T 1:25T")]
    public GameObject[] Gruas;
    public GameObject perillaSelectGancho;
    public GameObject perillaFull;
    public float AnguloPerilla;//+90 grados;
    public bool si_perilla;
    public bool perilla_correcta = true;
    public GameObject ControlOBJ;
    public float velrot;

    [Header("Viento")]
    public bool[] agarreViento;
    public int sentido;
    public GruaGanchoRot GGR;
    Vector3[] pos0AgarreViento = null;
    [Header("Elementos Post Verificacion")]
    public GameObject[] ElementoPost;
    public bool contactLockerEslingaRefe;
    public Transform pos_enMesa_Revision;
    public bool si_mt_enTaller;


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
                Tablero_Indicaciones[6].transform.SetParent(ElementoPost[3].transform);
                /*   //audioManager de bienvenida

                yield return new WaitForSeconds(0.5f);
                manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                */
                ElementoPost[5].SetActive(false);
                aSource.PlayMusica(aSource.MusicaSonidos[0].nombre, 0.75f, true);
                //aSource.MusicaVol(0.75f);//**************************************Sonido Musica Inicial*************
                aSource.FxVol(1);
                CtrlTotalBtn.SetActive(false);
                for (int i = 0; i < Tablero_Indicaciones.Length; i++)
                {
                    Tablero_Indicaciones[i].SetActive(false);
                }
                for (int i = 0; i < sE.Length; i++)//asignando caracteristicas a eslingas 
                {
                    int rnd = Random.Range(0, 8);
                    sE[i].set_Valores(rnd);
                    sE[i].Num_Material = rnd;
                }
                for (int i = 0; i < sE.Length; i++)//crear eslingas referencias 06-01-25
                {
                    sERefe[i].set_Valores(8);
                    sERefe[i].Num_Material = 8;
                    sERefe[i].ActivarMeshEslingas(false);
                }
                Saber_Eslinga_Correcta();
                colocar_Eslinga();
                yield return new WaitForSeconds(1f);
                yield return new WaitForSeconds(5f);
                Tablero_Indicaciones[3].SetActive(true);//panel de conformidad
                //Debug.Log("Se esta reproduciendo audio");

                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }

                break;
            case 1://cuando presiona el btn conforme y todo esta bien
                aSource.goFx("Bien");
                Muros[0].SetActive(false);//agregado 20-01-25
                Tablero_Indicaciones[3].SetActive(false);
                CtrlTotalBtn.SetActive(true);
                perillaSelectGancho.SetActive(false);
                Tablero_Indicaciones[4].SetActive(true);
                Tablero_Indicaciones[5].SetActive(true);
                ElementoPost[3].transform.SetParent(ElementoPost[2].transform);
                ElementoPost[10].transform.SetParent(ElementoPost[2].transform);
                ElementoPost[18].SetActive(true);//flechas indicacion para mover la mt hacia el taller
                ElementoPost[5].SetActive(true);
                ElementoPost[5].transform.SetParent(ElementoPost[2].transform);
                //ElementoPost[5].transform.SetParent(ElementoPost[2].transform);
                for (int i = 0; i < si_UsableEslinga.Length; i++)
                {
                    if (si_UsableEslinga[i] == true)
                    {
                        sERefe[i].ActivarMeshEslingas(false);
                    }
                }
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 2://cuando el mt esta en area interior del taller
                Tablero_Indicaciones[4].SetActive(false);

                /*ElementoPost[6].SetActive(true);//cable viento
                ElementoPost[7].SetActive(true);//agarre viento IZq
                ElementoPost[8].SetActive(true);//agarre viento der
                ElementoPost[7].transform.localPosition = pos0AgarreViento[0];
                ElementoPost[8].transform.localPosition = pos0AgarreViento[1];
                verificarContacto(5);*/
                aSource.goFx("Bien");
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 3://cuando la mt esta en su posicion final
                Tablero_Indicaciones[5].SetActive(false);
                aSource.goFx("Bien");
                ElementoPost[0].SetActive(false);//mt refe
                //ElementoPost[1].SetActive(true);//mt colocada
                //ElementoPost[3].SetActive(false);//mt colocada
                verificarContacto(5);
                /*ElementoPost[6].SetActive(false);//cable viento
                ElementoPost[7].SetActive(false);//agarre izq
                ElementoPost[8].SetActive(false);//agarrea der*/
                ElementoPost[11].SetActive(false);//detector1 de mt de lelgada
                ElementoPost[12].SetActive(false);//detector2 de mt de lelgada
                CtrlTotalBtn.SetActive(false);//apagar botones
                ElementoPost[13].SetActive(true);//refe control llegada
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;
            case 4://cuando se deja el control el refe, pedir desmontado de eslinga
                Tablero_Indicaciones[7].SetActive(true);
                aSource.goFx("Bien");
                ControlOBJ.SetActive(false);//control de puentegrua obj desactivar
                ElementoPost[13].SetActive(false);//refe control llegada
                ElementoPost[14].SetActive(true);//control mesh llegada
                
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 5://se devolvio la eslinga a su lugar
                Muros[1].SetActive(false);///muro de conos de concluciones
                //yield return new WaitForSeconds(5);
                ElementoPost[20].SetActive(true);//detector conclusiones
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;
            case 6://cuando acerca el jugador a area de contacto 
                Tablero_Indicaciones[8].SetActive(true);//panel conclusiones
                yield return new WaitForSeconds(5);
                ElementoPost[20].SetActive(false);//detector conclusiones
                ElementoPost[15].SetActive(true);//boton continuar
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;
            case 7://cuando se presiona el boton continuar
                Muros[1].SetActive(true);///muro de conos de concluciones
                Muros[2].SetActive(false);
                Tablero_Indicaciones[8].SetActive(false);//panel conclusiones
                Tablero_Indicaciones[9].SetActive(true);//panel final
                yield return new WaitForSeconds(5);
                ElementoPost[16].SetActive(true);//boton reset-reinicion
                yield return new WaitForSeconds(5);
                ElementoPost[17].SetActive(true);//boton quit-salir
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
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
                        if (TareaActual > 1)
                        {
                            EslingasObj[numEslingaColocada].GetComponent<Return_Pos0>().Pos0=Pos0EslingaCorrectoPostIzaje;
                            aSource.goFx("Bien");
                            aSource.goFx("Locu_Bien");

                        }
                        aSource.goFx("Soltar");
                        for (int i = 0; i < EslingasObj.Length; i++)
                        {
                            Eslinga_Colocada_MT[i].SetActive(false);
                            EslingasObj[i].SetActive(true);
                        }
                        Eslinga_Colocada_MT[8].SetActive(false);
                        Eslinga_Colocada_MT[MAT_Eslinga[contactoIntAux]].SetActive(true);
                        EslingasObj[contactoIntAux].GetComponent<Return_Pos0>().reposicionObj();
                        EslingasObj[contactoIntAux].SetActive(false);
                        numEslingaColocada = contactoIntAux;
                        sE[contactoIntAux].usable = si_UsableEslinga[contactoIntAux];
                        Si_HayEslingaColocada = true;
                        Si_EslingaColocadaUsable = (MAT_Eslinga[contactoIntAux] == nEslingaCorrecta && si_UsableEslinga[contactoIntAux]) ? true : false;
                        Debug.Log("Se coloco la eslinga " + contactoIntAux);
                        contacto_confirmado[confirmarContacto] = false;

                    }
                    SiTodoCorrecto();
                }
                //VerificarTodoCorrecto();
                break;
            case 1://verifica si un la eslinga es escogida y sacada de su area
                //Debug.Log("contacto 1 aux saliendo=" + contactoIntAux);
                if (contacto_confirmado[confirmarContacto] == false)// 06-1-25 sacar una eslinga de su sitio para aparezca la referencia para colocarla en el Locker
                {
                    sERefe[contactoIntAux].ActivarMeshEslingas(true);
                    contactLockerEslingaRefe = false;
                    Debug.Log("contacto aux saliendo=" + contactoIntAux);
                    EslingaContactoRefe[contactoIntAux] = false;
                }
                else
                {
                    //Debug.Log("contacto 1 aux entrand0 =" + contactoIntAux);
                    sERefe[contactoIntAux].ActivarMeshEslingas(false);
                    EslingasObj[contactoIntAux].GetComponent<Return_Pos0>().reposicionObj();
                }
                break;
            case 2://*****MT sale del area esxterior al taller
                if (contacto_confirmado[confirmarContacto] == false && TareaActual == 1)
                {

                    verificarContacto(5);
                    TareaCompletada(1);//completa la tarea 1

                }
                break;
            case 3://MT en posicion de llegada final de la base
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (contacto_confirmado[4] == true && TareaActual == 2) {
                        TareaCompletada(2);
                    }

                }

                break;
            case 4://MT en posicion de llegada final posterio
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (contacto_confirmado[3] == true && TareaActual == 2)
                    {
                        TareaCompletada(2);
                    }
                }
                break;
            case 5://***************cuando el jugador entra el taller**************en el detector de enMT, zona exterior al taller
                if (contacto_confirmado[confirmarContacto] == false)
                {
                    if (TareaActual > 1 && contacto_confirmado[2] == false)
                    {
                        ElementoPost[9].SetActive(false);
                        ControlOBJ.GetComponent<Return_Pos0>().Pos0 = new Vector3(-2.982f, 2.034668f, -5.226178f);
                        ControlOBJ.GetComponent<Return_Pos0>().Rot0 = new Vector3(ControlOBJ.GetComponent<Return_Pos0>().Rot0.x, ControlOBJ.GetComponent<Return_Pos0>().Rot0.y + 180, ControlOBJ.GetComponent<Return_Pos0>().Rot0.z);
                        Muros[0].SetActive(true);
                    }
                }
                break;
            case 6://se dejo el control en la referencia sobre el cajon
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (TareaActual == 3)
                    {
                        TareaCompletada(3);
                    }

                }
                break;
            case 12://el jugador se acerco al detector conclusiones
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (TareaActual == 5)
                    {
                        TareaCompletada(5);
                    }
                }
                break;
            case 7://se agarro el boton continuar
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    TareaCompletada(6);
                }

                break;
            case 8://boton reinicio
                IrEscenaAsincron(0);
                break;
            case 9://boton SALIR
                Application.Quit();
                break;
            case 10://verifica si mt contancto con detector de corneta presionada

                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (ControlOBJ.GetComponent<Control_Grua_Puente>().corneta_presionada == false)
                    {
                        Debug.Log("no corneta " + confirmarContacto);

                        Tablero_Indicaciones[6].SetActive(true);
                        aSource.goFx("Fallo");
                        aSource.goFx("Locu_Fallo");
                    }
                    else
                    {
                        Debug.Log("si corneta " + confirmarContacto);
                        Tablero_Indicaciones[6].SetActive(false);//tablero de error corneta
                        ElementoPost[19].SetActive(false);//detectores de corneta
                        ElementoPost[21].SetActive(false);//collider bloqueo de corneta
                        for (int i = 0; i < 6; i++)
                        {
                            ControlOBJ.GetComponent<Control_Grua_Puente>().bloqueo[i] = false;
                        }
                    }
                }
                break;
            case 11://en el boton corneta
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    Debug.Log("si corneta " + confirmarContacto);
                    ControlOBJ.GetComponent<Control_Grua_Puente>().corneta_presionada = true;
                    ElementoPost[19].SetActive(false);//detectores de corneta
                    ElementoPost[21].SetActive(false);//collider bloqueo de corneta
                    for (int i = 0; i < 6; i++)
                    {
                        ControlOBJ.GetComponent<Control_Grua_Puente>().bloqueo[i] = false;
                    }
                    if (Tablero_Indicaciones[6].activeInHierarchy)
                    {
                        Tablero_Indicaciones[6].SetActive(false);
                    }
                }
                break;
            case 13://eslinga correcta devuelta
                if (contacto_confirmado[confirmarContacto] == true&&TareaActual>1)
                {
                    Tablero_Indicaciones[11].SetActive(true);
                    EslingasObj[numEslingaColocada].GetComponent<Return_Pos0>().Pos0 = EslingasObj[numEslingaColocada].GetComponent<Return_Pos0>().auxPos0;
                    EslingasObj[numEslingaColocada].GetComponent<Return_Pos0>().reposicionObj();
                    for(int i=0; i < EslingasObj.Length; i++)
                    {
                        EslingasObj[i].GetComponent<One_Hand_PickUp>().enabled = false;
                    }
                    //aSource.goFx("Locu_Bien");
                    //aSource.goFx("Locu_Bien");
                    TareaCompletada(4);
                }
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
                if (sE[i].usable)
                    sE[i].usable = false;//reasigna valor falso
            }
        }
    }
    public void colocar_Eslinga()//colocar almenos una eslinga usable
    {
        bool existecorrecto = false;
        for (int i = 0; i < si_UsableEslinga.Length; i++)
        {
            if (si_UsableEslinga[i] == true)
            {
                existecorrecto = true;
                //Debug.Log("Se encontro una eslinga correcta,es " + i);
                break;
            }
        }
        if (existecorrecto == false)
        {
            int rnd = Random.Range(0, 8);
            sE[rnd].set_Valores(7, 1, 1, 1, true);
            sE[rnd].Num_Material = 7;
            MAT_Eslinga[rnd] = 7;
            si_UsableEslinga[rnd] = true;
            //sE[rnd].usable = true;
            //Debug.Log("No se encontro una eslinga correcta y se reasigno a " + rnd);
        }
    }
    public void Seleccion_Grua()
    {
        if (si_perilla)
        {
            si_perilla = false;
            perillaFull.transform.localEulerAngles = new Vector3(perillaSelectGancho.transform.localEulerAngles.x, 0, perillaSelectGancho.transform.localEulerAngles.z);
            Gruas[1].SetActive(false);
            Gruas[0].SetActive(true);
        }
        else
        {
            si_perilla = true;//si es true es la medida correcta
            perillaFull.transform.localEulerAngles = new Vector3(perillaSelectGancho.transform.localEulerAngles.x, AnguloPerilla, perillaSelectGancho.transform.localEulerAngles.z);
            Gruas[0].SetActive(false);
            Gruas[1].SetActive(true);
        }
        AudioManager.aSource.goFx("Boton");
        SiTodoCorrecto();
        //VerificarTodoCorrecto();
    }
    //*************************************FUNCIONES PARA INTERACCION DE CON MANOS*****************************/////////////////////////////////
    public void VerificarTodoCorrecto()//Boton de CONFORMIDAD PARA EMPEZAR ACTIVIDADES 30.12.24
    {
        aSource.goFx("Boton");
        if (Si_HayEslingaColocada == false)
        {
            Tablero_Indicaciones[2].SetActive(false);//si gancho correcta
            Tablero_Indicaciones[1].SetActive(false);//si eslinga es incorrecta o dañada
            Tablero_Indicaciones[0].SetActive(true);//si no detecta ninguna eslinga
            aSource.goFx("Locu_Fallo");
            aSource.goFx("Fallo");
            //Debug.Log("error no eslinga "+Si_HayEslingaColocada+" - TodoCorrecto : "+TodoCorrecto+" ->"+Si_EslingaColocadaUsable+" "+si_perilla );
        } else
        {
            Tablero_Indicaciones[0].SetActive(false);
            if (TodoCorrecto == false)
            {
                aSource.goFx("Locu_Fallo");
                aSource.goFx("Fallo");
                if (Si_EslingaColocadaUsable == false)
                {
                    //Debug.Log("error eslinga no usable" + Si_HayEslingaColocada + " - TodoCorrecto : " + TodoCorrecto + " ->" + Si_EslingaColocadaUsable + " " + si_perilla);
                    Tablero_Indicaciones[2].SetActive(false);//si gancho correcta
                    Tablero_Indicaciones[1].SetActive(true);//si eslinga es incorrecta o dañada
                    Tablero_Indicaciones[10].SetActive(true);//Advertencia sobre mesa si eslinga es incorrecta o dañada
                    EslingasObj[numEslingaColocada].SetActive(true);
                    EslingasObj[numEslingaColocada].transform.localPosition = new Vector3(pos_enMesa_Revision.localPosition.x, pos_enMesa_Revision.localPosition.y, pos_enMesa_Revision.localPosition.z);
                    EslingasObj[numEslingaColocada].transform.localEulerAngles = new Vector3(pos_enMesa_Revision.localEulerAngles.x, pos_enMesa_Revision.localEulerAngles.y, pos_enMesa_Revision.localEulerAngles.z);
                    Si_HayEslingaColocada = false;
                    for (int i = 0; i < EslingasObj.Length; i++)
                    {
                        Eslinga_Colocada_MT[i].SetActive(false);
                        EslingasObj[i].SetActive(true);
                    }
                    Eslinga_Colocada_MT[8].SetActive(true);
                }
                else
                {
                    if (si_perilla == false)
                    {
                        //Debug.Log("error Gancho incorrecto : Si eslinga =" + Si_HayEslingaColocada + " - TodoCorrecto : " + TodoCorrecto + " ->" + Si_EslingaColocadaUsable + " " + si_perilla);
                        //aSource.goFx("Locu_Fallo");
                        //aSource.goFx("Fallo");
                        Tablero_Indicaciones[2].SetActive(true);//si gancho incorrecta
                        Tablero_Indicaciones[1].SetActive(false);//si eslinga es correcta
                    }
                }
            }
            else
            {

                Tablero_Indicaciones[0].SetActive(false);//si no detecta ninguna eslinga
                Tablero_Indicaciones[2].SetActive(false);//si gancho incorrecta
                Tablero_Indicaciones[1].SetActive(false);//si eslinga es correcta
                aSource.goFx("Locu_Bien");
                aSource.goFx("Bien");
                Tablero_Indicaciones[3].SetActive(true);//si todo es correcto
                ElementoPost[4].SetActive(false);
                ElementoPost[0].SetActive(true);
                CtrlTotalBtn.SetActive(true);
                TareaCompletada(0);
            }
        }
    }
    public void Reinicar_Elementos()//Boton de REINICIAR PARA REINICAR POR DEFECTO LOS ELEMENTOS 08.01.25
    {
        aSource.goFx("Boton");
        for (int i = 0; i < sE.Length; i++)
        {
            sERefe[i].ActivarMeshEslingas(false);
            sE[i].reUbicarEslingas();
            contacto_confirmado[i] = false;
            Eslinga_Colocada_MT[i].SetActive(false);
            EslingasObj[i].SetActive(true);
        }
        Eslinga_Colocada_MT[8].SetActive(true);
        Si_HayEslingaColocada = false;
        Gruas[0].SetActive(true);
        Gruas[1].SetActive(false);
        Si_EslingaColocadaUsable = false;
        TodoCorrecto = false;
        si_perilla = false;
        perillaFull.transform.localEulerAngles = new Vector3(perillaSelectGancho.transform.localEulerAngles.x, 0, perillaSelectGancho.transform.localEulerAngles.z);

    }
    public void SiTodoCorrecto()
    {
        if (si_perilla && Si_EslingaColocadaUsable)
        {

            TodoCorrecto = true;
        }
        else
        {
            TodoCorrecto = false;
        }
        Debug.Log("TODO CORRECTO : " + TodoCorrecto + " - " + si_perilla + " " + Si_EslingaColocadaUsable);
    }
    public void colocarEslingaLocker()
    {
        for (int i = 0; i < sERefe.Length; i++)
        {
            if (EslingasObj[i].transform.localPosition == EslingasObj[i].GetComponent<Return_Pos0>().Pos0)
            {
                sERefe[i].ActivarMeshEslingas(false);
            }
        }
        /*if(EslingaContactoRefe[contactoIntAux] == true)
        {*/
        Debug.Log("contacto auxiliar2=" + contactoIntAux2);
        sERefe[contactoIntAux2].ActivarMeshEslingas(false);
        EslingasObj[contactoIntAux2].GetComponent<Return_Pos0>().reposicionObj();


        //}
    }
    public void siEslingaEnMano(int nEslinga)
    {
        EslingaEnMano[nEslinga] = true;
    }
    public void noEslingaEnMano(int nEslinga)
    {
        EslingaEnMano[nEslinga] = false;
    }
    //****************************************GIRO DE LA GRUA DESDE EL GANCHO**** VIENTO **** 13-01-25**************************
    public void siAgarreViento(int nV)
    {
        agarreViento[nV] = true;
        if (agarreViento[0] == true && agarreViento[1] == true)
        {
            GGR.GiroActivo = true;
            GGR.StartCoroutine(GGR.IniciarGiro());
        }
        else { GGR.GiroActivo = false;
        }

    }
    public void noAgarreViento(int nV)
    {
        agarreViento[nV] = false;
        if (agarreViento[0] == true && agarreViento[1] == true)
        {
            GGR.GiroActivo = true;
        }
        else
        {
            GGR.GiroActivo = false;
        }
        ElementoPost[7 + nV].transform.localPosition = pos0AgarreViento[nV];
    }
    public void DesmontarEslinga()
    {

        EslingasRefe[numEslingaColocada].GetComponent<Set_Eslingas>().MeshGameObj.SetActive(true);
        ElementoPost[22].SetActive(false);
        EslingasObj[numEslingaColocada].GetComponent<Rigidbody>().isKinematic = true;
        EslingasObj[numEslingaColocada].GetComponent<Rigidbody>().useGravity = false;
        EslingasObj[numEslingaColocada].SetActive(true);
        Muros[0].SetActive(false);
        EslingasObj[numEslingaColocada].transform.position = ElementoPost[22].transform.position;
        Pos0EslingaCorrectoPostIzaje = EslingasObj[numEslingaColocada].transform.position;
        EslingasObj[numEslingaColocada].GetComponent<Return_Pos0>().Pos0 = ElementoPost[22].transform.position;
        Eslinga_Colocada_MT[numEslingaColocada].SetActive(false);
        for (int i = 0; i < Eslinga_Colocada_MT.Length; i++)
        {
            Eslinga_Colocada_MT [i].SetActive(false);
        }
        ElementoPost[24].GetComponent<BoxCollider>().enabled=false;
    }
}
