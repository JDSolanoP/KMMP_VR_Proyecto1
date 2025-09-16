using UnityEngine;

public class HookObject : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToHookWith = null;
    [SerializeField] private Vector3 PositionOffset = Vector3.zero;
    [SerializeField] private Vector3 HookedObjectRotation = Vector3.zero;
    [SerializeField] private ChainGripArea ChainGripArea = null;
    [SerializeField] private int TareaACompletar = 0;
    [SerializeField] private GameObject OriginalChain = null;
    [SerializeField] private GameObject ConnectedChain = null;
    [SerializeField] private GameObject AnillaMayor = null;
    [SerializeField] private LayerMask LayersIgnore;
    [SerializeField] private LayerMask AllLayers;
    private GameObject ChainLink1 = null;
    private GameObject ChainLink2 = null;
    private GameObject ChainLink3 = null;
    private bool _isHooked = false;
    private bool _isBeignGrabbed = false;
    private bool _canBeCaught = true;
    private GameObject _originalParent = null;
    private Vector3 _originalPosition = Vector3.zero;
    private Vector3 _originalRotation = Vector3.zero;
    private Vector3 _originalScale = Vector3.zero;

    private void Start()
    {
        _originalParent = ObjectToHookWith.transform.parent.gameObject;
        _originalPosition = ObjectToHookWith.transform.localPosition;
        _originalRotation = ObjectToHookWith.transform.localRotation.eulerAngles;
        _originalScale = ObjectToHookWith.transform.localScale;
        ChainLink1 = ChainGripArea.Gancho.transform.parent.gameObject;
        ChainLink2 = ChainGripArea.Gancho.transform.parent.parent.gameObject;
        ChainLink3 = ChainGripArea.Gancho.transform.parent.parent.parent.gameObject;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == ObjectToHookWith.name && !_isHooked && _isBeignGrabbed)
        {
            other.transform.parent.transform.position = gameObject.transform.position + PositionOffset;
            other.transform.parent.transform.rotation = Quaternion.Euler(HookedObjectRotation);
            other.transform.parent.GetComponent<One_Hand_PickUp>().enabled = false;
            other.transform.parent.transform.SetParent(transform, true);
            other.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            
            ChainGripArea.SetIsActivate(false);
            _isHooked = true;
            //Parte 2
            //FixedJoint fixedJoint = gameObject.transform.parent.gameObject.AddComponent<FixedJoint>();
            //fixedJoint.connectedBody = other.GetComponent<Rigidbody>();
            FindObjectOfType<TM_IZAJE_M3>().CheckHooks(TareaACompletar);
            if (AnillaMayor != null)
            {
                AnillaMayor.SetActive(true);
            }
            if (ConnectedChain != null && OriginalChain != null)
            {
                OriginalChain.SetActive(false);
                ConnectedChain.SetActive(true);
                gameObject.SetActive(false);
            }
            /*ObjectToHookWith.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;*/
            //Destroy(ObjectToHookWith.GetComponent<FixedJoint>());
        }
    }

    public void SetIsHooked(bool state)
    {
        _isHooked = state;
    }

    public bool GetIsHooked()
    {
        return _isHooked;
    }

    public GameObject GetObjectToHookWith()
    {
        return ObjectToHookWith;
    }

    public void SetIsBeignGrabbed(bool state)
    {
        _isBeignGrabbed = state;
        if (_isBeignGrabbed)
        {
            ChainLink1.GetComponent<MeshCollider>().excludeLayers = LayersIgnore;
            ChainLink2.GetComponent<MeshCollider>().excludeLayers = LayersIgnore;
            ChainLink3.GetComponent<MeshCollider>().excludeLayers = LayersIgnore;
        }
        else
        {
            ChainLink1.GetComponent<MeshCollider>().excludeLayers = AllLayers;
            ChainLink2.GetComponent<MeshCollider>().excludeLayers = AllLayers;
            ChainLink3.GetComponent<MeshCollider>().excludeLayers = AllLayers;
        }
    }
}