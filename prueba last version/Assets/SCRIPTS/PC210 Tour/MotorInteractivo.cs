using System.Collections;
using UnityEngine;

public class MotorInteractivo : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float timeDuration = 1f;
    [SerializeField] private DetectorTapaAbierta detectorTapaAbierta;
    [SerializeField] private Collider tapaMotorGrabbable;
    [SerializeField] private TM_EPC210LC tm_epc210lc;

    private Vector3 _initialLocalPosition;
    private Quaternion _initialLocalRotation;
    private Collider _grabbableCollider;

    private void Awake()
    {
        _initialLocalPosition = transform.localPosition;
        _initialLocalRotation = transform.localRotation;
        _grabbableCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        _grabbableCollider.enabled = false;
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

        float elapsedTime = 0f;

        while (elapsedTime < timeDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / timeDuration;
            float easedT = Mathf.SmoothStep(0f, 1f, t);

            transform.localPosition = Vector3.Lerp(_initialLocalPosition, targetPosition.localPosition, easedT);

            yield return null;
        }

        transform.localPosition = targetPosition.localPosition;
        _grabbableCollider.enabled = true;
        tm_epc210lc.numerosDePanel.SetActive(true);
    }

    public void ReturnMotorToInitial()
    {
        StopAllCoroutines();
        StartCoroutine(ReturnSequence());
        detectorTapaAbierta.Button3DGuardar.SetActive(false);
        tapaMotorGrabbable.enabled = true;
    }

    private IEnumerator ReturnSequence()
    {
        _grabbableCollider.enabled = false;

        float rotationTime = 0f;
        Quaternion startRotation = transform.localRotation;

        while (rotationTime < timeDuration)
        {
            rotationTime += Time.deltaTime;
            float t = rotationTime / timeDuration;
            float easedT = Mathf.SmoothStep(0f, 1f, t);

            transform.localRotation = Quaternion.Slerp(
                startRotation,
                _initialLocalRotation,
                easedT
            );

            yield return null;
        }

        transform.localRotation = _initialLocalRotation;

        float positionTime = 0f;
        Vector3 startPosition = transform.localPosition;

        while (positionTime < timeDuration)
        {
            positionTime += Time.deltaTime;
            float t = positionTime / timeDuration;
            float easedT = Mathf.SmoothStep(0f, 1f, t);

            transform.localPosition = Vector3.Lerp(
                startPosition,
                _initialLocalPosition,
                easedT
            );

            yield return null;
        }

        transform.localPosition = _initialLocalPosition;
        tm_epc210lc.numerosDePanel.SetActive(false);
        tm_epc210lc.BotonContinuarDelMotor.SetActive(true);
    }

}