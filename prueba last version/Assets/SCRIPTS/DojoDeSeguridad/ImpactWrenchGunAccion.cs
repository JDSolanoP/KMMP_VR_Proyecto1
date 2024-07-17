using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ImpactWrenchGunAccion : MonoBehaviour
{
    public TM_DojoSeguridad TmDojo;
    public GameObject ObjRot;
    public float velInicial;
    public float TiempoIntervalo;
    public float velDesaceleracion;
    public bool Cargada;
    public bool conectada;
    public bool si_presionando;

    [Header("Objeto rota en que eje?")]
    public bool ejeX;
    public bool ejeY;
    public bool ejeZ;
    public float RoT0;
    public Vector3 RotInicial;
    public bool si_MaquinaPrendida;

    void Start()
    {
        //RoT0=ObjRot.transform.localEulerAngles.x;
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        //grabbable.activated.AddListener(activarGiro);
    }
    /*public void activarGiro(ActivateEventArgs arg)
    {
        Debug.Log("descargando "+this.gameObject.name);
        si_presionando = true;
        StartCoroutine(AccionRotBoquilla(true));
    }*/
    public void ActivarItem()
    {
        //Debug.Log("descargando por seleccion" + this.gameObject.name);
        si_presionando = true;
        StartCoroutine(AccionRotBoquilla());
    }
    public IEnumerator AccionRotBoquilla()
    {
        float tiempo=0;
        float velIniaux = velInicial;
        float Desaceaux = 0;
        if (si_MaquinaPrendida == true)//pregunta por la maquina
        {

            //Debug.Log("girando " + ObjRot.name + " velocidadInical = " + velIniaux);
            Desaceaux = 0;
            //Debug.Log("girando " + ObjRot.name + " desaceleracion = " + Desaceaux);
        }
        else
        {
            Desaceaux = velDesaceleracion;
            //Debug.Log("desaceleracion " + ObjRot.name + " desaceleracion = " + Desaceaux);
        }
        while (si_presionando == true&& Cargada == true)
            {
            float aux;//verificar la rotacion del adaptador
                if (si_presionando == false)
                {
                    break;
                }
            if (velIniaux < 0)
            {
                Cargada = false;
                TmDojo.PistolaDescargada = true;
                TmDojo.ObjConexionIWPCable.GetComponent<CapsuleCollider>().enabled = true;
                TmDojo.ObjConexionIWPCableRefe.SetActive(true);
                break;
            }
            if (ObjRot.transform.localEulerAngles.x > 180)
            {
                aux = ObjRot.transform.localEulerAngles.x - 360;
            }
            else
            {
                aux = ObjRot.transform.localEulerAngles.x;
            }

            if (ejeX == true)
                {
                ObjRot.transform.localEulerAngles += new Vector3(aux+ velIniaux-Desaceaux, 0, 0);
                //Debug.Log("ObjRot.transform.localEulerAngles.x = "+ ObjRot.transform.localEulerAngles);
                RoT0 = ObjRot.transform.localEulerAngles.x;
                RotInicial = ObjRot.transform.eulerAngles;
            }
                /*if (ejeY == true)
                {
                    ObjRot.transform.localEulerAngles = new Vector3(0, ObjRot.transform.localEulerAngles.y + velInicial-velDesaceleracion, 0);
                RoT0 = ObjRot.transform.localEulerAngles.y;
            }
                if (ejeZ == true)
                {
                    ObjRot.transform.localEulerAngles = new Vector3(0,0, ObjRot.transform.localEulerAngles.z + velInicial - velDesaceleracion);
                RoT0 = ObjRot.transform.localEulerAngles.z;
            }*/
                velIniaux=velIniaux-Desaceaux*tiempo;
            tiempo++;
            //Debug.Log("velInicial="+velInicial);
            //tiempo+=Time.deltaTime;

            yield return new WaitForSeconds(TiempoIntervalo);
            }

       
    }
    public void Press_Gatillo(bool si_press)
    {
        //Debug.Log(this.name + " esta presionando = " + si_press);
        si_presionando =si_press;
    }
    public void MaquinaON_OFF(bool prendida)
    {
        //Debug.Log("Maquina esta prendida = " + prendida);
        si_MaquinaPrendida =prendida;//true prendida
        if (si_MaquinaPrendida == true)
        {
            Cargada = true;
        }
    }
}

