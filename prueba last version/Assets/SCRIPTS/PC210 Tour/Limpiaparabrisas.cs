using System.Collections;
using UnityEngine;

public class Limpiaparabrisas : MonoBehaviour
{
    [SerializeField] private Horometro horometro;
    [SerializeField] private Transform targetRotation;

    [SerializeField] private float totalDuration = 3f;

    private Quaternion _initialLocalRotation;
    private bool _isRotating = false;

    private void Awake()
    {
        _initialLocalRotation = transform.localRotation;
    }

    public void PlayRotation()
    {
        if (_isRotating) return;
        if (!horometro.GetIsPowerOn()) return;

        StartCoroutine(RotateRoutine());
    }

    private IEnumerator RotateRoutine()
    {
        if (targetRotation == null) yield break;

        _isRotating = true;

        float halfDuration = totalDuration / 2f;

        Quaternion targetLocalRotation = targetRotation.localRotation;

        float timeDuration = 0f;
        while (timeDuration < 1f)
        {
            timeDuration += Time.deltaTime / halfDuration;
            transform.localRotation = Quaternion.Slerp(_initialLocalRotation, targetLocalRotation, timeDuration);
            yield return null;
        }

        timeDuration = 0f;
        while (timeDuration < 1f)
        {
            timeDuration += Time.deltaTime / halfDuration;
            transform.localRotation = Quaternion.Slerp(targetLocalRotation, _initialLocalRotation, timeDuration);
            yield return null;
        }

        transform.localRotation = _initialLocalRotation;
        _isRotating = false;
    }
}
