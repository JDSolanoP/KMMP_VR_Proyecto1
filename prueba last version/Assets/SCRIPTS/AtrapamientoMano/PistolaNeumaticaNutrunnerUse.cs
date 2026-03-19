using Oculus.Interaction.HandGrab;
using UnityEngine;

public class PistolaNeumaticaNutrunnerUse : MonoBehaviour, IHandGrabUseDelegate
{
    [Header("Input (Use / Gatillo)")]
    [SerializeField] private Transform _triggerVisual; // opcional: si quieres animar un gatillo
    [SerializeField] private AnimationCurve _triggerRotationCurve = AnimationCurve.Linear(0, 0, 1, 30);
    [SerializeField] private SnapAxis _triggerAxis = SnapAxis.X;

    [SerializeField, Range(0f, 1f)]
    private float _pressThreshold = 0.9f;     // a partir de aqui cuenta como "presionado"

    [SerializeField, Range(0f, 1f)]
    private float _releaseThreshold = 0.3f;   // para resetear cuando sueltas

    [SerializeField]
    private float _triggerSpeed = 3f;         // suavizado del strength, similar a WaterSpray

    [SerializeField]
    private AnimationCurve _strengthCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Header("Trigger Push")]
    [SerializeField] private Vector3 _triggerPushAxis = Vector3.back; // dirección en local
    [SerializeField] private float _triggerPushDistance = 0.02f; // cuánto se hunde
    private Vector3 _triggerStartLocalPos;

    [Header("Output (Reaction Bar)")]
    [SerializeField] private Transform _reactionBar;
    [SerializeField] private Vector3 _rotationAxisLocal = Vector3.up; // eje local de la barra
    [SerializeField] private float _degreesPerSecond = 60f;           // velocidad de giro

    private bool _isPressed = false;
    private float _dampedUseStrength = 0f;
    private float _lastUseTime = 0f;
    private bool _jammed = false;


    [Header("Use Enable (Polea)")]
    [SerializeField] private bool _useEnabled = false;
    [SerializeField] private DetectorManoEnPolea _detectorManoEnPolea;

    [Header("Rotation Limit")]
    [SerializeField] private float _maxRotationDegrees = 180f;

    [Header("Tarea Manager")]
    [SerializeField] private TM_AtrapamientoMano TM_AtrapamientoMano;
    [SerializeField] private int TareaPorCompletar;

    [Header("Audio")]
    [SerializeField] private AudioSource SonidoDePistolaAudioSource;
    private bool _isTheSoundOfTheGunPlaying = false;

    private float _rotatedDegrees = 0f;

    private void Start()
    {
        if (_triggerVisual != null)
            _triggerStartLocalPos = _triggerVisual.localPosition;
    }

    public void BeginUse()
    {
        _dampedUseStrength = 0f;
        _lastUseTime = Time.realtimeSinceStartup;
        // No marcamos pressed aqui; esperamos a ComputeUseStrength para saber el valor real
    }

    public void EndUse()
    {
        _isPressed = false;

        if (_isTheSoundOfTheGunPlaying)
        {
            SonidoDePistolaAudioSource.Pause(); // o Stop()
            _isTheSoundOfTheGunPlaying = false;
        }
        // Opcional: si quieres que el gatillo vuelva visualmente a 0 al soltar
        UpdateTriggerVisual(0f);
    }

    public float ComputeUseStrength(float strength)
    {
        // Suavizado tipo WaterSpray
        float delta = Time.realtimeSinceStartup - _lastUseTime;
        _lastUseTime = Time.realtimeSinceStartup;

        if (strength > _dampedUseStrength)
        {
            _dampedUseStrength = Mathf.Lerp(_dampedUseStrength, strength, _triggerSpeed * delta);
        }
        else
        {
            _dampedUseStrength = strength;
        }

        float progress = _strengthCurve.Evaluate(_dampedUseStrength);

        // Visual del gatillo (opcional)
        UpdateTriggerVisual(progress);

        // Logica de "mantener presionado"
        if (!_isPressed && progress >= _pressThreshold)
        {
            _isPressed = true;
        }
        else if (_isPressed && progress <= _releaseThreshold)
        {
            _isPressed = false;
        }

        // Solo permitir uso si la polea lo habilitó
        if (!_useEnabled || !_detectorManoEnPolea.IsHandOnPolea)
        {
            if (_isTheSoundOfTheGunPlaying)
            {
                SonidoDePistolaAudioSource.Pause(); // o Stop()
                _isTheSoundOfTheGunPlaying = false;
            }

            _isPressed = false; // opcional, pero recomendable
            return progress;
        }
            

        if (_isPressed && _reactionBar != null && _rotatedDegrees < _maxRotationDegrees)
        {
            if (_jammed) return progress;

            float remaining = _maxRotationDegrees - _rotatedDegrees;
            float angleThisFrame = _degreesPerSecond * Time.deltaTime;
            // No te pases del tope (180)
            angleThisFrame = Mathf.Min(angleThisFrame, remaining);

            if (!_isTheSoundOfTheGunPlaying)
            {
                SonidoDePistolaAudioSource.Play();
                _isTheSoundOfTheGunPlaying = true;
            }

            _reactionBar.Rotate(_rotationAxisLocal.normalized, angleThisFrame, Space.Self);
            _rotatedDegrees += angleThisFrame;

            if (_rotatedDegrees >= _maxRotationDegrees)
            {
                _rotatedDegrees = _maxRotationDegrees;
                _jammed = true;
                TM_AtrapamientoMano.TareaCompletada(TareaPorCompletar);
                SonidoDePistolaAudioSource.Stop();
            }
        }
        else if (!_isPressed)
        {
            SonidoDePistolaAudioSource.Pause();
            _isTheSoundOfTheGunPlaying = false;
        }

        return progress;
    }

    private void UpdateTriggerVisual(float progress)
    {
        if (_triggerVisual == null) return;

        float pushAmount = _triggerRotationCurve.Evaluate(progress) * _triggerPushDistance;

        Vector3 offset = _triggerPushAxis.normalized * pushAmount;

        _triggerVisual.localPosition = _triggerStartLocalPos + offset;
    }

    public void SetUseEnabled(bool enabled)
    {
        _useEnabled = enabled;

        // Si se deshabilita, también apaga el pressed para que no quede “pegado”
        if (!enabled) _isPressed = false;
    }

    public void ResetReactionBarTravel()
    {
        _rotatedDegrees = 0f;
    }

    public void ResetForNewAttempt()
    {
        _rotatedDegrees = 0f;
        _isPressed = false;
        _dampedUseStrength = 0f;
        UpdateTriggerVisual(0f);
    }

    public void HabilitarFisicas()
    {
        Rigidbody pistolaNeumaticaRigidBody = GetComponent<Rigidbody>();
        pistolaNeumaticaRigidBody.isKinematic = false;
        pistolaNeumaticaRigidBody.useGravity = true;
        pistolaNeumaticaRigidBody.constraints = RigidbodyConstraints.None;
    }
}