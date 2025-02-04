using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_teclado : MonoBehaviour
{
    public TM_Lobby tml;

    //public GameObject[] dedos;
    public string n_obj;//ingresar nombre de obj a contactar;
    public int confirmarContacto;
    public bool Mas_de_1_Objeto = false;
    public bool PermiteOntriggerStay = false;
    public string[] nn_obj;//ingresar nombre de obj a contactar;

    public bool usarAux;
    public int auxContac;



    void OnTriggerEnter(Collider other)
    {
        if (Mas_de_1_Objeto == false)
        {
            if (other.gameObject.name == n_obj && PermiteOntriggerStay == false)
            {
                if (usarAux == true)
                {
                    tml.auxcontacto = auxContac;
                }
                tml.contacto_confirmado[confirmarContacto] = true;
                tml.verificarContacto(confirmarContacto);
                Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);

            }
        }
        else
        {
            for (int i = 0; i < nn_obj.Length; i++)
            {
                if (other.gameObject.name == nn_obj[i])
                {
                    if (usarAux == true)
                    {
                        tml.auxcontacto = auxContac;
                    }

                    tml.contacto_confirmado[confirmarContacto] = true;
                    tml.verificarContacto(confirmarContacto);
                    //Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto + " entrando ala zona final");
                }
            }
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == n_obj && PermiteOntriggerStay == true)
        {
            if (usarAux == true)
            {
                tml.auxcontacto = auxContac;
            }
            tml.contacto_confirmado[confirmarContacto] = true;
            tml.verificarContacto(confirmarContacto);
            //Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (Mas_de_1_Objeto == false)
        {
            if (other.gameObject.name == n_obj)
            {
                if (usarAux == true)
                {
                    tml.auxcontacto = auxContac;
                }
                tml.contacto_confirmado[confirmarContacto] = false;
                tml.verificarContacto(confirmarContacto);
                Debug.Log("verificando salida de contacto: " + confirmarContacto);
            }
        }
        else
        {
            for (int i = 0; i < nn_obj.Length; i++)
            {
                if (other.gameObject.name == nn_obj[i])
                {
                    if (usarAux == true)
                    {
                        tml.auxcontacto = auxContac;
                    }
                    tml.contacto_confirmado[confirmarContacto] = false;
                    tml.verificarContacto(confirmarContacto);
                    Debug.Log(other.gameObject.name + " verificando contacto: " + confirmarContacto);

                    break;
                }
            }
        }
    }
}
