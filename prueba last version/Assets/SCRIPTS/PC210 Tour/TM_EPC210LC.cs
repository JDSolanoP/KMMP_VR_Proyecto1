using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TM_EPC210LC : Lista_Tareas_Controller
{
    [Header("PROPIAS DEL MODULO")]
    public GameObject usuario;
    public Transform[] pos;
    public GameObject[] muro; 
    public GameObject[] BtnContinue;
    public int actualPos = 0;
    public int aux;
    public GameObject[] Epps;

    [SerializeField] private GameObject[] Flechas;
    [SerializeField] private GameObject[] Referencias;

    [Header("Motor")]
    [SerializeField] public GameObject numerosDePanel;
    [SerializeField] public GameObject BotonContinuarDelMotor;

    private int _parteActualDelModulo = -1;

    private int totalEpps = 0;

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
        {
            case 0:////ARO_07

                for (int i = 0; i < Tablero_Indicaciones.Length; i++)
                {
                    Tablero_Indicaciones[i].SetActive(false);
                }
                /*for (int i = 0; i < aros_indicadores.Length; i++)
                {
                    Flechas[i].SetActive(false);
                    aros_indicadores[i].SetActive(false);
                }*/
                for (int i = 0; i < BtnContinue.Length; i++)
                {
                    BtnContinue[i].SetActive(false);
                }

                for (int i = 0; i < Flechas.Length; i++) Flechas[i].SetActive(false);
                for (int i = 0; i < Referencias.Length; i++) Referencias[i].SetActive(false);
                numerosDePanel.SetActive(false);
                BotonContinuarDelMotor.SetActive(false);

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
                break;
            case 1:
                Tablero_Indicaciones[1].SetActive(true);
                break;
            case 2:
                Debug.Log("Tarea 2, entrando al taller");
                TeleportToZone(0);
                Tablero_Indicaciones[1].SetActive(false);
                Tablero_Indicaciones[2].SetActive(true);
                break;
        }
    }
    public void activacionXR(int contacto) //contacto con manos
    {
        switch (contacto)
        {
            case 0://boton continue a siguiente area
                
                StartCoroutine(Transporte());
                break;
            case 1:
                Tablero_Indicaciones[0].SetActive(false);//intro
                muro[0].SetActive(false);
                TareaCompletada(0);
                //locucion
                break;
            case 2:
                BtnContinue[0].SetActive(false);
                TareaCompletada(1);
                break;
        }
    }
    public IEnumerator Transporte()
    {
        StartCoroutine(FadeOutIn(1, 1, 1));
        yield return new WaitForSecondsRealtime(1f);
        
        muro[seleccionMuro()].SetActive(false);
        Debug.Log(seleccionMuro()+"=nMuro - pos ="+ actualPos);
        muro[actualPos+1].SetActive(true);
        usuario.transform.position = pos[actualPos].position;
        actualPos++;
    }

    private void TeleportToZone(int zoneNumber)
    {
        usuario.transform.position = pos[0].position;
    }
    public int seleccionMuro()
    {
        int nMuro = 0;
        switch (actualPos)
        {
            case 1://epps a maquina
                nMuro = 0;
                break;
            case 3://horometro1
                nMuro = 1;
                break;
        }
        return nMuro;
    }

    public void EppPuesto(int numeroEpp)
    {
        if (numeroEpp == 0)
        {
            Debug.Log("Cambiar textura guante");
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
                if (totalEpps == 5)
                {
                    Debug.Log("Se agararon todos los EPPs");
                    BtnContinue[0].SetActive(true);
                }
                break;
        }
    }

    public void EstablecerSiguienteParteDelModulo(int siguienteParteDelModulo)
    {
        _parteActualDelModulo = siguienteParteDelModulo;
        ActualizarParteActual();
    }

    public void ActualizarParteActual()
    {
        for (int i = 0; i < Flechas.Length; i++) Flechas[i].SetActive(false);

        if (_parteActualDelModulo == 0) //Panel 1.1
        {

        }
        if (_parteActualDelModulo == 1) //Panel 1.2
        {
            Tablero_Indicaciones[4].SetActive(false);
            ActivarGrupoDeFlechas(0);
        }
        if (_parteActualDelModulo == 2)
        {
            ActivarGrupoDeFlechas(1);
        }
        if (_parteActualDelModulo == 3)
        {

        }
        if (_parteActualDelModulo == 4)
        {
            ActivarGrupoDeFlechas(4);
        }
        if (_parteActualDelModulo == 5)
        {
            ActivarGrupoDeFlechas(5);
        }
        if (_parteActualDelModulo == 6)
        {
            ActivarGrupoDeFlechas(6);
        }
        if (_parteActualDelModulo == 7)
        {
            ActivarGrupoDeFlechas(7);
        }
        if (_parteActualDelModulo == 8)
        {
            ActivarGrupoDeFlechas(8);
        }
        if (_parteActualDelModulo == 9)
        {
            ActivarGrupoDeFlechas(9);
        }
        if (_parteActualDelModulo == 10)
        {
            ActivarGrupoDeFlechas(10);
        }
        if (_parteActualDelModulo == 11)
        {
            ActivarGrupoDeFlechas(11);
        }
        if (_parteActualDelModulo == 12)
        {

        }
        if (_parteActualDelModulo == 13)
        {
            ActivarGrupoDeFlechas(13);
        }
        if (_parteActualDelModulo == 14)
        {

        }
        if (_parteActualDelModulo == 15)
        {

        }
        if (_parteActualDelModulo == 16)
        {

        }
    }
    public void PasarAlSiguienteTablero(int TableroPorActivar)
    {
        Tablero_Indicaciones[TableroPorActivar - 1].SetActive(false);
        Tablero_Indicaciones[TableroPorActivar].SetActive(true);
    }

    public void DesactivarTablero(int TableroPorDesactivar)
    {
        Tablero_Indicaciones[TableroPorDesactivar].SetActive(false);
    }

    public void ActivarTablero(int TableroPorActivar)
    {
        Tablero_Indicaciones[TableroPorActivar].SetActive(true);
        if (_parteActualDelModulo != 1)
        {
            Tablero_Indicaciones[3].SetActive(false);
        }
    }

    public void ActivarGrupoDeFlechas(int GrupoDeFlechasPorActivar)
    {
        Flechas[GrupoDeFlechasPorActivar].SetActive(true);
        if (_parteActualDelModulo != 2)
        {
            DesactivarGrupoDeFlechas(3);
        }
        if (_parteActualDelModulo != 11)
        {
            DesactivarGrupoDeFlechas(12);
        }
    }

    public void DesactivarGrupoDeFlechas(int GrupoDeFlechasPorDesactivar)
    {
        Flechas[GrupoDeFlechasPorDesactivar].SetActive(false);
    }

    public void ActivarReferencia(int ReferenciaPorActivar)
    {
        Referencias[ReferenciaPorActivar].SetActive(true);
    }

    public void DesactivarReferencia(int ReferenciaPorDesactivar)
    {
        Referencias[ReferenciaPorDesactivar].SetActive(false);
    }
}
