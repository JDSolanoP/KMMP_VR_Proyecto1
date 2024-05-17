using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverConRaycast : MonoBehaviour
{
    private Transform selectedObject;
    private bool isDragging = false;
    public string NombreColisionador;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<Rigidbody>() != null)
                {
                    selectedObject = hit.collider.transform;
                    isDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            selectedObject = null;
            isDragging = false;
        }

        if (isDragging && selectedObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, selectedObject.position);
            float distanceToPlane;

            if (plane.Raycast(ray, out distanceToPlane))
            {
                Vector3 newPosition = ray.GetPoint(distanceToPlane);

                // Verificar si está cerca de un objeto con el tag "Colisionador"
                Collider[] colliders = Physics.OverlapSphere(newPosition, 0.1f);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag(NombreColisionador))
                    {
                        newPosition = collider.ClosestPoint(newPosition);
                        break;
                    }
                }

                selectedObject.position = newPosition;
            }
        }
    }
}