using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorOBJOBJIZAJEM2 : MonoBehaviour
{
    public TM_IZAJE_M2 tm_IZ_M2;
    public string n_obj;//ingresar nombre de obj a contactar;
    public bool Mas_de_1_Objeto = false;
    public string[] nn_obj;//ingresar nombre de obj a contactar;
    public bool PermiteOntriggerStay = false;
    public bool permiteIntAux = false;
    public int confirmarContacto;
    public int aux;
    public bool permiteReposDespues = false;
    public bool permiteTmVerificar=true;

    void OnTriggerEnter(Collider other)
    {
        if (Mas_de_1_Objeto == false)
        {
            if (other.gameObject.name == n_obj && PermiteOntriggerStay == false)
            {
                
                if (permiteIntAux == true)
                {
                    tm_IZ_M2.contactoIntAux=aux;
                }
                //Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
                if (permiteReposDespues)
                {
                    this.gameObject.GetComponent<Return_Pos0>().inGravKinec = true;
                    this.gameObject.GetComponent<Return_Pos0>().reposicionObj();
                    Debug.Log(other.gameObject.name + " repos return_pos0 " + confirmarContacto+ " contacto aux "+aux);
                }
                tm_IZ_M2.contacto_confirmado[confirmarContacto] = true;
                if(permiteTmVerificar == true)
                {
                    tm_IZ_M2.verificarContacto(confirmarContacto);
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
                        this.gameObject.GetComponent<Return_Pos0>().inGravKinec=true;
                        this.gameObject.GetComponent<Return_Pos0>().reposicionObj();
                    }
                    tm_IZ_M2.contacto_confirmado[confirmarContacto] = true;
                    if (permiteTmVerificar == true)
                    {
                        tm_IZ_M2.verificarContacto(confirmarContacto);
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
                    tm_IZ_M2.contacto_confirmado[confirmarContacto] = true;
                    if (permiteTmVerificar == true)
                    {
                        tm_IZ_M2.verificarContacto(confirmarContacto);
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
                        tm_IZ_M2.contacto_confirmado[confirmarContacto] = true;
                        if (permiteTmVerificar == true)
                        {
                            tm_IZ_M2.verificarContacto(confirmarContacto);
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
            tm_IZ_M2.contactoIntAux = aux;
        }
        if (Mas_de_1_Objeto == false)
        {
            if (other.gameObject.name == n_obj)
            {
                tm_IZ_M2.contacto_confirmado[confirmarContacto] = false;
                if (permiteTmVerificar == true)
                {
                    tm_IZ_M2.verificarContacto(confirmarContacto);
                    Debug.Log("verificando salida de contacto: " + confirmarContacto);
                }
            }
        }
        else
        {
            for (int i = 0; i < nn_obj.Length; i++)
            {
                if (other.gameObject.name == nn_obj[i])
                {
                    tm_IZ_M2.contacto_confirmado[confirmarContacto] = false;
                    if (permiteTmVerificar == true)
                    {
                        tm_IZ_M2.verificarContacto(confirmarContacto);
                        Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
                    }
                }
            }
        }
    }
}
