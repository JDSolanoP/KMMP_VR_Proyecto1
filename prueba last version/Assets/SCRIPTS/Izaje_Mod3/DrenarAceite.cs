using System.Collections;
using UnityEngine;

public class DrenarAceite : MonoBehaviour
{
    [SerializeField] private GameObject Oil = null;
    [SerializeField] private Transform OilNewPoisition = null;
    [SerializeField] private float TimeToRefill = 1f;
    [SerializeField] private ParticleSystem OilDrops = null;
    [SerializeField] private AudioManager AudioManager = null;
    [SerializeField] private int EventoPorActivar = 0;
    [SerializeField] private TM_IZAJE_M3 TM_IZAJE_M3 = null;
    [SerializeField] private GameObject Button3D = null;
    [SerializeField] private GameObject IndustrialCart = null;
    private float _timer = 0f;
    private Vector3 _initialPosition = Vector3.zero;

    [Header("Perno")]
    [SerializeField] private GameObject PernoParaElAceite = null;
    [SerializeField] private Transform PernoNewPosition = null;
    [SerializeField] private Transform PernoFinalPosition = null;
    [SerializeField] private float TimeToMove = 1f;
    private float _timerPerno = 0f;
    private Vector3 _initialPositionPerno = Vector3.zero;

    

    public void ActivateOilRefilling()
    {
        Debug.Log("Drenando Aceite");
        StartCoroutine(OilRefilling());
    }

    private IEnumerator OilRefilling()
    {
        Button3D.SetActive(false);
        if (PernoParaElAceite != null)
        {
            StartCoroutine(MovePernoParaAceite());
            yield return new WaitForSeconds(TimeToMove / 4 * 3);
        }
        
        _initialPosition = Oil.transform.position;
        OilDrops.Play();
        AudioManager.goFxWithTime("Oil_Drops", 5f);
        yield return new WaitForSecondsRealtime(1f);
        while (_timer <= TimeToRefill)
        {
            _timer += Time.deltaTime;
            Vector3 newPosition = Vector3.Lerp(_initialPosition, OilNewPoisition.position, _timer/TimeToRefill);
            Oil.transform.position = newPosition;
            yield return null;
        }
        AudioManager.altoFx("Oil_Drops");
        OilDrops.Stop();
        AudioManager.goFx("Water_Pump", 0.5f, false, false);
        _timer = 0f;
        while (_timer <= TimeToRefill)
        {
            _timer += Time.deltaTime;
            Vector3 newPosition = Vector3.Lerp(OilNewPoisition.position, _initialPosition, _timer/TimeToRefill);
            Oil.transform.position = newPosition;
            yield return null;
        }
        AudioManager.altoFx("Water_Pump");
        IndustrialCart.SetActive(false);
        TM_IZAJE_M3.ActivarEvento(EventoPorActivar);
        gameObject.SetActive(false);
        if (PernoParaElAceite != null)
        {
            PernoParaElAceite.transform.position = PernoFinalPosition.position;
            PernoParaElAceite.SetActive(true);
        }
    }

    private IEnumerator MovePernoParaAceite()
    {
        _initialPositionPerno = PernoParaElAceite.transform.position;
        while (_timerPerno <= TimeToMove)
        {
            _timerPerno += Time.deltaTime;
            Vector3 newPosition = Vector3.Lerp(_initialPositionPerno, PernoNewPosition.position, _timerPerno / TimeToMove);
            PernoParaElAceite.transform.position = newPosition;
            yield return null;
        }
        PernoParaElAceite.SetActive(false);
    }
}