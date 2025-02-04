using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Policy;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
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
    public TMP_Text Panel_Txt;
    public TMP_Text Frase_Txt;
    public string[] n;
    public int nN;
    //*************************DATOS DE MODULO****************//
    public string ActualUsuario;//numero de usuarioa en la lista
    public int nTareas;//numero de tareas;
    public float[] nota;//captura la tareas por modulo
    //***************FIN DE DATOS DE MODULO****************************//

    //************DATOS TOTALES DE TODOS LOS USUARIOS-*************************
    public DatosTotales DTs;//****************************************Lista A GUARDAR
    public DatosUsuarios auxDU;//*************************************USUARIO AUXILIAR PARA ACTUALIZAR 

    public int nUsuariosTotal;//numero de usuarios registrados
    public List<string> listaDni;//nombres de ususarios segun dni

    public List<DatosUsuarios> DU = null;//***********************************LISTA DE USUARIOS
    public DateTime UltimaSesion;
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
        {
            CargarDatos(this);//************************************cargar  datos desde la lista
            if (DTs.DUs != null) { DU = DTs.DUs; }

        }
        else
        {
            Debug.Log("NO HAY DATOS");
        }
        //SesionAnterior = UltimaSesion.ToString("dd-MM-yyyy  HH:mm");
    }
    private void Start()
    {
        nota = new float[nTareas];
        auxDU.notas = new float[nTareas];
    }
    public void Mostrar_Usuarios()
    {
        foreach (DatosUsuarios du in DTs.DUs)
        {
            listaDni.Add(du.DNIs);
            nUsuariosTotal++;
            Debug.Log("dni: " + du.DNIs + " -fecha: " + du.ultimaSesion.ToString("dd-MM-yyyy  HH:mm"));
        }
    }
    public bool VerificadorDni(string d)//********AGREGA USUARIO a UNA LISTA*******03-02-25
    {
        bool primeracontada = false;//si usuario es nuevo
        DU.Add(auxDU);//agrego usuario
        auxDU.DNIs = d;
        //int auxNU=0;//indica, en la lista que numero de usuario es;
        foreach (DatosUsuarios du in DU)
        {
            //Debug.Log("codigo revisado : " + code);
            if (du.DNIs == auxDU.DNIs)//si se encontro 1er vez el dni
            {
                if (primeracontada == false)
                {
                    nUsuariosTotal++;
                    auxDU = du;//asigna que numero de usuario es en la lista
                    //CargarDatos();
                    primeracontada = true;
                    Frase_Txt.text = "1er intento,Bienvenido";
                    Debug.Log(du.DNIs + " Usuario encontrado ->contada:" + primeracontada);
                    listaDni.Add(auxDU.DNIs);
                }
                else
                {//si hay mas del mismo dni
                    nUsuariosTotal--;
                    listaDni.Remove(auxDU.DNIs);
                    DU.Remove(auxDU);//saco de la lista
                    Frase_Txt.text = "Bienvenido nuevamente";
                    Debug.Log(auxDU.DNIs + " Usuario ya agregado anteriormente");
                    primeracontada = false;
                    break;
                }
            }

        }
        GuardarDatos();
        return primeracontada;
    }
    public void GuardarDatos()
    {
        foreach (DatosUsuarios dni in DU)
        {
            if (dni.DNIs == auxDU.DNIs)
            {
                //Nombre = null;
                dni.ultimaSesion = UltimaSesion = DateTime.Now;
                for (int i = 0; i < nota.Length; i++)
                {
                    dni.notas[i] = nota[i];
                }
                break;
            }
        }
        Debug.Log("copiando Datos");

        DTs.DUs = DU;
        GestionUsuariosManager.GuardarDatosUsuarios(this);
        Debug.Log("Datos guardados");
    }
    public void CargarDatos(TM_Lobby slm)
    {
        DTs = GestionUsuariosManager.CargarDatosUsuarios(this);
        foreach (DatosUsuarios du in DTs.DUs)
        {
            DU.Add(du);
        }
        Mostrar_Usuarios();
        Debug.Log("Datos Cargados");
    }
    public void cargarDni(string dni)
    {
        VerificadorDni(dni);
    }
    public void verificarContacto(int confirmarContacto)//*******realiza acciones cuando2 objetos interactuan usan
    {
        switch (confirmarContacto)
        {
            case 0:
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
                        }
                        
                    }
                    else
                    {
                        if (auxcontacto == 10)//boton borrar
                        {
                            if (nCifras >= 0)
                            {
                                if (nCifras > 0)
                                {
                                    CodigoAcceso = n[nCifras];
                                }
                                else
                                {
                                    CodigoAcceso = " 0";
                                }
                                
                            }
                            if (nCifras > 0)
                            {
                                nCifras--;
                            }
                            Panel_Txt.text = CodigoAcceso;
                        }
                        else
                        {
                            if (contacto_confirmado[confirmarContacto] == true)
                            {
                                if (nCifras == 8)
                                {
                                    Debug.Log(CodigoAcceso + " dni de prueba mandado");
                                    cargarDni(CodigoAcceso);
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
        }
    }
}