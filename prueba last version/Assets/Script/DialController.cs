using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialController : MonoBehaviour
{
    public Transform[] targetPositions; // Array de posiciones a las que se rotará
    public float rotationSpeed = 30f; // Velocidad de rotación en grados por segundo
    private int currentTargetIndex = 0; // Índice de la posición actual

    private bool isRotating = false; // Controla si el objeto está girando

    private void Update()
    {
        if (isRotating)
        {
            Transform currentTarget = targetPositions[currentTargetIndex];

            // Calcula la dirección de rotación hacia el objetivo
            Vector3 targetDirection = currentTarget.position - transform.position;

            // Calcula el ángulo entre la dirección actual y la dirección al objetivo
            float angleToTarget = Vector3.SignedAngle(transform.up, targetDirection.normalized, Vector3.forward);

            // Gira solo en el eje Z con velocidad constante
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, step);

            // Comprueba si el ángulo está cerca de cero para detenerse en el objetivo
            if (Mathf.Abs(angleToTarget) < step)
            {
                // Establece la rotación exacta para detenerse en el objetivo
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleToTarget + currentTarget.eulerAngles.z);
                isRotating = false;
            }
        }
    }

    public void GoToNextPosition()
    {
        if (!isRotating)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Length;
            isRotating = true;
        }
    }

    public void GoToPreviousPosition()
    {
        if (!isRotating)
        {
            currentTargetIndex = (currentTargetIndex - 1 + targetPositions.Length) % targetPositions.Length;
            isRotating = true;
        }
    }

    private void FixedUpdate()
    {
        // Lanza un rayo en el eje X desde la posición actual
        RaycastHit hit;
        Vector3 rayDirection = Vector3.right; // Dirección del rayo en el eje X
        float rayDistance = 1f; // Distancia máxima del rayo

        if (Physics.Raycast(transform.position, rayDirection, out hit, rayDistance))
        {
            // Si el rayo colisiona con una posición predefinida, detiene la rotación
            if (hit.collider.CompareTag("PositionMarker"))
            {
                isRotating = false;
                Debug.Log("Rayo colisionó con una posición predefinida. Deteniendo la rotación.");
            
            }
        }
    }
}