using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BotonIniciar : MonoBehaviour
{
    public bool Estado = false;
    private TextArrayDisplay textArrayDisplay;
    private CountdownTimer countDownTimer;
    private GameObjectDisplay gameObjectDisplay;
    private GirarDial girarDial;
    private SelectorSeg selectorSeg;
    public GameObject panel4;

    public void Start()
    {
        textArrayDisplay = GetComponent<TextArrayDisplay>();
        countDownTimer = GetComponent<CountdownTimer>();
        gameObjectDisplay = GetComponent<GameObjectDisplay>();
        girarDial = GetComponent<GirarDial>();
        selectorSeg = GetComponent<SelectorSeg>();
    }

    public void Empezar()
    {
        Debug.Log("Estado int siguiente: " + girarDial.animacionActual + ", Estado de segundos: " + selectorSeg.indiceActual);
        panel4.SetActive(false);
       // if (girarDial.animacionActual == 1 && selectorSeg.indiceActual == 1)
       // {
            //countDownTimer.countdownTime = 60;
            textArrayDisplay.textArray = new string[] { "0", "0.1", "0.2", "0.3", "0.7", "1", "2", "5", "8", "10", "15", "35", "50",
                                                    "100", "101", "102", "106", "108", "112", "115", "117", "118", "119", "120"};
            textArrayDisplay.iniciarTextArrayDisplay = true;
            gameObjectDisplay.iniciarGameObjectDisplay = true;
            gameObjectDisplay.contadorGameObjectDisplay = 58;
        //}

       /*       
        else
        {
            countDownTimer.IniciarCountDown = true;
            textArrayDisplay.textArray = new string[] { "0.1", "0.2","0.3", "0.4", "0.45", "0.5", "0.55", "0.6", "0.7", "0.8", "0.9", "0.9" };
            textArrayDisplay.iniciarTextArrayDisplay = false;
            gameObjectDisplay.iniciarGameObjectDisplay = true;
            gameObjectDisplay.contadorGameObjectDisplay = 18;
        }*/
    }
}
