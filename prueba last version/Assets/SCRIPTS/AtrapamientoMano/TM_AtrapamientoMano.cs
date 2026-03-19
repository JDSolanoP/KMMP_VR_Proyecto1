using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_AtrapamientoMano : Lista_Tareas_Controller
{
    public GameObject manoMesh;
    public GameObject manoRefe;
    public GameObject manoUsuario;
    public GameObject PistolaObj;
    public GameObject PistolaRefe;
    public GameObject PistolaMesh;
    public GameObject[] Epps;
    public GameObject[] manosXR;
    public Material[] manosXRMaterial;//19.02
    public int totalEpps;
    public GameObject[] BtnContinue;
    [Header("Accion pistola")]
    public ImpactWrenchGunAccion iwg;

    public GameObject IWP_Refe;
    public GameObject IWP_Mesh;
    public GameObject IWP_OBJ;

    [Header("Primer Intento Pistola Neumatica")]
    [SerializeField] private GameObject PistolaNeumaticaOriginalGameObject;

    [Header("Reinicio de Ejercicio")]
    [SerializeField] private GameObject botonReiniciarEjercicio;
    [SerializeField] private float delayTimeAparicionDeBotonReiniciar;
    [SerializeField] private DetectorPistolaNeumaticaEnPolea DetectorPistolaNeumaticaEnPolea;
    [SerializeField] private DetectorManoEnPolea DetectorManoEnPolea;

    [Header("Segundo Intento Pistola Neumatica")]
    [SerializeField] private GameObject PistolaNeumaticaEnPoleaSegundoIntentoGameObject;
    [SerializeField] private GameObject ManoIzquierdaReferenciaBienPuestaGameObject;
    [SerializeField] private DetectorManoEnPolea DetectorManoEnPoleaBienPuesta;
    [SerializeField] private DetectorPistolaNeumaticaEnPolea DetectorPistolaNeumaticaEnPoleaBienPuesta;
    [SerializeField] private float delayTimeLiberacionDeManoYPistola;

    [Header("Efecto de Dolor")]
    [SerializeField] private Renderer DamageRenderer;
    [SerializeField] private float DamageFadeInTime = 1f;
    [SerializeField] private float DamageFadeOutTime = 4f;
    private Material _damageMaterial;

    [Header("Guardar Pistola Neumatica")]
    [SerializeField] private GameObject PistolaNeumaticaEnCajaReferencia;
    [SerializeField] private GameObject PistolaNeumaticaEnPoleaDejarEnCaja;
    [SerializeField] private Cuerda_Render Cuerda_Render;
    [SerializeField] private Transform PistolaNeumtaticaEnPoleaDejarEnCajaPuntoCab;
    [SerializeField] private GameObject BloqueoZonaDeMeta;

    [Header("Conclusiones")]
    [SerializeField] private GameObject DetectorConclusiones;
    [SerializeField] private GameObject BotonContinuarConclusiones;
    [SerializeField] private float TiempoDeAparicionDeBotonContinuarConclusiones;
    private bool _didThePlayerGoThroughTheConclusionsPanel = false;
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
                for (int i = 0; i < Tablero_Indicaciones.Length; i++)
                {
                    Tablero_Indicaciones[i].SetActive(false);
                }
                botonReiniciarEjercicio.SetActive(false);
                Tablero_Indicaciones[0].SetActive(true);
                yield return new WaitForSecondsRealtime(0.5f);
                //manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                //manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
                aSource.MusicaVol(0.15f);//**************************************Sonido Musica Inicial*************
                //aSource.FxVol(1);
                yield return new WaitForSecondsRealtime(2f);
                //Tablero_Indicaciones[16].SetActive(true);
                yield return new WaitForSecondsRealtime(1f);

                //Debug.Log("Se esta reproduciendo audio");
                //BtnContinue[0].SetActive(true);//iniciar
                yield return new WaitForSecondsRealtime(0.5f);

                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se tiene todos los EPPS
            case 1:
                Tablero_Indicaciones[1].SetActive(true);
                AudioManager.aSource.PlayVoz("DialogoParte1");
                yield return new WaitForSeconds(2f);
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se tiene todos los EPPS
            case 2:
                Tablero_Indicaciones[1].SetActive(false);
                Tablero_Indicaciones[2].SetActive(true);
                yield return new WaitForSeconds(2f);
                AudioManager.aSource.PlayVoz("DialogoParte2");
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se tiene todos los EPPS
            case 3: // Ocurre cuando se comete el error
                Tablero_Indicaciones[2].SetActive(false);
                Tablero_Indicaciones[3].SetActive(true);
                _damageMaterial = DamageRenderer.material;
                Color color = _damageMaterial.color;
                color.a = 0f;
                _damageMaterial.color = color;
                StartCoroutine(DamageRoutine());
                StartCoroutine(AparecerBotonReiniciarConDelay(delayTimeAparicionDeBotonReiniciar));
                while (AudioManager.aSource.IsPlayingVoz() == true)
                {
                    yield return new WaitForFixedUpdate();
                }
                break;//cuando se tiene todos los EPPS
            case 4: // Ocurre cuando se realiza el ejercicio correctamente
                Tablero_Indicaciones[3].SetActive(false);
                Tablero_Indicaciones[4].SetActive(true);
                StartCoroutine(LiberarManoYPistolaNeumaticaConDelay(delayTimeLiberacionDeManoYPistola));
                aSource.goFx("Bien");
                aSource.goFx("Locu_Bien");
                yield return new WaitForSeconds(2f);
                AudioManager.aSource.PlayVoz("DialogoVictoria");
                break;
            case 5: // Ocurre al dejar la pistola en la caja
                Tablero_Indicaciones[4].SetActive(false);
                Tablero_Indicaciones[5].SetActive(true);
                BloqueoZonaDeMeta.SetActive(false);
                aSource.goFx("Bien");
                aSource.goFx("Locu_Bien");
                DetectorConclusiones.SetActive(true);
                break;
            case 6: // Ocurre al presionar el boton Continuar de Conclusiones
                Tablero_Indicaciones[5].SetActive(false);
                Tablero_Indicaciones[6].SetActive(true);
                aSource.goFx("fanfarrias");
                aSource.goFx("aplausos");
                yield return new WaitForSeconds(1f);
                aSource.PlayVoz("DialogoFinal");
                break;
        }
    }
    public void EppPuesto(int numeroEpp)
    {
        if (numeroEpp == 0)
        {
            Debug.Log("Cambiar textura guante");
            manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[0];//19.01
            manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[1];
            //guantesComplementos[0].SetActive(true);
            //guantesComplementos[1].SetActive(true);
        }
        Epps[numeroEpp].SetActive(false);
        ActivarEvento(0);
    }
    private void ActivarEvento(int NumeroEvento)
    {
        switch (NumeroEvento)
        {

            case 0:
                totalEpps++;
                if (totalEpps == 6)
                {
                    Debug.Log("Se agararon todos los EPPs");
                    TareaCompletada(1);
                    aSource.goFx("Bien");
                    aSource.goFx("Locu_Bien");
                }
                break;
        }
    }
    public void ActivarXR(int NumeroEvento)
    {
        switch (NumeroEvento)
        {
            case 0:
                //BtnContinue[0].SetActive(false);
                Tablero_Indicaciones[0].SetActive(false);
                TareaCompletada(0);
                break;
            case 1:
                botonReiniciarEjercicio.SetActive(false);
                //Tablero_Indicaciones[3].SetActive(false);
                PistolaNeumaticaOriginalGameObject.SetActive(false);
                PistolaNeumaticaEnPoleaSegundoIntentoGameObject.SetActive(true);
                ManoIzquierdaReferenciaBienPuestaGameObject.SetActive(true);
                break;
            case 2:
                TareaCompletada(5);
                break;
            case 3:
                IrEscenaAsincron(0);
                break;
            case 4:
                Application.Quit();
                break;
            case 5:
                if (!_didThePlayerGoThroughTheConclusionsPanel)
                {
                    _didThePlayerGoThroughTheConclusionsPanel = true;
                    StartCoroutine(AparecerBotonContinuarDeConlusiones());
                }
                break;
        }
    }

    private IEnumerator AparecerBotonReiniciarConDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        botonReiniciarEjercicio.SetActive(true);
        DetectorManoEnPolea.RealLeftHandSkinnedMeshRenderer.material = DetectorManoEnPolea.GuantesMaterial;
        DetectorManoEnPolea.LeftHandMesh.SetActive(false);
        DetectorManoEnPolea.LeftHandMeshRenderer.enabled = true;
        DetectorManoEnPolea.IsHandOnPolea = false;
        DetectorManoEnPolea.ManoIzq_ReferenciaMalPuesta.SetActive(false);
    }

    private IEnumerator LiberarManoYPistolaNeumaticaConDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        DetectorManoEnPoleaBienPuesta.RealLeftHandSkinnedMeshRenderer.material = DetectorManoEnPolea.GuantesMaterial;
        DetectorManoEnPoleaBienPuesta.LeftHandMesh.SetActive(false);
        DetectorManoEnPoleaBienPuesta.LeftHandMeshRenderer.enabled = true;
        DetectorManoEnPoleaBienPuesta.IsHandOnPolea = false;
        DetectorManoEnPoleaBienPuesta.ManoIzq_ReferenciaMalPuesta.SetActive(false);
        PistolaNeumaticaEnCajaReferencia.SetActive(true);
        PistolaNeumaticaEnPoleaSegundoIntentoGameObject.SetActive(false);
        PistolaNeumaticaEnPoleaDejarEnCaja.SetActive(true);
        Cuerda_Render.posiciones[1] = PistolaNeumtaticaEnPoleaDejarEnCajaPuntoCab;
    }

    private IEnumerator DamageRoutine()
    {
        DamageRenderer.gameObject.SetActive(true);
        Color color = _damageMaterial.color;

        aSource.goFx("HuesoRoto");
        float timeTimer = 0f;
        while (timeTimer < DamageFadeInTime)
        {
            timeTimer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timeTimer / DamageFadeInTime);
            _damageMaterial.color = color;
            yield return null;
        }

        aSource.goFx("GritoDeDolor");
        timeTimer = 0f;
        while (timeTimer < DamageFadeOutTime)
        {
            timeTimer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timeTimer / DamageFadeOutTime);
            _damageMaterial.color = color;
            yield return null;
        }

        color.a = 0f;
        _damageMaterial.color = color;
        DamageRenderer.gameObject.SetActive(false);
        AudioManager.aSource.PlayVoz("DialogoAccidente");
    }

    private IEnumerator AparecerBotonContinuarDeConlusiones()
    {
        AudioManager.aSource.PlayVoz("DialogoConclusiones");
        while (AudioManager.aSource.IsPlayingVoz() == true)
        {
            yield return new WaitForFixedUpdate();
        }
        BotonContinuarConclusiones.SetActive(true);
    }
}