using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

//using Unity.Android.Gradle;

//using Unity.Android.Gradle;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Lista_Tareas_Controller TareaController;
    public static AudioManager aSource;
    public Sonido[] MusicaSonidos, FxSonidos, VocesSonidos;
    public AudioSource MusicaSource;
    public AudioSource[] FxSourceCanal;
    public AudioSource[] VocesSourceCanal;


    public float FxVolumenMaster, VocesVolumenMaster;
    public int FxCanalActual=0;
    public int VozCanalActual;
    public int VozCanalAntiguo;
    public bool isMusicaPlay;
    public bool isVozPlay;
    [Header("Valores para FXSounds")]
    public bool[] fxloop;
    public bool[] fxPlaying;
    public bool[] fxNoReemplazable;
    private bool testingFx = true;
    private bool testingVoz = true;
    private void Awake()
    {
        if (aSource == null)
        {
            aSource = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {

    }

    public void PlayMusica(string nombre)
    {
        Sonido s = Array.Find(MusicaSonidos, x => x.nombre == nombre);
        if (s == null)
        {
            Debug.Log("Sonido no Encontrado " + nombre);
        }
        else
        {
            MusicaSource.clip = s.clip;
            MusicaSource.loop = true;
            MusicaSource.volume = 1;
            //Debug.Log(nombre + " Sonido Encontrado y colocado->"+MusicaSource.clip.name);
            MusicaSource.Play();
        }
    }
    public void PlayMusica(string nombre,float vol, bool looping)//sobrecargado
    {
        Sonido s = Array.Find(MusicaSonidos, x => x.nombre == nombre);
        if (s == null)
        {
            Debug.Log("Sonido no Encontrado "+ nombre);
        }
        else
        {
            MusicaSource.clip = s.clip;
            MusicaSource.loop = looping;
            MusicaSource.volume = vol;
            Debug.Log(nombre + " Sonido Encontrado y colocado->" + MusicaSource.clip.name);
            MusicaSource.Play();
        }
    }
    /*public void PlayFx(string nombre)
    {
        Sonido s = Array.Find(FxSonidos, x => x.nombre == nombre);
        if (s == null)
        {
            Debug.Log("SonidoFX no Encontrado " + nombre);
        }
        else
        {
            Debug.Log("SonidoFX Encontrado" + nombre);
            /*while (AudioManager.aSource != null)
            {
                if (FxCanalActual > FxSourceCanal.Length-1) { FxCanalActual = 0; Debug.Log(" dAMOS LA VUELTA EN playFx en canal a usar " + FxCanalActual); }
                

                if (FxSourceCanal[FxCanalActual].clip != null)
                {
                    if (FxSourceCanal[FxCanalActual].isPlaying != true || s.clip.name == FxSourceCanal[FxCanalActual].clip.name)
                    {
                            FxSourceCanal[FxCanalActual].clip = s.clip;
                            FxSourceCanal[FxCanalActual].loop = false;
                            fxloop[FxCanalActual] = true;
                            Debug.Log(s.clip.name +" " +FxCanalActual + " Reemplazando SonidoFX Encontrado " + " " + FxSourceCanal[FxCanalActual].clip.name + " vol custom = " + 1 + " loop =" + false);
                            FxSourceCanal[FxCanalActual].volume = 1;
                        FxSourceCanal[FxCanalActual].Play();
                        Debug.Log(s.clip.name + " playFx en canal " + FxCanalActual);
                        //FxCanalActual++;//Para verificar que el mismo sonido no se repita en un mismo  instante

                        //Debug.Log("SonidoFX Encontrado, play en canal " + FxCanalActual);
                        break;
                    }
                }
                else
                {
                    FxSourceCanal[FxCanalActual].clip = s.clip;
                    FxSourceCanal[FxCanalActual].loop = false;

                    fxloop[FxCanalActual] = true;
                    Debug.Log(FxCanalActual + "Nuevo SonidoFX Encontrado " + " " + s.clip.name + " vol custom = " + 1 + " loop =" + false);
                    FxSourceCanal[FxCanalActual].volume = 1;
                    FxSourceCanal[FxCanalActual].Play();
                    Debug.Log(s.clip.name + " playFx en canal " + FxCanalActual);
                    //FxCanalActual++;//Para verificar que el mismo sonido no se repita en un mismo  instante
                    //Debug.Log("SonidoFX Encontrado, play en canal " + FxCanalActual);
                    break;
                }
                FxCanalActual++;
                if (FxCanalActual >= FxSourceCanal.Length) { FxCanalActual = 0; }//para dar la vuelta
                Debug.Log("playFx en canal " + FxCanalActual + " para verificar disponibilidad");
            }
            if (FxCanalActual >= FxSourceCanal.Length) { FxCanalActual = 0; }
            Debug.Log(s.clip.name + " playFx en canal a usar " + FxCanalActual);
        }
    }*/
    /*public void PlayFx(string nombre, float vol,bool Looping)//experimentando 30-07-24 Objetivo en caso de loop o de que se toque denuevo el sonido que se toque en solo un unico canalsource 
    {
        /*Sonido s = Array.Find(FxSonidos, x => x.nombre == nombre);
        if (s == null)
        {
            Debug.Log("SonidoFX no Encontrado "+ nombre);
        }
        else
        {
            Debug.Log("SonidoFX Encontrado"+ nombre);
            while (AudioManager.aSource != null)
            {
                if (FxCanalActual > FxSourceCanal.Length-1) { FxCanalActual = 0; }//para dar la vuelta
                if (FxSourceCanal[FxCanalActual].clip != null)
                {
                    for (int i = 0; i < FxSourceCanal.Length; i++)//EVITA QUE MISMO AUDIO SE REPITA MUCHAS VECES
                    {
                        int aux = FxCanalActual + i;
                        if (aux >= FxSourceCanal.Length - 1)
                        {
                            aux -= FxSourceCanal.Length - 1;
                        }
                        Debug.Log("FxSourceCanal[i].isPlaying="+ FxSourceCanal[i].isPlaying+" "+s.clip.name+"=="+ FxSourceCanal[i].clip.name);
                        /*if (FxSourceCanal[i].isPlaying == true && s.clip.name == FxSourceCanal[i].clip.name)
                        {
                            FxSourceCanal[i].volume = vol;
                            FxSourceCanal[i].loop = Looping;
                            FxSourceCanal[i].Play();
                            Debug.Log(i + " MISMO FX Encontrado " + " " + s.clip.name + " vol custom = " + vol + " loop =" + Looping);
                            FxCanalActual++;
                            break;
                        }
                        else
                        {
                            if (FxSourceCanal[i].isPlaying != true && s.clip.name == FxSourceCanal[i].clip.name)//tocando y nombre  igual
                            {
                                FxSourceCanal[i].volume = vol;
                                FxSourceCanal[i].loop = Looping;
                                FxSourceCanal[i].Play();
                                Debug.Log("se vuelve a tocar en" + i + " MISMO FX Encontrado " + s.clip.name + " vol custom = " + vol + " loop =" + Looping);
                                //FxCanalActual++;
                                break;
                            }
                            else
                            {
                                if (FxSourceCanal[i].isPlaying != true && s.clip.name != FxSourceCanal[i].clip.name)//no tocando y nombre diferente
                                {
                                    FxSourceCanal[i].clip = s.clip;
                                    FxSourceCanal[i].volume = vol;
                                    FxSourceCanal[i].loop = Looping;
                                    FxSourceCanal[i].Play();
                                    Debug.Log(s.nombre + " FX Encontrado " + s.clip.name + " colocado en FXSOURCE[ " + i + "] vol custom = " + vol + " loop =" + Looping);
                                }
                                else
                                {
                                    Debug.Log(s.clip.name + "->" + s.nombre + " no se toca en el canal " + i);
                                    if (i == FxSourceCanal.Length - 1)
                                    {
                                        i = -1;
                                    }

                                }
                            }
                        }
                    }
                }
                if (FxSourceCanal[FxCanalActual].isPlaying != true || s.clip.name == FxSourceCanal[FxCanalActual].clip.name)
                {
                    FxSourceCanal[FxCanalActual].clip = s.clip;
                    FxSourceCanal[FxCanalActual].loop = Looping;

                    fxloop[FxCanalActual] = true;
                    Debug.Log(FxCanalActual + " SonidoFX Encontrado " + " " + s.clip.name + " vol custom = " + vol + " loop =" + Looping);
                    FxSourceCanal[FxCanalActual].volume = vol;
                    FxSourceCanal[FxCanalActual].Play();
                    Debug.Log("playFxL en canal actual " + FxCanalActual);
                    //FxCanalActual++;//Para verificar que el mismo sonido no se repita en un mismo  instante
                    break;
                }
                /*else
                {
                    /*if (Looping == true)
                    {
                        if (s.clip.name == FxSourceCanal[FxCanalActual].clip.name)
                        {
                            FxSourceCanal[FxCanalActual].clip = s.clip;
                            FxSourceCanal[FxCanalActual].loop = Looping;

                            fxloop[FxCanalActual] = true;
                            Debug.Log(s.clip.name + " " + FxCanalActual + " SonidoFX Encontrado " + " " + FxSourceCanal[FxCanalActual].clip.name + " vol actualizado = " + vol + " loop =" + Looping);
                            FxSourceCanal[FxCanalActual].volume = vol;
                            FxSourceCanal[FxCanalActual].Play();
                            Debug.Log("playFxL en canal actual " + FxCanalActual);
                            //FxCanalActual++;//Para verificar que el mismo sonido no se repita en un mismo  instante
                            break;
                        }//finde if
                    //}//fin de isloop
                }//fin de else
                //fin de if is playing
                else
                {
                    FxSourceCanal[FxCanalActual].clip = s.clip;
                    FxSourceCanal[FxCanalActual].loop = Looping;

                    fxloop[FxCanalActual] = true;
                    Debug.Log(s.clip.name+ " y colocado en " + FxCanalActual + "Nuevo FXSOURCE Encontrado  " + " " + FxSourceCanal[FxCanalActual].clip.name + " vol custom = " + vol + " loop =" + Looping);
                    FxSourceCanal[FxCanalActual].volume = vol;
                    FxSourceCanal[FxCanalActual].Play();
                    Debug.Log("playFxL en canal actual " + FxCanalActual);
                    //FxCanalActual++;//Para verificar que el mismo sonido no se repita en un mismo  instante
                    break;
                }
                //Debug.Log("SonidoFX Encontrado, play en canal " + FxCanalActual);
                
                FxCanalActual++;
                if (FxCanalActual >= FxSourceCanal.Length) { FxCanalActual = 0; }//para dar la vuelta
                Debug.Log("playFxLoop en canal " + FxCanalActual+" para verificar disponibilidad");
            }
            if (FxCanalActual >= FxSourceCanal.Length) { FxCanalActual = 0; }//para dejarlo listo par ael siguiente audio
            Debug.Log(s.clip.name + " playFx en canal a usar " + FxCanalActual);
            
        }
    }*/
    public void PlayVoz(string nombre)
    {
        Sonido s = Array.Find(VocesSonidos, x => x.nombre == nombre);
        if (s == null)
        {
            Debug.Log("SonidoFX no Encontrado");
        }
        else
        {
            while (AudioManager.aSource != null)
            {
                //if(AudioManager.aSource.isVozPlay == true) { }
                if (VocesSourceCanal[VozCanalActual].isPlaying != true/* || s.clip.name != VocesSourceCanal[VozCanalActual].name*/)
                {
                    if (VozCanalActual == 0&& s.clip != VocesSourceCanal[0].clip)
                    {
                        VocesSourceCanal[VozCanalActual].clip = s.clip;
                        Debug.Log(VocesSourceCanal[VozCanalActual].clip.name + " SonidoVoz Encontrado tocado en el canal " + VozCanalActual);
                        VocesSourceCanal[VozCanalActual].Play();
                        isVozPlay = true;
                        StartCoroutine(CheckingPlayVoz());//************************************************************07-08-24
                        break;
                    }
                    else
                    {
                        if (VozCanalActual == 1 && s.clip != VocesSourceCanal[1].clip)
                        {
                            VocesSourceCanal[VozCanalActual].clip = s.clip;
                            Debug.Log(VocesSourceCanal[VozCanalActual].clip.name + " SonidoVoz Encontrado tocado en el canal " + VozCanalActual);
                            VocesSourceCanal[VozCanalActual].Play();
                            isVozPlay = true;
                            StartCoroutine(CheckingPlayVoz());//************************************************************07-08-24
                            break;
                        }
                        else { break; 
                        }
                    
                    }/*
                else {
                    if (s.clip.name != VocesSourceCanal[VozCanalActual].name)
                    {
                        if (VozCanalActual == 0)
                        {
                            VocesSourceCanal[1].clip = s.clip;
                            VocesSourceCanal[1].Play();
                            isVozPlay = true;
                            Debug.Log("Forzado playVoz en canal " + VozCanalActual + " para sonido " + s.nombre);
                            StartCoroutine(CheckingPlayVoz());//************************************************************07-08-24
                            break;
                        }
                        else
                        {
                            VocesSourceCanal[0].clip = s.clip;
                            VocesSourceCanal[0].Play();
                            isVozPlay = true;
                            Debug.Log("Forzado playVoz en canal 0 para sonido " + s.nombre);
                            StartCoroutine(CheckingPlayVoz());//************************************************************07-08-24
                            break;
                        }
                    }*/
                }
                else

                {

                    VocesSourceCanal[VozCanalAntiguo].clip = s.clip;

                    //Debug.Log(VocesSourceCanal[VozCanalActual].clip.name + " SonidoVoz Encontrado tocado en el canal " + VozCanalActual);

                    VocesSourceCanal[VozCanalAntiguo].Play();

                    isVozPlay = true;
                    break;
                }

            }
            Debug.Log(VocesSourceCanal[VozCanalActual].clip+" playVoz en canal " + VozCanalActual);
            VozCanalActual++;
            if (VozCanalActual >= VocesSourceCanal.Length) { VozCanalActual = 0; }
            Debug.Log("playVoz en canal a usar " + VozCanalActual);
            
        }
    }
    /*public void playFxLoop(string nombre)
    {/*
        Sonido s = Array.Find(FxSonidos, x => x.nombre == nombre);
        if (s == null)
        {
            Debug.Log("SonidoFX no Encontrado");
        }
        else
        {
            Debug.Log("SonidoFX Encontrado");
            while (AudioManager.aSource != null)
            {
                if (fxloop[FxLoopCanal] == false) { FxSourceCanal[FxLoopCanal].loop = fxloop[FxLoopCanal];break; }
                if (FxSourceCanal[FxCanalActual].isPlaying != true || FxSonidos[FxCanalActual].nombre == FxSourceCanal[FxCanalActual].clip.name)
                {
                    Debug.Log("SonidoFX Encontrado LOOP CANAL, play en canal " + FxCanalActual);
                    if (FxSonidos[FxCanalActual].nombre != FxSourceCanal[FxCanalActual].clip.name)
                    {

                        FxSourceCanal[FxCanalActual].clip = s.clip;
                    }
                    FxLoopCanal = FxCanalActual;
                    FxSourceCanal[FxLoopCanal].loop = fxloop[FxLoopCanal];
                    
                    FxSourceCanal[FxLoopCanal].Play();
                    Debug.Log(FxCanalActual+"SonidoFX Encontrado LOOP CANAL, play en canal " + FxLoopCanal);
                    break;
                }
                FxCanalActual++;
                if (FxCanalActual >= FxSourceCanal.Length) { FxCanalActual = 0; }
            }
        }
    }*/
    /*public void StopFX(string nombre)//***************************************DETENCION DE MUSICA**********************************
    {/*
        for (int i = 0; i < FxSourceCanal.Length; i++)
        {
            if (nombre == FxSourceCanal[i].clip.name)
            {
                fxloop[i] = false;
                FxSourceCanal[i].Stop();
                Debug.Log(nombre+" se detuvo en el canal "+i);
                break;
            }
        }
        
    }*/
    /*public void MusicaBoton(int isMusica)
    {
        switch (isMusica)
        {
            case 0:
                MusicaSource.mute = !MusicaSource.mute;
                sceneLoadManager.slm.setMuteM(0, MusicaSource.mute);
                break;
            case 1:
                for (int i = 0; i < FxSourceCanal.Length; i++)
                {
                    FxSourceCanal[i].mute = !FxSourceCanal[i].mute;
                }
                sceneLoadManager.slm.setMuteM(1, FxSourceCanal[0].mute);
                break;
            case 2:
                for (int i = 0; i < VocesSourceCanal.Length; i++)
                {
                    VocesSourceCanal[i].mute = !VocesSourceCanal[i].mute;
                }
                sceneLoadManager.slm.setMuteM(2, VocesSourceCanal[0].mute);
                break;
        }
        sceneLoadManager.slm.GuardarDatos();
    }*/
    public void MusicaVol(float vol)
    {
        Debug.Log(vol + " se coloco a MusicaVol");
        MusicaSource.volume = vol;
        //TareaController.setSonidoVolumen(0, vol);
    }
    public void FxVol(float vol)
    {/*
        Debug.Log(vol + " se coloco a todos los canales ");
        FxVolumenMaster = vol;
        for (int i = 0; i < FxSourceCanal.Length; i++)
        {
            FxSourceCanal[i].volume = vol;
        }
        */
    }
    public void VozVol(float vol)
    {
        VocesVolumenMaster = vol;
        for (int i = 0; i < VocesSourceCanal.Length; i++)
        {
            VocesSourceCanal[i].volume = vol;
        }

    }
    /*public IEnumerator testFx()
    {
        testingFx = false;
        Debug.Log("testing FX");
        PlayFx("ArpegioLvlUp1");
        yield return new WaitForSeconds(1f);
        testingFx = true;
        if (testingFx == true)
        {
            StopAllCoroutines();
        }
    }
    public IEnumerator testVoz()
    {
        testingVoz = false;
        PlayVoz(VocesSonidos[1].nombre);
        yield return new WaitForSeconds(1f);
        testingVoz = true;
        if (testingVoz == true)
        {
            StopAllCoroutines();
        }
    }*/
    public bool IsPlayingVoz()
    {
        for(int i = 0;i < VocesSourceCanal.Length; i++)
        {
            if (VocesSourceCanal[i].isPlaying == true)
            {
                isVozPlay = true;
                break;
            }
            else
            {
                isVozPlay = false;
            }
        }
        
    return isVozPlay; 
    }
    /*public void FxAlto(string name) 
    {/*
        Sonido s = Array.Find(FxSonidos, x => x.nombre == name);
        if (s == null)
        {
            Debug.Log("SonidoFX en FXAlto no Encontrado "+name);
        }
        else
        {
            Debug.Log("Deteniendo " + name);
            for (int i = 0; i < FxSourceCanal.Length; i++)
            {
                if (FxSourceCanal[i].clip.name == s.clip.name)
                {
                    FxSourceCanal[i].loop = false;
                    FxSourceCanal[i].Stop();
                    Debug.Log(FxSourceCanal[i].clip.name+" Clip en FXSource " + FxSourceCanal[i].name +"-> "+i + " Detenido " + s.clip.name);
                    FxSourceCanal[i].volume = 1;
                    break;
                }
                Debug.Log(i + " No se Detuvo " + name);
            }
        }
        
    }*/
    public void FxVolPropio(string name, float vol)
    {
        Sonido s = Array.Find(FxSonidos, x => x.nombre == name);
        if (s == null)
        {
            Debug.Log("SonidoFX e FX Vol Propio no Encontrado "+name);
        }
        else
        {
            for (int i = FxSourceCanal.Length-1; i >=0; i--)
            {
                if (FxSourceCanal[i].clip != null)
                {
                    if (FxSourceCanal[i].clip.name == s.clip.name)
                    {
                        FxSourceCanal[i].volume = vol;
                        Debug.Log(vol + " Nuevo vol para " + name);
                        break;
                    }
                    //Debug.Log(FxSourceCanal[i].clip.name + " Clip en FXSource " + FxSourceCanal[i].name + "-> " + i + " SonidoFX no Encontrado en el FXSource " + s.clip.name);
                }
                
            }
        }
    }
    public void goFx(string nombre, float vol, bool Looping, bool reemplazable)
    {
        Sonido s = Array.Find(FxSonidos, x => x.nombre == nombre);
        if (s == null)
        {
            Debug.Log("SonidoFX no Encontrado " + nombre);
        }
        else
        {
            for (int i = 0; i< FxSourceCanal.Length; i++)
            {
                if (FxSourceCanal[i].isPlaying == false)
                {
                    fxPlaying[i]=false;
                    fxNoReemplazable[i] = false;
                    fxloop[i] = false;
                    //Debug.Log("Limpieza de valores para insertar" + nombre);
                }
            }
            if (Looping == false)
            {
                for (int i = 0; i < FxSourceCanal.Length; i++)
                {
                    if (fxPlaying[i] == false)
                    {
                        if (FxSourceCanal[i].clip == null || fxNoReemplazable[i] == false)
                        {
                            fxDarValores(s, i, vol, Looping, reemplazable);
                            FxSourceCanal[i].Play();
                            break;
                        }
                    }
                    else
                    {
                        if (fxNoReemplazable[i] == false)
                        {
                            if (fxloop[i] == true)
                            {
                                if (s.clip.name == FxSourceCanal[i].clip.name)
                                {
                                    //Debug.Log("se volvio a dar play a " + FxSourceCanal[i].clip.name+"en el canal "+i);
                                    FxSourceCanal[i].Play();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else//looping = true
            {
                for (int i = FxSourceCanal.Length - 1; i >= 0; i--)
                {
                    if (fxPlaying[i] == false)
                    {
                        if (FxSourceCanal[i].clip == null || fxNoReemplazable[i] == false)
                        {
                            fxDarValores(s, i, vol, Looping, reemplazable);
                            FxSourceCanal[i].Play();
                                break;
                        }
                    }
                    else
                    {
                        if (fxNoReemplazable[i] == false)
                        {
                            if (fxloop[i] == true)
                            {
                                if (s.clip.name == FxSourceCanal[i].clip.name)
                                {
                                    //Debug.Log("se volvio a dar play a " + FxSourceCanal[i].clip.name + "en el canal " + i);
                                    if (FxSourceCanal[i].loop == false)
                                    {
                                        Debug.Log("Loopeando " + FxSourceCanal[i].clip.name);
                                        FxSourceCanal[i].loop = true;
                                    }
                                    FxSourceCanal[i].Play();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (fxloop[i] == true)
                            {
                                if (s.clip.name == FxSourceCanal[i].clip.name)
                                {
                                    Debug.Log("NO se volvio a dar play a " + FxSourceCanal[i].clip.name + "en el canal " + i);
                                    if (FxSourceCanal[i].loop == false)
                                    {
                                        Debug.Log("Loopeando " + FxSourceCanal[i].clip.name);
                                        FxSourceCanal[i].loop = true;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public void goFx(string nombre)
    {
        Sonido s = Array.Find(FxSonidos, x => x.nombre == nombre);
        if (s == null)
        {
            Debug.Log("SonidoFX no Encontrado " + nombre);
        }
        else
        {
            for (int i = 0; i < FxSourceCanal.Length; i++)
            {
                if (FxSourceCanal[i].isPlaying == false)
                {
                    fxPlaying[i] = false;
                    fxNoReemplazable[i] = false;
                    fxloop[i] = false;
                    //Debug.Log("Limpieza de valores para insertar" + nombre);
                }
            }

            for (int i = 0; i < FxSourceCanal.Length; i++)
                {
                if (fxPlaying[i] == false)
                {
                    fxDarValores(s, i, 1, false, false);
                    FxSourceCanal[i].Play();
                    break;

                }
                else
                {
                    if (fxNoReemplazable[i] == false)
                    {
                        if (fxloop[i] == true)
                        {
                            if (s.clip.name == FxSourceCanal[i].clip.name)
                            {
                               // Debug.Log("se volvio a dar play a " + FxSourceCanal[i].clip.name + "en el canal " + i);
                                FxSourceCanal[i].Play();
                                //fxDarValores(s, i, 1, false, false);
                                break;
                            }
                        }
                    }

                }
            }
        }
    }

    public void goFxWithTime(string nombre, float time)
    {
        Sonido s = Array.Find(FxSonidos, x => x.nombre == nombre);
        if (s == null)
        {
            Debug.Log("SonidoFX no Encontrado " + nombre);
        }
        else
        {
            for (int i = 0; i < FxSourceCanal.Length; i++)
            {
                if (FxSourceCanal[i].isPlaying == false)
                {
                    fxPlaying[i] = false;
                    fxNoReemplazable[i] = false;
                    fxloop[i] = false;
                    //Debug.Log("Limpieza de valores para insertar" + nombre);
                }
            }

            for (int i = 0; i < FxSourceCanal.Length; i++)
            {
                if (fxPlaying[i] == false)
                {
                    fxDarValores(s, i, 1, false, false);
                    FxSourceCanal[i].time = time;
                    FxSourceCanal[i].Play();
                    break;

                }
                else
                {
                    if (fxNoReemplazable[i] == false)
                    {
                        if (fxloop[i] == true)
                        {
                            if (s.clip.name == FxSourceCanal[i].clip.name)
                            {
                                // Debug.Log("se volvio a dar play a " + FxSourceCanal[i].clip.name + "en el canal " + i);
                                FxSourceCanal[i].Play();
                                //fxDarValores(s, i, 1, false, false);
                                break;
                            }
                        }
                    }

                }
            }
        }
    }

    public void altoFxLoop(string nombre)
    {
        Sonido s = Array.Find(FxSonidos, x => x.nombre == nombre);
        if (s == null)
        {
            Debug.Log("SonidoFX no Encontrado " + nombre);
        }
        else
        {
            for (int i = FxSourceCanal.Length - 1; i >= 0; i--)
            {
                if (FxSourceCanal[i].clip != null)
                {
                    if (s.clip.name == FxSourceCanal[i].clip.name)
                    {
                        FxSourceCanal[i].Stop();
                        FxRestauradorValores(i);
                        break;
                    }
                }
            }
        }
    }
    public void altoFx(string nombre)
    {
        Sonido s = Array.Find(FxSonidos, x => x.nombre == nombre);
        if (s == null)
        {
            Debug.Log("SonidoFX no Encontrado " + nombre);
        }
        else
        {
            for (int i = 0; i < FxSourceCanal.Length; i++)
            {
                if (FxSourceCanal[i].clip != null)
                {
                    if (s.clip.name == FxSourceCanal[i].clip.name)
                    {
                        FxSourceCanal[i].Stop();
                        FxRestauradorValores(i);
                        break;
                    }
                }
            }
        }
    }
    public void FxRestauradorValores(int i)
    {
        Debug.Log("detenido " + FxSourceCanal[i].clip.name + "en el canal " + i);
        FxSourceCanal[i].clip = null;
                FxSourceCanal[i].volume = 1;
        FxSourceCanal[i].loop = false;
                fxloop[i] = false;
        fxPlaying[i] = false;
        fxNoReemplazable[i] = false;
    }
    public void fxDarValores(Sonido s, int i, float vol, bool Looping, bool reemplazable)
    {
        
        FxSourceCanal[i].clip = s.clip;
        FxSourceCanal[i].loop = Looping;

        FxSourceCanal[i].volume = vol;
       // Debug.Log(s.clip.name + " " + i + "  goSonidoFX Encontrado " + " " + FxSourceCanal[i].clip.name + " vol custom = " + vol + " loop =" + Looping + " reemplazable = " + reemplazable);
        fxPlaying[i] = true;
        fxloop[i] = Looping;
        fxNoReemplazable[i] = reemplazable;
        if (s.nombre == "Bien")
        {
            FxSourceCanal[i].volume = 0.5f;
        }
        //FxSourceCanal[i].Play();
        //Debug.Log(s.clip.name + " playFx en canal " + i);
    }
    IEnumerator CheckingPlayVoz()
    {
        while(isVozPlay == true)
        {
            for (int i = 0; i < VocesSourceCanal.Length; i++)
            {
                if (VocesSourceCanal[i].isPlaying == true)
                {
                    isVozPlay = true;
                    yield return new WaitForSeconds(.5f);
                    break;
                    
                }
                else
                {
                    isVozPlay = false;
                }
            }
            //yield return new WaitForSeconds(1f);
        }
    }
}