using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChainGripArea : MonoBehaviour
{
    [SerializeField] private float Radius = 10f;
    [SerializeField] private Vector3 PositionOffset = Vector3.zero;
    [SerializeField] public GameObject Gancho = null;
    [SerializeField] private Transform PuntoDeReferenciaTransform = null;
    [SerializeField] private float TimeOutOfArea = 0.2f;
    [SerializeField] private bool _isActivate = true;
    //private bool _ganchoIsInArea = false;
    //private bool _isHeld = false;

    private void Update()
    {
        if (_isActivate)
        {
            float distance = Vector3.Distance(transform.position + PositionOffset, PuntoDeReferenciaTransform.position);

            if (distance <= Radius)
            {
                Gancho.GetComponent<One_Hand_PickUp>().enabled = true;
                //print($"Está dentro {Gancho}");
            }
            else
            {
                Gancho.GetComponent<One_Hand_PickUp>().enabled = false;
                //print($"Gancho salió del área {Gancho}");
                StartCoroutine(ActivateOneHandPickUp());
            }
        }
    }

    public void SetIsActivate(bool state)
    {
        _isActivate = state;
    }
    private IEnumerator ActivateOneHandPickUp()
    {
        yield return new WaitForSeconds(0.5f);
        Gancho.GetComponent<One_Hand_PickUp>().enabled = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position + PositionOffset, Radius);
    }


}