using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TM_Oficina : Lista_Tareas_Controller
{
    public TMP_Text NumPizarron;
    public TMP_Text Panel_Txt;
    public TMP_Text Frase_Txt;
    public GameObject[] Paneles;
    public bool[] contacto_confirmado;
    public int auxcontacto;
    public GameObject[] muros;
    public GameObject[] flechas;
    [Header("Modulo 1")]
    public int limiteCifras;
    public int nCifras;
    public int numero;
    public int rN;
    [Header("Modulo 2")]
    public GameObject[] Epps;
    public GameObject[] correasGuantes;
    public GameObject[] manosXR;
    public Material[] manosXRMaterial;
    [Header("Modulo 3")]
    public GameObject[] Items;
    // Start is called before the first frame update
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
                Paneles[1].SetActive(false);
                
                DarRandomNum();
                Frase_Txt.text = "";
                //aSource.MusicaVol(0.5f);//**************************************Sonido Musica Inicial*************
                //aSource.FxVol(1);
                yield return new WaitForSeconds(0.1f);
                Paneles[0].SetActive(true);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se tiene todos los EPPS
            case 1://Tarea de numero completa
                aSource.goFx("Bien");
                flechas[0].SetActive(true);
                muros[0].SetActive(false);//para la siguiente area
                Paneles[0].SetActive(false);
                Paneles[1].SetActive(true);
                Paneles[2].SetActive(true);
                /*while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }*/
                break;//cuando se tiene todos los EPPSs
            case 2://se agarro los guantes
                Epps[1].SetActive(true);//refe prendido
                aSource.goFx("Bien");
                Paneles[3].SetActive(true);
                muros[0].SetActive(true);//Muro inter areas m0 a m1
                /*while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }*/
                break;//cuando se tiene todos los EPPS
            case 3://se dejo los guantes
                aSource.goFx("Bien");
                Epps[0].SetActive(true);//refe prendido
                Epps[0].GetComponent<BoxCollider>().enabled = false;
                muros[2].SetActive(false);//para la siguiente area de m1  a m2
                flechas[1].SetActive(true);
                Paneles[5].SetActive(true);
                /*while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }*/
                break;//cuando se tiene todos los EPPS
            case 4://firmo
                muros[2].SetActive(true);//para la siguiente area
                flechas[2].SetActive(true);
                aSource.goFx("Bien");
                /*while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }*/
                break;//cuando se tiene todos los EPPS
            case 5://dejo el tablero en la otra area
                muros[3].SetActive(true);//para la siguiente area
                aSource.goFx("Bien");
                /*while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }*/
                break;//cuando se tiene todos los EPPS
            case 6://FINNNN
                //muros[3].SetActive(false);//para la siguiente area

                /*while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }*/
                break;//cuando se tiene todos los EPPS

        }
    }
    public void verificarContacto(int confirmarContacto)//*******realiza acciones cuando2 objetos interactuan usan
    {
        
        switch (confirmarContacto)
        {
            case 0:
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (muros[1].activeInHierarchy == false)
                    {
                        muros[1].SetActive(true);//evita regresar
                    }
                    if (auxcontacto<=9)
                    {
                        if (nCifras < limiteCifras)
                        {
                            if (nCifras == 0)
                            {
                                numero = auxcontacto;
                            }
                            else
                            {
                                numero = numero * 10 + auxcontacto;
                            }
                            Panel_Txt.text = "" + numero;
                            nCifras++;
                        }
                    }
                    else
                    {
                        if (auxcontacto == 10)
                        {
                            if (nCifras > 0)
                            {
                                if (nCifras > 1)
                                {
                                    int aux = numero % 10;
                                    Debug.Log("numero actual : " + aux);
                                    numero -= aux;
                                    numero = numero / 10;
                                }
                                else
                                {
                                    numero = 0;
                                }
                                Panel_Txt.text = "" + numero;
                                nCifras--;
                            }
                        }
                        else
                        {
                        if (contacto_confirmado[confirmarContacto] == true)
                                {
                                    if (numero == rN)
                                    {
                                        Frase_Txt.text = "¡¡¡Bien Hecho!!!";
                                    TareaCompletada(0);
                                    }
                                    else
                                    {
                                    aSource.goFx("Fallo");
                                    Frase_Txt.text = "¡¡¡Fallo!!!";
                                    }
                                }
                        }
                    }
                }

            break;
            case 1://firmado
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    Paneles[5].SetActive(false);
                    Paneles[6].SetActive(true);
                    Items[0].SetActive(false);//recuadro de firma
                    Items[1].SetActive(true);//refe lapicero
                    Items[4].GetComponent<One_Hand_PickUp>().enabled = true;//desactiva este escript para que pueda ser agarrado EL TABLERO 13-0-24
                    TareaCompletada(3);
                }
                break;
            case 2://devolver lapicero
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    Items[1].SetActive(false);//refe lapicero
                    Items[3].SetActive(false);//lapicero
                    Items[2].SetActive(true);//mesh lapicero
                    Items[5].SetActive(true);//refe tablero
                    muros[3].SetActive(false);
                    aSource.goFx("Bien");
                }
                break;
            case 3:
                Items[5].SetActive(false);//refe tablero
                Items[6].SetActive(true);//mesh tablero
                Items[4].SetActive(false);// tablero
                Paneles[7].SetActive(true);
                TareaCompletada(4);
                break;
        }
    }
    public void AccionEvento(int nE)
    {
        switch (nE)
        {
            case 0:
                Epps[0].SetActive(false);
                Paneles[0].SetActive(false);
                Paneles[2].SetActive(false);
                Paneles[3].SetActive(true);
                correasGuantes[0].SetActive(true);
                correasGuantes[1].SetActive(true);
                manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[0];
                manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[0];
                TareaCompletada(1);///////////////////////////////////////////////////////////////TAREA 1 COMPLETADA/////////////////////////
                break;
            case 1:
                manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                correasGuantes[0].SetActive(false);
                correasGuantes[1].SetActive(false);

                Epps[1].SetActive(false);//apagar guantes refe
                Paneles[3].SetActive(false);
                Paneles[4].SetActive(true);

                TareaCompletada(2);///////////////////////////////////////////////////////////////TAREA 2 COMPLETADA/////////////////////////
                break;

        }
    }
    public void DarRandomNum()
    {
        rN=Random.Range(100, 999);
        NumPizarron.text = rN.ToString();
    }

}