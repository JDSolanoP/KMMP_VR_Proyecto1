using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApareceryDesaparecer : MonoBehaviour
{
    public GameObject MensajeA;
    public GameObject MensajeB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MostrarMensaje()
    {
        MensajeA.SetActive(false);
        MensajeB.SetActive(true);
    }
}
