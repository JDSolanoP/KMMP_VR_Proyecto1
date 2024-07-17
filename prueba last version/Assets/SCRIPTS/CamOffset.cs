using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOffset : MonoBehaviour
{
    public GameObject XRRig;
    public GameObject Camera;
    public Vector3 Resize;

    void Start()
    {
        Resize.x = - Camera.transform.localPosition.x;
        Resize.z = - Camera.transform.localPosition.z;
        Resize.y = this.gameObject.transform.localPosition.y;
        this.gameObject.transform.localPosition = Resize;
    }
    void FixedUpdate()
    {
        Resize.x = - Camera.transform.localPosition.x;
        Resize.z = - Camera.transform.localPosition.z;
        Resize.y = this.gameObject.transform.localPosition.y;
        this.gameObject.transform.localPosition = Resize;
    }

}
