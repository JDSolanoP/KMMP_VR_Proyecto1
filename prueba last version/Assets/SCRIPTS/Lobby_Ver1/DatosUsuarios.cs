using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DatosUsuarios
{
    public string DNIs = null;
    public string nombres=null;
    public DateTime ultimaSesion;
    public float[] notas;//segunda fase
    public DatosUsuarios(TM_Lobby tmo) 
    {
        notas =new float[tmo.nTareas];
        for(int i = 0; i < tmo.nTareas; i++)
        {
            notas[i] = tmo.nota[i];
        }
        DNIs = tmo.auxDU.DNIs;
        ultimaSesion = tmo.auxDU.ultimaSesion;
        nombres = tmo.auxDU.nombres;
    }
}
