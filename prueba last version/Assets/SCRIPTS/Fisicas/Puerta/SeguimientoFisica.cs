using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoFisica : MonoBehaviour
{
    public Transform obj;
    Rigidbody rb;
    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(obj.transform.position);
    }
}
