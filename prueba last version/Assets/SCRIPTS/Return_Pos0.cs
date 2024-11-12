using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Return_Pos0 : MonoBehaviour
{
    public Vector3 Pos0;
    public Vector3 Rot0;
    public Vector3 LocalPos0;
    public Vector3 LocalRot0;
    public bool enPos0=true;
    public string TagEspecial = "";
    public bool en_piso;
    public float tiempoDeEspera =500;
    public float contador;
    GameObject NivelDeReferencia;
    public bool inGravKinec = false;
    public bool ActivarMargenMov = false;
    public Vector3 margenDemovimiento;
    

    void Start()
    {
        if (ActivarMargenMov == false)
        {
            margenDemovimiento = new Vector3(100,100,100);
        }
        //NivelDeReferencia = GameObject.FindGameObjectWithTag("Ground");
        Pos0=this.gameObject.transform.position;
        Rot0=this.gameObject.transform.eulerAngles;
        LocalPos0 = this.gameObject.transform.localPosition;
        LocalRot0 = this.gameObject.transform.localEulerAngles;
    }

    void Update()
    {
        if (this.gameObject.transform.position.y >= margenDemovimiento.y|| this.gameObject.transform.position.y <= -margenDemovimiento.y) {
            reposicionObj();
        }
    }
    void OnCollisionStay(Collision col)
    {
        //Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Ground")
        {
            AudioManager.aSource.goFx("Fallo");
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
            AudioManager.aSource.goFx("Fallo");
            reposicionObj();
            en_piso = false;
        }
        else
        {
            en_piso = true;
        }
    }
    public void reposicionObj()
    {
        Debug.Log("REpos "+this.gameObject.name);
        enPos0= true;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.transform.position = Pos0;
        this.gameObject.transform.eulerAngles = Rot0;
        if (inGravKinec == true)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        
    }
}
