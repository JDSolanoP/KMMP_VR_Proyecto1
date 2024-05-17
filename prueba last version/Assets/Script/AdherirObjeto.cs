using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdherirObjeto : MonoBehaviour
{
    public string collisionTag = "Colisionador";
    public float adherenceDistance = 0.1f;

    private bool isAdhered = false;
    private Transform adheredTransform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(collisionTag) && !isAdhered)
        {
            isAdhered = true;
            adheredTransform = collision.transform;

            // Desactivar el componente Rigidbody para que el objeto se adhiera
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }

    private void Update()
    {
        if (isAdhered && adheredTransform != null)
        {
            // Actualizar la posición del objeto para adherirse a la posición del colisionador
            Vector3 desiredPosition = adheredTransform.position + adheredTransform.forward * adherenceDistance;
            transform.position = desiredPosition;
        }
    }
}
