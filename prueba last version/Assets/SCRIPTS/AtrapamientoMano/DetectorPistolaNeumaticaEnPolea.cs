using Oculus.Interaction;
using UnityEditor;
using UnityEngine;

public class DetectorPistolaNeumaticaEnPolea : MonoBehaviour
{
    [SerializeField] private GameObject RightHandMesh;
    [SerializeField] private SkinnedMeshRenderer RealLeftHandSkinnedMeshRenderer;
    [SerializeField] private Material InvisibleMaterial;
    [SerializeField] private GameObject PistolaNeumaticaEnPolea;
    [SerializeField] private GameObject PistolaNeumaticaEnMano;
    [SerializeField] private GrabFreeTransformerCustom PistolaNeumaticaGrabFreeTransformer;
    [SerializeField] private PistolaNeumaticaNutrunnerUse NutrunnerUse;
    [SerializeField] private GameObject PistolaReferencia3DModel;

    // Transform objetivo donde la pistola debe quedar fijada
    [SerializeField] private Transform LockTransform;

    private bool _lockedThisAttempt = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PistolaNeumaticaEnMano && !_lockedThisAttempt)
        {
            _lockedThisAttempt = true;
            LockPistolToTransform();
            NutrunnerUse.SetUseEnabled(true); // habilitas uso cuando ya está “en estación”
            PistolaReferencia3DModel.SetActive(false);
            print("La pistola neumatica entro en la polea");
            AudioManager.aSource.PlayVoz("DialogoSimulacion");
        }
    }

    private void LockPistolToTransform()
    {
        if (PistolaNeumaticaEnMano == null || LockTransform == null || PistolaNeumaticaGrabFreeTransformer == null)
        {
            Debug.LogWarning("Faltan referencias en DetectorPistolaNeumaticaEnPolea.");
            return;
        }

        Rigidbody pistolaNeumaticaRigidBody = PistolaNeumaticaEnMano.GetComponent<Rigidbody>();
        pistolaNeumaticaRigidBody.isKinematic = true;
        pistolaNeumaticaRigidBody.useGravity = false;
        pistolaNeumaticaRigidBody.constraints = RigidbodyConstraints.FreezeAll;

        // 1) Poner la pistola exactamente en la pose objetivo (world)
        PistolaNeumaticaEnMano.transform.position = LockTransform.position;
        PistolaNeumaticaEnMano.transform.rotation = LockTransform.rotation;

        // 2) Construir constraints que bloqueen posición y rotación.
        // Convertimos la posición objetivo al espacio local del parent de la pistola porque
        // GrabFreeTransformer genera constraints relativas al parent (igual que Initialize hace).
        Transform pistolParent = PistolaNeumaticaEnMano.transform.parent;
        Vector3 localPos;
        Vector3 localEuler;

        if (pistolParent != null)
        {
            localPos = pistolParent.InverseTransformPoint(LockTransform.position);
            localEuler = (Quaternion.Inverse(pistolParent.rotation) * LockTransform.rotation).eulerAngles;
        }
        else
        {
            // si no hay parent, usamos la pose en world (no ideal pero funcional)
            localPos = LockTransform.position;
            localEuler = LockTransform.rotation.eulerAngles;
        }

        // Crear PositionConstraints con min == max en cada eje para bloquear
        var posConstraints = new TransformerUtils.PositionConstraints()
        {
            XAxis = new TransformerUtils.ConstrainedAxis()
            {
                ConstrainAxis = true,
                AxisRange = new TransformerUtils.FloatRange() { Min = localPos.x, Max = localPos.x }
            },
            YAxis = new TransformerUtils.ConstrainedAxis()
            {
                ConstrainAxis = true,
                AxisRange = new TransformerUtils.FloatRange() { Min = localPos.y, Max = localPos.y }
            },
            ZAxis = new TransformerUtils.ConstrainedAxis()
            {
                ConstrainAxis = true,
                AxisRange = new TransformerUtils.FloatRange() { Min = localPos.z, Max = localPos.z }
            }
        };

        // Crear RotationConstraints con min == max en cada eje (grados)
        var rotConstraints = new TransformerUtils.RotationConstraints()
        {
            XAxis = new TransformerUtils.ConstrainedAxis()
            {
                ConstrainAxis = true,
                AxisRange = new TransformerUtils.FloatRange() { Min = localEuler.x, Max = localEuler.x }
            },
            YAxis = new TransformerUtils.ConstrainedAxis()
            {
                ConstrainAxis = true,
                AxisRange = new TransformerUtils.FloatRange() { Min = localEuler.y, Max = localEuler.y }
            },
            ZAxis = new TransformerUtils.ConstrainedAxis()
            {
                ConstrainAxis = true,
                AxisRange = new TransformerUtils.FloatRange() { Min = localEuler.z, Max = localEuler.z }
            }
        };

        // 3) Inyectar las constraints en el transformer (métodos públicos que ya tienes).
        PistolaNeumaticaGrabFreeTransformer.InjectOptionalPositionConstraints(posConstraints);
        PistolaNeumaticaGrabFreeTransformer.InjectOptionalRotationConstraints(rotConstraints);

        // IMPORTANT: recalcula los relativos
        PistolaNeumaticaGrabFreeTransformer.RefreshRelativeConstraints();

        // Opcional: si quieres prevenir cualquier otro ajuste inmediato, puedes forzar una actualización rápida:
        // (solo si tu flujo lo permite y tienes acceso al IGrabbable/Initialize en otro lugar)
        // PistolaNeumaticaGrabFreeTransformer.enabled = true;

        NutrunnerUse.SetUseEnabled(true);
        NutrunnerUse.ResetReactionBarTravel();

        Debug.Log("Pistola fijada al LockTransform y constraints inyectados.");
    }

    public void UnlockPistolFromPolea()
    {
        if (PistolaNeumaticaEnMano == null || PistolaNeumaticaGrabFreeTransformer == null)
        {
            Debug.LogWarning("Faltan referencias en DetectorPistolaNeumaticaEnPolea.");
            return;
        }

        Rigidbody pistolaNeumaticaRigidBody = PistolaNeumaticaEnMano.GetComponent<Rigidbody>();

        // 1) Restaurar física
        pistolaNeumaticaRigidBody.isKinematic = false;
        pistolaNeumaticaRigidBody.useGravity = true;
        pistolaNeumaticaRigidBody.constraints = RigidbodyConstraints.None;

        // 2) Quitar constraints del GrabFreeTransformer
        var freePosConstraints = new TransformerUtils.PositionConstraints()
        {
            XAxis = new TransformerUtils.ConstrainedAxis() { ConstrainAxis = false },
            YAxis = new TransformerUtils.ConstrainedAxis() { ConstrainAxis = false },
            ZAxis = new TransformerUtils.ConstrainedAxis() { ConstrainAxis = false }
        };

        var freeRotConstraints = new TransformerUtils.RotationConstraints()
        {
            XAxis = new TransformerUtils.ConstrainedAxis() { ConstrainAxis = false },
            YAxis = new TransformerUtils.ConstrainedAxis() { ConstrainAxis = false },
            ZAxis = new TransformerUtils.ConstrainedAxis() { ConstrainAxis = false }
        };

        PistolaNeumaticaGrabFreeTransformer.InjectOptionalPositionConstraints(freePosConstraints);
        PistolaNeumaticaGrabFreeTransformer.InjectOptionalRotationConstraints(freeRotConstraints);

        // recalcular para que el transformer use los nuevos valores
        PistolaNeumaticaGrabFreeTransformer.RefreshRelativeConstraints();

        // 3) Deshabilitar uso del nutrunner
        NutrunnerUse.SetUseEnabled(false);

        // permitir que vuelva a detectarse en el trigger si vuelve a entrar
        _lockedThisAttempt = false;

        // opcional: volver a mostrar el modelo guía
        PistolaReferencia3DModel.SetActive(true);

        Debug.Log("Pistola liberada de la polea.");
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PistolaNeumaticaEnMano)
        {
            //NutrunnerUse.SetUseEnabled(false);
            //print("La pistola neumatica salio de la polea");
        }
    }
}