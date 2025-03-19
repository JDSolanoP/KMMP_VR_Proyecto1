using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DatosUsuarios
{
    public string DNIs = null;
    public string nombres=null;
    public DateTime anteriorSesion;//cuando se da a login en una anterior vez
    public DateTime inicioSesion;//cuando se da a login
    public DateTime desarrolloSesion;////Agregado el 18-03-25 cuando se completa el modulo
    public DateTime ultimaSesion;//ultima ves desarrollado el modulo
    public string fechaAnteriorSesion;
    public string fechaInicioSesion;
    public string fechaDesarrolloSesion;
    public string fechaUltimaSesion;
    public float[] notas;//segunda fase
    public bool si_Supervisor;//verifica si tiene acceso a buscar resultados de otros usuarios
    public DatosUsuarios(TM_Lobby tmo) 
    {
        fechaAnteriorSesion= anteriorSesion.ToString("dd-MM-yyyy HH:mm");
        fechaInicioSesion = inicioSesion.ToString("dd-MM-yyyy HH:mm");
        fechaUltimaSesion = ultimaSesion.ToString("dd-MM-yyyy HH:mm");
        fechaDesarrolloSesion = desarrolloSesion.ToString("dd-MM-yyyy HH:mm");
        notas =new float[tmo.nTareas];
        for(int i = 0; i < tmo.nTareas; i++)
        {
            notas[i] = tmo.nota[i];
        }
        DNIs = tmo.auxDU.DNIs;
        desarrolloSesion = tmo.auxDU.desarrolloSesion;
        ultimaSesion = tmo.auxDU.ultimaSesion;
        inicioSesion=tmo.auxDU.inicioSesion;
        anteriorSesion = tmo.auxDU.anteriorSesion;
        nombres = tmo.auxDU.nombres;
    }public DatosUsuarios(string dni, string nombre,bool supervisor,int nt)
    {
        DNIs = dni;
        nombres= nombre;
        si_Supervisor= supervisor;
        notas = new float[nt];
    }
}
