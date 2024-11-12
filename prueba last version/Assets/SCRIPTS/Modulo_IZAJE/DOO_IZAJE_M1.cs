using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOO_IZAJE_M1 : MonoBehaviour
{
    public TM_IZAJE_M1 tm_IZ;
    public string n_obj;//ingresar nombre de obj a contactar;
    public bool Mas_de_1_Objeto = false;
    public string[] nn_obj;//ingresar nombre de obj a contactar;
    public bool PermiteOntriggerStay = false;
    public bool permiteIntAux = false;
    public int confirmarContacto;
    public int aux;
    public bool permiteReposDespues = false;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + " entrando ");
        if (Mas_de_1_Objeto == false)
        {
            if (other.gameObject.name == n_obj && PermiteOntriggerStay == false)
            {

                if (permiteIntAux == true)
                {
                    tm_IZ.GlobalInt[0] = aux;
                }
                Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
                if (permiteReposDespues)
                {
                    this.gameObject.GetComponent<Return_Pos0>().inGravKinec = true;
                    this.gameObject.GetComponent<Return_Pos0>().reposicionObj();
                    Debug.Log(other.gameObject.name + " repos return_pos0 " + confirmarContacto + " contacto aux " + aux);
                }
                tm_IZ.contacto_confirmado[confirmarContacto] = true;
                tm_IZ.verificarContacto(confirmarContacto);
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
                    tm_IZ.contacto_confirmado[confirmarContacto] = true;
                    tm_IZ.verificarContacto(confirmarContacto);
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
                    tm_IZ.contacto_confirmado[confirmarContacto] = true;
                    tm_IZ.verificarContacto(confirmarContacto);
                    Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
                }
            }
            else
            {
                for (int i = 0; i < nn_obj.Length; i++)
                {
                    if (other.gameObject.name == nn_obj[i])
                    {
                        tm_IZ.contacto_confirmado[confirmarContacto] = true;
                        tm_IZ.verificarContacto(confirmarContacto);
                        Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto + " entrando ala zona final desde el stay");
                        break;
                    }
                }
            }
        }


    }
    void OnTriggerExit(Collider other)
    {
        if (Mas_de_1_Objeto == false)
        {
            if (other.gameObject.name == n_obj)
            {
                tm_IZ.contacto_confirmado[confirmarContacto] = false;
                tm_IZ.verificarContacto(confirmarContacto);
                Debug.Log("verificando salida de contacto: " + confirmarContacto);
            }
        }
        else
        {
            for (int i = 0; i < nn_obj.Length; i++)
            {
                if (other.gameObject.name == nn_obj[i])
                {
                    tm_IZ.contacto_confirmado[confirmarContacto] = false;
                    tm_IZ.verificarContacto(confirmarContacto);
                    Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
                }
            }
        }
    }
}
