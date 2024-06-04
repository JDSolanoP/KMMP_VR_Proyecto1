using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Profiling;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Pooling;
using HInteractions;

public class One_Hand_PickUp : XRGrabInteractable
{
    [Header("Extras")]
    public bool UsarMasAttachs;
    [Header("Izq=0-Der=1")]
    public Transform[] attachs;

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isGrabed = selectingInteractor && !interactor.Equals(selectingInteractor) && selectingInteractor.CompareTag("Player");
        if (selectingInteractor != null)
        {

            AttachAsingation(selectingInteractor.name);
        }
        //Debug.Log(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isGrabed;
    }

    public override bool IsHoverableBy(XRBaseInteractor interactor)
    {
        //Debug.Log(interactor);
        if (interactor != null)
        {
            AttachAsingation(interactor.name);
        }
        return base.IsHoverableBy(interactor);
    }
    void AttachAsingation(string name)
    {
        if (UsarMasAttachs == true)
        {
            //Debug.Log(name);
            switch (name)
            {
                case "Left_hand":
                    attachTransform = attachs[0];
                    //Debug.Log(attachs[0].name);
                    break;
                    
                case "Right_hand":
                    attachTransform = attachs[1];
                    //Debug.Log(attachs[1].name);
                    break;
                    
            }
            return;
        }
        //Debug.Log($"no usa attachs/no reconoce nombre {name}");
    }
}
