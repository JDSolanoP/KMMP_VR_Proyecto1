using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Remoting.Messaging;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class TraductorMultiTextos : MonoBehaviour
{
    [Header("***Textos del Módulo***")]
    public Texto[] Parrafos;
    
    [Header("Esp=0,Eng=1,Otro:2")]
    public int IdiomaBase;
    public int IdiomaFinal;
    public string IdiomaActual;
    string aux;
    
    void Start()
    {
            switch (IdiomaFinal)
            {
                case 0:
                    IdiomaActual = "Español";
                    if (IdiomaBase != IdiomaFinal)
                    {
                    traduccionTextos();
                }
                    break;
                case 1:
                    IdiomaActual = "English";
                if (IdiomaBase != IdiomaFinal)
                {
                    traduccionTextos();
                }
                break;
                case 2:
                    IdiomaActual = "Otro";
                if (IdiomaBase != IdiomaFinal)
                {
                    traduccionTextos();
                }
                break;
            }
    }
    public void traduccionTextos()
    {
        for (int j = 0; j < Parrafos.Length; j++)
        {
            if (Parrafos[j].linea.Length > 0)
            {
                for (int i = 0; i < Parrafos[j].linea.Length; i++)
                {
                    aux += "" + Parrafos[j].linea[i] + "\n";
                }
            }
            else
            {
                aux = "" + Parrafos[j].linea[0];
            }
            
            Parrafos[j].obj.GetComponent<TMP_Text>().text = aux;
            if (Parrafos[j].FontSize != 0)
            {
                Parrafos[j].obj.GetComponent<TMP_Text>().fontSize = Parrafos[j].FontSize;
            }
        }
    }
}
[System.Serializable]
public class Texto
{
    [Header("Ubicacion:Panel/numero de Parrafo o boton")]
    public string ubicacion;
    public GameObject obj;
    public string[] linea;
    public float FontSize;
}
