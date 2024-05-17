using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdherirObjeto1 : MonoBehaviour
{
    public string targetTag = "";
    public float adherenceDistance = 0.1f;

    private Transform targetTransform;
    private bool isAdhered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && !isAdhered)
        {
            targetTransform = other.transform;
            isAdhered = true;
        }
    }

    private void Update()
    {
        if (isAdhered && targetTransform != null)
        {
            // Calcula la posición deseada para adherir el objeto
            Vector3 desiredPosition = targetTransform.position + targetTransform.forward * adherenceDistance;

            // Actualiza la posición del objeto para que se adhiera a la posición del objeto B
            transform.position = desiredPosition;

            Debug.Log("Ingreso al Colisionador");
        }
    }
}