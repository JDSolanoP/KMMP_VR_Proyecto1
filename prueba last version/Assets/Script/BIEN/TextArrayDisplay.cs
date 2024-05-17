using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextArrayDisplay : MonoBehaviour
{
    public TextMeshPro text3D;
    public string[] textArray; // Agrega tus strings al arreglo.
    public float totalTime = 1; // Cambia este valor a la cantidad total de segundos.

    private float timePerElement;
    private int currentIndex;
    private float timer;
    public bool iniciarTextArrayDisplay = false;
    public bool BanderaInicio = false;
    private void Start()
    {
        currentIndex = 0;
        timePerElement = totalTime / textArray.Length;
        timer = 0;
        //  UpdateDisplayText();
    }

    private void Update()
    {
        if (currentIndex < textArray.Length  && BanderaInicio == true)
        {
            timer += Time.deltaTime;

            if (timer >= timePerElement)
            {
                // Muestra el siguiente elemento en el arreglo.
                currentIndex++;
                if (currentIndex < textArray.Length)
                {
                    Debug.Log("Voltaje");
                    timer = 0;
                    UpdateDisplayText();
                }
            }
        }
        else
        {
            // Todos los elementos han sido mostrados.
            iniciarTextArrayDisplay = false;
        }
    }

    public void UpdateDisplayText()
    {
        // Actualiza el objeto de texto 3D con el texto actual del arreglo.
        if (currentIndex < textArray.Length)
        {
            Debug.Log(text3D.text);
            text3D.text = textArray[currentIndex];
        }
    }

    public void VoltajeContinuar(bool FlagInit)
    {
        BanderaInicio = FlagInit;
        UpdateDisplayText();
    }
}