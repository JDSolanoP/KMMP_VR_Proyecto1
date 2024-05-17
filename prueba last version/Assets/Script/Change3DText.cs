using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change3DText : MonoBehaviour
{
    public TextMesh timeDisplay;
    private int[] durations = { 3, 10, 15 };
    private int currentIndex = 0;
    private bool isCounting = false;

    private void Start()
    {
        UpdateTimeDisplay();
    }

    public void OnTimeButtonClick()
    {
        currentIndex = (currentIndex + 1) % durations.Length;
        UpdateTimeDisplay();
    }

    public void OnStartButtonClick()
    {
        if (!isCounting)
        {
            isCounting = true;
            StartCoroutine(Countdown());
        }
    }

    private System.Collections.IEnumerator Countdown()
    {
        int remainingTime = durations[currentIndex];

        while (remainingTime > 0)
        {
            timeDisplay.text = remainingTime.ToString() + " seg";
            yield return new WaitForSeconds(1);
            remainingTime--;
        }

        isCounting = false;
        UpdateTimeDisplay();
    }

    private void UpdateTimeDisplay()
    {
        timeDisplay.text = durations[currentIndex] + " seg";
    }
}