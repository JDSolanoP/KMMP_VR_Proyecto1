using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lista_Tareas_Controller : MonoBehaviour
{
    public static Lista_Tareas_Controller tm;
    public string nombre_funcion;
    public AudioManager aSource;
    public int TareaActual;
    public int totalTareas;
    [Header("*****Opciones de Desarrollador*****")]
    public bool EnPruebas = true;
    [Header("EnPruebas=false->Para Pruebas de todas las tareas y animaciones")]
    public bool si_login=false;//01-03-25//espera la pausa
    public GameObject[] aros_indicadores;
    public bool[] GlobalBool;
    public int[] GlobalInt;
    public float[] GlobalFloat;
    [Header("Transiciones_Elementos")]
    public bool SiFadeActivo;
    public Renderer rend;//Renderimg Mode => Fade
    public Color fadeColor;
    public float fadeTiempo = 2;
    public bool IniciaFade = true;
    [Header("ELEMENTOS DE MODULOS")]
    public GameObject[] Tablero_Indicaciones;
    public bool[] contacto_confirmado;

    // Ejemplo metodo para hacer herencia
    private void Awake()
    {
        if (tm == null)
        {
            tm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public virtual void Start()
    {
        if (SiFadeActivo)//mantener desactivado para usar render asigando en el editor
        {
            rend = GetComponent<Renderer>();
            if (IniciaFade) FadeIn();
        }
        //rend = GetComponent<Renderer>();
        if (IniciaFade) FadeIn();
        Debug.Log("Inicia herencia del Start");
        //totalTareas = aSource.VocesSonidos.Length;

        
        StartCoroutine(InicioDeNivel());
    }

    public virtual void TareaCompletada(int indexTarea)
    {
        if (indexTarea == TareaActual)
        {
            CargarSiguienteTarea();
            return;
        }
        Debug.Log("La tarea numero " + indexTarea + " no es n{"+TareaActual+"}");
    }
    public void CargarSiguienteTarea()
    {
        //cambiamos a la siguiente tarea.
        TareaActual++;
        //cargamos nuevo clip de audio.
        if (TareaActual < totalTareas)
        {
            StartCoroutine(CargarReproducir_Clip());
            Debug.Log("se cargo tarea numero " + TareaActual);
            return;
        }
        else
        {
            Debug.Log("Fin de lista de la tareas");
        }
    }

    IEnumerator CargarReproducir_Clip()
    {
        yield return new WaitForSeconds(0.5f);
        if(aSource != null) {
            if (aSource.VocesSonidos.Length > TareaActual)
            {
                AudioManager.aSource.PlayVoz(aSource.VocesSonidos[TareaActual].nombre);
                Debug.Log($"se cargo clip de tarea n " + TareaActual + " correctamente");
            }
        }
        
        yield return new WaitForSeconds(0.5f);

        //audioManager.ReproducirAudioClip();
        Debug.Log("Audio en curso");
    }
    public virtual void DelayInicioNivel()
    {
        StartCoroutine(InicioDeNivel());
    }
    IEnumerator InicioDeNivel()
    {
        if (si_login)
        {
            //audio inicial
            while (TM_Lobby.lb.si_inicioModulo == false)
            {
                yield return new WaitForFixedUpdate();
            }
        }
        yield return new WaitForSeconds(0.5f);
        //tiempo de espera del audio inicial(tarea = 0)
        StartCoroutine(CargarReproducir_Clip());
    }
    IEnumerator espera(float t)
    {
        yield return new WaitForSeconds(t);
    }
    //***********************************************FADE MANAGER***********************************//
    
    public void FadeIn()
    {
        if (fadeColor.a == 1) { Fade(1, 0); } else { Fade(0,1);Fade(1,0); }
        
    }
    public void FadeIn(float tiempo)
    {
        fadeTiempo = tiempo;
        FadeIn();
    }
    public void FadeOut()
    {
        Fade(0, 1);
    }
    public void FadeOut(float tiempo)
    {
        fadeTiempo = tiempo;
        FadeOut();
    }
    public void Fade(float alfaIn,float alfaOut)
    {
        StartCoroutine(CoroutineFade(alfaIn, alfaOut));
    }
    public IEnumerator CoroutineFade(float alfaIn, float alfaOut)
    {
        float timer = 0;
        while (timer <= fadeTiempo)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alfaIn, alfaOut, timer / fadeTiempo);
            rend.material.SetColor("_Color",newColor);
            timer += Time.deltaTime;
            yield return null;
        }
        Color newColor2 = fadeColor;
        newColor2.a = alfaOut;
        rend.material.SetColor("_Color",newColor2);
    }
    //*********************************CAMBiO DE ESCENA CON FADE*********************************
    public void IrEscenaAsincron(int escena)
    {
        StartCoroutine(GoEscenaAsincro(escena));
    }
    IEnumerator GoEscenaAsincro(int nEscena)
    {
        FadeOut();
        AsyncOperation ope = SceneManager.LoadSceneAsync(nEscena);
        ope.allowSceneActivation = false;
        float time = 0;
        while (time < fadeTiempo && !ope.isDone)
        { 
            time += Time.deltaTime;
            yield return null;
        }   
        ope.allowSceneActivation = true;
        yield return new WaitForSeconds(fadeTiempo);
        SceneManager.LoadScene(nEscena);
    }

}
