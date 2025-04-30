using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarObjObjInspeccion : MonoBehaviour
{
    public TM_INSPECCIONCAMION tm_;
    public string n_obj;//ingresar nombre de obj a contactar;
    public int confirmarContacto;
    public bool Mas_de_1_Objeto = false;
    public bool PermiteOntriggerStay = false;
    public string[] nn_obj;//ingresar nombre de obj a contactar;
    //public bool para_grua = false;
    //public int nb_grua;
    //public bool si_verif_Obj_Perno;

    void OnTriggerEnter(Collider other)
    {
        if (Mas_de_1_Objeto == false)
        {
            if (other.gameObject.name == n_obj && PermiteOntriggerStay == false)
            {
                tm_.contacto_confirmado[confirmarContacto] = true;
                tm_.verificarContacto(confirmarContacto);
                Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
            }
        }
        else
        {
            for (int i = 0; i < nn_obj.Length; i++)
            {
                if (other.gameObject.name == nn_obj[i])
                {
                    
                    tm_.contacto_confirmado[confirmarContacto] = true;
                    tm_.verificarContacto(confirmarContacto);
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
                    tm_.contacto_confirmado[confirmarContacto] = true;
                    tm_.verificarContacto(confirmarContacto);
                    Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
                }
            }
            else
            {
                for (int i = 0; i < nn_obj.Length; i++)
                {
                    if (other.gameObject.name == nn_obj[i])
                    {
                        
                            tm_.contacto_confirmado[confirmarContacto] = true;
                            tm_.verificarContacto(confirmarContacto);
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
                tm_.contacto_confirmado[confirmarContacto] = false;
                tm_.verificarContacto(confirmarContacto);
                Debug.Log("verificando salida de contacto: " + confirmarContacto);
            }
        }
        else
        {
            for (int i = 0; i < nn_obj.Length; i++)
            {
                if (other.gameObject.name == nn_obj[i])
                {
                    tm_.contacto_confirmado[confirmarContacto] = false;
                    tm_.verificarContacto(confirmarContacto);
                    Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);

                    break;
                }
            }
        }
    }

}
