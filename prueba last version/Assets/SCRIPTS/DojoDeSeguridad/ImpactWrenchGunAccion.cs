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
    //funcion 2 prueba
    /*public WheelCollider wc;//agregado 22-07-24
    public int torque;//agregado 22-07-24
    public float accel;*/


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
        //si_presionando = true;
        Press_Gatillo(true);

        //AudioManager.aSource.goFx("IWG_Pick",1,true,false);
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
                AudioManager.aSource.altoFxLoop("IWG_Rot01");////////////////////////////////////////////
                break;
                }
            if (velIniaux < 0)
            {
                AudioManager.aSource.altoFxLoop("IWG_Rot01");////////////////////////////////////////////
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
        AudioManager.aSource.goFx("IWG_Pick",0.5f,false,false);
        if (si_press == true)//*********************************************Agregado el 30-07-24
        {
            if (Cargada == true)
            {
                AudioManager.aSource.goFx("IWG_Rot01", 1, true, false);///////////////////////////////////////////////////02-08-24
            }
            
        }
        else
        {
            if (TmDojo.Boquilla_ContactoRefe == false)
            {
                AudioManager.aSource.altoFxLoop("IWG_Rot01");////////////////////////////////////////////02-08-24
            }
            else
            {
                Debug.Log("deteneindo IWG_ROT02 por perno sacado");
                AudioManager.aSource.altoFxLoop("IWG_Rot02");////////////////////////////////////////////02-08-24
                TmDojo.Boquilla_ContactoRefe = false;
            }
            
        }
        
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
    /*void Update()
    {
        if (si_presionando == true&&si_MaquinaPrendida==false)
        {
            accion(velDesaceleracion);
        }
    }
    void accion(float desaccel)
    {
        accel= Mathf.Clamp(accel-desaccel, -1, 1);
        float TorqueFinal = accel * torque;
        wc.motorTorque = TorqueFinal;

        Quaternion quat;
        Vector3 pos;
        wc.GetWorldPose(out pos, out quat);
        ObjRot.transform.position = pos;
        ObjRot.transform.rotation = quat;
    }*/
}

