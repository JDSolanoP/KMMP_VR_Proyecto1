using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnClick : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Velocidad de rotación en grados por segundo

    private bool isRotating = false;
    private Vector3 initialMousePosition;

    private void Update()
    {
        if (isRotating)
        {
            float mouseX = Input.mousePosition.x;
            float deltaY = mouseX - initialMousePosition.x;
            float rotationAmount = deltaY * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.forward, rotationAmount);

            initialMousePosition = new Vector3(mouseX, initialMousePosition.y, initialMousePosition.z);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) // Si haces clic con el botón izquierdo del ratón
        {
            isRotating = true;
            initialMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) // Si sueltas el botón izquierdo del ratón
        {
            isRotating = false;
        }
    }

    private void OnMouseExit()
    {
        isRotating = false;
    }
}