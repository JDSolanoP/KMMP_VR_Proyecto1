using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuertasManijjaFix : MonoBehaviour
{
    public GameObject objPrincipal;
    public GameObject[] Manijas;
    public bool activar;
    Vector3 pos0;

    public void Start()
    {
        pos0 = objPrincipal.transform.localPosition;
    }
    public void desactivar(bool activar)
    {
        objPrincipal.GetComponent<Rigidbody>().isKinematic = activar;
        objPrincipal.GetComponent<Rigidbody>().useGravity = !activar;
        for (int i = 0; i < Manijas.Length; i++)
        {
            if (Manijas[i].gameObject.name != this.gameObject.name)
            {
                Manijas[i].gameObject.SetActive(activar);
            }
        }
        //pararRb();
    }
    public void pararRb()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        objPrincipal.GetComponent<Rigidbody>().isKinematic = false;
        objPrincipal.GetComponent<Rigidbody>().useGravity = true;

    }

}
