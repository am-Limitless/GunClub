using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15f;
    [SerializeField] Camera _playerCamera;
    private PhotonView _photonView;

    [SerializeField] CinemachineFreeLook _virtualCam;


    void Start()
    {
        _photonView = GetComponent<PhotonView>();

        if (!_photonView.IsMine)
        {
            _playerCamera.enabled = false;
            Destroy(_virtualCam.gameObject);
        }
    }


    void FixedUpdate()
    {
        if (!_photonView.IsMine)
        {
            return;
        }

        // Rotate character based on the player's camera yaw
        float yawCamera = _playerCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }
}
