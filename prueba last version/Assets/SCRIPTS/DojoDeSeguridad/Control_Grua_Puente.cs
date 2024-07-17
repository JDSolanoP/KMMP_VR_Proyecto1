using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Grua_Puente : MonoBehaviour { 
public GameObject grua;
public GameObject puente;
public GameObject i_izq;
public GameObject i_der;
public GameObject gancho;
    public Transform[] LimitesXYZ;//2 limites XYZ
    public bool[] btn_controlgrua;
    public int botonPresionado;
    public Vector3 velo;

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
/*private void Update()
{
    pos_Gruay = gancho.transform.localPosition.y;
}*/
public void press(int nB)
{
    //Debug.Log("presiono " + nB);

    switch (nB)
    {
        case 0:
            if (puente.transform.localPosition.x >= LimitesXYZ[0].localPosition.x && bloqueo[0] == false)
            {
                //moviendo = 0;
                    desplazamiento(0);
                //StartCoroutine(move1);
                Debug.Log("Hacia la adelante");//revisado 02-07-24 resto se usa el menor
            }
            else
            {
                Debug.Log("Limite1 en x alcanzado bloqueo[0]="+bloqueo[0]);
               // No_press();
            }
            break;
        case 1:
            if (puente.transform.localPosition.x <= LimitesXYZ[1].localPosition.x && bloqueo[1] == false)
            {
                //moviendo = 1;
                    desplazamiento(1);
                    //StartCoroutine(move2);
                    Debug.Log("Hacia la atras");// sumo con respecto a la posicion local por lotanto es el mayor
            }
            else
            {
                Debug.Log("Limite2 en x alcanzado bloqueo[1]=" + bloqueo[1]);
                //No_press();
            }
            break;
        case 2:
            if (grua.transform.localPosition.z >= LimitesXYZ[0].localPosition.z && bloqueo[2] == false)
            {
                    desplazamiento(2);
                    //StartCoroutine(move3);
                    Debug.Log("Hacia izquierda grua");
            }
            else
            {
                Debug.Log("Limite1 en z alcanzado bloqueo[2]=" + bloqueo[2]);
                //No_press();
            }
            break;
        case 3:
            if (grua.transform.localPosition.z <= LimitesXYZ[1].localPosition.z && bloqueo[3] == false)
            {
                    desplazamiento(3);
                    //StartCoroutine(move4);
                    Debug.Log("Hacia derecha grua");
            }
            else
            {
                Debug.Log("Limite2 en z alcanzado bloqueo[3]" + bloqueo[3]);
                //No_press();
            }
            break;
        case 4:
            if (gancho.transform.localPosition.y <= LimitesXYZ[1].localPosition.y && bloqueo[4] == false)
            {
                    desplazamiento(4);
                    //StartCoroutine(move5);
                    Debug.Log("Hacia arriba");
            }
            else
            {
                Debug.Log("Limite1 en y alcanzado bloqueo[4]=" + bloqueo[4]);
                //No_press();
            }
            break;
        case 5:
            if (gancho.transform.localPosition.y >= LimitesXYZ[0].localPosition.y && bloqueo[5] == false)
            {
                    desplazamiento(5);
                    //StartCoroutine(move6); 
                    Debug.Log("Hacia abajo");
            }
            else
            {
                Debug.Log("Limite2 en y alcanzado bloqueo[5]=" + bloqueo[5]);
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

}
/*IEnumerator plat_move(bool en_z, int dir)
{
    while (on_press == true)
    {
            if (en_z == true)
                {

                    grua.transform.localPosition += movex * dir * velocidad * 0.1f; Debug.Log("enZ");
                    yield return new WaitForSeconds(0.01f);
                    if (dir < 0)
                    {
                        if (grua.transform.localPosition.z >= lx2) { 
                            StartCoroutine(move1);
                        }
                        else { No_press(); }
                    } else { 
                        if (dir > 0) { if (grua.transform.localPosition.z <= lx1)
                            { StartCoroutine(move2); }else { No_press(); } } 
                    }


                }
            else
            {
                puente.transform.localPosition += movez * dir*velocidad * 0.1f; Debug.Log("enZ"); ;
                    yield return new WaitForSeconds(0.01f);
                    if (dir < 0) {
                        if (puente.transform.localPosition.x >= lz1)
                        {
                            StartCoroutine(move3); Debug.Log("Hacia adelante");
                        }
                        else
                        {
                            Debug.Log("Limite1 en z alcanzado");
                            No_press();
                        }
                    } else { if (dir > 0)
                        {
                            if (puente.transform.localPosition.x <= lz2)
                            {
                                StartCoroutine(move4); Debug.Log("Hacia atras");
                            }
                            else
                            {
                                Debug.Log("Limite2 en z alcanzado");
                                No_press();
                            }
                        }
                    }
            }
            yield return new WaitForSeconds(0.01f);
        }
}*/
/*IEnumerator plat_move(string movil, int dir)
{
    while (on_press == true)
    {
        switch (movil)
        {
            case "gancho":
                    if (on_press == false) { break; }
                if (dir < 0)
                {
                    if (choque == true)
                    {
                        bloqueo[4] = true;//colision por debajo del obj
                        //objIz.Abajo = true;
                    }
                    else
                    {
                        if (gancho.transform.localPosition.y >= ly1 && bloqueo[4] == false)
                        {
                                gancho.transform.localPosition += ganchoVelocidad * dir * velocidad * 0.1f;
                                Debug.Log("en gancho");
                                yield return new WaitForSeconds(0.01f);

                                //StartCoroutine(move5);
                        }
                        else { 
                                //No_press();
                                break;
                            }
                    }
                }
                else
                {
                    if (gancho.transform.localPosition.y <= ly2 && bloqueo[5] == false)
                    { //StartCoroutine(move6);
                      
                      }
                    else { //No_press();
                            break;
                        }

                }
                yield return new WaitForSeconds(0.01f);
                break;
            case "grua":
                
                if (dir < 0)
                {
                    if (grua.transform.localPosition.z >= lz2 && bloqueo[0] == false)
                    {
                        //StartCoroutine(move1);
                    }
                    else { No_press(); }
                }
                else
                {
                    if (dir > 0)
                    {
                        if (grua.transform.localPosition.z <= lz1 && bloqueo[1] == false)
                        {
                                grua.transform.localPosition += gruaVelocidad * dir * velocidad * 0.1f; Debug.Log("enZ");
                                yield return new WaitForSeconds(0.01f);
                                //StartCoroutine(move2);
                                }
                        else { No_press(); }
                    }
                }
                break;
            case "puente":
                
                if (dir < 0)
                {
                    if (puente.transform.localPosition.x >= lx1 && bloqueo[2] == false)
                    {
                            puente.transform.localPosition += puenteVelocidad * dir * velocidad * 0.1f;
                            Debug.Log("enZ"); ;
                            yield return new WaitForSeconds(0.01f);
                            //StartCoroutine(move3); 
                            Debug.Log("Hacia adelante");
                    }
                    else
                    {
                        Debug.Log("Limite1 en puente alcanzado");
                        No_press();
                    }
                }
                else
                {
                    if (dir > 0)
                    {
                        if (puente.transform.localPosition.x <= lx2 && bloqueo[3] == false)
                        {
                                puente.transform.localPosition += puenteVelocidad * dir * velocidad * 0.1f; Debug.Log("enZ"); ;
                                //StartCoroutine(move4);
                                Debug.Log("Hacia atras");
                        }
                        else
                        {
                            Debug.Log("Limite2 puente alcanzado");
                            No_press();
                        }
                    }
                }
                break;
        }
    }
    yield return new WaitForSeconds(0.01f);
}
public void No_press()
{
    Debug.Log("deteniendo coroutines");
    /*StopCoroutine(move1);
    StopCoroutine(move2);
    StopCoroutine(move3);
    StopCoroutine(move4);
    StopCoroutine(move5);
    StopCoroutine(move6);
}*/
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
        
        int sentido = 0;
        while (btn_controlgrua[botonPresionado] == true) {
            if (btn_controlgrua[botonPresionado] == false)
            {
                Debug.Log("alto puente");
                break;
            }
            if (si_adelante == true)//resto con respecto a localposition
             {
                if (bloqueo[0] == true)
                {
                    Debug.Log("alto puente (bloqueo[0]="+bloqueo[0]);
                    break;
                }
                sentido = -1;
                if(puente.transform.localPosition.x >= LimitesXYZ[0].localPosition.x)
                {
                    //Debug.Log("moviendo "+sentido);
                    puente.transform.localPosition = new Vector3(puente.transform.localPosition.x + velo.x * sentido, puente.transform.localPosition.y, puente.transform.localPosition.z);
                    yield return new WaitForSeconds(0.01f);
                }
                else
                {
                    Debug.Log(sentido+" "+ puente.transform.localPosition.x+ " en x Limite0 hacia adelante alcanzado puente bloqueo[0]="+ LimitesXYZ[0].localPosition.x+" " + bloqueo[0]);
                    break;
                }
            }
            else {
                if (bloqueo[1] == true)
                {
                    Debug.Log("alto puente grua bloqueo[1]=" + bloqueo[1]);
                    break;
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
                    Debug.Log(sentido + " Limite alcanzado puente bloqueo[1]="+ puente.transform.localPosition.x+">= " + LimitesXYZ[1].localPosition.x+" o "+ bloqueo[1]);
                    break;
                }
            }
        }
    }
    IEnumerator GruaMove(bool si_izquierda)
    {
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
                if (bloqueo[2] == true)
                {
                    Debug.Log("alto grua bloqueo[2]=" + bloqueo[2]);
                    break;
                }
            sentido = -1;
                if(grua.transform.localPosition.z >= LimitesXYZ[0].localPosition.z)
                {
                    //Debug.Log("moviendo " +sentido);
                    grua.transform.localPosition = new Vector3(grua.transform.localPosition.x, grua.transform.localPosition.y, grua.transform.localPosition.z + velo.z * sentido);
                    yield return new WaitForSeconds(0.01f);
                }
                else
                {
                    Debug.Log(grua.transform.localPosition.z + "Limite izquierdo grua alcanzado bloqueo[2]=" + LimitesXYZ[0].localPosition.z + " " + bloqueo[2]);
                    break;
                }
        }
        else {
                if (bloqueo[3] == true)
                {
                    Debug.Log("alto grua bloqueo[3]=" + bloqueo[3]);
                    break;
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
                    Debug.Log(grua.transform.localPosition.z +"Limite derecho grua alcanzado grua bloqueo[3]= " + LimitesXYZ[1].localPosition.z+" " + bloqueo[3]);
                    break;
                }
            }
        }
    }
    IEnumerator GanchoMove(bool si_arriba)
    {
        int sentido = 0;
        while (btn_controlgrua[botonPresionado] == true)
        {
            if (btn_controlgrua[botonPresionado] == false)
            {
                Debug.Log("alto gancho");
                break;
            }
            if (si_arriba == true)
            {
                if (bloqueo[4] == true)
                {
                    Debug.Log("alto gancho bloqueo[4]=" + bloqueo[4]);
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
                    Debug.Log(sentido + "+Limite alcanzado gancho bloqueo[4]=" + bloqueo[4]);
                    break;
                }

            }
            else {
                if (bloqueo[5] == true)
                {
                    Debug.Log("alto gancho bloqueo[5]=" + bloqueo[5]);
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
                    Debug.Log(sentido + " Limite alcanzado gancho bloqueo[5]=" + bloqueo[5]);
                    break;
                }
            }
        

        }
    }
    public void Soltar_Boton()
    {
        btn_controlgrua[botonPresionado] = false;
    }
    

}
