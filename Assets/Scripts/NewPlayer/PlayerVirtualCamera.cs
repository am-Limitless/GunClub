using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class PlayerVirtualCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private PhotonView photonView;

    void Awake()
    {
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        // Only enable this virtual camera if it belongs to the local player
        if (!photonView.IsMine)
        {
            virtualCamera.enabled = false;
        }
    }
}