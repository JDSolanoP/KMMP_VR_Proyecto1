using UnityEngine;

public class Bocina : MonoBehaviour
{
    [SerializeField] private Horometro horometro;
    [SerializeField] private AudioManager audioManager;

    public void PlayBocina()
    {
        if (!horometro.GetIsPowerOn()) return;
        audioManager.goFx("Bocina_PC210LC", 1, false, false);
    }
}