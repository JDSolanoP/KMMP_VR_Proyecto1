using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarObjIzajeMod3 : MonoBehaviour
{
    public TM_IZAJE_M3 Tm_Izaje_M3;
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
                Tm_Izaje_M3.contacto_confirmado[confirmarContacto] = true;
                Tm_Izaje_M3.verificarContacto(confirmarContacto);
                Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);

                if (si_verif_Obj_Perno == true)
                {
                    Tm_Izaje_M3.verificarNombrePernoSingular();
                }
            }
            else if (other.gameObject.name == n_obj)
            {
                Tm_Izaje_M3.contacto_confirmado[confirmarContacto] = true;
                Tm_Izaje_M3.verificarContacto(confirmarContacto);
                Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);

                if (si_verif_Obj_Perno == true)
                {
                    Tm_Izaje_M3.verificarNombrePernoSingular();
                }
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
                        if (Tm_Izaje_M3.PernoEnMano[i] == false)
                        {
                            Debug.Log(other.gameObject.name + " verificando no enMano y " + confirmarContacto + " entrando ala zona final");
                            Tm_Izaje_M3.verificarNombrePerno(nn_obj[i], true);
                        }

                    }
                    Tm_Izaje_M3.contacto_confirmado[confirmarContacto] = true;
                    Tm_Izaje_M3.verificarContacto(confirmarContacto);
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
                    Tm_Izaje_M3.contacto_confirmado[confirmarContacto] = true;
                    Tm_Izaje_M3.verificarContacto(confirmarContacto);
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
                            //Debug.Log("Detectado NPerno : " + nn_obj[i]);
                            if (Tm_Izaje_M3.PernoEnMano[i] == false && !Tm_Izaje_M3.PernoEnDado)
                            {
                                Debug.Log(other.gameObject.name + " verificando no enMano y no en dado, " + confirmarContacto + " entrando ala zona final desde el stay");
                                Tm_Izaje_M3.verificarNombrePerno(nn_obj[i], true);
                                Tm_Izaje_M3.contacto_confirmado[confirmarContacto] = true;
                                Tm_Izaje_M3.verificarContacto(confirmarContacto);
                                Debug.Log(other.gameObject.name + " verificando en detector perno de obj contacto: " + confirmarContacto + " entrando ala zona final desde el stay");
                                break;
                            }

                        }
                        else
                        {
                            Tm_Izaje_M3.contacto_confirmado[confirmarContacto] = true;
                            Tm_Izaje_M3.verificarContacto(confirmarContacto);
                            Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto + " entrando ala zona final desde el stay");
                            break;
                        }

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
                Tm_Izaje_M3.contacto_confirmado[confirmarContacto] = false;
                Tm_Izaje_M3.verificarContacto(confirmarContacto);
                Debug.Log("verificando salida de contacto: " + confirmarContacto);
            }
        }
        else
        {
            for (int i = 0; i < nn_obj.Length; i++)
            {
                if (other.gameObject.name == nn_obj[i])
                {
                    Tm_Izaje_M3.contacto_confirmado[confirmarContacto] = false;
                    Tm_Izaje_M3.verificarContacto(confirmarContacto);
                    Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);

                    if (si_verif_Obj_Perno == true)
                    {
                        Debug.Log("Detectado NPerno : " + nn_obj[i] + "saliendo de la zona final");
                        Tm_Izaje_M3.verificarNombrePerno(nn_obj[i], false);
                    }
                    break;
                }
            }
        }
    }
}
