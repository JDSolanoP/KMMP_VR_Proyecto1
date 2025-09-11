using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
//using System.Security.Policy;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
public class TM_Lobby : MonoBehaviour
{/*Creado por Julio Solano:03-02-25*/
    public static TM_Lobby lb;

    public Lista_Tareas_Controller ltc;
    public bool SoloTiempoPrevio;//solo para ganar tiempo antes de empezar tareas
    public bool ParaPC=false;
    public string NombreProyecto;
    public bool[] contacto_confirmado;
    public GameObject[] objs;
    public int auxcontacto;
    public int nCifras;
    public int limiteCifras;
    public string CodigoAcceso;
    public bool si_inicioModulo=false;
    //**********************GUI**************************//
    [Header("*******GUI**********")]
    public GameObject[] btnPanel;
    public GameObject[] GUI_Panel;
    public string[] n;//captura auxliar de cifras
    public int nN;
    public TMP_Text Panel_Txt;
    public TMP_Text Frase_Txt;
    public GameObject[] img_bg_nota;
    public TMP_Text[] datosU_panel;
    public int contadorMostrarUsuario=0;//sirve para llevar la relacion de total de usuarios
    public Transform PosFinalPanelNotas;
    //Notas
    public TMP_Text[] Notas_Txt;
    //**********************FIN DE GUI**************************//
    //*********************DATOS DE USUARIO AUXILIAR*******************//
    public string auxDni;
    public string auxNombre;
    public float[] auxNotas;
    public bool si_supervisor;
    //*************************DATOS DE MODULO****************//
    public string ActualUsuario;//numero de usuarioa en la lista
    public string auxUsuario;//si supervisor esta logeado, sirve para ver otros usuarios
    public int nTareas;//numero de tareas;
    public float[] nota;//captura la tareas por modulo
    public bool si_binario;//verifica si es el sistema de calificacion es solo bien o mal;o es por numero
    public float notaMinAprobatoria;//Nota minima aprobatoria
    public float notaMaxAprobatoria;//Nota maxima aprobatoria
    //***************FIN DE DATOS DE MODULO****************************//

    //************DATOS TOTALES DE TODOS LOS USUARIOS-*************************
    public bool Ya_Existen_Datos=false;//verifica si ya se tiene datos
    public DatosTotales DTs;//****************************************Lista A GUARDAR
    public DatosUsuarios auxDU;//*************************************USUARIO AUXILIAR PARA ACTUALIZAR 
    
    public DatosUsuarios admin;
    public int nUsuariosTotal;//numero de usuarios registrados
    public List<string> listaDni;//nombres de ususarios segun dni
    public List <string> listaSupervisores;//Lista de supervires
    public List<DatosUsuarios> DU = null;//***********************************LISTA DE USUARIOS
    public DateTime DTAnteriorSesion;//para reporte
    public DateTime DTUltimaSesion;//para reporte
    public DateTime DTInicioSesion;//para reporte
    public string DTUsuarioAnterior;//ultimo usuario en usar el modulo-ligado a ultimo usuario
    public string DTsupervisorAnterior;//ultimo supervisor en logearse
    public string infoTotalExport;//cadena -> para exportar info total
    //***************FIN DE DATOS DE USUARIOS*********************************



    private void Awake()
    {
        
            if (lb != null && lb != this)
            {
                Destroy(this);
            }
            else
            {
                lb = this;
                DontDestroyOnLoad(this);
            }
        if (SoloTiempoPrevio == false)
        {
            //CargarDatos();
            if (File.Exists(Application.persistentDataPath + "/VR_" + NombreProyecto + "_Usuarios.txt"))
            {
                CargarDatos(this);//************************************cargar  datos desde la lista
                if (DTs.DUs != null && DTs.DUs.Count != 0)
                {
                    Ya_Existen_Datos = true;
                    DU = DTs.DUs;
                }
            }
            else
            {
                Debug.Log("NO HAY DATOS, agregado admin");
            }
            admin = new DatosUsuarios(admin.DNIs, admin.nombres, admin.si_Supervisor, nTareas);
            DU.Add(admin);
            //SesionAnterior = UltimaSesion.ToString("dd-MM-yyyy  HH:mm");
        }

    }
    private void Start()
    {
        GUI_Panel[0].SetActive(true);
        if (SoloTiempoPrevio == false)
        {
            StartCoroutine(Locu_Lobby());
            for (int i = 0; i < btnPanel.Length; i++)
            {
                btnPanel[i].SetActive(false);
            }

            GUI_Panel[1].SetActive(false);//gui llogin
            GUI_Panel[2].SetActive(false);//canvas de notas
                                          //img_bg_nota.GetComponent<RawImage>().color = new Color32(73, 168, 80, 255);
            auxNotas = new float[nTareas];
            nota = new float[nTareas];
            auxDU.notas = new float[nTareas];
            if (!Ya_Existen_Datos)//****************************resgistra primer usuario como supervisor
            {
                Frase_Txt.text = "Registre identificación de Supervisor:";
                auxDU.si_Supervisor = true;
            }
        }
        else
        {
            AudioManager.aSource.goFx("Locu_Lobby", 1, false, false);//Elemento 40 en audiomanager->FX Sonidos
        }
        GameObject gtm = new GameObject();
        gtm = GameObject.Find("TareaManager");
    }
    public void Transferir_Usuarios()//*******transfiere datos desde DTs(el guardado) a DU lista auxiliar***********
    {
        foreach (DatosUsuarios du in DU)
        {
            listaDni.Add(du.DNIs);
            du.fechaUltimaSesion= du.ultimaSesion.ToString("dd-MM-yyyy HH:mm");
            du.fechaInicioSesion = du.inicioSesion.ToString("dd-MM-yyyy HH:mm");
            du.fechaAnteriorSesion = du.anteriorSesion.ToString("dd-MM-yyyy HH:mm");
            du.fechaDesarrolloSesion = du.desarrolloSesion.ToString("dd-MM-yyyy HH:mm");
            nUsuariosTotal++;
            if (du.si_Supervisor == true)
            {
                listaSupervisores.Add(du.DNIs);
            }
            Debug.Log("dni: " + du.DNIs + " -fecha: " + du.ultimaSesion.ToString("dd-MM-yyyy HH:mm")+""+du.si_Supervisor);
            // mostrarNotas(du.DNIs);
            //CreaReporte();
            int a = listaDni.Count;
            //Debug.Log("a"+a);
        }
    }
    public void actualizarUsuario(string d)
    {
        foreach (DatosUsuarios du in DU)
        {
            //Debug.Log("codigo revisado : " + code);
            if (du.DNIs == d)
            {
                du.DNIs = d;
                du.nombres= auxDU.nombres;
                for (int i = 0; i < nTareas; i++)
                {
                    du.notas[i]=auxDU.notas[i];
                }
                du.si_Supervisor = auxDU.si_Supervisor;
                break;
            }
        }
        GuardarDatos(d);
    }
    public void VerificadorDni(string d)//********AGREGA USUARIO a UNA LISTA*******03-02-25//boton de login
    {
        ActualUsuario = d;
        DTInicioSesion = DateTime.Now;//para ver en el reporte cuando se inicio
        bool primeracontada = false;//si usuario es nuevo
            foreach (DatosUsuarios du in DU)
            {
                //Debug.Log("codigo revisado : " + code);
                if (du.DNIs == d)//si se encontro 1er vez el dni
                {
                    if (primeracontada == false)
                    {
                        /*if (du.inicioSesion.ToString("dd-MM-yyyy") != du.anteriorSesion.ToString("dd-MM-yyyy"))//distinto dia de inicio
                        {
                            auxDU.anteriorSesion = du.inicioSesion;
                        Debug.Log("guardado fecha de sesion anterior - "+ du.anteriorSesion.ToString("dd-MM-yyyy"));
                        }
                        else
                        {
                            
                        }*/
                    auxDU.anteriorSesion = du.anteriorSesion;
                    auxDU.inicioSesion = du.inicioSesion;
                    auxDU.ultimaSesion = du.ultimaSesion;
                    auxDU.desarrolloSesion = du.desarrolloSesion;
                    auxDU.fechaDesarrolloSesion=du.desarrolloSesion.ToString("dd-MM-yyyy HH:mm");
                    auxDU.inicioSesion = DateTime.Now;
                    auxDU.ultimaSesion = DateTime.Now;
                    auxDU.fechaAnteriorSesion = du.anteriorSesion.ToString("dd-MM-yyyy HH:mm");
                    auxDU.fechaInicioSesion = du.inicioSesion.ToString("dd-MM-yyyy HH:mm");
                    auxDU.fechaUltimaSesion = du.ultimaSesion.ToString("dd-MM-yyyy HH:mm");
                    auxDU = du;//
                        auxDU.DNIs = du.DNIs;
                        auxDU.nombres = du.nombres;
                        for (int i = 0; i < nTareas; i++)
                        {
                            auxDU.notas[i] = du.notas[i];
                        }
                        primeracontada = true;
                        Frase_Txt.text = "Bienvenido devuelta\n" + d;
                        auxDU.si_Supervisor = du.si_Supervisor;
                    DTsupervisorAnterior = auxDU.DNIs;
                    Debug.Log(du.DNIs + " Usuario encontrado ->contada:" + primeracontada);
                        //break;
                    }
                }
            }
            if (primeracontada == false)
            {//**************************Registro nuevo usuario**********************
                auxDU.inicioSesion = DateTime.Now;
                auxDU.ultimaSesion = DateTime.Now;
            
            auxDU.DNIs = d;
                DU.Add(auxDU);//agrego usuario
                listaDni.Add(auxDU.DNIs);
                
                if (auxDU.si_Supervisor == true)
                {
                    listaSupervisores.Add(auxDU.DNIs);
                    DTsupervisorAnterior = auxDU.DNIs;
                }
                Frase_Txt.text = "Nuevo usuario\n" + d;
                Debug.Log(auxDU.DNIs + " Usuario nuevo " + primeracontada);
                nUsuariosTotal++;
            }
        //d = ActualUsuario;
        DTUsuarioAnterior = DTs.ultimoUsuario;//guarda dni de anterior usuario
        DTUltimaSesion = DTs.ultimaSesion;//guarda la fecha
        GuardarDatos(d);
        btnPanel[0].SetActive(false);
        btnPanel[1].SetActive(false);
        btnPanel[2].SetActive(true);
        btnPanel[3].SetActive(true);
        
        if (auxDU.si_Supervisor == true)
        {
            if (ParaPC == true)
            {
                btnPanel[4].SetActive(true);//exportar
                
            }
            else
            {
                
                btnPanel[4].SetActive(false);//exportar
            }
            btnPanel[12].SetActive(true);//borrar datos
            //btnPanel[4].SetActive(true);
            btnPanel[5].SetActive(true);
            btnPanel[6].SetActive(true);
            btnPanel[7].SetActive(false);
        }
    }
    public void cargarDni(string dni)
    {
        VerificadorDni(dni);
    }
    public void registrarNotasAuxDU(int nNota, float nota)//GUARDA NOTA SEGUN TAREA REALIZADA
    {
        auxDU.notas[nNota] = nota;
    }
    public void mostrarNotas(string dni)//***Muestra notas de por usuario segun dni//***********17-02-25*********
    {
        foreach (DatosUsuarios du in DU)
        {
            if (du.DNIs == dni)
            {
                for (int i = 0; i < nTareas; i++)
                {
                    Debug.Log("Usuario " + dni + " ->nota " + i + " : " + du.notas[i]);
                }
                break;
            }
        }
    }
    //**************************    INICIO DE FUNCIONES PARA EL GUARDADO DE DATOS   ************************//
    public void GuardarDatos()
    {
        Debug.Log("copiando Datos Totales");
        DU.Remove(admin);
        DTs.DUs = DU;
        
        DTs.notaMin = notaMinAprobatoria;
        DTs.notaMax = notaMaxAprobatoria;
        GestionUsuariosManager.GuardarDatosUsuarios(this);
        DU.Add(admin);
        Debug.Log("Datos Totales guardados");
    }
    public void GuardarDatos(string d)//******se usa una vez se actualice el las notas de usuario despues del ejercicio
    {
        foreach (DatosUsuarios dni in DU)
        {
            if (dni.DNIs == d)
            {
                //Nombre = null;
                dni.anteriorSesion= auxDU.anteriorSesion;
                dni.inicioSesion = auxDU.inicioSesion;
                dni.ultimaSesion = auxDU.ultimaSesion;
                dni.desarrolloSesion = auxDU.desarrolloSesion;
                dni.fechaAnteriorSesion = auxDU.fechaAnteriorSesion;
                dni.fechaInicioSesion = auxDU.fechaInicioSesion;
                dni.fechaUltimaSesion = auxDU.fechaUltimaSesion;
                dni.fechaDesarrolloSesion = auxDU.fechaDesarrolloSesion;//Agregado el 18-03-25
                for (int i = 0; i < nota.Length; i++)
                {
                    dni.notas[i] = auxDU.notas[i];
                }
                dni.si_Supervisor= auxDU.si_Supervisor;
                break;
            }
        }
        Debug.Log("copiando Datos de usuario"+d);

        GuardarDatos();
        Debug.Log("Datos de usuario"+d+" guardados");
    }
    public void CargarDatos(TM_Lobby slm)
    {
        DTs = GestionUsuariosManager.CargarDatosUsuarios(this);
        if (DU != null)
        {
            Debug.Log("Limpiando DU");
            DU.Clear();
        }
        foreach (DatosUsuarios du in DTs.DUs)
        {
            DU.Add(du);
        }
        Transferir_Usuarios();//***Desde el txt a lista auxiliar
        Debug.Log("Datos Cargados");
    }
     public void CreaReporte()//***Crea report en txt de TODOS los usuario//***********25-02-25*********
    {
        infoTotalExport = "*****Reporte de Usuarios del Módulo "
            +NombreProyecto+"*****\n***Credits***\n" +
            "->Property of CFK - KMMP<-\n" +
            "->Project Leader : Hector Herrera<-" +
            "\n->Developed by Julio Solano<-\n*************\n\n" +
            "->Reporte solicitado por el usuario : "+ActualUsuario;
        Debug.Log(ActualUsuario + " reporte");
        infoTotalExport+="\nFecha de Reporte : "+DateTime.Now+
            "\n\n***RESULTADOS***\n";
        Debug.Log("Creando Reporte");
        foreach (DatosUsuarios du in DU)
        {
            if (du.nombres != "admin")
            {
                infoTotalExport += "\nIdentificación : " + du.DNIs;
                du.fechaUltimaSesion = du.ultimaSesion.ToString("dd-MM-yyyy HH:mm");
                 if (du.si_Supervisor)
                {
                    infoTotalExport += "\nNivel de Acceso : Supervisor";
                }
                else
                {
                    infoTotalExport += "\nNivel de Acceso : Usuario";
                }
                /*infoTotalExport += "\nÚltima Fecha de Sesión : " + du.inicioSesion.ToString("dd-MM-yyyy HH:mm");
                if (du.anteriorSesion != null && du.anteriorSesion.ToString("dd-MM-yyyy HH:mm") != "01-01-0001 00:00")
                {
                    infoTotalExport += "\nAnterior Fecha de Sesión : " + du.anteriorSesion.ToString("dd-MM-yyyy HH:mm");
                }
                else
                {
                    infoTotalExport += "\nAnterior Fecha de Sesión : No accedió anteriormente";
                }
                infoTotalExport += "\nÚltima Sesión de Módulo : " + du.ultimaSesion.ToString("dd-MM-yyyy HH:mm");*/
                
                if(du.desarrolloSesion != null && du.desarrolloSesion.ToString("dd-MM-yyyy HH:mm") != "01-01-0001 00:00")
                {
                    infoTotalExport += "\nÚltima Fecha de Desarrollo de Módulo : " + du.desarrolloSesion.ToString("dd-MM-yyyy HH:mm");
                    infoTotalExport += "\nCalificaciones:";

                    //Debug.Log("dni: " + du.DNIs + " -fecha: " + du.ultimaSesion.ToString("dd-MM-yyyy  HH:mm"));
                    for (int i = 0; i < nTareas; i++)
                    {
                        if (si_binario)
                        {
                            if (du.notas[i] == 0) { infoTotalExport += "\nEjercicio " + (i + 1) + " : Aprobado"; }
                            else
                            {
                                infoTotalExport += "\nEjercicio " + (i + 1) + " : Desaprobado";
                            }
                        }
                        else
                        {
                            infoTotalExport += "\nnota " + (i + 1) + " : " + du.notas[i];
                        }
                    }
                }
                else
                {
                    infoTotalExport += "\nÚltima Fecha de Desarrollo de Módulo : No se desarrollo el módulo anteriormente";
                }
                
                infoTotalExport += "\n";
            }
        }
        //Debug.Log(infoTotalExport);
    }
    /*public void CreaReporte(string dni)//***crea reporte de usuario segun dni//***********25-02-25*********
    {
        Debug.Log("Creando Reporte de "+dni);
        foreach (DatosUsuarios du in DTs.DUs)
        {
            if (du.DNIs == dni)
            {
                du.fechaUltimaSesion = du.ultimaSesion.ToString("dd-MM-yyyy  HH:mm");
                infoTotalExport += "\nDNI : " + du.DNIs + "\nUlltima Fecha de Sesion : " + du.ultimaSesion;
                Debug.Log("dni: " + du.DNIs + " -fecha: " + du.ultimaSesion.ToString("dd-MM-yyyy  HH:mm"));
                mostrarNotas(du.DNIs);


                for (int i = 0; i < nTareas; i++)
                {
                    infoTotalExport += "\nnota " + i + " : " + du.notas[i];
                    Debug.Log("Usuario " + dni + " ->nota " + i + " : " + du.notas[i]);

                    infoTotalExport += "\n";
                }
                break;
            }
        }
    }*/
    //**************************    FIN DE FUNCIONES PARA EL GUARDADO DE DATOS   ************************//

    //**************************    INICIO DE FUNCIONES PROPIAS DEL MODULO LOBBY   ************************//
    public void AsignarAux(int aux)
    {
        auxcontacto = aux;
    }
    public void ActivarEventoXRHand(int nEvento)
    {
        switch (nEvento)
        {
            case 0://btn de panel numerico
                if (objs[0].activeInHierarchy == false)
                {
                    objs[0].SetActive(true);//evita regresar
                }
                if (auxcontacto <= 9)
                {
                    if (nCifras < limiteCifras)
                    {
                        if (nCifras == 0)
                        {
                            CodigoAcceso = "" + auxcontacto;
                            n[0] = CodigoAcceso;
                        }
                        else
                        {
                            CodigoAcceso = CodigoAcceso + auxcontacto;
                            if (nCifras < limiteCifras)
                            {
                                n[nCifras] = CodigoAcceso;
                            }
                        }
                        Panel_Txt.text = CodigoAcceso;
                        Debug.Log(CodigoAcceso + " - cifras : " + nCifras);
                        nCifras++;
                        if (nCifras == 8)
                        {
                            if (ActualUsuario == "")
                            {
                                btnPanel[1].SetActive(true);
                            }
                            else
                            {
                                btnPanel[9].SetActive(true);
                            }
                        }
                    }
                }
                if (auxcontacto == 10)
                {
                    if (nCifras > 1)
                    {
                        nCifras--;
                        CodigoAcceso = n[nCifras-1];
                    }
                    else
                    {
                        nCifras = 0;
                        CodigoAcceso = "";
                    }
                    Panel_Txt.text = CodigoAcceso;
                    Debug.Log(CodigoAcceso+" - cifras : "+nCifras);
                    if (nCifras < 8)
                    {
                        if (ActualUsuario == "")
                        {
                            btnPanel[1].SetActive(false);
                        }
                        else
                        {
                            btnPanel[9].SetActive(false);
                        }
                    }
                }
                if (auxcontacto == 11)
                {//*********btn LOGIN**************************************************LOGIN********************
                    if (nCifras == 8)
                    {
                        /*if (auxcontacto == 12)
                        {//********BTN ACTUALIZAR USUARIO
                            actualizarUsuario(CodigoAcceso);
                            Debug.Log(CodigoAcceso + " datos actualizados");
                        }*/
                        Debug.Log(CodigoAcceso + " dni de prueba mandado");
                        cargarDni(CodigoAcceso);
                        btnPanel[1].SetActive(false);
                        CodigoAcceso = "";
                        Panel_Txt.text = CodigoAcceso;
                        nCifras = 0;
                        AudioManager.aSource.goFx("Bien");
                    }
                    else
                    {
                        AudioManager.aSource.goFx("Fallo");
                    }
                }
                break;
            case 1://btn de ingreso y salir
                if (auxcontacto == 0)//17-02-25botones
                {
                    btnPanel[0].SetActive(true);
                    GUI_Panel[0].SetActive(false);
                    GUI_Panel[1].SetActive(true);
                    //img_bg_not[].GetComponent<RawImage>().color = new Color(73,168,80,255);
                }
                else
                {//salir
                    if (SoloTiempoPrevio == false)
                    {
                        if (ActualUsuario != null && ActualUsuario != "")
                        {
                            GuardarDatos(ActualUsuario);
                        }
                    }
                    Debug.Log("Se salio");
                    Application.Quit();
                }
                break;
            case 2://ver notas y iniciar
                if (auxcontacto == 0)//iniciar
                {
                    si_inicioModulo = true;
                    ltc.si_login = false;
                    if (SoloTiempoPrevio == false)
                    {
                        btnPanel[0].transform.SetParent(GUI_Panel[1].transform);
                        objs[1].SetActive(false);
                    }
                    else
                    {
                        GUI_Panel[0].SetActive(false);
                        ltc.Start();
                    }
                    
                    
                }
                else//ver notas
                {
                    mostrarGUI(auxDU);
                    if (GUI_Panel[2].activeInHierarchy == true)//PARA ABRIR Y CERRAR
                    {
                        GUI_Panel[2].SetActive(false);
                    }
                    else
                    {
                        GUI_Panel[2].SetActive(true);
                    }
                }

                break;
            case 3://botones de supervisor-superusuarios

                switch (auxcontacto)
                {
                    case 0://**exporta txt
                        
                        GuardarDatos();
                        actualizarSupervisor();
                        CargarDatos(this);
                        CreaReporte();
                        GestionUsuariosManager.exportTxt(NombreProyecto, infoTotalExport);
                        break;
                    case 1://**borra data
                        GUI_Panel[4].SetActive(false);
                        GUI_Panel[2].SetActive(false);
                        GestionUsuariosManager.BorrarDatosUsuarios(this);
                        Ya_Existen_Datos=false;
                        ltc.IrEscenaAsincron(0);
                        break;
                    case 2://**Buscar
                        GUI_Panel[4].SetActive(false);
                        Frase_Txt.text = "Ingrese identificación de usuario a Buscar:";
                        CodigoAcceso = "";
                        Panel_Txt.text = CodigoAcceso;
                        nCifras = 0;
                        GUI_Panel[2].SetActive(false);
                        GUI_Panel[3].SetActive(false);
                        btnPanel[0].SetActive(true);//panel numerico
                        btnPanel[2].SetActive(false);//iniciar
                        btnPanel[3].SetActive(false);//ver notas
                        btnPanel[8].SetActive(true);//cancelar
                        
                            btnPanel[4].SetActive(false);//exportar
                            btnPanel[12].SetActive(false);//borrar datos
                        
                        btnPanel[5].SetActive(false);//buscar
                        btnPanel[9].SetActive(false);//buscar usuario
                        btnPanel[6].SetActive(false);//mostrar todo
                        btnPanel[7].SetActive(false);//actualizar
                        
                        break;
                    case 4://**Buscar accion
                        if (buscarUsuario(CodigoAcceso) == true)
                        {
                            mostrarGUI(auxDU);
                            GUI_Panel[2].SetActive(true);
                            //if (ParaPC == true)
                                ///btnPanel[7].SetActive(true);//actualizar
                        }
                        actualizarSupervisor();
                        foreach (DatosUsuarios du in DU)
                        {
                            Debug.Log(auxUsuario+" Buscando - " + auxDU.DNIs);
                            if (du.DNIs==auxUsuario)
                            {
                                auxDU = du;
                                Debug.Log(auxUsuario + " - " + auxDU.DNIs+ " encontrado");
                                break;
                            }
                        }
                        Debug.Log(auxUsuario+" - "+auxDU.DNIs);
                        break;
                    case 3://**Cancelar
                        Frase_Txt.text = "Bienvenido :\n" + ActualUsuario;
                        btnPanel[8].SetActive(false);//btn atras
                        GuardarDatos(ActualUsuario);
                        btnPanel[0].SetActive(false);//panel numerico
                        btnPanel[2].SetActive(true);//iniciar
                        btnPanel[3].SetActive(true);//ver notas
                        btnPanel[1].SetActive(false);//login
                        btnPanel[12].SetActive(true);//borrar datos
                        if (ParaPC == true)
                        {
                            btnPanel[4].SetActive(true);//exportar
                            
                        }
                        else
                        {
                            
                            btnPanel[4].SetActive(false);//exportar
                        }
                        btnPanel[5].SetActive(true);//buscar usuario
                        btnPanel[9].SetActive(false);//accion buscar
                        btnPanel[6].SetActive(true);//mostrar todo
                        btnPanel[7].SetActive(false);//actualizar
                        GUI_Panel[2].SetActive(false);//panel total de notas canvas
                        GUI_Panel[4].SetActive(false);//panel BORRAR DATOS canvas
                        btnPanel[10].SetActive(false);//anterior
                        btnPanel[11].SetActive(false);//siguiente
                        actualizarSupervisor();
                        break;
                    case 5://atras
                        contadorMostrarUsuario--;
                        mostrarGUI(DU[contadorMostrarUsuario]);
                        btnPanel[11].SetActive(true);
                        if (contadorMostrarUsuario == 0)
                        {
                            btnPanel[10].SetActive(false);
                        }
                        break;
                    case 6://siguiente
                        contadorMostrarUsuario++;
                        mostrarGUI(DU[contadorMostrarUsuario]);
                        btnPanel[10].SetActive(true);
                        if (contadorMostrarUsuario == DU.Count - 2)
                        {

                            btnPanel[11].SetActive(false);
                        }
                        break;
                    case 7://mostrar todo
                        for (int i = 0; i < btnPanel.Length; i++)
                        {
                            btnPanel[i].SetActive(false);
                        }
                        btnPanel[8].SetActive(true);//btn atras
                        btnPanel[10].SetActive(false);
                        if (DU.Count - 2 != 0)
                        {
                            btnPanel[11].SetActive(true);
                        }

                        GUI_Panel[2].SetActive(true);//panel total de notas canvas
                        mostrarGUI(DU[0]);
                        contadorMostrarUsuario = 0;
                        break;
                    case 8://asignar nuevo nivel de acceso
                            auxDU.si_Supervisor=!auxDU.si_Supervisor;
                        Frase_Txt.text = "Usuario : "+auxUsuario+"\nNivel de Acceso Cambiado";
                        GuardarDatos(auxUsuario);
                        actualizarSupervisor();
                        CargarDatos(this);
                        foreach (DatosUsuarios du in DU)
                        {
                            if (auxUsuario == du.DNIs)
                            {
                                auxDU = du;
                                Debug.Log(auxUsuario + " - " + auxDU.DNIs + " encontrado");
                                break;
                            }
                        }
                        Debug.Log(auxUsuario + " - " + auxDU.DNIs);
                        mostrarGUI(auxDU);
                        break;
                        case 9:
                            GUI_Panel[4].SetActive(true);
                            GUI_Panel[2].SetActive(false);
                        Frase_Txt.text = "BORRADO TOTAL DE DATOS DEL MÓDULO";
                        GUI_Panel[2].SetActive(false);
                        GUI_Panel[3].SetActive(false);
                        btnPanel[0].SetActive(false);//panel numerico
                        btnPanel[2].SetActive(false);//iniciar
                        btnPanel[3].SetActive(false);//ver notas
                        btnPanel[8].SetActive(true);//cancelar
                        btnPanel[12].SetActive(false);//borrar datos
                        btnPanel[5].SetActive(false);//buscar
                        btnPanel[9].SetActive(false);//buscar usuario
                        btnPanel[6].SetActive(false);//mostrar todo
                        btnPanel[7].SetActive(false);//actualizar
                        btnPanel[4].SetActive(false);//exportar

                        break;
                    case 10:
                        GUI_Panel[4].SetActive(false);
                        Frase_Txt.text = "Bienvenido :\n" + ActualUsuario;
                        btnPanel[8].SetActive(false);//btn atras
                        GuardarDatos(ActualUsuario);
                        btnPanel[0].SetActive(false);//panel numerico
                        btnPanel[2].SetActive(true);//iniciar
                        btnPanel[3].SetActive(true);//ver notas
                        btnPanel[1].SetActive(false);//login
                        btnPanel[12].SetActive(true);//borrar datos
                        if (ParaPC == true)
                        {
                            btnPanel[4].SetActive(true);//exportar
                            
                        }
                        else
                        {
                           
                            btnPanel[4].SetActive(false);//exportar
                        }
                        btnPanel[5].SetActive(true);//buscar usuario
                        btnPanel[9].SetActive(false);//accion buscar
                        btnPanel[6].SetActive(true);//mostrar todo
                        btnPanel[7].SetActive(false);//actualizar
                        GUI_Panel[2].SetActive(false);//panel total de notas canvas
                        GUI_Panel[4].SetActive(false);//panel BORRAR DATOS canvas
                        btnPanel[10].SetActive(false);//anterior
                        btnPanel[11].SetActive(false);//siguiente
                        actualizarSupervisor();
                        break;
                }
                break;
        }
    } 
    public void actualizarSupervisor()
    {
        foreach (DatosUsuarios du in DU)
        {
            if (du.DNIs == ActualUsuario)
            {
                auxDU = du;
                auxDU.ultimaSesion = DateTime.Now;
                auxDU.fechaAnteriorSesion = du.anteriorSesion.ToString("dd-MM-yyyy HH:mm");
                auxDU.fechaInicioSesion = du.inicioSesion.ToString("dd-MM-yyyy HH:mm");
                auxDU.fechaUltimaSesion = du.ultimaSesion.ToString("dd-MM-yyyy HH:mm");
                auxDU.fechaDesarrolloSesion = du.desarrolloSesion.ToString("dd-MM-yyyy HH:mm");
                auxDU.si_Supervisor=true;
            }
        }
        GuardarDatos(ActualUsuario);
    }
    public void verificarContacto(int confirmarContacto)//*******realiza acciones cuando2 objetos interactuan usan
    {/*
        switch (confirmarContacto)
        {
            case 0://btn de panel numerico
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (objs[0].activeInHierarchy == false)
                    {
                        objs[0].SetActive(true);//evita regresar
                    }
                    if (auxcontacto <= 9)
                    {
                        if (nCifras < limiteCifras)
                        {
                            if (nCifras == 0)
                            {
                                CodigoAcceso = "" + auxcontacto;
                                n[0] = CodigoAcceso;
                            }
                            else
                            {
                                CodigoAcceso = CodigoAcceso + auxcontacto;
                                if (nCifras < limiteCifras)
                                {
                                    n[nCifras] = CodigoAcceso;
                                }
                            }
                            Panel_Txt.text = CodigoAcceso;
                            nCifras++;
                            if(nCifras==8)
                            {
                                if (ActualUsuario == "")
                                {
                                    btnPanel[1].SetActive(true);
                                    
                                }
                                else
                                {
                                    btnPanel[9].SetActive(true);
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        if (auxcontacto == 10)//boton borrar
                        {
                            if (btnPanel[1].activeInHierarchy == true) { btnPanel[1].SetActive(false); }//desactiva login al borrar
                            
                            if (nCifras > 0)
                            {
                                nCifras--;
                            }
                            if (nCifras >= 0)
                            {
                                if (nCifras > 0)
                                {
                                    CodigoAcceso = n[nCifras - 1];
                                }
                                else
                                {
                                    CodigoAcceso = "";
                                }

                            }

                            Debug.Log("mostrando: " + CodigoAcceso);
                            Panel_Txt.text = CodigoAcceso;
                        }
                        else
                        {
                            if (contacto_confirmado[confirmarContacto] == true)
                            {
                                if (nCifras == 8)
                                {
                                    if (auxcontacto == 12)
                                    {//********BTN ACTUALIZAR USUARIO
                                        actualizarUsuario(CodigoAcceso);
                                        Debug.Log(CodigoAcceso + " datos actualizados");
                                    }
                                    else
                                    {//*********btn LOGIN**************************************************LOGIN********************
                                        Debug.Log(CodigoAcceso + " dni de prueba mandado");
                                        
                                        cargarDni(CodigoAcceso);
                                        btnPanel[1].SetActive(false);
                                        CodigoAcceso = "";
                                        Panel_Txt.text = CodigoAcceso;
                                        nCifras = 0;
                                    }

                                    AudioManager.aSource.goFx("Bien");
                                }
                                else
                                {
                                    AudioManager.aSource.goFx("Fallo");
                                }
                            }
                        }
                    }
                }
                break;
            case 1://btn de ingreso y salir
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (auxcontacto == 0)//17-02-25botones
                    {
                        btnPanel[0].SetActive(true);
                        GUI_Panel[0].SetActive(false);
                        GUI_Panel[1].SetActive(true);
                        //img_bg_not[].GetComponent<RawImage>().color = new Color(73,168,80,255);
                    }
                    else {
                        GuardarDatos();
                        Application.Quit();
                    }
                }
                break;
            case 2://ver notas y iniciar
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    if (auxcontacto == 0)//iniciar
                    {
                        btnPanel[0].transform.SetParent(GUI_Panel[1].transform);
                        si_inicioModulo=true;
                        objs[1].SetActive(false);
                    }
                    else//ver notas
                    {
                        mostrarGUI(auxDU);
                        if (GUI_Panel[2].activeInHierarchy == true)//PARA ABRIR Y CERRAR
                        {
                            GUI_Panel[2].SetActive(false);
                        }
                        else { GUI_Panel[2].SetActive(true); }
                    }
                }
                break;
            case 3://botones de supervisor-superusuarios
                if (contacto_confirmado[confirmarContacto] == true)
                {
                    switch (auxcontacto)
                    {
                        case 0://**exporta txt
                            GuardarDatos();
                            GestionUsuariosManager.exportTxt(NombreProyecto, infoTotalExport);
                            
                            break;
                            case 1://**borra data
                            GestionUsuariosManager.BorrarDatosUsuarios(this);
                            break;
                        case 2://**Buscar
                            Frase_Txt.text = "Ingrese identificación de usuario a Buscar:";
                            btnPanel[0].SetActive(true);//panel numerico
                            btnPanel[8].SetActive(true);
                            btnPanel[4].SetActive(false);//btn exportar
                            btnPanel[5].SetActive(false);//buscar
                            btnPanel[9].SetActive(false);//buscar usuario
                            btnPanel[6].SetActive(false);//mostrar todo
                            btnPanel[7].SetActive(false);//actualizar
                            btnPanel[2].SetActive(false);//iniciar
                            btnPanel[3].SetActive(false);//ver notas

                            break;
                        case 4://**Buscar accion
                            if (buscarUsuario(CodigoAcceso) == true)
                            {
                                mostrarGUI(auxDU);
                                GUI_Panel[2].SetActive(true);
                            }
                            
                            break;
                        case 3://**Cancelar
                            Frase_Txt.text = "Bienvenido :\n"+ActualUsuario;
                            btnPanel[8].SetActive(false);//btn atras
                            //GUI_Panel[1].SetActive(false);//panel numerico
                            GuardarDatos();
                            btnPanel[0].SetActive(false);//panel numerico
                            btnPanel[2].SetActive(true);//iniciar
                            btnPanel[3].SetActive(true);//ver notas
                            btnPanel[1].SetActive(false);//login
                            btnPanel[4].SetActive(true);//btn exportar
                            btnPanel[5].SetActive(true);//buscar usuario
                            btnPanel[9].SetActive(false);//accion buscar
                            btnPanel[6].SetActive(true);//mostrar todo
                            btnPanel[7].SetActive(true);//actualizar
                            GUI_Panel[2].SetActive(false);//panel total de notas canvas
                            btnPanel[10].SetActive(false);//atras
                            btnPanel[11].SetActive(false);//siguiente
                            break;
                        case 5://atras
                            contadorMostrarUsuario--;
                            mostrarGUI(DU[contadorMostrarUsuario]);
                            btnPanel[11].SetActive(true);
                            if (contadorMostrarUsuario == 0)
                            {
                                btnPanel[10].SetActive(false);
                            }
                            break;
                        case 6://siguiente
                            contadorMostrarUsuario++;
                            mostrarGUI(DU[contadorMostrarUsuario]);
                            btnPanel[10].SetActive(true);
                            if (contadorMostrarUsuario == DU.Count-2)
                            {
                                
                                btnPanel[11].SetActive(false);
                            }
                            break;
                        case 7://mostrar todo
                            btnPanel[8].SetActive(true);//btn atras
                            btnPanel[10].SetActive(false);
                            if (DU.Count - 2 != 0)
                            {
                                btnPanel[11].SetActive(true);
                            }
                            
                            GUI_Panel[2].SetActive(true);//panel total de notas canvas
                            mostrarGUI(DU[0]);
                            contadorMostrarUsuario = 0;
                            break;
                        case 8://asignar nuevo nivel de acceso
                            Frase_Txt.text = "Ingrese identificación de usuario : ";
                            btnPanel[0].SetActive(true);//panel numerico
                            btnPanel[8].SetActive(true);
                            btnPanel[4].SetActive(false);//btn exportar
                            btnPanel[5].SetActive(false);//buscar
                            btnPanel[9].SetActive(false);//buscar usuario
                            btnPanel[6].SetActive(false);//mostrar todo
                            btnPanel[7].SetActive(false);//actualizar
                            btnPanel[2].SetActive(false);//iniciar
                            btnPanel[3].SetActive(false);//ver notas
                            break;
                    }
                }
             break;
        }*/
    }
    public void mostrarGUI(DatosUsuarios du)//mostra resultado a nivel de usuario
    {
        datosU_panel[0].text = "Identificación : "+du.DNIs;
        datosU_panel[1].text = "Fecha de último desarrollo : \nNo tiene registro";
        GUI_Panel[3].SetActive(false);
        if (du.fechaDesarrolloSesion != "01-01-0001 00:00")
        {
            string auxNota = "Desaprobado";
            if (du.desarrolloSesion!=null&&du.desarrolloSesion.ToString("dd-MM-yyyy HH:mm")!="01-01-0001 00:00")
            {
                auxNota = du.desarrolloSesion.ToString("dd-MM-yyyy HH:mm");
                datosU_panel[1].text = "Fecha de último desarrollo : \n" + auxNota;
                GUI_Panel[3].SetActive(true);
                Debug.Log(auxNota+" desarrollo");
                if (si_binario == false)
                {
                    for (int i = 0; i < nTareas; i++)
                    {
                        auxNota = "Desaprobado";
                        datosU_panel[i + 2].text = " Nota " + (i + 1) + " : " + du.notas[i];
                        img_bg_nota[i].GetComponent<RawImage>().color = new Color32(255, 0, 28, 255);//color Rojo
                        if (du.notas[i] >= notaMinAprobatoria)
                        {
                            img_bg_nota[i].GetComponent<RawImage>().color = new Color32(73, 168, 80, 255);//color verde
                            auxNota = "Aprobado";
                        }
                        Notas_Txt[i].text = auxNota;//se ve en el recuadro
                    }
                }
                else//*****Metodo de acumulacion de errores: 0 => bien ;mayor A 0 => DESAPROBADO
                {
                    for (int i = 0; i < nTareas; i++)
                    {
                        auxNota = "Desaprobado";
                        img_bg_nota[i].GetComponent<RawImage>().color = new Color32(255, 0, 28, 255);//color Rojo
                        if (du.notas[i] == 0)//cuando no  hay errores
                        {
                            img_bg_nota[i].GetComponent<RawImage>().color = new Color32(73, 168, 80, 255);//color verde
                            auxNota = "Aprobado";
                        }
                        datosU_panel[i + 2].text = " Ejercicio " + (i + 1) + " : ";
                        Notas_Txt[i].text = auxNota;//se ve en el recuadro
                    }
                }
            }
            
            
        }
    }
    public bool buscarUsuario(string d)
    {
        bool encontrado=false;
        foreach (DatosUsuarios du in DU)
        {
            if (du.DNIs == d&&du.nombres!="admin")
            {
                encontrado = true;
                auxUsuario = d;
                auxDU = du;
                Frase_Txt.text = "Busqueda exitosa\n"+auxDU.DNIs+ "\nUsuario Encontrado";
            }
        }
        if (encontrado == false)
        {
            Frase_Txt.text = "No se encontro Usuario:\n" + d;
        }
        return encontrado;
    }
    //**********************************Metodos Para Agregar Notas*********************
    public void AgregarNota(int numNota,float nota)//Agrega de nota de forma individual
    {
        auxNotas[numNota] = nota;
    }
    public void GuardarNotasTotales()//Agrega las notas al final del recorrido y las guarda
    {
        for(int i = 0;i < auxNotas.Length; i++)
        {
            auxDU.notas[i]=auxNotas[i];
        }
        auxDU.ultimaSesion = DateTime.Now;
        auxDU.desarrolloSesion = DateTime.Now;
        auxDU.fechaDesarrolloSesion = auxDU.desarrolloSesion.ToString("dd-MM-yyyy HH:mm");
        auxDU.fechaUltimaSesion = auxDU.ultimaSesion.ToString("dd-MM-yyyy HH:mm");
        actualizarUsuario(ActualUsuario);
    }
    public void moverPanelFinal()
    {
        GUI_Panel[2].SetActive(true);
        GUI_Panel[2].transform.SetParent(PosFinalPanelNotas);
        GUI_Panel[2].transform.localPosition = Vector3.zero;
        mostrarGUI(auxDU);
        Debug.Log("moviendo panel de notas");
    }
    public IEnumerator Locu_Lobby() 
    {
        AudioManager.aSource.goFx("Locu_Lobby",1,false,false);
        if (Ya_Existen_Datos == false)
        {
            yield return new WaitForSecondsRealtime(11.15f);
            AudioManager.aSource.goFx("Locu_Lobby_1", 1, false, false);
        }
        
    }
}
