using Photon.Pun;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    private Animator _animator;
    private Vector2 _input;
    private PhotonView _photonView;
    Rigidbody _rb;


    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        if (!_photonView.IsMine)
        {
            Destroy(_rb);
        }

    }
    void Update()
    {
        if (!_photonView.IsMine)
        {
            return;
        }

        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");

        _animator.SetFloat("InputX", _input.x);
        _animator.SetFloat("InputY", _input.y);
    }
}

