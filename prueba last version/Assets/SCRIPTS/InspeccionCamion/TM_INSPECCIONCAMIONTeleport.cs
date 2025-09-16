using System.Collections;
using System.Collections.Generic;
//using Oculus.Interaction.Editor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class TM_INSPECCIONCAMIONTeleport : Lista_Tareas_Controller
{
    public int auxContacto;
    [Header("Elementos")]
    public GameObject[] Muros;
    public GameObject[] Flechas;
    public GameObject[] BtnContinue;
    public float tiempoBTNContinuar;
    public GameObject[] perillaPuertaCabinaOBJ;//******11-06-25******//Para verificar si puerta cerrada
    public Transform[] pos0PerillasPCabina;//******11-06-25******//Para verificar si puerta cerrada
    public bool si_PuertaCabinaCerrada = true;
    public bool si_PJEnCabina = false;
    [SerializeField] private bool CanTeleportationBeEnabledInTheModule = false;
    [SerializeField] private GameObject ConfirmationIntroPanel = null;
    [SerializeField] private bool isTeleportationEnabled = false;
    [SerializeField] private GameObject TeleportationPanel = null;
    [SerializeField] private TMP_Text TeleportationText = null;
    [SerializeField] private XRRayInteractor TeleportInteractor = null;
    [SerializeField] private GameObject[] TeleportationBlockers = null;
    [SerializeField] private TeleportationArea[] TeleportationAreas = null;
    public override void Start()
    {
        base.Start();
        PreparationsBeforeStarting();
        if (!ConfirmationIntroPanel)
        {
            StartCoroutine(ListaTareas(TareaActual));
        }
        if (CanTeleportationBeEnabledInTheModule)
        {
            TeleportationPanel.SetActive(CanTeleportationBeEnabledInTheModule);
        }
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

                yield return new WaitForSecondsRealtime(0.5f);
                pos0PerillasPCabina[0] = perillaPuertaCabinaOBJ[0].transform;
                pos0PerillasPCabina[1] = perillaPuertaCabinaOBJ[1].transform;
                Tablero_Indicaciones[0].SetActive(true);//panel de bienvenido
                /*   //audioManager de bienvenida

                yield return new WaitForSeconds(0.5f);
                manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                */

                /*aSource.PlayMusica(aSource.MusicaSonidos[0].nombre, 0.75f, true);*/
                //aSource.MusicaVol(0.75f);//**************************************Sonido Musica Inicial*************
                aSource.FxVol(1);

                //yield return new WaitForSeconds(1f);
                //yield return new WaitForSecondsRealtime(23f);

                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                yield return new WaitForSecondsRealtime(23f);
                aSource.goFx(aSource.FxSonidos[33].nombre);
                Tablero_Indicaciones[1].SetActive(true);//Locucion para panel intro de P1
                Flechas[0].SetActive(true);
                aros_indicadores[0].SetActive(true);
                break;
            case 1://ARO_00
                aSource.goFx(aSource.FxSonidos[40].nombre);
                auxContacto++;
                Tablero_Indicaciones[TareaActual + 1].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(tiempoBTNContinuar);
                BtnContinue[TareaActual - 1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 2://ARO_01
                aSource.goFx(aSource.FxSonidos[40].nombre);
                auxContacto++;
                Tablero_Indicaciones[TareaActual + 1].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(tiempoBTNContinuar);
                BtnContinue[TareaActual - 1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 3://ARO_02
                aSource.goFx(aSource.FxSonidos[40].nombre);
                auxContacto++;
                Debug.Log(TareaActual + "Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                Tablero_Indicaciones[TareaActual + 1].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(tiempoBTNContinuar);
                BtnContinue[TareaActual - 1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                if (CanTeleportationBeEnabledInTheModule)
                {
                    TeleportationBlockers[2].SetActive(false);
                }
                break;
            case 4://ARO_03
                aSource.goFx(aSource.FxSonidos[40].nombre);
                auxContacto++;
                Debug.Log(TareaActual + "Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                Tablero_Indicaciones[TareaActual + 1].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(tiempoBTNContinuar);
                BtnContinue[TareaActual - 1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                if (CanTeleportationBeEnabledInTheModule) TeleportationAreas[0].enabled = true;
                break;
            case 5://ARO_04
                aSource.goFx(aSource.FxSonidos[40].nombre);
                auxContacto++;
                Debug.Log(TareaActual + "Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                Tablero_Indicaciones[TareaActual + 2].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(tiempoBTNContinuar);
                BtnContinue[TareaActual - 1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                if (CanTeleportationBeEnabledInTheModule) TeleportationBlockers[0].SetActive(false);
                break;
            case 6://ARO_05
                aSource.goFx(aSource.FxSonidos[40].nombre);
                auxContacto++;
                Debug.Log(TareaActual + "Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                Tablero_Indicaciones[TareaActual + 2].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(tiempoBTNContinuar);
                BtnContinue[TareaActual - 1].SetActive(true);
                Flechas[8].SetActive(true);
                Muros[4].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 7://ARO_06
                aSource.goFx(aSource.FxSonidos[40].nombre);
                auxContacto++;
                Debug.Log(TareaActual + " Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                Tablero_Indicaciones[TareaActual + 2].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(tiempoBTNContinuar);
                BtnContinue[TareaActual - 1].SetActive(true);

                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 8://CONTINUAR DE CABINA
                auxContacto++;
                Debug.Log(TareaActual + " Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                Tablero_Indicaciones[TareaActual + 2].SetActive(true);//PANEL DE VICTORIA
                Muros[4].SetActive(false);
                yield return new WaitForSecondsRealtime(5);
                //BtnContinue[TareaActual - 1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 9:////ARO_07

                aSource.goFx(aSource.FxSonidos[38].nombre);
                Muros[8].SetActive(true);
                aros_indicadores[7].SetActive(false);
                auxContacto++;
                Debug.Log(TareaActual + " Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                Tablero_Indicaciones[TareaActual + 2].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(18.5f);
                BtnContinue[TareaActual - 2].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 10://CONTINUAR DE PANEL DE CONCLUSIONES
                aSource.goFx(aSource.FxSonidos[39].nombre);
                aSource.goFx(aSource.FxSonidos[30].nombre);
                aSource.goFx(aSource.FxSonidos[31].nombre);
                Tablero_Indicaciones[11].SetActive(false);//Locucion para panel intro de P1
                Tablero_Indicaciones[12].SetActive(true);//Locucion para panel intro de P1
                Muros[8].SetActive(false);
                Muros[9].SetActive(false);
                auxContacto++;
                Debug.Log(TareaActual + " Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                //Tablero_Indicaciones[TareaActual + 2].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(5);
                BtnContinue[8].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                yield return new WaitForSecondsRealtime(6);
                BtnContinue[9].SetActive(true);
                break;
        }
    }
    //**********************VERIFICAR CONTACTO OBJETO A OBJETO****************************************
    public void verificarContacto(int confirmarcontacto)
    {
        switch (confirmarcontacto)
        {
            case 0://contacto con aros, completa tareas segun aux
                aSource.goFx("Bien");
                Debug.Log("auxcontacto=" + auxContacto);
                if (auxContacto < Flechas.Length)
                {
                    Flechas[auxContacto].SetActive(false);
                }
                if (auxContacto < aros_indicadores.Length)
                {
                    aros_indicadores[auxContacto].SetActive(false);
                }
                TareaCompletada(auxContacto);
                break;
            case 1:
                aSource.goFx("Bien");
                Debug.Log(confirmarcontacto + " auxcontacto=" + auxContacto);
                Debug.Log("confirmarContacto=" + confirmarcontacto);
                aros_indicadores[7].SetActive(false);
                Flechas[8].SetActive(false);
                TareaCompletada(8);
                break;
            case 2:
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    if (si_PuertaCabinaCerrada == false)//cerrando
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
                    if (si_PuertaCabinaCerrada == true)//abriendo
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
            case 3:
                if (contacto_confirmado[confirmarcontacto] == true)
                {
                    si_PJEnCabina = true;
                }
                else
                {
                    si_PJEnCabina = false;
                }
                break;
        }
    }
    public void activacionXR(int contacto) //contacto con manos
    {
        switch (contacto)
        {
            case 0://boton continue
                aSource.goFx(aSource.FxSonidos[8].nombre);
                Debug.Log("auxcontacto=" + auxContacto);
                if (auxContacto < 8)
                {
                    Muros[auxContacto - 1].SetActive(false);
                    aros_indicadores[auxContacto].SetActive(true);
                    Flechas[auxContacto].SetActive(true);
                    if (auxContacto < 7)
                    {
                        aSource.goFx(aSource.FxSonidos[34].nombre);
                    }
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
                    Debug.Log(auxContacto + " Verificacion " + TareaActual);
                    Tablero_Indicaciones[auxContacto + 1].SetActive(false);
                    Tablero_Indicaciones[auxContacto + 2].SetActive(true);
                    StartCoroutine(TiempoParaFX(35));
                }
                else
                {
                    if (TareaActual == 6)
                    {
                        StartCoroutine(TiempoParaFX(36));
                    }
                    Tablero_Indicaciones[auxContacto + 2].SetActive(false);
                }
                if (auxContacto == 7)
                {
                    aSource.goFx(aSource.FxSonidos[21].nombre);
                    aSource.goFx(aSource.FxSonidos[23].nombre);
                    Muros[7].SetActive(false);
                    Tablero_Indicaciones[6].SetActive(false);
                    TareaCompletada(TareaActual);
                    Muros[8].SetActive(true);
                }
                break;
            case 1://boton reinicio
                Debug.Log("Reiniciando");
                IrEscenaAsincron(0);
                break;
            case 2://boton SALIR
                Debug.Log("Saliendo");
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
    public IEnumerator TiempoParaFX(int nAudio)
    {
        Debug.Log("audioFX " + nAudio + " Llamando en 5s");
        yield return new WaitForSecondsRealtime(3f);
        Debug.Log("audioFX " + nAudio + " reproduciendo");
        aSource.goFx(aSource.FxSonidos[nAudio].nombre);
    }

    public void TryQuitGame()
    {
        Debug.Log("Se intento salir");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void StartCoroutineListaDeTareas()
    {
        StartCoroutine(ListaTareas(TareaActual));
        ConfirmationIntroPanel.SetActive(false);
        if (CanTeleportationBeEnabledInTheModule)
        {
            TeleportationPanel.SetActive(false);
        }
    }

    private void PreparationsBeforeStarting()
    {
        aSource.PlayMusica(aSource.MusicaSonidos[0].nombre, 0.75f, true);
        for (int i = 0; i < Tablero_Indicaciones.Length; i++)
        {
            Tablero_Indicaciones[i].SetActive(false);
        }
        for (int i = 0; i < aros_indicadores.Length; i++)
        {
            Flechas[i].SetActive(false);
            aros_indicadores[i].SetActive(false);
        }
        for (int i = 0; i < BtnContinue.Length; i++)
        {
            BtnContinue[i].SetActive(false);
        }
        if (CanTeleportationBeEnabledInTheModule)
        {
            TeleportInteractor.enabled = false;
        }
        if (!CanTeleportationBeEnabledInTheModule)
        {
            for (int i = 0; i < TeleportationBlockers.Length; i++)
            {
                TeleportationBlockers[i].SetActive(false);
            }
        }
        if (CanTeleportationBeEnabledInTheModule)
        {
            TeleportationAreas[0].enabled = false;
        }
    }

    public void EnableTeletransportation()
    {
        isTeleportationEnabled = true;
        TeleportInteractor.enabled = true;
        TeleportationText.text = "Teleportation is enabled.";
    }

    public void DisableTeletransportation()
    {
        isTeleportationEnabled = false;
        TeleportInteractor.enabled = false;
        TeleportationText.text = "Teleportation is disabled.";
    }
}
