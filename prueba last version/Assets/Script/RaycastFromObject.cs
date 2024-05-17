using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastFromObject : MonoBehaviour
{
    public float raycastDistance = 10f; // Distancia máxima del raycast
    public TextMesh resultText; // Referencia al objeto de texto en el que mostrarás el nombre del GameObject

    public Transform objetoGiratorio;
    public float velocidadRotacion = 10.0f;

    public Transform dial; // El dial que quieres que gire.

    private void Update()
    {
        // Lanza un raycast hacia adelante desde la posición del objeto
        RaycastHit hit;
        Vector3 raycastDirection = transform.right; // Dirección del raycast (hacia adelante)

        if (Physics.Raycast(transform.position, raycastDirection, out hit, raycastDistance))
        {
            // El raycast ha golpeado un objeto
            Debug.DrawRay(transform.position, raycastDirection * hit.distance, Color.green);

            // Muestra el nombre del objeto golpeado en el objeto de texto (UI Text)
            if (resultText != null)
            {
                Vector3 rotationAxis = Vector3.up;

                // Obtener la dirección hacia el objeto colisionador.
                Vector3 directionToColisionador = hit.collider.transform.position - dial.localPosition;

                // Calcular la rotación necesaria para mirar en esa dirección.
                Quaternion targetRotation = Quaternion.LookRotation(directionToColisionador, Vector3.up);

                // Interpolar suavemente hacia la nueva rotación.
                dial.rotation = Quaternion.Slerp(dial.rotation, targetRotation, velocidadRotacion * Time.deltaTime);
            }

            resultText.text = hit.collider.gameObject.name;
            }
        
        else
        {
            // El raycast no ha golpeado nada
            Debug.DrawRay(transform.position, raycastDirection * raycastDistance, Color.red);

            // Si no hay colisión, muestra un mensaje vacío en el objeto de texto (UI Text)
            if (resultText != null)
            {
                resultText.text = "";
            }
        }
    }
}


