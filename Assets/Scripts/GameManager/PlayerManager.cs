using Photon.Pun;
using System.IO;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    PhotonView photonView;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if (photonView.IsMine)
        {
            CreateController();
        }
    }
    private void CreateController()
    {
        Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
        GameObject playerInstance = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Robot_humanoid"), spawnpoint.position, spawnpoint.rotation, 0, new object[] { photonView.ViewID });

        // Optional: Additional logging
        if (playerInstance != null)
        {
            PhotonView playerPhotonView = playerInstance.GetComponent<PhotonView>();
            if (playerPhotonView != null)
            {
                Debug.Log($"Player instantiated with PhotonView ID: {playerPhotonView.ViewID}");
            }
        }
    }
}
