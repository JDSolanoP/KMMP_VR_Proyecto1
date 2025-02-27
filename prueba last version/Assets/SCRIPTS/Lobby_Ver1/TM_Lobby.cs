using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Policy;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TM_Lobby : MonoBehaviour
{/*Creado por Julio Solano:03-02-25*/
    public static TM_Lobby lb;

    public Lista_Tareas_Controller ltc;
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
    public DateTime AnteriorSesion;//para reporte
    public DateTime UltimaSesion;//para reporte
    public DateTime InicioSesion;//para reporte
    public string UsuarioAnterior;//ultimo usuario en usar el modulo-ligado a ultimo usuario
    public string supervisorAnterior;//ultimo supervisor en logearse
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
        //CargarDatos();
        if (File.Exists(Application.persistentDataPath + "/VR_"+ NombreProyecto + "_Usuarios.txt"))
        {Ya_Existen_Datos = true;
            CargarDatos(this);//************************************cargar  datos desde la lista
            if (DTs.DUs != null) { DU = DTs.DUs; }
        }
        else
        {
            Debug.Log("NO HAY DATOS, agregado admin");
        }
        admin = new DatosUsuarios(admin.DNIs,admin.nombres,admin.si_Supervisor,nTareas);
        DU.Add(admin);
        //SesionAnterior = UltimaSesion.ToString("dd-MM-yyyy  HH:mm");
    }
    private void Start()
    {
        GUI_Panel[0].SetActive(true);
        GUI_Panel[1].SetActive(false);
        GUI_Panel[2].SetActive(false);
        //img_bg_nota.GetComponent<RawImage>().color = new Color32(73, 168, 80, 255);
        auxNotas = new float[nTareas];
        nota = new float[nTareas];
        auxDU.notas = new float[nTareas];
    }
    public void Transferir_Usuarios()//*******transfiere datos desde DTs(el guardado) a DU lista auxiliar***********
    {
        foreach (DatosUsuarios du in DTs.DUs)
        {
            listaDni.Add(du.DNIs);
            du.fechaUltimaSesion= du.ultimaSesion.ToString("dd-MM-yyyy  HH:mm");
            nUsuariosTotal++;
            if (du.si_Supervisor == true)
            {
                listaSupervisores.Add(du.DNIs);
            }
            Debug.Log("dni: " + du.DNIs + " -fecha: " + du.ultimaSesion.ToString("dd-MM-yyyy  HH:mm")+""+du.si_Supervisor);
            // mostrarNotas(du.DNIs);
            CreaReporte();
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
    public void VerificadorDni(string d)//********AGREGA USUARIO a UNA LISTA*******03-02-25
    {
        bool primeracontada = false;//si usuario es nuevo
        ActualUsuario = d;
        InicioSesion = DateTime.Now;//para ver en el reporte cuando se inicio
        foreach (DatosUsuarios du in DU)
        {
            //Debug.Log("codigo revisado : " + code);
            if (du.DNIs == d)//si se encontro 1er vez el dni
            {
                if (primeracontada == false)
                {
                    if(du.inicioSesion.ToString("dd-MM-yyyy")!= du.anteriorSesion.ToString("dd-MM-yyyy"))
                    {
                        auxDU.anteriorSesion = du.inicioSesion;
                    }
                    else
                    {
                        auxDU.anteriorSesion= du.anteriorSesion;
                    }
                    auxDU.inicioSesion=DateTime.Now;
                    auxDU = du;//
                    auxDU.DNIs = du.DNIs;
                    auxDU.nombres = du.nombres;
                    for (int i = 0; i < nTareas; i++)
                    {
                        auxDU.notas[i] = du.notas[i];
                    }
                    primeracontada = true;
                    Frase_Txt.text = "Bienvenido devuelta\n"+d;
                    auxDU.si_Supervisor = du.si_Supervisor;
                    Debug.Log(du.DNIs + " Usuario encontrado ->contada:" + primeracontada);
                    //break;
                }
            }
        }
        if (primeracontada == false)
        {//**************************Registro nuevo usuario**********************
            auxDU.inicioSesion = DateTime.Now;
            auxDU.DNIs = d;
            DU.Add(auxDU);//agrego usuario
            listaDni.Add(auxDU.DNIs);
            Frase_Txt.text = "Nuevo usuario\n" + d;
            Debug.Log(auxDU.DNIs + " Usuario nuevo " + primeracontada);
            nUsuariosTotal++;
        }
        
        d=ActualUsuario;
        UsuarioAnterior=DTs.ultimoUsuario;//guarda dni de anterior usuario
        UltimaSesion=DTs.ultimaSesion;//guarda la fecha
        GuardarDatos(d);
        btnPanel[0].SetActive(false);
        btnPanel[2].SetActive(true);
        btnPanel[3].SetActive(true);
        btnPanel[1].SetActive(false);
        if (auxDU.si_Supervisor == true)
        {
            btnPanel[4].SetActive(true);
            btnPanel[5].SetActive(true);
            btnPanel[6].SetActive(true);
            btnPanel[7].SetActive(true);
        }
        
        //return primeracontada;
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
        
        foreach (DatosUsuarios du in DTs.DUs)
        {
            DU.Add(du);
        }
        Transferir_Usuarios();//***Desde el txt a lista auxiliar
        Debug.Log("Datos Cargados");
    }

    public void CreaReporte()//***Crea report en txt de TODOS los usuario//***********25-02-25*********
    {
        infoTotalExport = "*****Reporte de Usuarios del Módulo "+NombreProyecto+"*****\n***Credits***\n->Property of CFK - KMMP<-\n->Project Leader : Hector Herrera<-\n->Developed by Julio Solano<-\n*************\n\n->Reporte solicitado por el usuario : "+ActualUsuario;
        infoTotalExport+="\nFecha de Reporte : "+DateTime.Now+"\n\n***RESULTADOS***";
        Debug.Log("Creando Reporte");
        foreach (DatosUsuarios du in DTs.DUs)
        {
            if (du.nombres != "admin")
            {
                infoTotalExport += "\nDNI : " + du.DNIs;
                du.fechaUltimaSesion = du.ultimaSesion.ToString("dd-MM-yyyy  HH:mm");
                 if (du.si_Supervisor)
                {
                    infoTotalExport += "\nNivel de Acceso : Supervisor";
                    
                }
                else
                {
                    infoTotalExport += "\nNivel de Acceso : Usuario";
                }
                
                
                infoTotalExport += "\nÚltima Fecha de Sesión : " + du.inicioSesion.ToString("dd-MM-yyyy  HH:mm");
                if (du.anteriorSesion != null)
                {
                    infoTotalExport += "\nAnterior Fecha de Sesión : " + du.anteriorSesion.ToString("dd-MM-yyyy  HH:mm");
                }
                else
                {
                    infoTotalExport += "\nAnterior Fecha de Sesión : No accedió anteriormente";
                }
                infoTotalExport += "\nÚltima Fecha de Desarrollo de Módulo : " + du.ultimaSesion.ToString("dd-MM-yyyy  HH:mm");
                
            
                //Debug.Log("dni: " + du.DNIs + " -fecha: " + du.ultimaSesion.ToString("dd-MM-yyyy  HH:mm"));
                for (int i = 0; i < nTareas; i++)
                {
                    if (si_binario)
                    {
                        if (du.notas[i] == 0) { infoTotalExport += "\nEjercicio "+(i+1)+" : Aprobado"; }
                        else
                        {
                            infoTotalExport += "\nEjercicio " + (i+1) + " : Desaprobado";
                        }
                    }
                    else {
                        infoTotalExport += "\nnota " + (i+1) + " : " + du.notas[i];
                    }
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
    public void verificarContacto(int confirmarContacto)//*******realiza acciones cuando2 objetos interactuan usan
    {
        switch (confirmarContacto)
        {
            case 0://btn de apnel numerico
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
                                btnPanel[1].SetActive(true);
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
                    if (auxcontacto == 0)//exportar info
                    {
                        GestionUsuariosManager.exportTxt(NombreProyecto,infoTotalExport);
                    }
                    else
                    {
                        if (auxcontacto == 1)//borrar datos
                        {
                            GestionUsuariosManager.BorrarDatosUsuarios(this);
                        }//exportar info
                            
                    }

                }
                    break;
        }
    }
    public void mostrarGUI(DatosUsuarios du)//mostra resultado a nivel de usuario
    {
        datosU_panel[0].text = "DNI : "+du.DNIs;
        datosU_panel[1].text = "Fecha de último acceso : \nNo tiene registro";
        if (du.fechaUltimaSesion != "")
        {
            GUI_Panel[3].SetActive(true);
            string auxNota = "Desaprobado";
            datosU_panel[1].text = "Fecha de último acceso : \n" + du.ultimaSesion.ToString("dd-MM-yyyy HH:mm");
            if (si_binario == false)
            {
                for (int i = 0; i < nTareas; i++)
                {
                    auxNota = "Desaprobado";
                    datosU_panel[i + 2].text = " Nota " + (i + 1) + " : " + du.notas[i];
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
    public void buscarUsuario(string d)
    {
        foreach (DatosUsuarios du in DU)
        {
            if (du.DNIs == d)
            {
                auxDU = du;
            }
        }
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
}
