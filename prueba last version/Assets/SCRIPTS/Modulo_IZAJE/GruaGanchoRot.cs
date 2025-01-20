using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class GruaGanchoRot : MonoBehaviour
{
    public GameObject Usuario;
    public GameObject GanchoRefe;//detecta la posicion en la que estaria el meshpara detectar el sentido de giro
    public Vector3 UPos;
    public Vector3 GPos;
    public Vector3 Dir;
    public Vector3 direccion;
    public Vector3 SentidoG;
    public float arc;
    public int sentido;
    public bool GiroActivo;
    public bool siGiroHorario;
    public bool siGiroAntihorario;
    public bool existeBloque;
    public bool si_giro;
    public float intervalo;
    public float velRot;//velocidad de giro
    public float rot0;
    public float rot1;
    public float rotaux;
    
    public CargaIzable_ColViento[] cCViento;
    public bool[] siBloqueo;
    [Header ("Lista de Objetos bloqueando")]
    public GameObject[] listaObjBloqueo;
    public int nObjBloqueando=0;
    // Start is called before the first frame update
    void Start()
    {
        rot0 = transform.localEulerAngles.y;
        Dir = new Vector3(0,0,0);//aux direccion

    }
    public IEnumerator IniciarGiro()
    {

        UPos = Usuario.transform.position;
        GPos = transform.position;
        if (GiroActivo)
        {//***HALLAR DIRECCION***
            rotaux = rot0;
            
            direccion = Usuario.transform.position - transform.position;
            //Dir = direccion;
            Vector3 soloXZ = new Vector3(direccion.x, 0, direccion.z);
            Debug.Log("Girado");
            GanchoRefe.transform.forward = soloXZ;
            /*SentidoG = direccion - Dir;
            arc = Mathf.Abs(SentidoG.z / SentidoG.x);*/

            if (GanchoRefe.transform.eulerAngles.y-transform.eulerAngles.y>0)
            {
                sentido = 1;
            }
            else
            {
                if (GanchoRefe.transform.eulerAngles.y - transform.eulerAngles.y < 0)
                {
                    sentido = -1;
                }
                else { sentido = 0; }
            }
            Debug.Log("Giro en sentido " + sentido + " arco=" + (GanchoRefe.transform.eulerAngles.y - transform.eulerAngles.y));//20-1-25
            
             Debug.Log("****ALERTA**** ERROR de Giro en sentido " + sentido + " arco Gancho refe=" + GanchoRefe.transform.eulerAngles.y+ " this objmesh: " +transform.eulerAngles.y); 
            siGiroAntihorario = true;
            siGiroHorario = true;
            ActualizarBloqueos();//ACTUALIZA BLOQUEOS ACTUALES 
            DetectoBloqueo();
            if (existeBloque == true)
            {
                si_giro = false;//permite el movimiento
                if (siBloqueo[0] == true || siBloqueo[2] == true || siBloqueo[4] == true || siBloqueo[6] == true)
                {
                    siGiroAntihorario = false;
                    //siGiroHorario = true;
                    Debug.Log("bloqueo en sentido AntiHorario");
                }
                else {
                    if (siBloqueo[0] == false && siBloqueo[2] == false && siBloqueo[4] == false && siBloqueo[6] == false)
                    {
                        siGiroAntihorario = true;
                        //si_giro=true;
                        siGiroHorario = false;
                        Debug.Log("DESBLOQUEO Verdarero en sentido AntiHorario");
                    }
                }
                if (siBloqueo[1] == true || siBloqueo[3] == true || siBloqueo[5] == true || siBloqueo[7] == true)
                {
                    siGiroHorario = false;
                    //siGiroHorario = true;
                    Debug.Log("bloqueo en sentido Horario");
                }
                else
                {
                    if (siBloqueo[1] == false && siBloqueo[3] == false && siBloqueo[5] == false && siBloqueo[7] == false)
                    {
                        siGiroHorario = true;
                        //si_giro = true;
                        siGiroAntihorario = false;
                        Debug.Log("DESbloqueo en sentido Horario");
                    }
                }
            }
            if (sentido <0)
            {
                Debug.Log(-1 + " intentando Girar AntiHorario ");
                if (siGiroAntihorario ==true)
                {
                    si_giro=true;
                    Debug.Log(sentido+" Girando AntiHorario : " + siGiroAntihorario);
                }
            }
            else
            {
                if (sentido > 0)
                {
                    Debug.Log(1 + " intentando Girar Horario ");
                    if (siGiroHorario == true)
                    {
                        si_giro = true;
                        Debug.Log(sentido+" Girando Horario : " + siGiroHorario);
                    }
                }
            }
            if (si_giro)
            {
                Debug.Log("Girado"); 
                transform.forward = Vector3.MoveTowards(transform.forward, soloXZ, velRot /** Time.deltaTime*/);
                Debug.Log("bloqueo en AntiHorario : " + siGiroAntihorario + " -en Horario : " + siGiroHorario + " sentido : " + sentido + " arco:" + arc);
            }
            else
            {
                Debug.Log("bloqueo TOTAL-AntiHorario : " + siGiroAntihorario + " -Horario : " + siGiroHorario + " sentido : " + sentido+" arco:"+arc);
            }
            
        }
        
        yield return new WaitForSeconds(intervalo);
        
        if (GiroActivo)
        {
            rot0 = transform.localEulerAngles.y;
            Dir = direccion;
            StartCoroutine(IniciarGiro());
        }
        
    }

    // Update is called once per frame
    /*void Update()
    {
        UPos=Usuario.transform.position;
        GPos=transform.position;
        if (GiroActivo)
        {//***HALLAR DIRECCION***
            rotaux= rot0;
            direccion = Usuario.transform.position - transform.position;
            //Dir = direccion;
            Vector3 soloXZ = new Vector3(direccion.x, 0, direccion.z);
            //SentidoG = direccion - Dir;
            //arc = (SentidoG.z / SentidoG.x);

            if (arc > 0)
            {
                sentido = 1;
            }
            else
            {
                if (arc < 0)
                {
                    sentido = -1;
                }
                else { sentido = 0; }
            }
            Debug.Log("Giro en sentido "+sentido+" arcTg="+arc);
            ActualizarBloqueos();//ACTUALIZA BLOQUEOS ACTUALES 
            DetectoBloqueo();
            if ()//VERIFICA SI HAY BLOQUEO Y BLOQUEA GIRO EN ESE INTENTO 
            {
                
                switch (sentido)
                {
                    case 1:siGiroHorario=false;
                        Debug.Log("bloqueo " + true+" en sentido Horario");
                        break;
                    case -1: siGiroAntihorario = false;
                        Debug.Log("bloqueo Verdarero en sentido AntiHorario");
                        break;
                }
            }
            else
            {
                Debug.Log("Sin bloqueo Giro permitido en sentido AntiHorario y Horario");
                siGiroAntihorario = true; siGiroHorario = true;
            }
            if (siGiroAntihorario == true && sentido == -1) {
                transform.forward = Vector3.MoveTowards(transform.forward, soloXZ, velRot * Time.deltaTime);
                rot0 = transform.localEulerAngles.y;
            }
            if (siGiroHorario == true && sentido == 1)
            {
                transform.forward = Vector3.MoveTowards(transform.forward, soloXZ, velRot * Time.deltaTime);
                rot0 = transform.localEulerAngles.y;
            }
            Debug.Log("bloqueo AntiHorario : "+siGiroAntihorario+" y Horario : "+siGiroHorario+":: Vector("+SentidoG.x+ ";"+SentidoG.z+") sentido : "+sentido+" rotaux:"+rotaux+" rot0:"+rot0);
            Dir = direccion;
        }
    }*/
    public void ActualizarBloqueos()
    {
        for(int i = 0; i < siBloqueo.Length; i++)
        {
            //Debug.Log("actualizando bloqueos");
            if (cCViento[i] != null) { cCViento[i].BloqueaActivo = siBloqueo[i];
               // Debug.Log("actualizado bloqueo "+i);
            }
            else
            {
                break;
            }
            
        }
    }
    public void DetectoBloqueo()
    {
        existeBloque = false;
        //Debug.Log("detectando bloqueos");
        for (int i = 0; i < siBloqueo.Length; i++) 
        {
            if (siBloqueo[i] == true)
            {
                if (existeBloque == false)
                {
                    existeBloque = true;
                    Debug.Log("Detectado: bloqueo " + i);
                }
            }
        }
        
    }
    public void AddGObjBloqueando(GameObject GO)
    {
        listaObjBloqueo[nObjBloqueando] = GO;
        nObjBloqueando++;
    }
    public int siNuevoObjBloq(GameObject go) 
    {
        int n=-1;
        for(int i = 0; i < nObjBloqueando; i++)
        {
            if (go == listaObjBloqueo[i])
            {
                n=i;
                break;
            }
        }

        return n;
    }
    public void BorrarObjBloqueando(GameObject GO) 
    {
        for (int i = 0; i < nObjBloqueando; i++)
        {
            if (listaObjBloqueo[i] != null)
            {
                if (listaObjBloqueo[i] == GO)
                {
                    if (nObjBloqueando == 1)
                    {
                        listaObjBloqueo[i] = null;
                    }
                    else
                    {
                        for (int j = 0; j < nObjBloqueando - i - 1; j++)
                        {
                            if (listaObjBloqueo[i + j] != null)
                            {
                                listaObjBloqueo[i + j] = listaObjBloqueo[i + j + 1];
                            }

                        }
                        listaObjBloqueo[nObjBloqueando-1]=null;
                    }
                    nObjBloqueando--;
                }
            }
            else
            {
                break;
            }
        }
    }
}
