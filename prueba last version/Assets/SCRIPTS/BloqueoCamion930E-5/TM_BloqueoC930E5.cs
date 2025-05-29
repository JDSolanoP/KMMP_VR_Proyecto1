using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_BloqueoC930E5 : Lista_Tareas_Controller
{
    public int auxContacto;//para activavionXR u otros
    public int contactoIntAux;//exclusivo de DetectorObj
    public string NombreAuxAudio;
    public float tiempoEsperaAux;
    [Header("ElementosEscenario")]
    public GameObject PJ;
    public Transform[] posPJ;
    public GameObject CamionAnim;
    public GameObject CamionC930;
    public GameObject[] Muros;
    public GameObject[] Flechas;
    [Header("ElementosModulo")]
    public GameObject[] Tacos;
    public GameObject[] Palancas;//caja de bloqueo
    public GameObject[] LucesLEDCaja;
    public GameObject[] Items;//LOTO
    float[] rotPalancas;
    int nPalancasOff=0;
    bool[] si_TacoColocado;

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
                    CamionC930.SetActive(false);
                    PJ.transform.position = posPJ[0].position;
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
                rotPalancas = new float[3];
                si_TacoColocado = new bool[2];
                rotPalancas[0] = Palancas[0].transform.localEulerAngles.x;// captura de Rotacion
                rotPalancas[1] = Palancas[2].transform.localEulerAngles.x;
                rotPalancas[2] = Palancas[4].transform.localEulerAngles.x;
                Palancas[0].transform.localEulerAngles = new Vector3(Palancas[1].transform.localEulerAngles.x, Palancas[1].transform.localEulerAngles.y, Palancas[1].transform.localEulerAngles.z);//colocacion de rot de prendido
                Palancas[2].transform.localEulerAngles = new Vector3(Palancas[3].transform.localEulerAngles.x, Palancas[3].transform.localEulerAngles.y, Palancas[3].transform.localEulerAngles.z);
                Palancas[4].transform.localEulerAngles = new Vector3(Palancas[5].transform.localEulerAngles.x, Palancas[5].transform.localEulerAngles.y, Palancas[5].transform.localEulerAngles.z);
                Palancas[1].SetActive(false);
                Palancas[3].SetActive(false);
                Palancas[5].SetActive(false);
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
                StartCoroutine(ActivarObjxTiempo(Palancas[5]));
                StartCoroutine( DeactivarObjxTiempo(LucesLEDCaja[6]));
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;
            case 2://LOTO
                Tablero_Indicaciones[3].SetActive(true);//P2
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;
            case 3:
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;
        }
    }
    //**********************VERIFICAR CONTACTO OBJETO A OBJETO****************************************
    public void verificarContacto(int confirmarcontacto)
    {
        switch (confirmarcontacto)
        {
           case 0://contactoTacoGRab00
                Debug.Log("confirmarcontacto : 0 auxcontacto=" + auxContacto);
                if (contacto_confirmado[0] == true)
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
                if (contacto_confirmado[1] == true)
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
                        Palancas[auxContacto+1].SetActive (false);
                        Palancas[auxContacto].transform.eulerAngles = new Vector3(rotPalancas[auxContacto], Palancas[auxContacto].transform.localEulerAngles.y, Palancas[auxContacto].transform.localEulerAngles.z);
                        LucesLEDCaja[auxContacto].SetActive(false);
                        LucesLEDCaja[auxContacto+1].SetActive(true);
                        nPalancasOff++;
                        if (nPalancasOff == 3)
                        {
                            StartCoroutine(TiempoEsperaTarea(1));
                            aSource.goFx(aSource.FxSonidos[21].nombre);
                            aSource.goFx(aSource.FxSonidos[23].nombre);
                        }
                        break;
                    case 1://starter
                        Palancas[auxContacto + 1].SetActive(false);
                        Palancas[auxContacto].transform.eulerAngles = new Vector3(rotPalancas[auxContacto], Palancas[auxContacto].transform.localEulerAngles.y, Palancas[auxContacto].transform.localEulerAngles.z);
                        LucesLEDCaja[auxContacto].SetActive(false);
                        LucesLEDCaja[auxContacto + 1].SetActive(true);
                        nPalancasOff++;
                        if (nPalancasOff == 3)
                        {
                            StartCoroutine(TiempoEsperaTarea(1));
                            aSource.goFx(aSource.FxSonidos[21].nombre);
                            aSource.goFx(aSource.FxSonidos[23].nombre);
                        }
                        break;
                    case 2://Master
                        Palancas[auxContacto + 1].SetActive(false);
                        Palancas[auxContacto].transform.eulerAngles = new Vector3(rotPalancas[auxContacto], Palancas[auxContacto].transform.localEulerAngles.y, Palancas[auxContacto].transform.localEulerAngles.z);
                        LucesLEDCaja[auxContacto].SetActive(false);
                        LucesLEDCaja[auxContacto + 1].SetActive(true);
                        LucesLEDCaja[1].SetActive(false);
                        LucesLEDCaja[3].SetActive(false);
                        nPalancasOff++;
                        if (nPalancasOff == 3)
                        {
                            StartCoroutine(TiempoEsperaTarea(1));
                            aSource.goFx(aSource.FxSonidos[21].nombre);
                            aSource.goFx(aSource.FxSonidos[23].nombre);
                        }
                        break;
                }
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
        yield return new WaitForSecondsRealtime(5);
        obj.SetActive(true);
    }
    public IEnumerator DeactivarObjxTiempo(GameObject obj)
    {
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
}
