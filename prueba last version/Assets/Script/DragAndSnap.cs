using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndSnap : MonoBehaviour
{
    private bool isGrabbed = false;
    private Vector3 offset;

    private void Update()
    {
        if (!isGrabbed && Input.GetMouseButtonDown(0)) // Hacer clic para agarrar
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isGrabbed = true;
                    offset = transform.position - hit.point;
                }
            }
        }

        if (isGrabbed)
        {
            Vector3 mousePos = GetMouseWorldPos();
            transform.position = new Vector3(mousePos.x + offset.x, mousePos.y + offset.y, mousePos.z + offset.z);

            if (Input.GetMouseButtonUp(0)) // Soltar clic
            {
                isGrabbed = false;
            }
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}