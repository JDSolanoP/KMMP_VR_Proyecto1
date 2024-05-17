using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioMaterialConBoton : MonoBehaviour
{/*
    public Material[] materiales;
    private Renderer rend;
    private int materialIndex = 0;
    public GameObject guante1;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = materiales[materialIndex];
    }

    public void CambiarMaterial()
    {
        guante1.SetActive(false);
        materialIndex = (materialIndex + 1) % materiales.Length;
        rend.material = materiales[materialIndex];

    }
}*/


    public Material[] materiales;
    public List<GameObject> objetosACambiar;
    private List<Renderer> renderers;
    private int materialIndex = 0;
    public GameObject guante1;
    public GameObject guante2;

    public GameObject Mensaje1;
    public GameObject Mensaje2;
    private void Start()
    {
        Mensaje1.SetActive(true);
        Mensaje2.SetActive(false);

        renderers = new List<Renderer>();

    foreach (GameObject objeto in objetosACambiar)
    {
        Renderer rend = objeto.GetComponent<Renderer>();
        if (rend != null)
        {
            renderers.Add(rend);
            rend.material = materiales[materialIndex];
        }
    }
}

public void CambiarMaterial()
{

        guante1.SetActive(false);

        guante2.SetActive(false);
        materialIndex = (materialIndex + 1) % materiales.Length;

    foreach (Renderer rend in renderers)
    {
        rend.material = materiales[materialIndex];
    }
        Mensaje1.SetActive(false);
        Mensaje2.SetActive(true);
    }
}