using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;


public static class GestionUsuariosManager
{
    public static void GuardarDatosUsuarios(TM_Lobby slm)
    {
        Debug.Log("copiando2 Datos");
        DatosTotales dt = new DatosTotales();
                dt = slm.DTs;
                dt.ultimaSesion = DateTime.Now;
        Debug.Log("respaldo Datos");
        string datos = Application.persistentDataPath + "/VR_" + slm.NombreProyecto + "_Usuarios.txt";
        //DIRECCION PARA PC
        //********************C:\Users\julio.solano\AppData\LocalLow\KMMP_CFK_JSolano\Izaje_M1_CFK***04-02-25
        //Direccion para Android
        ///****storage/emulated/<userid>/Android/data/<packagename>/files

        FileStream fs = new FileStream(datos, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, dt);
                
                fs.Close();
    }
    public static void GuardarDatosUsuarios(TM_Lobby slm, bool sinBF)
    {
        Debug.Log("copiando Datos sin BF");
        DatosTotales dt = new DatosTotales();
        dt = slm.DTs;
        dt.ultimaSesion = DateTime.Now;
        Debug.Log("respaldo Datos");
        string datos = Application.persistentDataPath + "/VR_" + slm.NombreProyecto + "_UsuariosSBF.txt";
        //DIRECCION PARA PC
        //********************C:\Users\julio.solano\AppData\LocalLow\KMMP_CFK_JSolano\Izaje_M1_CFK***04-02-25
        //Direccion para Android
        ///****storage/emulated/<userid>/Android/data/<packagename>/files

        FileStream fs = new FileStream(datos, FileMode.Create);
        //BinaryFormatter bf = new BinaryFormatter();
        //bf.Serialize(fs, dt);

        fs.Close();
    }
    public static DatosTotales CargarDatosUsuarios(TM_Lobby slm)
    {
        string datos = Application.persistentDataPath + "/VR_" + slm.NombreProyecto + "_Usuarios.txt";
        if (File.Exists(datos))
        {
            FileStream fs = new FileStream(datos, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            DatosTotales dt = (DatosTotales)bf.Deserialize(fs);
            fs.Close();
            return dt;
        }
        else
        {
            Debug.LogError("No se encontro el archivo guardado");
            return null;
        }
    }
    public static DatosTotales CargarDatosUsuarios(TM_Lobby slm,bool sinBF)
    {
        string datos = Application.persistentDataPath + "/VR_" + slm.NombreProyecto + "_UsuariosSBF.txt";
        if (File.Exists(datos))
        {
            FileStream fs = new FileStream(datos, FileMode.Open);
            
            BinaryFormatter bf = new BinaryFormatter();
            DatosTotales dt = (DatosTotales)bf.Deserialize(fs);
            fs.Close();
            return dt;
        }
        else
        {
            Debug.LogError("No se encontro el archivo guardado");
            return null;
        }
    }
    public static void BorrarDatosUsuarios(TM_Lobby slm)
    {
        string datos = Application.persistentDataPath + "/VR_" + slm.NombreProyecto + "_Usuarios.txt";

        if (File.Exists(datos))
        {
            File.Delete(datos);
        }
    }
    public static void exportTxt(string NombreProyecto, string info)
    {
        string direccion = Application.persistentDataPath + "/VR_" + NombreProyecto + "_Usuarios_Info.txt";
        File.WriteAllText(direccion, info);
        //UnityEditor.EditorUtility.RevealInFinder(direccion);
        //UnityEditor.EditorUtility.OpenWithDefaultApp(direccion);
        
    }
}
