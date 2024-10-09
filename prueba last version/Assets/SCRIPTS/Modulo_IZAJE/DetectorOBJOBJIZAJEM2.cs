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


    void OnTriggerEnter(Collider other)
    {
        if (Mas_de_1_Objeto == false)
        {
            if (other.gameObject.name == n_obj && PermiteOntriggerStay == false)
            {
                tm_IZ_M2.contacto_confirmado[confirmarContacto] = true;
                tm_IZ_M2.verificarContacto(confirmarContacto);
                if (permiteIntAux == true)
                {
                    tm_IZ_M2.contactoIntAux=aux;
                }
                Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
            }
        }
        else
        {
            for (int i = 0; i < nn_obj.Length; i++)
            {
                if (other.gameObject.name == nn_obj[i])
                {

                    tm_IZ_M2.contacto_confirmado[confirmarContacto] = true;
                    tm_IZ_M2.verificarContacto(confirmarContacto);
                    Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto + " entrando ala zona final");
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
                    tm_IZ_M2.verificarContacto(confirmarContacto);
                    Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
                }
            }
            else
            {
                for (int i = 0; i < nn_obj.Length; i++)
                {
                    if (other.gameObject.name == nn_obj[i])
                    {
                        tm_IZ_M2.contacto_confirmado[confirmarContacto] = true;
                        tm_IZ_M2.verificarContacto(confirmarContacto);
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
                tm_IZ_M2.contacto_confirmado[confirmarContacto] = false;
                tm_IZ_M2.verificarContacto(confirmarContacto);
                Debug.Log("verificando salida de contacto: " + confirmarContacto);
            }
        }
        else
        {
            for (int i = 0; i < nn_obj.Length; i++)
            {
                if (other.gameObject.name == nn_obj[i])
                {
                    tm_IZ_M2.contacto_confirmado[confirmarContacto] = false;
                    tm_IZ_M2.verificarContacto(confirmarContacto);
                    Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
                }
            }
        }
    }
}
