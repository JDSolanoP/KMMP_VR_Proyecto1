using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdherirObjeto3 : MonoBehaviour
{
    public string objetoATag = ""; // Etiqueta del objeto A
    private bool estaAdherido = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!estaAdherido && collision.gameObject.CompareTag(objetoATag))
        {
            // Hacer que el objeto A se adhiera al objeto B
            collision.transform.parent = transform;
            estaAdherido = true;
        }
    }
}