using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15f;
    public float aimDuration = 0.3f;
    public Rig aimLayer;
    public GameObject debugTransform;
    [SerializeField] Camera _playerCamera;
    private PhotonView _photonView;
    [SerializeField] CinemachineFreeLook _virtualCam;
    [SerializeField] LayerMask aimColliderLayerMask = new LayerMask();


    void Start()
    {
        _photonView = GetComponent<PhotonView>();

        if (!_photonView.IsMine)
        {
            _playerCamera.enabled = false;
            Destroy(_virtualCam.gameObject);
            debugTransform.SetActive(false);
            aimLayer.weight = 0;
        }
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            GunAiming();
        }

    }

    void FixedUpdate()
    {
        if (_photonView.IsMine)
        {
            CameraRotate();
            LookAtTarget();
        }

    }

    public void GunAiming()
    {
        if (Input.GetMouseButton(1))
        {
            aimLayer.weight += Time.deltaTime / aimDuration;
        }
        else
        {
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }
    }

    public void CameraRotate()
    {
        // Rotate character based on the player's camera yaw
        float yawCamera = _playerCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }

    public void LookAtTarget()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.transform.position = raycastHit.point;
        }
    }
}
