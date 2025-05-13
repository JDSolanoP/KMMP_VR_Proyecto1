using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_INSPECCIONCAMION : Lista_Tareas_Controller
{
    public int auxContacto;
    [Header("Elementos")]
    public GameObject[] Muros;
    public GameObject[] Flechas;
    public GameObject[] BtnContinue;
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
                for (int i = 0; i < aros_indicadores.Length; i++)
                {
                    Flechas[i].SetActive(false);
                    aros_indicadores[i].SetActive(false);
                }
                for (int i = 0; i < BtnContinue.Length; i++)
                {
                    BtnContinue[i].SetActive(false);
                }
                yield return new WaitForSecondsRealtime(0.5f);
                Tablero_Indicaciones[0].SetActive(true);//panel de bienvenido
                /*   //audioManager de bienvenida

                yield return new WaitForSeconds(0.5f);
                manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                */

                aSource.PlayMusica(aSource.MusicaSonidos[0].nombre, 0.75f, true);
                //aSource.MusicaVol(0.75f);//**************************************Sonido Musica Inicial*************
                aSource.FxVol(1);
                
                //yield return new WaitForSeconds(1f);
                //yield return new WaitForSecondsRealtime(23f);

                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                yield return new WaitForSecondsRealtime(5f);
                Tablero_Indicaciones[1].SetActive(true);//Locucion para panel intro de P1
                Flechas[0].SetActive(true);
                aros_indicadores[0].SetActive(true);
                break;
            case 1://ARO_00
                auxContacto++;
                Tablero_Indicaciones[TareaActual+1].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(5);
                BtnContinue[TareaActual-1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 2://ARO_01
                auxContacto++;
                Tablero_Indicaciones[TareaActual + 1].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(5);
                BtnContinue[TareaActual - 1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 3://ARO_02
                auxContacto++;
                Debug.Log(TareaActual + "Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                Tablero_Indicaciones[TareaActual + 1].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(5);
                BtnContinue[TareaActual - 1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 4://ARO_03
                auxContacto++;
                Debug.Log(TareaActual + "Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                Tablero_Indicaciones[TareaActual + 1].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(5);
                BtnContinue[TareaActual - 1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 5://ARO_04
                auxContacto++;
                Debug.Log(TareaActual + "Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                Tablero_Indicaciones[TareaActual + 2].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(5);
                BtnContinue[TareaActual - 1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 6://ARO_05
                auxContacto++;
                Debug.Log(TareaActual + "Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                Tablero_Indicaciones[TareaActual + 2].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(5);
                BtnContinue[TareaActual - 1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 7://ARO_06
                auxContacto++;
                Debug.Log(TareaActual+" Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                Tablero_Indicaciones[TareaActual + 2].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(5);
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
                yield return new WaitForSecondsRealtime(5);
                //BtnContinue[TareaActual - 1].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 9:////ARO_07
                Muros[8].SetActive(true);
                aros_indicadores[7].SetActive(false);
                auxContacto++;
                Debug.Log(TareaActual + " Nuevo AuxContacto aumentado en Tarea==" + auxContacto);
                Tablero_Indicaciones[TareaActual + 2].SetActive(true);//Locucion para panel intro de P1
                yield return new WaitForSecondsRealtime(5);
                BtnContinue[TareaActual - 2].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {

                    yield return new WaitForFixedUpdate();
                }
                break;
            case 10://CONTINUAR DE PANEL DE CONCLUSIONES
                Tablero_Indicaciones[11].SetActive(false);//Locucion para panel intro de P1
                Tablero_Indicaciones[12].SetActive(true);//Locucion para panel intro de P1
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
                yield return new WaitForSecondsRealtime(5);
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
                if (auxContacto <aros_indicadores.Length)
                {
                    aros_indicadores[auxContacto].SetActive(false);
                }
                TareaCompletada(auxContacto);
                break;
            case 1:
                aSource.goFx("Bien");
                Debug.Log(confirmarcontacto+" auxcontacto=" + auxContacto);
                Debug.Log("confirmarContacto=" +confirmarcontacto);
                aros_indicadores[7].SetActive(false);
                Flechas[8].SetActive(false);
                TareaCompletada(8);
                break;
        }
    }
    public void activacionXR(int contacto) //contacto con manos
    {
        switch (contacto) 
        {
            case 0://boton continue
                Debug.Log("auxcontacto=" + auxContacto);
                if (auxContacto < 8) {
                    Muros[auxContacto - 1].SetActive(false);
                    aros_indicadores[auxContacto].SetActive(true);
                    Flechas[auxContacto].SetActive(true);
                }
                
                if (TareaActual < 4)//antes de subir a cabina
                {
                    Tablero_Indicaciones[auxContacto + 1].SetActive(false);
                    if (TareaActual==2)
                    {
                        Tablero_Indicaciones[1].SetActive(false);
                        Tablero_Indicaciones[0].SetActive(false);
                    }
                }
                else if(TareaActual==4)
                {
                    Tablero_Indicaciones[auxContacto + 1].SetActive(false);
                    Tablero_Indicaciones[auxContacto + 2].SetActive(true);
                }
                else 
                {

                    Tablero_Indicaciones[auxContacto + 2].SetActive(false);
                }
                if (auxContacto == 7)
                {
                    Muros[7].SetActive(false);
                    Tablero_Indicaciones[6].SetActive(false);
                    TareaCompletada(TareaActual);
                    Muros[8].SetActive(true);
                }
                break;
            case 1://boton reinicio
                IrEscenaAsincron(0);
                break;
            case 2://boton SALIR
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
}
