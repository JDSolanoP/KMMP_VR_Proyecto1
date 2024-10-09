using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Eslingas : MonoBehaviour//Creado el 01-0-2024
{
    [Header("Numero de Material")]
    public int Num_Material;
    public bool usable;//si cumple con todo lo necesario para utilizarse sin caer
    [Header("Etiqueta")]
    public GameObject E_Etiqueta;//para poder apagarla
    public GameObject Quemadura_Icon;//para colocar quemadura
    public Vector3 rot0,pos0, pos_E;//para poder devolver eslinga y mover la etiqueta
    [Header("Eslinga_Partes")]
    public GameObject[] E_Partes;
    [Header("Etiquetas")]
    public Sprite[] etiqueta;//del 1 al 8
    [Header("Materiales")]
    public Material[] E_Material;
    

    void Start()
    {
        rot0=transform.localEulerAngles;
        pos0 = transform.localPosition;
        //set_Valores(Num_Material);
    }

    public void set_Valores(int m)//del 1 al 7
    {
        if (m < 8)
        {
            int rnd = Random.Range(0, 4);
            if (rnd == 0)//no etiqueta
            {
                E_Etiqueta.SetActive(false);
                usable = false;
            }
            else
            {
                rnd = Random.Range(0, 2);//si reubicamos en la otra posicion
                if (rnd == 0)
                {
                    
                    E_Etiqueta.transform.localPosition = new Vector3(pos_E.x, pos_E.y, pos_E.z);
                    E_Etiqueta.transform.localEulerAngles = new Vector3(-90,0, -90);
                }
                
            }
            rnd = Random.Range(0, 2);
            if (rnd == 0)//si corte
            {
                E_Partes[13].SetActive(false);//corecto
                E_Partes[14].SetActive(true);//incorrecto
                usable = false;
            }
            else
            {
                E_Partes[14].SetActive(false);
                E_Partes[13].SetActive(true);
            }
            rnd = Random.Range(0, 2);
            if (rnd == 0)//si quemadura
            {
                E_Etiqueta.GetComponent<SpriteRenderer>().sprite = etiqueta[8];//etiqueta de error
                usable = false;
            }
            else
            {
                E_Etiqueta.GetComponent<SpriteRenderer>().sprite = etiqueta[m];
            }
            for (int i = 0; i < E_Partes.Length; i++)
            {
                E_Partes[i].GetComponent<MeshRenderer>().sharedMaterial = E_Material[m];
            }
        }
        else {//pintar referencia

            for (int i = 0; i < E_Partes.Length; i++)
            {
                E_Etiqueta.SetActive(false);
                E_Partes[i].GetComponent<MeshRenderer>().sharedMaterial = E_Material[m];
            }
        }

        
    }
    public void set_Valores(int m,int r1,int r2, int r3)//del 1 al 7
    {
        if (m < 8)
        {
            int rnd = r1;
            if (rnd == 0)//no etiqueta
            {
                E_Etiqueta.SetActive(false);
                usable = false;
            }
            else
            {
                rnd = Random.Range(0, 2);//si reubicamos en la otra posicion
                if (rnd == 0)
                {

                    E_Etiqueta.transform.localPosition = new Vector3(pos_E.x, pos_E.y, pos_E.z);
                    E_Etiqueta.transform.localEulerAngles = new Vector3(-90, 0, -90);
                }

            }
            rnd = r2;
            if (rnd == 0)//si corte
            {
                E_Partes[13].SetActive(false);//corecto
                E_Partes[14].SetActive(true);//incorrecto
                usable = false;
            }
            else
            {
                E_Partes[14].SetActive(false);
                E_Partes[13].SetActive(true);
            }
            rnd = r3;
            if (rnd == 0)//si quemadura
            {
                E_Etiqueta.GetComponent<SpriteRenderer>().sprite = etiqueta[8];//etiqueta de error
                usable = false;
            }
            else
            {
                E_Etiqueta.GetComponent<SpriteRenderer>().sprite = etiqueta[m];
            }
            for (int i = 0; i < E_Partes.Length; i++)
            {
                E_Partes[i].GetComponent<MeshRenderer>().sharedMaterial = E_Material[m];
            }
        }
        else
        {//pintar referencia

            for (int i = 0; i < E_Partes.Length; i++)
            {
                E_Etiqueta.SetActive(false);
                E_Partes[i].GetComponent<MeshRenderer>().sharedMaterial = E_Material[m];
            }
        }


    }
    public bool siUsable()
    {
        return usable;
    }
    public void repos()
    {
        transform.localEulerAngles = rot0;
        transform.localPosition = pos0;
    }
}
