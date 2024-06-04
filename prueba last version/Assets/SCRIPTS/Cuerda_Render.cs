using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuerda_Render : MonoBehaviour
{
    public Vector3[] posCuerda;
    public Transform[] posiciones;
    public LineRenderer linea_R;

    void Update()
    {
        ConectarPuntosR();
        linea_R.SetPositions(posCuerda);
    }

    public void ConectarPuntosR()
    {
        //gancho.position = new Vector3(Mathf.Clamp(gancho.position.x, engancheInferior.position.x, engacheSuperior.position.x), gancho.position.y, Mathf.Clamp(gancho.position.z, engancheInferior.position.z, engacheSuperior.position.z));
        for (int i = 0; i < posiciones.Length; i++)
        {
            posCuerda[i] = posiciones[i].position;
        }

    }
}
