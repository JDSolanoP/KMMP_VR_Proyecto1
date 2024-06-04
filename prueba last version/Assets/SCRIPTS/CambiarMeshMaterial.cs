 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Interaction.Toolkit;
using HInteractions;

public class CambiarMeshMaterial : MonoBehaviour
{
    [Tooltip("tipos: Cabeza , Manos , Cuerpo o Pies_Piernas")]
    public string tipoXR;
    public SetEpps sE;
    //public AudioSource settingEpps;
    [Header("Si Iniciar con Material")]
    public bool iniciar_NuevoMaterial;

    //Cambiar por uno generico cuando se tenga cuerpo completo
    //public HandPresence ModelSkinned;
    [Tooltip("Si Iniciar con Material")]
    public Material InicialMaterial;

    void Start()
    {
        

    }
    IEnumerator ColocarMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        //ModelSkinned = GetComponentInChildren<HandPresence>();
    }
}
