using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Lista_Tareas_Controller tareaManager;
    public AudioSource aSource;
    public AudioClip[] narraciones;

    // Start is called before the first frame update
    void Awake()
    {
        aSource = GetComponent<AudioSource>();
        tareaManager = GameObject.Find("TareaManager").GetComponent<Lista_Tareas_Controller>();
    }


    public void CargarClip(int audioIndice)
    {
        aSource.clip = narraciones[audioIndice];
        return;
    }

    public void ReproducirAudioClip()
    {
        aSource.Play();
    }
}
