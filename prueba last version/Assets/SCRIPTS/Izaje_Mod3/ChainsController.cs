using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsController : MonoBehaviour
{
    [SerializeField] HookObject HookObject1 = null;
    [SerializeField] HookObject HookObject2 = null;

    private void Update()
    {
        /*if (HookObject1.GetIsHooked() && HookObject2.GetIsHooked())
        {
            HookObject1.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            HookObject1.GetObjectToHookWith().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            HookObject2.GetObjectToHookWith().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }*/
    }
}
