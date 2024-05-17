using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDisplay : MonoBehaviour
{
    public List<GameObject> gameObjectsList; // Lista de GameObjects a mostrar.
    private int a = 1; // Número "a" utilizado para dividir el tiempo.
    public float totalTime = 60.0f; // Cantidad total de segundos.
    public int contadorGameObjectDisplay;
    public bool iniciarGameObjectDisplay = false;

    private float timePerElement;
    private int currentIndex;
    private float timer;

    private void Start()
    {
        currentIndex = 0;
        timePerElement = totalTime;
        timer = 0;
        ActivateNextObject();
    }

    private void Update()
    {
        if (currentIndex < contadorGameObjectDisplay && iniciarGameObjectDisplay == true)
        {
            timer += Time.deltaTime;

            if (timer >= timePerElement)
            {
                // Muestra el siguiente GameObject en la lista.
                currentIndex++;
                timer = 0;
                ActivateNextObject();
            }
        }
        else
        {
            // Todos los GameObjects han sido mostrados.
        }
    }

    private void ActivateNextObject()
    {
        // Activa el GameObject actual en la lista.
        if (currentIndex < contadorGameObjectDisplay)
        {
            gameObjectsList[currentIndex].SetActive(true);
        }
    }
}