using System.Collections;
using UnityEngine;
using Oculus.Interaction.Locomotion;

public class ForceTeleportOnStart_Meta : MonoBehaviour
{
    [SerializeField] private FirstPersonLocomotor locomotor;
    [SerializeField] private Transform targetPoint; // tu hotspot TargetPoint

    [Header("Teleport config")]
    [SerializeField] private bool eyeLevel = false;          // AbsoluteEyeLevel vs Absolute
    [SerializeField] private bool faceTargetDirection = true; // Rotation Absolute vs None

    private IEnumerator Start()
    {
        // Espera a que el tracking del HMD y el locomotor estén listos
        yield return new WaitForEndOfFrame();

        if (!locomotor || !targetPoint) yield break;

        Pose pose = new Pose(targetPoint.position, targetPoint.rotation);

        var translation = eyeLevel
            ? LocomotionEvent.TranslationType.AbsoluteEyeLevel
            : LocomotionEvent.TranslationType.Absolute;

        var rotation = faceTargetDirection
            ? LocomotionEvent.RotationType.Absolute
            : LocomotionEvent.RotationType.None;

        var evt = new LocomotionEvent(
            identifier: gameObject.GetInstanceID(),
            pose: pose,
            translationType: translation,
            rotationType: rotation
        );

        locomotor.HandleLocomotionEvent(evt);
    }
}