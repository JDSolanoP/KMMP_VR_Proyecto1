using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

public class AccionPuertaDojo : MonoBehaviour
{
    [Header("Front Izq=0 ; Front Der=1 ; Back Izq=2 ; Back Der=3")]
    public GameObject[] puertas;
    public float[] posFinales;
    public float vel;
    public bool si_frente;
    public bool go_anim=false;
    public bool permite_anim=true;
    public bool abriendo=true;
    public float[] posZ0;
    public int actualPuerta;
    float z0 = 0;
    float z1 = 0;
    int sentido = 0;
    bool si_puertasKRCP=false;


    void Start()
    {
        for(int i = 0; i < puertas.Length; i++)
        {
            posZ0[i]= puertas[i].transform.localPosition.z;
        }
        posFinales[0] = puertas[0].transform.localPosition.z;
        posFinales[2] = puertas[2].transform.localPosition.z;
    }
    public void aperturaDojo(bool orden)
    {

        if (orden == true)
        {
            Apertura(0, 1, 0,true);
            //Debug.Log("aperturaDojo "+orden+" "+ 0+"-"+1);
        }
        else 
        {
            Apertura(2, 3, 2,true);

        }
    }
    /*public void UnaPuerta(int nPuerta)
    {

    }*/
    public void cerrandoDojo(bool orden)
    {
        if (orden == true)
        {
            Apertura(0, 1, 0, false);
            Debug.Log("cerrandoDojo " + orden + " " + 0 + "-" + 1);
            si_puertasKRCP = false;
        }
        else
        {
            Apertura(2, 3, 2, false);
            si_puertasKRCP = true;
        }
    }
    void Apertura(int izq, int der, int posRefe,bool tipo)
    {
        actualPuerta = izq;
        
        if (tipo==true)
        {
            sentido = 1;
        }
        else
        {
            sentido = -1;
        }
        go_anim = true;
        //permite_anim = true;
        StartCoroutine(animPuerta(tipo));
    }
    /*IEnumerator animPuertaUnicaDerecha(bool abriendo)
    {
        
        while (go_anim == true)
        {
            
            //Debug.Log("aperturaDojo animacion puerta derecha");
            z0 = puertas[actualPuerta].transform.localPosition.z;
            Debug.Log(z0);
            z1 = puertas[actualPuerta + 1].transform.localPosition.z;
            z0 -= vel * sentido;
            //Debug.Log(z0 + " " + sentido);
            z1 += vel * sentido;
            //puertas[actualPuerta].transform.localPosition = new Vector3(puertas[actualPuerta].transform.localPosition.x, puertas[actualPuerta].transform.localPosition.y, z0);
            puertas[actualPuerta + 1].transform.localPosition = new Vector3(puertas[actualPuerta + 1].transform.localPosition.x, puertas[actualPuerta + 1].transform.localPosition.y, z1);
            if (abriendo == true)
            {
                if (z1 >= posFinales[actualPuerta + 1])//abrir
                {
                    //Debug.Log(z0 + " " + posFinales[actualPuerta + 1] + "alto anim");
                    go_anim = false;
                    break;
                }
            }
            else
            {
                if (z1 <= posZ0[actualPuerta+1])//cerrar
                {
                    //puertas[actualPuerta].transform.localPosition = new Vector3(puertas[actualPuerta].transform.localPosition.x, puertas[actualPuerta].transform.localPosition.y, posZ0[actualPuerta]);
                    puertas[actualPuerta + 1].transform.localPosition = new Vector3(puertas[actualPuerta + 1].transform.localPosition.x, puertas[actualPuerta + 1].transform.localPosition.y, posZ0[actualPuerta + 1]);
                    //Debug.Log("alto anim");
                    go_anim = false;
                    break;

                }
            }
            yield return new WaitForSeconds(0.025f);
        }
    }*/
    
    IEnumerator animPuerta(bool abriendo)
    {
        permite_anim = false;
        AudioManager.aSource.goFx(AudioManager.aSource.FxSonidos[3].nombre);//*******************************************SONIDO DE APERTURA********************
        while (go_anim == true) {
            
            //Debug.Log("aperturaDojo animacion");
            z0 = puertas[actualPuerta].transform.localPosition.z;
            //Debug.Log(z0);
            z1 = puertas[actualPuerta + 1].transform.localPosition.z;
            z0 -= vel * sentido;
            //Debug.Log(z0 + " " + sentido);
            z1 += vel * sentido;
            puertas[actualPuerta].transform.localPosition = new Vector3(puertas[actualPuerta].transform.localPosition.x, puertas[actualPuerta].transform.localPosition.y, z0);
            puertas[actualPuerta + 1].transform.localPosition = new Vector3(puertas[actualPuerta + 1].transform.localPosition.x, puertas[actualPuerta + 1].transform.localPosition.y, z1);
            if (abriendo == true)
            {
                if (z0 <= posFinales[actualPuerta + 1])//abrir
                {
                    AudioManager.aSource.altoFx(AudioManager.aSource.FxSonidos[3].nombre);////////////////////////////////////////////////SONIDO DE ALTO A PUERTA*********************************
                    AudioManager.aSource.goFx(AudioManager.aSource.FxSonidos[4].nombre);
                    //Debug.Log(z0 + " " + posFinales[actualPuerta + 1] + "alto anim");
                    go_anim = false;
                    permite_anim = true;
                    break;
                }
            }
            else
            {
                if (z0 >= posZ0[actualPuerta])//cerrar
                {
                    AudioManager.aSource.altoFx(AudioManager.aSource.FxSonidos[3].nombre);
                    AudioManager.aSource.goFx(AudioManager.aSource.FxSonidos[4].nombre);
                    puertas[actualPuerta].transform.localPosition = new Vector3(puertas[actualPuerta].transform.localPosition.x, puertas[actualPuerta].transform.localPosition.y, posZ0[actualPuerta]);
                    puertas[actualPuerta + 1].transform.localPosition = new Vector3(puertas[actualPuerta + 1].transform.localPosition.x, puertas[actualPuerta + 1].transform.localPosition.y, posZ0[actualPuerta + 1]);
                    //Debug.Log("alto anim");
                    go_anim = false;
                    permite_anim = true;
                    if (si_puertasKRCP == true)
                    {
                        AudioManager.aSource.altoFxLoop(AudioManager.aSource.FxSonidos[0].nombre);//**********AGREGADO EL 07-08-24*************************************************
                        AudioManager.aSource.altoFxLoop(AudioManager.aSource.FxSonidos[1].nombre);
                        AudioManager.aSource.altoFxLoop(AudioManager.aSource.FxSonidos[2].nombre);
                    }
                    break;
                }
            }
            yield return new WaitForSeconds(0.025f);
        }
    }
            
}
