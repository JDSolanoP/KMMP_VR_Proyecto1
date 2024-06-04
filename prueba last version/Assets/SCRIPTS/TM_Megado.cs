using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class TM_Megado : Lista_Tareas_Controller
{
    // Start is called before the first frame update
    public GameObject[] Tablero_Indicaciones;
    public GameObject[] URefe;//Puntos que indica ubicacion de Referencia
    public GameObject[] ObjRefe;//Indican Objetos que interviene en la tarea de pinzas y/o empiezan o culminan una tarea
    public GameObject[] ObjRefeBotones;//Indican Objetos que interviene en la tarea donde intervienen botones  de Megometro y/o empiezan o culminan una tarea
    public GameObject[] ObjRefePantalla;//Indica objetos de la pantalla que intervienen en las tareas;
    public Transform[] dialPosRefe;//posiciones de referencia para el dial
    public GameObject CanvasPantallaMegometro;//todo el canvas que muestra los objetos en la pantalla
    public GameObject[] pantallaUIMegometro;//Elementos de la pantalla
    public GameObject[] manosXR;//agregos viernes 31-05-24
    public GameObject[] guantesComplementos;
    public Material[] manosXRMaterial;//agregos viernes 31-05-24
    [Header ("Izq=0, Der=1")]
    public GameObject[] Indices_Mano;//indica los detectores de dedos indices de cada mano para presionar los botones
    public bool[] CheckListaTarea;
    public GameObject[] BarraUIMedicion;
    public int BarraUI_contador=0;//contador que indica hasta donde se marcara dependiendo del ejercicio
    public int intentos, MaxIntentos;
    public bool contactoPinza =false;
    public bool SoltarObj=false;
    public bool ContactoManoDer;
    public bool ContactoManoIzq;
    public bool objPantalla0=false;
    public bool objPantalla1=false;
    public bool objPantalla2 = false;
    public int Dialpos = 0;
    public int TiempoPress = 0;
    public int contadorTiemposPosibles = 4;
    public bool manoContacto=false;
    public bool voltajeCorrecto=false;
    public bool tiempoCorrecto = false;
    public int tiempoMedicion = 0;
    public float tiempoContador=0;
    public bool finDeMedicion=false;
    public bool[] si_realizar_accion;
    public TMP_Text tmp_voltaje;
    public TMP_Text tmp_voltajeSecun;
    public TMP_Text tmp_tiempo;
    public TMP_Text tmp_Resistencia;


    public override void Start()
    {
        base.Start();
        StartCoroutine(ListaTareas(TareaActual));
        //ObjRefeBotones[2].transform.localEulerAngles = Vector3.zero;

    }

    public override void TareaCompletada(int TareaSiguiente)
    {
        base.TareaCompletada(TareaSiguiente);
        StartCoroutine(ListaTareas(TareaActual));
    }

    
    // Corrutina que activa los elemento a utilizar en cada una de las tareas por realizar.
    
    IEnumerator ListaTareas(int tarea)
    {
        switch (tarea)
        {//Agregar notaciones de tareas en cada caso
            case 0:// AQUI
                //Tarea inicial, se ejecuta por defecto al iniciar el proyecto
                //Codigo de referencia : yield return new WaitForSeconds(2f);
                Tablero_Indicaciones[0].SetActive(false);//tablero0:bienvenida colocar guantes
                yield return new WaitForSeconds(0.5f);
                Tablero_Indicaciones[0].SetActive(true);//tablero0:bienvenida colocar guantes
                guantesComplementos[0].SetActive(false);
                guantesComplementos[1].SetActive(false);
                while (audioManager.aSource.isPlaying == true)
                {
                    //Debug.Log("Se esta reproduciendo audio");
                    yield return new WaitForFixedUpdate();

                }
                //ORefe[0].SetActive(true);
                break;
                case 1://colocar caBLE ROJO
                manosXR[0].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[0];
                manosXR[1].GetComponent<SkinnedMeshRenderer>().sharedMaterial = manosXRMaterial[0];
                guantesComplementos[0].SetActive(true);
                guantesComplementos[1].SetActive(true);
                //manosXR[0].gameObject.GetComponent<Material>().SetColor("_TintColor", Color.black);
                //manosXR[1].gameObject.GetComponent<Material>().color = Color.black;
                ObjRefe[0].SetActive(false);//DESACTIVAR GUANTES
                Tablero_Indicaciones[0].SetActive(false);// APAGAR TABLERO 0
                yield return new WaitForSeconds(1f);
                Tablero_Indicaciones[1].SetActive(true);//TABLERO 1 PRENDER
                ObjRefe[1].SetActive(true);//ACTIVAR PINZA ROJA REFERENCIAS
                while (audioManager.aSource.isPlaying == true)
                {
                    //Debug.Log("Se esta reproduciendo audio");
                    yield return new WaitForFixedUpdate();
                }

                break;
            case 2://colocar caBLE negro
                ObjRefe[1].SetActive(false);//DESACTIVAR PINZA ROJA REFE
                Tablero_Indicaciones[1].SetActive(false);//APAGAR TABLERO1
                yield return new WaitForSeconds(1f);
                Tablero_Indicaciones[2].SetActive(true);//PRENDER TABLERO2
                ObjRefe[6].SetActive(true);//PRENDER PPINZA NEGRA REFE
                while (audioManager.aSource.isPlaying == true)
                {
                    //Debug.Log("Se esta reproduciendo audio");
                    yield return new WaitForFixedUpdate();
                }
                break;
            case 3://Encender MEgometro
                ObjRefe[6].SetActive(false);//APAGAR PINZA NEGRA DE REFERENCIA
                Tablero_Indicaciones[2].SetActive(false);//DESACTIVAR TABLERO2
                yield return new WaitForSeconds(1f);
                ObjRefeBotones[0].SetActive(true);//ACTIVAR BOTON DE REFERENCIA DE ENCENDIDO
                Tablero_Indicaciones[3].SetActive(true);//ACTIVAR TABLERO3

                //Tablero_Indicaciones[2].SetActive(true);
                //ObjRefe[2].SetActive(true);
                while (audioManager.aSource.isPlaying == true)
                {
                    //Debug.Log("Se esta reproduciendo audio");
                    yield return new WaitForFixedUpdate();
                }
                break;
            case 4://Esperar encendidio y Calibrar voltaje y tiempo
                activarBTN(0);
                ObjRefeBotones[0].SetActive(false);//DESACTIVAR BOTON DE REFERENCIA DE ENCENDIDO
                Tablero_Indicaciones[3].SetActive(false);//DESACTIVAR TABLERO03
                yield return new WaitForSeconds(1f);
                Tablero_Indicaciones[4].SetActive(true);//ACTIVAR TABLERO04
                yield return new WaitForSeconds(1f);
                pantallaUIMegometro[8].SetActive(false);
                yield return new WaitForSeconds(4f);
                pantallaUIMegometro[8].SetActive(false);
                ObjRefeBotones[1].SetActive(true);//ACTIVAR DIAL DE REFERENCIA
                yield return new WaitForSeconds(4f);
                ObjRefeBotones[3].SetActive(true);//ACTIVAR de tiempo DE REFERENCIA
                
                yield return new WaitForSeconds(6);

                //Debug.Log("activar OBJ boton STart");
                ObjRefeBotones[4].SetActive(true);//ACTIVAR de Start de referencia
                while (audioManager.aSource.isPlaying == true)
                {
                    //Debug.Log("Se esta reproduciendo audio");
                    yield return new WaitForFixedUpdate();
                }
                break;
            case 5://Esperando a que termine la medicion
                ObjRefeBotones[0].SetActive(false);//Apagar Boton de referencia de encendido
                Tablero_Indicaciones[4].SetActive(false);//apagar panel 02
                ObjRefeBotones[1].SetActive(false);//encender dial de referencia;Movera el ObjRefeBotones[2], no debe apagarse
                ObjRefeBotones[3].SetActive(false);//encnder dial de referencia;Movera el ObjRefeBotones[2], no debe apagarse
                ObjRefeBotones[4].SetActive(false);//encender dial de referencia;Movera el ObjRefeBotones[2], no debe apagarse
                yield return new WaitForSeconds(1f);
                Tablero_Indicaciones[5].SetActive(true);//apagar panel 02
                while (audioManager.aSource.isPlaying == true)
                {
                    //Debug.Log("Se esta reproduciendo audio");
                    yield return new WaitForFixedUpdate();
                }
                //TareaCompletada(5);
                break;
            case 6://Se uso boton Start y se mostrar� resulta de victoria o derrota
                //ObjRefeBotones[0].SetActive(false);//Apagar Boton de referencia de encendido
                //Tablero_Indicaciones[4].SetActive(false);//apagar panel 02
                //ObjRefeBotones[1].SetActive(false);//encender dial de referencia;Movera el ObjRefeBotones[2], no debe apagarse
                //ObjRefeBotones[3].SetActive(false);//encender dial de referencia;Movera el ObjRefeBotones[2], no debe apagarse
                //ObjRefeBotones[4].SetActive(false);//encender dial de referencia;Movera el ObjRefeBotones[2], no debe apagarse
                pantallaUIMegometro[0].SetActive(false);
                while (audioManager.aSource.isPlaying == true)
                {
                    //Debug.Log("Se esta reproduciendo audio");
                    yield return new WaitForFixedUpdate();
                }
                //TareaCompletada(5);
                break;
                
            /*case 6://Usar boton tiempo para calibrar tiempo
                ObjRefeBotones[1].SetActive(false);//Apagar dial de encendido
                Tablero_Indicaciones[5].SetActive(false);//apagar panel 05
                ObjRefeBotones[3].SetActive(true);//encender boton de referencia de tiempo;
                Tablero_Indicaciones[6].SetActive(true);
                //Tablero_Indicaciones[2].SetActive(true);
                //ObjRefe[2].SetActive(true);
                while (audioManager.aSource.isPlaying == true)
                {
                    //Debug.Log("Se esta reproduciendo audio");
                    yield return new WaitForFixedUpdate();
                }
                break;
            case 7://Usar boton tiempo para calibrar tiempo
                ObjRefeBotones[2].SetActive(false);//Apagar Boton de referencia de encendido
                Tablero_Indicaciones[2].SetActive(false);//apagar panel 02
                ObjRefeBotones[1].SetActive(true);//encender dial de referencia;
                Tablero_Indicaciones[3].SetActive(true);
                //Tablero_Indicaciones[2].SetActive(true);
                //ObjRefe[2].SetActive(true);
                while (audioManager.aSource.isPlaying == true)
                {
                    //Debug.Log("Se esta reproduciendo audio");
                    yield return new WaitForFixedUpdate();
                }
                break;*/
        }
    }
    //*********************** TAREA 1 y TAREA 2*****************
    //************VERIFICACION DE PINZAS************************
    public void SoltarPinza(int tarea)
    {
        switch (tarea)
        {
            case 1:
                congelarObj(2);//PINZA ROJA GRAB
                ObjRefe[2].SetActive(false);
                ObjRefe[3].SetActive(false);
                ObjRefe[4].SetActive(true);
                ObjRefe[5].SetActive(true);
                //ObjRefe[6].SetActive(true);
                TareaCompletada(tarea);
                //TareaCompletada(1);
                break;
            case 2:
                congelarObj(7);// PINZA NEGRA GRAB
                ObjRefe[7].SetActive(false);
                ObjRefe[8].SetActive(false);
                ObjRefe[9].SetActive(true);
                ObjRefe[10].SetActive(true);
                TareaCompletada(tarea);
                break;
        }
    }
    public void congelarObj(int nObjClave)
    {
        ObjRefe[nObjClave].GetComponent<Rigidbody>().isKinematic = true;
        ObjRefe[nObjClave].transform.position = ObjRefe[1].transform.position;
        ObjRefe[nObjClave].transform.localEulerAngles = ObjRefe[1].transform.localEulerAngles;
        ObjRefe[nObjClave].GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeRotationZ;
        ObjRefe[nObjClave].GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeRotationY;
        ObjRefe[nObjClave].GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeRotationX;
        ObjRefe[nObjClave].GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;
        ObjRefe[nObjClave].GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
        ObjRefe[nObjClave].GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionX;
        //Debug.Log("congelando obj en pos indicada");
    }
    public void verificarPosPinza(int tarea)
    {
        if (contactoPinza == true && SoltarObj==true)
        {
            //Debug.Log("verificando obj para completar tarea");
            SoltarPinza(tarea);
            contactoPinza = false;
            SoltarObj = false;
        }
    }
    public void SoltarObjClave() 
    {
        //Debug.Log("soltando obj");
        SoltarObj = true;
    }
    public void AgarraObjClave()
    {
        //Debug.Log("cogiendo obj");
        SoltarObj = false;
    }
    //***************FIN DE PROCEDIMIENTOS *************************
    //****************Boton inicio y tintineo********************
    public void activarBTN(int nboton)
    {
        //ObjRefeBotones[tarea].SetActive(false);
        switch (nboton)
        {
            case 0:
                //tintineo(0);
                //TareaCompletada(3);
                StartCoroutine(Tintineo(0));

                ObjRefePantalla[0].SetActive(true);
                for(int i = 0; i < pantallaUIMegometro.Length; i++)
                {
                    pantallaUIMegometro[i].SetActive(false);
                }
                pantallaUIMegometro[2].SetActive(false);
                ObjRefePantalla[1].SetActive(true);
                pantallaUIMegometro[3].SetActive(true);
                pantallaUIMegometro[4].SetActive(true);
                pantallaUIMegometro[5].SetActive(true);
                pantallaUIMegometro[6].SetActive(true);
                pantallaUIMegometro[7].SetActive(true);
                pantallaUIMegometro[8].SetActive(true);
                break;
            case 1:
//                Debug.Log("contacto boton tiempo, pos = "+TiempoPress);
                TiempoPress++;
                if (TiempoPress > contadorTiemposPosibles)
                {
                    TiempoPress = 0;
                }
                colocarValores(1);
                //TiempoPress++;
                break;
            case 2:
                TareaCompletada(4);
                ObjRefeBotones[4].SetActive(false);
                StartCoroutine(animacionCalibracion());
                break;

        }
        //TareaCompletada(tarea);
        
    }
    public void tintineo(int objeto)
    {
        ObjRefePantalla[objeto].SetActive(true);
    }
    public IEnumerator Tintineo(int objeto)//tintineo de obj para el TMP en la pantalla del megometro-Calibracion
    {
        ObjRefePantalla[objeto].SetActive(true);
        yield return new WaitForSeconds(0.25f);
        ObjRefePantalla[objeto].SetActive(false);
        yield return new WaitForSeconds(0.25f);
        ObjRefePantalla[objeto].SetActive(true);
        yield return new WaitForSeconds(0.25f);
        ObjRefePantalla[objeto].SetActive(true);
        yield return new WaitForSeconds(0.25f);
        ObjRefePantalla[objeto].SetActive(false);
        yield return new WaitForSeconds(0.25f);
        ObjRefePantalla[objeto].SetActive(true);
        yield return new WaitForSeconds(0.25f);
        ObjRefePantalla[objeto].SetActive(false);
        yield return new WaitForSeconds(0.25f);
        ObjRefePantalla[objeto].SetActive(true);
        yield return new WaitForSeconds(0.25f);
        ObjRefePantalla[objeto].SetActive(true);
        yield return new WaitForSeconds(0.25f);
        ObjRefePantalla[objeto].SetActive(false);
        yield return new WaitForSeconds(0.25f);
        ObjRefePantalla[objeto].SetActive(true);
    }
    //****************************Fin de procedimiento de Tintineo*************
    public void giroDial()
    {
        if (manoContacto == true)
        {
            Dialpos++;
            if (Dialpos >= dialPosRefe.Length)
            {
                Dialpos = 0;
            }
            ObjRefeBotones[2].transform.localEulerAngles = new Vector3(0, 0, dialPosRefe[Dialpos].localEulerAngles.z);
            colocarValores(0);
            //Debug.Log("Girando dial a pos " + Dialpos);
        }
        
    }
    public void colocarValores(int objPantalla)
    {
        switch (objPantalla)
        {
            case 0:
                switch (Dialpos)
                {
                    case 0:
                        tmp_voltaje.text = "00.0 V";
                        voltajeCorrecto = false;
                        break;
                    case 1:
                        tmp_voltaje.text = "200.0 mV";
                        break;
                    case 2:
                        tmp_voltaje.text = "2.0 V";
                        break;
                    case 3:
                        tmp_voltaje.text = "20.0 V";
                        break;
                    case 4:
                        tmp_voltaje.text = "200.0 V";
                        break;
                    case 5://Voltaje correcto
                        tmp_voltaje.text = "500.0 V";
                        voltajeCorrecto = true;
                        break;
                }
                StartCoroutine(Tintineo(2));
                break;
            case 1:
                switch (TiempoPress)
                {
                    case 0:
                        tmp_tiempo.text = "00:00";
                        tiempoCorrecto = false;
                        tiempoMedicion = 0;
                        break;
                    case 1:
                        tmp_tiempo.text = "00:30";
                        tiempoCorrecto = false;
                        tiempoMedicion = 30;
                        break;
                    case 2:
                        tmp_tiempo.text = "01:00";
                        tiempoCorrecto = true;
                        tiempoMedicion = 60;
                        break;
                    case 3:
                        tmp_tiempo.text = "05:00";
                        tiempoCorrecto = false;
                        tiempoMedicion = 300;
                        break;
                    case 4:
                        tmp_tiempo.text = "10:00";
                        tiempoMedicion = 600;
                        tiempoCorrecto = false;
                        break;
                }
                StartCoroutine(Tintineo(3));
                break;
        }
    }

    public IEnumerator animacionCalibracion()
    {
        int pantallaMostrar;
        pantallaUIMegometro[8].SetActive(true);
        Debug.Log("iniciando animacion de calibracion");
        ObjRefeBotones[4].SetActive(false);
        StartCoroutine(llamado_tiempo(tiempoMedicion));
        StartCoroutine(tintineoPorBool(4));
        StartCoroutine(tintineoPorBool(5));
        pantallaUIMegometro[0].SetActive(true);
        pantallaUIMegometro[8].SetActive(true);
        for (int i = 0; i < BarraUIMedicion.Length; i++)//desactivar barra de medicion
        {
            BarraUIMedicion[i].SetActive(false);
        }
        tmp_voltajeSecun.text = tmp_voltaje.text;
        if (voltajeCorrecto == true && tiempoCorrecto == true)
        {
            BarraUI_contador = 43;
            Debug.Log("verificando calibracion correcta");
            pantallaMostrar = 6;
            
            //Tablero_Indicaciones[6].SetActive(true);
        }
        else
        {
            BarraUI_contador = 5;
            pantallaMostrar = 7;
            //mostrarBarraUI(5,false);
            Debug.Log("verificando calibracion incorrecta");
            tmp_Resistencia.text = "< 1";
            
        }
        StartCoroutine(mostrarBarraUI());
        if (tiempoCorrecto == false)
        {
            yield return new WaitForSeconds(30);
        }
        else
        {
            yield return new WaitForSeconds(58);
        }
        Tablero_Indicaciones[5].SetActive(false);
        yield return new WaitForSeconds(1);
        Tablero_Indicaciones[pantallaMostrar].SetActive(true);
        yield return new WaitForSeconds(2);
        
        //StartCoroutine(Tintineo(4));//prueba1 on
        //StartCoroutine(Tintineo(5));
        //yield return new WaitForSeconds(2);
    }
    public IEnumerator mostrarBarraUI()
    {

        for (int i = 0; i <= BarraUI_contador; i++)
        {
            Debug.Log("barra activada : "+i);
            BarraUIMedicion[i].SetActive(true);
            switch (i)
            {
                case 0:
                    tmp_Resistencia.text = "< 1";
                    break;
                case 6:
                    tmp_Resistencia.text = "100 k";
                    break;
                case 13:
                    tmp_Resistencia.text = "1 M";
                    yield return new WaitForSeconds(12f);
                    break;
                case 20:
                    tmp_Resistencia.text = "10 M";
                    break;
                case 27:
                    tmp_Resistencia.text = "100 M";
                    yield return new WaitForSeconds(12f);
                    break;
                case 34:
                    tmp_Resistencia.text = "1 G";
                    break;
                case 41:
                    tmp_Resistencia.text = "10 G";
                    yield return new WaitForSeconds(12f);
                    break;
                case 42:
                    tmp_Resistencia.text = "11 G";
                    yield return new WaitForSeconds(0.4f);
                    break;
                case 43:
                    tmp_Resistencia.text = "12 G";
                    yield return new WaitForSeconds(1f);//tiempo extra de tintineo al final de la medicion
                    
                    break;
            }
            //Invoke("MostradorBarraUI", 0.5f);//Verificar invoke
            //BarraUI_contador++;
            yield return new WaitForSeconds(0.4f);
        }
        
        if (BarraUI_contador == 43)
        {
            StartCoroutine(tintineoPorBool(6));
        }
        if (BarraUI_contador == 5)
        {
            StartCoroutine(Tintineo(7));
        }
        
    }
    public IEnumerator llamado_tiempo(float tiempo)
    {
        float mod = 0;
         float auxmin = 0;
        //tiempoContador += Time.deltaTime;
        //tiempo = Time.realtimeSinceStartup;
        for (int i = 0; i < tiempo; i++) 
        {

            if (tiempo > 60)
            {
                //Tablero_Indicaciones[5].SetActive(false);//apagar panel 02
                TareaCompletada(TareaActual);
            }
            if (i > 60)
            {
                auxmin = i;
                mod = i % 60;
                auxmin = (auxmin-mod)/60;
                tiempoContador = mod;
            }
            //string fTime=System.TimeSpan.FromSeconds(tiempoContador).ToString("mm\\:ss");
            tmp_tiempo.text = auxmin.ToString("00:")+tiempoContador.ToString("00");
            tiempoContador++;
            yield return new WaitForSecondsRealtime(1);
        }
        //Tablero_Indicaciones[5].SetActive(false);//apagar panel 02
        if (tiempo == 30)
        {
            tmp_tiempo.text = "00:30";
            TareaCompletada(TareaActual);
            Tablero_Indicaciones[5].SetActive(false);//apagar panel 02
        }
        else
        {
            tmp_tiempo.text = (tiempo / 60).ToString("00") + ":00";
            Tablero_Indicaciones[5].SetActive(false);//apagar panel 02
            TareaCompletada(TareaActual);

        }
        finDeMedicion = true;
    }
    public IEnumerator tintineoPorBool(int objeto)
    {
        while (finDeMedicion == false)
        {
            Debug.Log("Tintieno de medicion");
            
            yield return new WaitForSeconds(0.25f);
            ObjRefePantalla[objeto].SetActive(false);
            yield return new WaitForSeconds(0.25f);
            ObjRefePantalla[objeto].SetActive(true);
        }
    }
}
/*public void mostrarBarraUI(int BarraIUcontadorfin, bool valor_Correcto)
{
    for(int i=0; i<=BarraIUcontadorfin;i++)
    {
        //Debug.Log("barra activada : "+i);
        //BarraUIMedicion[BarraUI_contador].SetActive(true);
        Invoke("MostradorBarraUI", 0.5f);//Verificar invoke
        BarraUI_contador++;

    }
    if (BarraIUcontadorfin == BarraUI_contador && valor_Correcto == true)
    {
        StartCoroutine(Tintineo(6));
    }
    if (BarraIUcontadorfin == BarraUI_contador && valor_Correcto == false)
    {
        StartCoroutine(Tintineo(7));
    }
}*/
/*public void MostradorBarraUI()
{
    Debug.Log("barra activada : "+BarraUI_contador);
    BarraUIMedicion[BarraUI_contador].SetActive(true);
    //yield return new WaitForSeconds(0.2f);
}*/


/*public void accionConfirmadaPorContactoMano(int tarea)
{
    if (manoContacto == true)
    {
        switch (tarea)
        {
            case 0:
                Debug.Log("contacto boton tiempo");
                if (TiempoPress > contadorTiemposPosibles)
                {
                    TiempoPress = 0;
                }
                colocarValores(1);
                TiempoPress++;
                break;
                case 1:
                ObjRefeBotones[4].SetActive(false);
                //StartCoroutine(animacionCalibracion());

                break;
        }
    }
}*/