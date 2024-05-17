using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarColision : MonoBehaviour
{
    public GameObject objeto1;
    public GameObject objeto2;
    public bool EncendidoOno = false;

    private void Start()
    {
        // Desactivamos el segundo objeto al inicio
        objeto1.SetActive(false);
        objeto2.SetActive(false);
    }

    public void BotonPrenderPantalla()
    {
        EncendidoOno = true;
            StartCoroutine(AlternarObjetos());
    }

    public IEnumerator AlternarObjetos()
    {
        if(EncendidoOno == true)
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
            EncendidoOno = false;
            yield return new WaitForSeconds(0.5f);
        }

    }
}
