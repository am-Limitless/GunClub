using Photon.Pun;
using UnityEngine;

public class NetworkCameraManager : MonoBehaviourPunCallbacks
{
    public static NetworkCameraManager Instance;
    private Camera activeLocalPlayerCamera;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterLocalPlayerCamera(Camera camera)
    {
        // If no active camera, set this as the primary
        if (activeLocalPlayerCamera == null)
        {
            activeLocalPlayerCamera = camera;
            camera.gameObject.SetActive(true);
            Debug.Log("Primary local player camera registered");
        }
        else
        {
            // Disable additional local player cameras
            camera.gameObject.SetActive(false);
            Debug.Log("Additional local player camera disabled");
        }
    }
}