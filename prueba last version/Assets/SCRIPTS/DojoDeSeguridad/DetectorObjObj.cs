using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorObjObj : MonoBehaviour//PROPIO DE DOJO DE SEGURIDAD
{
    public TM_DojoSeguridad tm_Dojo;
    public string n_obj;//ingresar nombre de obj a contactar;
    public int confirmarContacto;
    public bool Mas_de_1_Objeto = false;
    public bool PermiteOntriggerStay = false;
    public string[] nn_obj;//ingresar nombre de obj a contactar;
    public bool para_grua = false;
    public int nb_grua;
    public bool si_verif_Obj_Perno;

    void OnTriggerEnter(Collider other)
    {
        if (Mas_de_1_Objeto == false)
        {
            if (other.gameObject.name == n_obj && PermiteOntriggerStay == false)
            {
                tm_Dojo.contacto_confirmado[confirmarContacto] = true;
                tm_Dojo.verificarContacto(confirmarContacto);
                Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
            }
        }
        else
        {
            for (int i = 0; i < nn_obj.Length; i++)
            {
                if (other.gameObject.name == nn_obj[i])
                {
                    if (si_verif_Obj_Perno == true)//USADO EN DOJO DE SEGURIDAD
                    {
                        Debug.Log("Detectado NPerno : " + nn_obj[i]);
                        if (tm_Dojo.PernoEnMano[i] == false)
                        {
                            Debug.Log(other.gameObject.name + " verificando no enMano y " + confirmarContacto + " entrando ala zona final");
                            tm_Dojo.verificarNombrePerno(nn_obj[i], true);
                        }
                        
                    }
                    tm_Dojo.contacto_confirmado[confirmarContacto] = true;
                    tm_Dojo.verificarContacto(confirmarContacto);
                    Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto+" entrando ala zona final");
                    break;
                }
            }
        }
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == n_obj&&PermiteOntriggerStay==true)
        {
            tm_Dojo.contacto_confirmado[confirmarContacto] = true;
            tm_Dojo.verificarContacto(confirmarContacto);
            //Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);


        }

    }
    void OnTriggerExit(Collider other)
    {
        if (Mas_de_1_Objeto == false)
        {
            if (other.gameObject.name == n_obj)
            {
                tm_Dojo.contacto_confirmado[confirmarContacto] = false;
                tm_Dojo.verificarContacto(confirmarContacto);
                Debug.Log("verificando salida de contacto: " + confirmarContacto);
            }
        }
        else
        {
            for (int i = 0; i < nn_obj.Length; i++)
            {
                if (other.gameObject.name == nn_obj[i])
                {
                    tm_Dojo.contacto_confirmado[confirmarContacto] = false;
                    tm_Dojo.verificarContacto(confirmarContacto);
                    Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
                    
                    if (si_verif_Obj_Perno == true)
                    {
                        Debug.Log("Detectado NPerno : " + nn_obj[i]+"saliendo de la zona final");
                        tm_Dojo.verificarNombrePerno(nn_obj[i], false);
                    }
                    break;
                }
            }
        }
    }

}
