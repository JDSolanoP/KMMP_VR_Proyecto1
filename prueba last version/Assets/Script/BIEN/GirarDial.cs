using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirarDial : MonoBehaviour
{
    //public string colliderBName = "ColliderB";

    //public float raycastDistance = 10f; // Distancia máxima del raycast
    public Animator animador;
    public int animacionActual = 0; // Iniciar con la primera animación
    bool flag = false;

    public void ComenzarAnim()
    {
            // Incrementa el número de animación
            animacionActual++;

            // Si alcanza el límite, vuelve a la primera animación
            if (animacionActual > 4)
            {
                animacionActual = 2;
            }

            // Actualiza el valor de la variable de animación en el Animator
            animador.SetInteger("Siguiente", animacionActual);
            flag = false;
            //Debug.Log(animacionActual);
        
    }

}

