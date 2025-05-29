using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorObjObjBloqueC930 : MonoBehaviour
{
    public TM_BloqueoC930E5 tm_;
    public string n_obj;//ingresar nombre de obj a contactar;
    public bool Mas_de_1_Objeto = false;
    public string[] nn_obj;//ingresar nombre de obj a contactar;
    public bool PermiteOntriggerStay = false;
    public bool permiteIntAux = false;
    public int confirmarContacto;
    public int aux;
    public bool permiteReposDespues = false;
    public bool permiteTmVerificar = true;

    void OnTriggerEnter(Collider other)
    {
        if (Mas_de_1_Objeto == false)
        {
            if (other.gameObject.name == n_obj && PermiteOntriggerStay == false)
            {

                if (permiteIntAux == true)
                {
                    tm_.contactoIntAux = aux;
                }
                Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto + " este obj :" + this.gameObject.name);
                if (permiteReposDespues)
                {
                    this.gameObject.GetComponent<Return_Pos0>().inGravKinec = true;
                    this.gameObject.GetComponent<Return_Pos0>().reposicionObj();
                    Debug.Log(other.gameObject.name + " repos return_pos0 " + confirmarContacto + " contacto aux " + aux);
                }
                tm_.contacto_confirmado[confirmarContacto] = true;
                if (permiteTmVerificar == true)
                {
                    tm_.verificarContacto(confirmarContacto);
                }

            }
        }
        else
        {
            for (int i = 0; i < nn_obj.Length; i++)
            {
                if (other.gameObject.name == nn_obj[i])
                {
                    Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto + " entrando ala zona final");
                    if (permiteReposDespues)
                    {
                        this.gameObject.GetComponent<Return_Pos0>().inGravKinec = true;
                        this.gameObject.GetComponent<Return_Pos0>().reposicionObj();
                    }
                    tm_.contacto_confirmado[confirmarContacto] = true;
                    if (permiteTmVerificar == true)
                    {
                        tm_.verificarContacto(confirmarContacto);
                    }
                    break;
                }
            }
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (PermiteOntriggerStay == true)
        {
            if (Mas_de_1_Objeto == false)
            {
                if (other.gameObject.name == n_obj)
                {
                    tm_.contacto_confirmado[confirmarContacto] = true;
                    if (permiteTmVerificar == true)
                    {
                        tm_.verificarContacto(confirmarContacto);
                        Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
                    }
                }
            }
            else
            {
                for (int i = 0; i < nn_obj.Length; i++)
                {
                    if (other.gameObject.name == nn_obj[i])
                    {
                        tm_.contacto_confirmado[confirmarContacto] = true;
                        if (permiteTmVerificar == true)
                        {
                            tm_.verificarContacto(confirmarContacto);
                            Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto + " entrando ala zona final desde el stay");
                        }
                        break;
                    }
                }
            }
        }


    }
    void OnTriggerExit(Collider other)
    {
        if (permiteIntAux == true)
        {
            tm_.contactoIntAux = aux;
        }
        if (Mas_de_1_Objeto == false)
        {
            if (other.gameObject.name == n_obj)
            {
                tm_.contacto_confirmado[confirmarContacto] = false;
                if (permiteTmVerificar == true)
                {
                    tm_.verificarContacto(confirmarContacto);
                    Debug.Log(gameObject.name+" verificando salida de contacto: " + confirmarContacto);
                }
            }
        }
        else
        {
            for (int i = 0; i < nn_obj.Length; i++)
            {
                if (other.gameObject.name == nn_obj[i])
                {
                    tm_.contacto_confirmado[confirmarContacto] = false;
                    if (permiteTmVerificar == true)
                    {
                        tm_.verificarContacto(confirmarContacto);
                        Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
                    }
                }
            }
        }
    }
}
