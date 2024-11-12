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
    public bool En_Mano;
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
                Debug.Log("Eslinga " + this.gameObject.name + " sin etiqueta, es usable : " + usable);
            }
            else
            {
                E_Etiqueta.SetActive(true);
                rnd = Random.Range(0, 2);//si reubicamos en la otra posicion
                if (rnd == 0)
                {
                    
                    E_Etiqueta.transform.localPosition = new Vector3(pos_E.x, pos_E.y, pos_E.z);
                    E_Etiqueta.transform.localEulerAngles = new Vector3(-90,0, -90);
                    Debug.Log("Eslinga " + this.gameObject.name + " etiqueta movida , es usable : " + usable);
                }
                
            }
            rnd = Random.Range(0, 2);
            if (rnd == 0)//si corte
            {
                E_Partes[13].SetActive(false);//corecto
                E_Partes[14].SetActive(true);//incorrecto
                usable = false;
                Debug.Log("Eslinga " + this.gameObject.name + " con corte, es usable : " + usable);
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

        Debug.Log("Eslinga " + this.gameObject.name+" es usable : "+usable );
    }
    public void set_Valores(int m,int r1,int r2, int r3, bool u)//del 1 al 7
    {
        usable = u;
        if (m < 8)
        {
            int rnd = r1;
            if (rnd == 0)//no etiqueta
            {
                E_Etiqueta.SetActive(false);
                usable = false;
                Debug.Log("Eslinga " + this.gameObject.name + " sin etiqueta, es usable : " + usable);
            }
            else
            {
                E_Etiqueta.SetActive(true);
                rnd = Random.Range(0, 2);//si reubicamos en la otra posicion
                if (rnd == 0)
                {
                    Debug.Log("Eslinga " + this.gameObject.name + " etiqueta movida , es usable : " + usable);
                    E_Etiqueta.transform.localPosition = new Vector3(pos_E.x, pos_E.y, pos_E.z);
                    E_Etiqueta.transform.localEulerAngles = new Vector3(-90, 0, -90);
                }

            }
            rnd = r2;
            if (rnd == 0)//si corte
            {
                
                E_Partes[13].SetActive(false);//correcto
                E_Partes[14].SetActive(true);//incorrecto
                usable = false;
                Debug.Log("Eslinga " + this.gameObject.name + " con corte, es usable : " + usable);
            }
            else
            {
                E_Partes[14].SetActive(false);
                E_Partes[13].SetActive(true);
                Debug.Log("Eslinga " + this.gameObject.name + " sin corte, es usable : " + usable);
            }
            rnd = r3;
            if (rnd == 0)//si quemadura
            {

                E_Etiqueta.GetComponent<SpriteRenderer>().sprite = etiqueta[8];//etiqueta de error
                usable = false;
                Debug.Log("Eslinga " + this.gameObject.name + " con quemadura, es usable : " + usable);
            }
            else
            {
                E_Etiqueta.GetComponent<SpriteRenderer>().sprite = etiqueta[m];
                Debug.Log("Eslinga " + this.gameObject.name + " con etiqueta "+m+" , usable : " + usable);
            }
            for (int i = 0; i < E_Partes.Length; i++)
            {
                E_Partes[i].GetComponent<MeshRenderer>().sharedMaterial = E_Material[m];
            }
            Debug.Log("SE colocaron los valores " + r1 + " " + r2 + " " + r3 + " que son correctos en Set_eslingas");
            usable=true;
            Debug.Log("Eslinga " + this.gameObject.name + " es usable : " + usable);
        }
        else
        {//pintar referencia 8

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
    public void reUbicarEslingas()//cuando haga contacto con detector, se colocara denuevo en su sitio
    {
        transform.localEulerAngles = rot0;
        transform.localPosition = pos0;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        this.gameObject.SetActive(false);
        Debug.Log("SE recoloco eslinga "+this.gameObject.name);
    }
    public void activarFisicas()
    {
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("SE activo fisicas en eslinga " + this.gameObject.name);
    }
    public void Si_EslingaPick(bool si)
    {
        En_Mano = si;
    }
}
