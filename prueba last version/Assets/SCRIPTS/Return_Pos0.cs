using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Return_Pos0 : MonoBehaviour
{
    public Vector3 Pos0;
    public Vector3 Rot0;
    public string TagEspecial = "";
    public bool en_piso;
    public float tiempoDeEspera =500;
    public float contador;
    GameObject NivelDeReferencia;

    void Start()
    {
        //NivelDeReferencia = GameObject.FindGameObjectWithTag("Ground");
        Pos0=this.gameObject.transform.position;
        Rot0=this.gameObject.transform.localEulerAngles;
    }

    void OnCollisionStay(Collision col)
    {
        //Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Ground")
        {
            reposicionObj();
            en_piso = false;
           
            /*if (contador >= tiempoDeEspera)
            {

                //contador = 0;
            }*/
        }
        else
        {
            en_piso = false;
            
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            reposicionObj();
            en_piso = false;
        }
        else
        {
            en_piso = true;
        }
    }
    void reposicionObj()
    {
        this.gameObject.transform.position = Pos0;
        this.gameObject.transform.localEulerAngles = Rot0;
    }
}
