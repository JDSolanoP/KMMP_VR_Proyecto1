using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshPro countdownText;
    public float countdownTime = 60.0f; // Cambia este valor a la cantidad de segundos que desees.
    public bool IniciarCountDown = false;
    private float currentTime;
    public bool BanderaInicio = false;
    public GameObject TextoFinal;

    private void Start()
    {
        currentTime = countdownTime;
        TextoFinal.SetActive(false);
    }

    private void Update()
    {
        if (currentTime > 0 && BanderaInicio == true)
        {
            currentTime -= Time.deltaTime;
            UpdateCountdownText();
        }
        else if (currentTime <= 0)
        {
            TextoFinal.SetActive(true);
        }
        Debug.Log(currentTime);
    }

    private void UpdateCountdownText()
    {
        // Actualiza el texto en el objeto TextMeshPro con el tiempo restante formateado.
        countdownText.text = Mathf.Ceil(currentTime).ToString();
    }
    public void TiempojeContinuar(bool FlagInit)
    {
        BanderaInicio = FlagInit;
        UpdateCountdownText();
    }
}