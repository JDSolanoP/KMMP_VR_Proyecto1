using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionPuenteGrua : MonoBehaviour//esta en el control de la grua
{
    public GameObject puenteGrua;
    public GameObject grua;
    public GameObject gancho;
    public Transform[] LimitesXYZ;//2 limites XYZ
    public bool[] btn_controlgrua;
    public int botonPresionado;
    public Vector3 velocidad;
    public void desplazamiento(int nbtn)
    {Debug.Log("llamando a desplazamiento  " + nbtn);
        botonPresionado = nbtn;
        switch (nbtn)
        {
            case 0:
                StartCoroutine(PuenteGruaMove(true));
                break;
            case 1:
                StartCoroutine(PuenteGruaMove(false));
                break;
            case 2:
                StartCoroutine(PuenteGruaMove(true));
                break;
            case 3:
                StartCoroutine(PuenteGruaMove(false));
                break;
            case 4:
                StartCoroutine(GanchoMove(true));
                break;
            case 5:
                StartCoroutine(GanchoMove(false));
                break;
        }
    }
    IEnumerator PuenteGruaMove(bool si_adelante) 
    {
        int sentido = 0;
        if (si_adelante == true)
        {
            sentido = 1;
        }
        else { sentido = -1; }
        while (btn_controlgrua[botonPresionado] == true)
        {
            if (puenteGrua.transform.position.x >= LimitesXYZ[0].position.x&& puenteGrua.transform.position.x <= LimitesXYZ[1].position.x)
            {
                if (btn_controlgrua[botonPresionado] == false)
                {
                    Debug.Log("alto grua");
                    break;
                }
                Debug.Log("moviendo");
                puenteGrua.transform.localPosition = new Vector3(puenteGrua.transform.localPosition.x + velocidad.x * sentido, puenteGrua.transform.localPosition.y, puenteGrua.transform.localPosition.z);
                yield return new WaitForSecondsRealtime(0.1f);
            }
            
        }
    }
    IEnumerator GruaMove(bool si_izquierda)
    {
        int sentido = 0;
        if (si_izquierda == true)
        {
            sentido = 1;
        }
        else { sentido = -1; }
        while (btn_controlgrua[botonPresionado] == true)
        {
            if (puenteGrua.transform.position.z >= LimitesXYZ[0].position.z && puenteGrua.transform.position.z <= LimitesXYZ[1].position.z)
            {
                if (btn_controlgrua[botonPresionado] == false)
                {
                    Debug.Log("alto grua");
                    break;
                }
                Debug.Log("moviendo");
                grua.transform.localPosition = new Vector3(puenteGrua.transform.localPosition.x, puenteGrua.transform.localPosition.y, puenteGrua.transform.localPosition.z + velocidad.x * sentido);
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
    }
    IEnumerator GanchoMove(bool si_abajo)
    {
        int sentido = 0;
        if (si_abajo == true)
        {
            sentido = 1;
        }
        else { sentido = -1; }
        while (btn_controlgrua[botonPresionado] == true)
        {
            if (puenteGrua.transform.position.y >= LimitesXYZ[0].position.y && puenteGrua.transform.position.y <= LimitesXYZ[1].position.y)
            {
                if (btn_controlgrua[botonPresionado] == false)
                {
                    Debug.Log("alto grua");
                    break;
                }
                Debug.Log("moviendo");
                gancho.transform.localPosition = new Vector3(puenteGrua.transform.localPosition.x, puenteGrua.transform.localPosition.y + velocidad.x * sentido, puenteGrua.transform.localPosition.z + velocidad.x * sentido);
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
    }
    public void Soltar_Boton()
    {
        btn_controlgrua[botonPresionado] = false;
    }
}
