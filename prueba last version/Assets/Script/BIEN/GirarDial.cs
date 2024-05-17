using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirarDial : MonoBehaviour
{
    //public string colliderBName = "ColliderB";

    //public float raycastDistance = 10f; // Distancia m�xima del raycast
    public Animator animador;
    public int animacionActual = 0; // Iniciar con la primera animaci�n
    bool flag = false;

    public void ComenzarAnim()
    {
            // Incrementa el n�mero de animaci�n
            animacionActual++;

            // Si alcanza el l�mite, vuelve a la primera animaci�n
            if (animacionActual > 4)
            {
                animacionActual = 2;
            }

            // Actualiza el valor de la variable de animaci�n en el Animator
            animador.SetInteger("Siguiente", animacionActual);
            flag = false;
            //Debug.Log(animacionActual);
        
    }

}

