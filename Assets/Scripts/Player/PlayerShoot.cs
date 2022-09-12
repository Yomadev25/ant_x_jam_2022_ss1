using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("GUN REFERENCES")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPos;
    [SerializeField] private Transform _targetPos;
    private Vector3 _screenCenter;

    [Header("SHOOTING REFERENCE")]
    [SerializeField] private float _fireRate;
    [SerializeField] private float _nextFire;
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private GameObject _jetpack;
    [SerializeField] private Animator _anim;

    private PlayerCamera _playerCamera;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerCamera = FindObjectOfType<PlayerCamera>();
        _screenCenter = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > _nextFire && !_playerController.isRun && !_playerController.isDash)
        {
            _nextFire = Time.time + _fireRate;
            Fire();
        }

        FireAnimation();
    }

    private void LateUpdate()
    {
        if (_playerCamera.cameraType == PlayerCamera.CameraType.Combat)
            _shootPos.LookAt(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 100)));
        else if (_playerCamera.cameraType == PlayerCamera.CameraType.Basic)
            _shootPos.localEulerAngles = new Vector3(45f, -90f, 0);
    }

    void Fire()
    {
        Instantiate(_bulletPrefab, _shootPos.position, _shootPos.rotation);
        _shootSound.Play();
    }

    void FireAnimation()
    {
        if (Input.GetMouseButton(0) && !_playerController.isRun && !_playerController.isDash)
        {
            _anim.SetBool("isShoot", true);
            _anim.SetFloat("Shoot", _playerController.isWalk ? 0.5f : 1);
            _jetpack.SetActive(true);
        }
        else
        {
            _anim.SetBool("isShoot", false);
            _jetpack.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _anim.SetBool("isShoot", false);
    }
}
