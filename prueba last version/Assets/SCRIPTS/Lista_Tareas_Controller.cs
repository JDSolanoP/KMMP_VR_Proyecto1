using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lista_Tareas_Controller : MonoBehaviour
{
    public AudioManager audioManager;
    public int TareaActual;
    public int totalTareas;
    public bool tutorial = true;


    // Ejemplo metodo para hacer herencia
    public virtual void Start()
    {
        Debug.Log("Inicia herencia del Start");
        totalTareas = audioManager.narraciones.Length;
        StartCoroutine(InicioDeNivel());
    }

    public virtual void TareaCompletada(int indexTarea)
    {
        if (indexTarea == TareaActual)
        {
            CargarSiguienteTarea();
            return;
        }
        Debug.Log("La tarea numero "+indexTarea+" no es n{TareaActual}");
    }
    public void CargarSiguienteTarea()
    {
        //cambiamos a la siguiente tarea.
        TareaActual++;
        //cargamos nuevo clip de audio.
        if (TareaActual < totalTareas)
        {
            //StartCoroutine(CargarReproducir_Clip());
            Debug.Log("se cargo tarea numero "+TareaActual);
            return;
        }
        Debug.Log("Fin de lista de la tareas");
    }

    IEnumerator CargarReproducir_Clip()
    {
        yield return new WaitForSeconds(0.5f);

        audioManager.CargarClip(TareaActual);
        Debug.Log($"se cargo clip de tarea n"+TareaActual+"correctamente");

        yield return new WaitForSeconds(0.5f);

        audioManager.ReproducirAudioClip();
        Debug.Log("Audio en curso");
    }
    public virtual void DelayInicioNivel()
    {
        StartCoroutine(InicioDeNivel());
    }
    IEnumerator InicioDeNivel()
    {
        yield return new WaitForSeconds(0.5f);
        //tiempo de espera del audio inicial(tarea = 0)
        //StartCoroutine(CargarReproducir_Clip());
    }
}
