using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Return_Pos0 : MonoBehaviour
{
    public Vector3 Pos0;
    public Vector3 auxPos0;
    public Vector3 Rot0;
    public Vector3 auxRot0;
    public Vector3 LocalPos0;
    public Vector3 LocalRot0;
    public bool enPos0=true;
    public string TagEspecial = "";
    public bool en_piso;
    public float tiempoDeEspera =500;
    public float contador;
    GameObject NivelDeReferencia;
    public bool usarAuxPos0=false;
    public bool inGravKinec = false;
    public bool ActivarMargenMov = false;
    public Vector3 margenDemovimiento;
    public bool si_otroSfx;
    public string nombreSfx;
    public bool Agarrable=false;//si dicho objeto se puede agarrar con mano
    public XRGrabInteractable grabInteractable;


    void Start()
    {
        if (ActivarMargenMov == false)
        {
            margenDemovimiento = new Vector3(100,100,100);
        }
        //NivelDeReferencia = GameObject.FindGameObjectWithTag("Ground");
        grabInteractable = this.GetComponent<XRGrabInteractable>();
        auxPos0 =Pos0=this.gameObject.transform.position;
        auxRot0=Rot0 = this.gameObject.transform.eulerAngles;
        LocalPos0 = this.gameObject.transform.localPosition;
        LocalRot0 = this.gameObject.transform.localEulerAngles;
    }

    void Update()
    {
        if (ActivarMargenMov)
        {
            if (this.gameObject.transform.position.y >= margenDemovimiento.y || this.gameObject.transform.position.y <= -margenDemovimiento.y)
            {
                reposicionObj();
            }
        }
    }
    void OnCollisionStay(Collision col)
    {
        //Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Ground")
        {
            if (grabInteractable.interactorsSelecting.Count > 0)
            {

            }
            else
            {

                reposicionObj();
                AudioManager.aSource.goFx("Fallo");
                en_piso = false;

                /*if (contador >= tiempoDeEspera)
                {

                    //contador = 0;
                }*/
            }
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
            if (grabInteractable.interactorsSelecting.Count > 0)
            {

            }
            else
            {
                if (other.GetComponent<BoxCollider>().isTrigger == true)
                {
                    if (si_otroSfx)
                    {
                        AudioManager.aSource.goFx(nombreSfx);
                    }
                    else
                    {
                        AudioManager.aSource.goFx("Fallo");
                    }
                }
                else
                {
                    AudioManager.aSource.goFx("Fallo");
                }
                reposicionObj();
                en_piso = false;
            }
        }
        else
        {
            en_piso = true;
        } 
    }
    public void reposicionObj()
    {
        //Debug.Log("REpos "+this.gameObject.name);
        enPos0= true;
        //this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (usarAuxPos0 == false) {
            this.gameObject.transform.position = Pos0;
            this.gameObject.transform.eulerAngles = Rot0;
        }
        else
        {
            this.gameObject.transform.position = auxPos0;
            this.gameObject.transform.eulerAngles = auxRot0;
        }
        
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        if (inGravKinec == false)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        
    }
}
