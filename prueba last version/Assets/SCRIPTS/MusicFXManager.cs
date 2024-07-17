using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public class MusicFXManager : MonoBehaviour
{
    public static MusicFXManager MFXM;

    public Sonido[] musica, sfx;
    public AudioSource MusicaSource; 
    public AudioSource[] SfxSource;
    public int Canalactual=0;

    private void Awake()
    {
        if (MFXM == null)
        {
            MFXM = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusica(musica[0].nombre);
    }
    public void PlayMusica(string nom)
    {
        Sonido s = Array.Find(musica, x => x.nombre == nom);
        if (s == null)
        {
            Debug.Log("Sonido no encontrado");
        }
        else
        {
            MusicaSource.clip = s.clip;
            MusicaSource.Play();
        }
    }
    public void PlaySFX(string nom)
    {
        Sonido s = Array.Find(sfx, x => x.nombre == nom);
        if (s == null)
        {
            Debug.Log("SonidoSFX no encontrado");
        }
        else
        {
            for (int i = 0; i < sfx.Length; i++) 
            {
                if (i == Canalactual)
                {
                    SfxSource[i].clip = s.clip;
                    SfxSource[i].Play();
                    Canalactual++;
                }
                if (Canalactual == sfx.Length)
                {
                    Canalactual = 0;
                    
                }
                break;
            }
        }
    }
    public void StopSFX(string nom)
    {
        if (SfxSource != null)
        {
            SfxSource[Canalactual].Stop();
        }
    }
}
