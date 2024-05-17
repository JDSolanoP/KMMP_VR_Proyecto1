using UnityEngine;

public class MedidorVoltaje : MonoBehaviour
{
    public float voltajeMaximo = 100f;
    public float voltajeActual = 0f;

    public GameObject aguja; // El objeto 3D que representa la aguja del medidor

    void Update()
    {
        // Aquí puedes simular cómo cambia el voltaje de la batería (por ejemplo, usando una batería virtual o algún otro método).
        // Asegúrate de mantener el valor de voltajeActual dentro del rango de 0 a voltajeMaximo.

        // El siguiente código es solo un ejemplo que cambia el valor del voltaje en el medidor con el tiempo.
        voltajeActual = Mathf.PingPong(Time.time * 10f, voltajeMaximo);

        // Gira la aguja del medidor de acuerdo al voltaje actual
        float angulo = -120f + (voltajeActual / voltajeMaximo) * 240f; // Mapea el voltaje al ángulo de rotación de la aguja
        aguja.transform.localRotation = Quaternion.Euler(angulo, 0f, 0f);
    }
}

