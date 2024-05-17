using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConteoRegresivo : MonoBehaviour
{
    public TextMeshPro countdownText; // Texto para mostrar el conteo regresivo
    public TextMeshPro VoltajeText;
    public string[] stringArray; // Arreglo de strings para mostrar
    public GameObject[] objectArray; // Arreglo de GameObjects para activar
    public int objectsToActivate; // Cantidad de objetos a activar (input)

    private float countdownTimer; // Contador de tiempo
    private int currentStringIndex; // Índice actual del arreglo de strings
    private int currentObjectIndex; // Índice actual del arreglo de GameObjects
    private float timePerString; // Tiempo asignado a cada string
    private float timePerObject; // Tiempo asignado a cada objeto

    void Start()
    {
        // Calcula el tiempo asignado a cada string
        timePerString = (float)stringArray.Length / 60f;

        // Calcula el tiempo asignado a cada objeto
        timePerObject = 60f / objectsToActivate;

        // Inicializa el contador de tiempo
        countdownTimer = 60f;
    }

    void Update()
    {
        // Actualiza el contador de tiempo
        countdownTimer -= Time.deltaTime;

        // Actualiza el texto del conteo regresivo
        countdownText.text = Mathf.RoundToInt(countdownTimer).ToString();

        // Muestra elementos del arreglo de strings de manera equitativa
        if (currentStringIndex < stringArray.Length && countdownTimer <= (stringArray.Length - currentStringIndex) * timePerString)
        {
            Debug.Log(stringArray[currentStringIndex]);
            currentStringIndex++;
        }

        // Activa objetos del arreglo de GameObjects de manera equitativa
        if (currentObjectIndex < objectArray.Length && countdownTimer <= (objectArray.Length - currentObjectIndex) * timePerObject)
        {
            objectArray[currentObjectIndex].SetActive(true);
            currentObjectIndex++;
        }
    }
}