using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectorSeg : MonoBehaviour
{
    public string[] valores; // Arreglo de valores que deseas seleccionar.
    public int indiceActual = 0; // �ndice actual del arreglo.
    public  TextMeshPro texto3D; // Referencia al componente TextMeshPro

    public void SeleccionarSegundos()
    { 
            // Aseg�rate de que el �ndice est� dentro de los l�mites del arreglo.
            if (indiceActual < valores.Length)
            {
                Debug.Log("Valor seleccionado: " + valores[indiceActual]);
                texto3D.text = valores[indiceActual];
                indiceActual++; // Avanza al siguiente valor en el arreglo.

             // Puedes reiniciar el �ndice si deseas volver a empezar.
            if (indiceActual >= valores.Length)
            {
                    indiceActual = 0;
            }
            }
    }    
}