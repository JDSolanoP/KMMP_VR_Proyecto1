using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DatosTotales
{
    public List<DatosUsuarios> DUs=null;
    public DateTime ultimaSesion;
    public string ultimoUsuario;
    public float notaMin;
    public float notaMax;
    public List<string> ListaSupervisores=null;
    public DatosTotales() 
    {
        //DUs = new List<DatosUsuarios>();
    }
    public DatosTotales(TM_Lobby tmo)
    {
        foreach (DatosUsuarios usu in tmo.DU)
        {
            DUs.Add(usu);
        }
        ultimaSesion = DateTime.Now;
        notaMin = tmo.notaMinAprobatoria;
        notaMax = tmo.notaMaxAprobatoria;
    }
}
