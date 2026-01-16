using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorPJHotspot : MonoBehaviour
{
    public TM_EPC210LC tm_;
    public int nContacto;
    public int nFX;
    public int nPanel;
    private void Awake()
    {
        tm_ = GameObject.Find("TareaManager").GetComponent<TM_EPC210LC>(); ;
    }
    private void OnTriggerEnter(Collider other)
    {
        tm_.activarEvento(nContacto);        
    }
}
