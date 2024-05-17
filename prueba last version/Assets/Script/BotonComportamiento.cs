using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonComportamiento : MonoBehaviour
{
    public GameObject objeto1;
    public GameObject objeto2;

    private void Start()
    {
        // Desactivamos el segundo objeto al inicio
        objeto1.SetActive(false);
        objeto2.SetActive(false);
    }

    public void OnButtonClick()
    {
        print("hola");
        // Iniciamos una corrutina para mostrar y ocultar los objetos
        StartCoroutine(AlternarObjetos());
    }

    private IEnumerator AlternarObjetos()
    {
        // Mostrar el primer objeto por 0.5 segundos
        objeto1.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        // Ocultar el primer objeto por 0.5 segundos
        objeto1.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        // Mostrar el segundo objeto por 0.5 segundos
        objeto1.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        // Ocultar el primer objeto por 0.5 segundos
        objeto1.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        // Mostrar el segundo objeto por 0.5 segundos
        objeto1.SetActive(false);
        objeto2.SetActive(true);
        yield return new WaitForSeconds(0.5f);

    }
}