using System.Collections;
using UnityEngine;

public class MotorInteractivo : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float timeDuration = 1f;
    [SerializeField] private DetectorTapaAbierta detectorTapaAbierta;
    [SerializeField] private Collider tapaMotorGrabbable;

    private Vector3 _initialLocalPosition;
    //private Collider _grabbableCollider;

    private void Awake()
    {
        _initialLocalPosition = transform.localPosition;
        //_grabbableCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        //_grabbableCollider.enabled = false;
    }

    public void PlayMoveMotor()
    {
        StartCoroutine(MoveMotor());
        detectorTapaAbierta.SetButtonWasActivated(true);
        detectorTapaAbierta.Button3DInspeccionar.SetActive(false);
        tapaMotorGrabbable.enabled = false;
    }

    private IEnumerator MoveMotor()
    {
        if (!targetPosition) yield break;
        float localTimeDuration = 0f;
        Transform localTargetPosition = targetPosition;
        while (localTimeDuration < timeDuration)
        {
            localTimeDuration += Time.deltaTime / timeDuration;
            float t = localTimeDuration;
            float easedT = Mathf.SmoothStep(0f, 1f, t);
            transform.localPosition = Vector3.Lerp(_initialLocalPosition, localTargetPosition.localPosition, easedT);
            yield return null;
        }
        transform.localPosition = targetPosition.localPosition;
        //_grabbableCollider.enabled = true;
    }
}