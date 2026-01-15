using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerEvents : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private GameObject targetObject;

    [Header("Trigger Events")]
    public UnityEvent OnTriggerEntered;
    public UnityEvent OnTriggerExited;
    public UnityEvent OnTriggerStayed;

    private void Reset()
    {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private bool IsValid(Collider other)
    {
        if (targetObject == null) return true;

        return other.gameObject.name == targetObject.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsValid(other)) return;

        OnTriggerEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsValid(other)) return;

        OnTriggerExited?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsValid(other)) return;

        OnTriggerStayed?.Invoke();
    }
}
