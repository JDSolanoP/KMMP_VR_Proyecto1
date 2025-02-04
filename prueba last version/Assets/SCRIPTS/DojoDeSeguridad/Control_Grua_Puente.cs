using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Grua_Puente : MonoBehaviour
{
    public bool permiteGiroGancho = false;
    public GameObject ganchoRot;
    public float angulo;
    public int[] nBloqueoAux;
    [Header("Botones de Control_Grua")]
    public GameObject[] BTN_CTRL;
    public int numMod;
    public bool usar_globalFuncion;
    public int BoolG;
    public bool corneta_presionada;
    [Header("Elementos Puente_Grua")]
    public GameObject grua;
    public GameObject puente;
    public GameObject i_izq;
    public GameObject i_der;
    public GameObject gancho;

    public bool[] btn_controlgrua;
    public int botonPresionado;
    public Vector3 velo;
    [Header("LIMITES")]
    public Transform[] LimitesXYZ;//2 limites XYZ
    public Transform LimitX1;
    public Transform LimitX2;
    public Transform LimitZ1;
    public Transform LimitZ2;
    public Transform LimitY1;
    public Transform LimitY2;
    public Vector3 limiteMenor;
    public Vector3 limiteMayor;
    public float lx1;
    public float lx2;
    public float lz1;
    public float lz2;
    public float ly1;
    public float ly2;
    public float LimitY0;
    public bool on_press;
    //public int nBoton;
    public IEnumerator move1;
    public IEnumerator move2;
    public IEnumerator move3;
    public IEnumerator move4;
    public IEnumerator move5;
    public IEnumerator move6;
    public float velocidad;
    public float pos_Gruay;
    public bool enganchar;
    public bool[] bloqueo;
    public bool choque = false;
    public Vector3 limite_inferior;
    int moviendo;
    bool mano_derecha;

    // Start is called before the first frame update
    void Start()
    {
        if (ganchoRot != null)
        {
            angulo = ganchoRot.transform.localEulerAngles.y;
        }
        
        //LimitesXYZ = new Transform[2];
        bloqueo = new bool[6];
        LimitY0 = LimitY1.localPosition.y;
        lz1 = LimitZ1.transform.localPosition.z;
        lx1 = LimitX1.transform.localPosition.x;
        lz2 = LimitZ2.transform.localPosition.z;
        lx2 = LimitX2.transform.localPosition.x;
        ly1 = LimitY1.transform.localPosition.y;
        ly2 = LimitY2.transform.localPosition.y;
        if (lx1 < lx2)
        {
            LimitesXYZ[0].localPosition = new Vector3(lx1, LimitesXYZ[0].localPosition.y, LimitesXYZ[0].localPosition.z);
            LimitesXYZ[1].localPosition = new Vector3(lx2, LimitesXYZ[1].localPosition.y, LimitesXYZ[1].localPosition.z);
        }
        else
        {
            LimitesXYZ[0].localPosition = new Vector3(lx2, LimitesXYZ[0].localPosition.y, LimitesXYZ[0].localPosition.z);
            LimitesXYZ[1].localPosition = new Vector3(lx1, LimitesXYZ[1].localPosition.y, LimitesXYZ[1].localPosition.z);
        }
        if (lz1 < lz2)
        {
            LimitesXYZ[0].localPosition = new Vector3(LimitesXYZ[0].localPosition.x, LimitesXYZ[0].localPosition.y, lz1);
            LimitesXYZ[1].localPosition = new Vector3(LimitesXYZ[1].localPosition.x, LimitesXYZ[1].localPosition.y, lz2);
        }
        else
        {
            LimitesXYZ[0].localPosition = new Vector3(LimitesXYZ[0].localPosition.x, LimitesXYZ[0].localPosition.y, lz2);
            LimitesXYZ[1].localPosition = new Vector3(LimitesXYZ[1].localPosition.x, LimitesXYZ[1].localPosition.y, lz1);
        }
        if (ly1 < ly2)
        {
            LimitesXYZ[0].localPosition = new Vector3(LimitesXYZ[0].localPosition.x, ly1, LimitesXYZ[0].localPosition.z);
            LimitesXYZ[1].localPosition = new Vector3(LimitesXYZ[1].localPosition.x, ly2, LimitesXYZ[1].localPosition.z);
        }
        else
        {
            LimitesXYZ[0].localPosition = new Vector3(LimitesXYZ[0].localPosition.x, ly2, LimitesXYZ[0].localPosition.z);
            LimitesXYZ[1].localPosition = new Vector3(LimitesXYZ[1].localPosition.x, ly1, LimitesXYZ[1].localPosition.z);
        }
        limiteMenor = LimitesXYZ[0].localPosition;
        limiteMayor = LimitesXYZ[1].localPosition;

    }

    public void press(int nB)
    {
        AudioManager.aSource.goFx("Boton");//****************************************************sonido boton*****************************************
        switch (nB)
        {
            case 7://corneta
                if (btn_controlgrua[7] == true)
                {
                    int aux = 0;
                    AudioManager.aSource.goFx("Corneta_aviso", 1, true, false);

                    corneta_presionada = true;///////////////////////////////////////*******************************para tarea de revision si se presiono la corneta de aviso***************************
                }
                else
                {
                    AudioManager.aSource.altoFxLoop("Corneta_aviso");
                }
                //AudioManager.aSource.goFx("Corneta_aviso");

                //btn_controlgrua[nB] = true;
                break;
            case 0:
                if (puente.transform.localPosition.x >= LimitesXYZ[0].localPosition.x && bloqueo[0] == false)
                {
                    if (permiteGiroGancho)
                    {

                    }
                    corneta_presionada = false;
                    //moviendo = 0;
                    desplazamiento(0);
                    //StartCoroutine(move1);

                    Debug.Log("Hacia la adelante");//revisado 02-07-24 resto se usa el menor
                }
                else
                {
                    Debug.Log("Limite1 en x alcanzado bloqueo[0]=" + bloqueo[0]);
                    AudioManager.aSource.altoFxLoop("Sirena_grua");
                    // No_press();
                }
                break;
            case 1:
                if (puente.transform.localPosition.x <= LimitesXYZ[1].localPosition.x && bloqueo[1] == false)
                {
                    corneta_presionada = false;
                    //moviendo = 1;
                    desplazamiento(1);
                    //StartCoroutine(move2);
                    Debug.Log("Hacia la atras");// sumo con respecto a la posicion local por lotanto es el mayor
                }
                else
                {
                    Debug.Log("Limite2 en x alcanzado bloqueo[1]=" + bloqueo[1]);
                    AudioManager.aSource.altoFxLoop("Sirena_grua");
                    //No_press();
                }
                break;
            case 2:
                if (grua.transform.localPosition.z >= LimitesXYZ[0].localPosition.z && bloqueo[2] == false)
                {
                    corneta_presionada = false;
                    desplazamiento(2);
                    //StartCoroutine(move3);
                    Debug.Log("Hacia izquierda grua");
                }
                else
                {
                    Debug.Log("Limite1 en z alcanzado bloqueo[2]=" + bloqueo[2]);
                    AudioManager.aSource.altoFxLoop("Rieles_grua");
                    //No_press();
                }
                break;
            case 3:
                if (grua.transform.localPosition.z <= LimitesXYZ[1].localPosition.z && bloqueo[3] == false)
                {
                    corneta_presionada = false;
                    desplazamiento(3);
                    //StartCoroutine(move4);
                    Debug.Log("Hacia derecha grua");
                }
                else
                {
                    Debug.Log("Limite2 en z alcanzado bloqueo[3]" + bloqueo[3]);
                    AudioManager.aSource.altoFxLoop("Rieles_grua");
                    //No_press();
                }
                break;
            case 4:
                if (gancho.transform.localPosition.y <= LimitesXYZ[1].localPosition.y && bloqueo[4] == false)
                {
                    corneta_presionada = false;
                    desplazamiento(4);
                    //StartCoroutine(move5);
                    Debug.Log("Hacia arriba");
                }
                else
                {
                    Debug.Log("Limite1 en y alcanzado bloqueo[4]=" + bloqueo[4]);
                    AudioManager.aSource.altoFxLoop("Ascenso_grua");
                    //No_press();
                }
                break;
            case 5:
                if (gancho.transform.localPosition.y >= LimitesXYZ[0].localPosition.y && bloqueo[5] == false)
                {
                    corneta_presionada = false;
                    desplazamiento(5);
                    //StartCoroutine(move6); 
                    Debug.Log("Hacia abajo");
                }
                else
                {
                    Debug.Log("Limite2 en y alcanzado bloqueo[5]=" + bloqueo[5]);
                    AudioManager.aSource.altoFxLoop("Ascenso_grua");
                    //No_press();
                }
                break;
            case 6: enganchar = true; break;


                /*case 7:
                    enganchar = false;
                    lg1.transform.localPosition = new Vector3(lg1.transform.localPosition.x, LimitY0, lg1.transform.localPosition.z);
                    ly1 = lg1.transform.localPosition.y;
                    break;*/
        }
        if (corneta_presionada == true)///////////////////////////////////////*******************************para tarea de revision si se presiono la corneta de aviso***************************
        {

        }
    }
    public void paradaXbloqueo(int nMove)
    {
        if (nMove >= 0)
        {
            bloqueo[nMove] = true;
            Debug.Log("obstaculo en " + nMove);
        }
        else
        {
            for (int i = 0; i < bloqueo.Length; i++)
            {
                bloqueo[i] = false;
            }
        }
    }
    //***********************************************28-06-24**********************
    public void desplazamiento(int nbtn)
    {
        //Debug.Log("llamando a desplazamiento  " + nbtn);
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
                StartCoroutine(GruaMove(true));
                break;
            case 3:
                StartCoroutine(GruaMove(false));
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
        AudioManager.aSource.goFx("Sirena_grua", 1, true, false);
        int sentido = 0;
        while (btn_controlgrua[botonPresionado] == true)
        {
            if (btn_controlgrua[botonPresionado] == false)
            {
                AudioManager.aSource.altoFxLoop("Sirena_grua");//***************************AGREGADO EL 02-08-24**************************
                AudioManager.aSource.goFx("Corneta_freno");
                Debug.Log("alto puente");
                break;
            }
            if (si_adelante == true)//resto con respecto a localposition
            {
                if (permiteGiroGancho == false) {//**************************Agregado 16-01-25****verifica rotacion
                    if (bloqueo[0] == true)
                    {
                                       
                        AudioManager.aSource.goFx("4Puerta_Corrediza_Alto");//**************************Agregado 30-07-24****reemplazado el 31-07-24
                        AudioManager.aSource.altoFxLoop("Sirena_grua");//***************************AGREGADO EL 02-08-24**************************
                        Debug.Log("alto puente (bloqueo[0]=" + bloqueo[0]);
                        break;
                    }
                }
                else
                {
                    asignarBloqAux(hallarCuadrante(), 0); 
                    if (bloqueo[nBloqueoAux[0]]|| bloqueo[nBloqueoAux[1]])
                    {
                        AudioManager.aSource.goFx("4Puerta_Corrediza_Alto");//**************************Agregado 30-07-24****reemplazado el 31-07-24
                        AudioManager.aSource.altoFxLoop("Sirena_grua");//***************************AGREGADO EL 02-08-24**************************
                        Debug.Log("alto puente (bloqueo[nBloqueoAux[0]]=" + bloqueo[nBloqueoAux[0]]+ bloqueo[nBloqueoAux[1]]);
                        break;
                    }
                }
                 
                sentido = -1;
                if (puente.transform.localPosition.x >= LimitesXYZ[0].localPosition.x)
                {
                    //Debug.Log("moviendo "+sentido);
                    puente.transform.localPosition = new Vector3(puente.transform.localPosition.x + velo.x * sentido, puente.transform.localPosition.y, puente.transform.localPosition.z);
                    yield return new WaitForSeconds(0.01f);
                }
                else
                {
                    AudioManager.aSource.altoFxLoop("Sirena_grua");//***************************AGREGADO EL 02-08-24**************************
                    AudioManager.aSource.goFx("Corneta_freno");
                    Debug.Log(sentido + " " + puente.transform.localPosition.x + " en x Limite0 hacia adelante alcanzado puente bloqueo[0]=" + LimitesXYZ[0].localPosition.x + " " + bloqueo[0]);
                    break;
                }
            }
            else
            {
                if (permiteGiroGancho == false)
                {//**************************Agregado 16-01-25****verifica rotacion
                    if (bloqueo[1] == true)
                    {
                        AudioManager.aSource.goFx("4Puerta_Corrediza_Alto");//**************************Agregado 30-07-24****reemplazado el 31-07-24
                        AudioManager.aSource.altoFxLoop("Sirena_grua");//***************************AGREGADO EL 02-08-24**************************
                        Debug.Log("alto puente (bloqueo[1]=" + bloqueo[1]);
                        break;
                    }
                }
                else
                {
                    asignarBloqAux(hallarCuadrante(), 1);
                    if (bloqueo[nBloqueoAux[0]] || bloqueo[nBloqueoAux[1]])
                    {
                        AudioManager.aSource.goFx("4Puerta_Corrediza_Alto");//**************************Agregado 30-07-24****reemplazado el 31-07-24
                        AudioManager.aSource.altoFxLoop("Sirena_grua");//***************************AGREGADO EL 02-08-24**************************
                        Debug.Log("alto puente (bloqueo[nBloqueoAux[1]]=" + bloqueo[nBloqueoAux[0]] + bloqueo[nBloqueoAux[1]]);
                        break;
                    }
                }
                sentido = 1;
                if (puente.transform.localPosition.x <= LimitesXYZ[1].localPosition.x)
                {
                    //Debug.Log("moviendo "+sentido);
                    puente.transform.localPosition = new Vector3(puente.transform.localPosition.x + velo.x * sentido, puente.transform.localPosition.y, puente.transform.localPosition.z);
                    yield return new WaitForSeconds(0.01f);
                }
                else
                {
                    AudioManager.aSource.altoFxLoop("Sirena_grua");//***************************AGREGADO EL 02-08-24**************************
                    AudioManager.aSource.goFx("Corneta_freno");
                    Debug.Log(sentido + " Limite alcanzado puente bloqueo[1]=" + puente.transform.localPosition.x + ">= " + LimitesXYZ[1].localPosition.x + " o " + bloqueo[1]);
                    break;
                }
            }
        }
    }
    IEnumerator GruaMove(bool si_izquierda)
    {
        AudioManager.aSource.goFx("Rieles_grua", 1, true, false);
        int sentido = 0;
        while (btn_controlgrua[botonPresionado] == true)
        {
            if (btn_controlgrua[botonPresionado] == false)
            {
                Debug.Log("alto grua");
                break;
            }

            if (si_izquierda == true)
            {
                if (permiteGiroGancho == false)
                {
                    if (bloqueo[2] == true)
                    {
                        AudioManager.aSource.goFx("4Puerta_Corrediza_Alto");//**************************Agregado 30-07-24****reemplazado el 31-07-24
                        AudioManager.aSource.altoFxLoop("Rieles_grua");
                        AudioManager.aSource.goFx("Corneta_freno");
                        Debug.Log("alto grua bloqueo[2]=" + bloqueo[2]);
                        break;
                    } 
                }
                else
                {
                    asignarBloqAux(hallarCuadrante(), 2);
                    if (bloqueo[nBloqueoAux[0]] || bloqueo[nBloqueoAux[1]])
                    {
                        AudioManager.aSource.goFx("4Puerta_Corrediza_Alto");//**************************Agregado 30-07-24****reemplazado el 31-07-24
                        AudioManager.aSource.altoFxLoop("Rieles_grua");
                        AudioManager.aSource.goFx("Corneta_freno");
                        Debug.Log("alto grua bloqueo[nBloqueo[0]], bloqueo[nBloqueo[0]]=" + bloqueo[nBloqueoAux[0]] + bloqueo[nBloqueoAux[1]]);
                        break;
                    }
                }
                sentido = -1;
                if (grua.transform.localPosition.z >= LimitesXYZ[0].localPosition.z)
                {
                    //Debug.Log("moviendo " +sentido);
                    grua.transform.localPosition = new Vector3(grua.transform.localPosition.x, grua.transform.localPosition.y, grua.transform.localPosition.z + velo.z * sentido);
                    yield return new WaitForSeconds(0.01f);
                }
                else
                {
                    AudioManager.aSource.altoFxLoop("Rieles_grua");
                    AudioManager.aSource.goFx("Corneta_freno");

                    Debug.Log(grua.transform.localPosition.z + "Limite izquierdo grua alcanzado bloqueo[2]=" + LimitesXYZ[0].localPosition.z + " " + bloqueo[2]);
                    break;
                }
            }
            else
            {
                if (permiteGiroGancho == false)
                {
                    if (bloqueo[3] == true)
                    {
                        AudioManager.aSource.goFx("4Puerta_Corrediza_Alto");//**************************Agregado 30-07-24****reemplazado el 31-07-24
                        AudioManager.aSource.altoFxLoop("Rieles_grua");
                        AudioManager.aSource.goFx("Corneta_freno");
                        Debug.Log("alto grua bloqueo[3]=" + bloqueo[3]);
                        break;
                    }
                }
                else
                {
                    asignarBloqAux(hallarCuadrante(), 3);
                    if (bloqueo[nBloqueoAux[0]] || bloqueo[nBloqueoAux[1]])
                    {
                        AudioManager.aSource.goFx("4Puerta_Corrediza_Alto");//**************************Agregado 30-07-24****reemplazado el 31-07-24
                        AudioManager.aSource.altoFxLoop("Rieles_grua");
                        AudioManager.aSource.goFx("Corneta_freno");
                        Debug.Log("alto grua bloqueo[nBloqueo[0]], bloqueo[nBloqueo[0]]=" + bloqueo[nBloqueoAux[0]] + bloqueo[nBloqueoAux[1]]);
                        break;
                    }
                }
               
                sentido = 1;
                if (grua.transform.localPosition.z <= LimitesXYZ[1].localPosition.z)
                {

                    //Debug.Log("moviendo "+sentido);
                    grua.transform.localPosition = new Vector3(grua.transform.localPosition.x, grua.transform.localPosition.y, grua.transform.localPosition.z + velo.z * sentido);
                    yield return new WaitForSeconds(0.01f);
                }
                else
                {
                    AudioManager.aSource.altoFxLoop("Rieles_grua");
                    AudioManager.aSource.goFx("Corneta_freno");
                    Debug.Log(grua.transform.localPosition.z + "Limite derecho grua alcanzado grua bloqueo[3]= " + LimitesXYZ[1].localPosition.z + " " + bloqueo[3]);
                    break;
                }
            }
        }
    }
    IEnumerator GanchoMove(bool si_arriba)
    {
        AudioManager.aSource.goFx("Ascenso_grua", 1, true, false);
        int sentido = 0;
        while (btn_controlgrua[botonPresionado] == true)
        {
            if (btn_controlgrua[botonPresionado] == false)
            {
                AudioManager.aSource.altoFxLoop("Ascenso_grua");
                Debug.Log("alto gancho");
                break;
            }
            if (si_arriba == true)
            {
                if (bloqueo[4] == true)
                {
                    AudioManager.aSource.goFx("4Puerta_Corrediza_Alto");//**************************Agregado 30-07-24****reemplazado el 31-07-24
                    Debug.Log("alto gancho bloqueo[4]=" + bloqueo[4]);
                    AudioManager.aSource.altoFxLoop("Ascenso_grua");
                    break;
                }
                sentido = 1;
                if (gancho.transform.localPosition.y <= LimitesXYZ[1].localPosition.y)
                {
                    //Debug.Log("moviendo");
                    gancho.transform.localPosition = new Vector3(gancho.transform.localPosition.x, gancho.transform.localPosition.y + velo.y * sentido, gancho.transform.localPosition.z);
                    yield return new WaitForSeconds(0.01f);
                }
                else
                {
                    AudioManager.aSource.altoFxLoop("Ascenso_grua");
                    Debug.Log(sentido + "+Limite alcanzado gancho bloqueo[4]=" + bloqueo[4]);

                    break;
                }

            }
            else
            {
                if (bloqueo[5] == true)
                {
                    AudioManager.aSource.goFx("4Puerta_Corrediza_Alto");//**************************Agregado 30-07-24****reemplazado el 31-07-24
                    Debug.Log("alto gancho bloqueo[5]=" + bloqueo[5]);
                    AudioManager.aSource.altoFxLoop("Ascenso_grua");
                    break;
                }
                sentido = -1;
                if (gancho.transform.localPosition.y >= LimitesXYZ[0].localPosition.y)
                {

                    //Debug.Log("moviendo");
                    gancho.transform.localPosition = new Vector3(gancho.transform.localPosition.x, gancho.transform.localPosition.y + velo.y * sentido, gancho.transform.localPosition.z);
                    yield return new WaitForSeconds(0.01f);
                }
                else
                {
                    AudioManager.aSource.altoFxLoop("Ascenso_grua");
                    Debug.Log(sentido + " Limite alcanzado gancho bloqueo[5]=" + bloqueo[5]);
                    break;
                }
            }
        }
    }
    public void disponiblesOtrosBTN(int btn, bool siAct)
    {
        for (int i = 0; i < BTN_CTRL.Length; i++)
        {
            if (i != btn)
            {
                BTN_CTRL[i].SetActive(siAct);
            }
        }
    }
//**************************************************HALLAR CUADRANTE Y BLOQUEO RESPECTIVO A LA ROTACION DE LA CARGA ---------- 16-01-25------***************
    public int hallarCuadrante()
    {
        angulo = ganchoRot.transform.localEulerAngles.y;
        while (angulo < 0)//volver un angulo medible
        {
            angulo = angulo + 360;
        }
        while (angulo > 360)
        {
            angulo = angulo - 360;
        }
        int cd = 0;
        while (cd == 0)
        {
            if (angulo > 0 && angulo < 90) { cd = 1; break; }
            if (angulo > 90 && angulo < 180) { cd = 2; break; }
            if (angulo > 180 && angulo < 270) { cd = 3; break; }
            if (angulo > 270 && angulo < 360) { cd = 4; break; }
            if (angulo == 0) { cd = -1; break; }
            if (angulo == 90) { cd = -2; break; }
            if (angulo == 180) { cd = -3; break; }
            if (angulo == 270) { cd = -4; break; }
        }
        return cd;
    }
    public int getAdyaIzq(int bloq)
    {
        switch (bloq)
        {
            case 0: bloq = 2; break;
            case 3: bloq = 0; break;
            case 1: bloq = 3; break;
            case 2: bloq = 1; break;
        }
        return bloq;
    }
    public int getAdyaDer(int bloq)
    {
        switch (bloq)
        {
            case 0: bloq = 3; break;
            case 3: bloq = 1; break;
            case 1: bloq = 2; break;
            case 2: bloq = 0; break;
        }
        return bloq;
    }
    public int getOpuesto(int bloq)
    {
        switch (bloq)
        {
            case 0: bloq = 1; break;
            case 3: bloq = 2; break;
            case 1: bloq = 0; break;
            case 2: bloq = 3; break;
        }
        return bloq;
    }
    public void asignarBloqAux(int cuadrante,int bloq)
    {
        switch (cuadrante)
        {
            case 1:
                nBloqueoAux[0] = bloq;
                nBloqueoAux[1] = getAdyaIzq(bloq);
                break;
            case 2:
                nBloqueoAux[1] = getOpuesto(bloq);
                nBloqueoAux[0] = getAdyaIzq(bloq);
                break;
            case 3:
                nBloqueoAux[1] = getOpuesto(bloq);
                nBloqueoAux[0] = getAdyaDer(bloq);
                break;
            case 4:
                nBloqueoAux[0] = bloq;
                nBloqueoAux[1] = getAdyaDer(bloq);
                break;
            case -1: nBloqueoAux[0] =nBloqueoAux[1] = bloq;break;
            case -2: nBloqueoAux[0] = nBloqueoAux[1] = getAdyaIzq(bloq); break;
            case -3: nBloqueoAux[0] = nBloqueoAux[1] = getOpuesto(bloq); break;
            case -4: nBloqueoAux[0] = nBloqueoAux[1] = getAdyaDer(bloq); break;
        }
    }
}