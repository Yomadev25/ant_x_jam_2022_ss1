using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    public enum CameraType { Basic, Combat }

    [Header("REFERENCES")]
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _playerPos;
    [SerializeField] private Transform _playerObjPos;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _rotationSpeed;

    [Header("CAMERA STYLE")]   
    [SerializeField] private CameraType _cameraType;
    [SerializeField] private Transform _combatLookAt;
    [SerializeField] private GameObject _thirdPersonCam;
    [SerializeField] private GameObject _combatCam;
    private CinemachineFreeLook _tpsCam;
    private CinemachineFreeLook _lockCam;

    public CameraType cameraType => _cameraType;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _tpsCam = _thirdPersonCam.GetComponent<CinemachineFreeLook>();
        _lockCam = _combatCam.GetComponent<CinemachineFreeLook>();

        SwitchCameraStyle(CameraType.Basic);
    }

    private void Update()
    {
        if (Input.GetMouseButton(1)) SwitchCameraStyle(CameraType.Combat);
        else SwitchCameraStyle(CameraType.Basic);
    }

    private void LateUpdate()
    {
        Vector3 direction = _playerPos.position - new Vector3(transform.position.x, _playerPos.position.y, transform.position.z);
        _orientation.forward = direction.normalized;

        if (_cameraType == CameraType.Basic)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 inputDirection = _orientation.forward * vertical + _orientation.right * horizontal;

            if (inputDirection != Vector3.zero)
                _playerObjPos.forward = Vector3.Slerp(_playerObjPos.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
        }
        else if (_cameraType == CameraType.Combat)
        {
            Vector3 dirToCombatLookAt = _combatLookAt.position - new Vector3(transform.position.x, _combatLookAt.position.y, transform.position.z);
            _orientation.forward = dirToCombatLookAt.normalized;

            _playerObjPos.forward = dirToCombatLookAt.normalized;
        }
    }

    private void SwitchCameraStyle(CameraType newType)
    {
        if (_cameraType == newType) return;

        _thirdPersonCam.SetActive(false);
        _combatCam.SetActive(false);

        if (newType == CameraType.Basic) _thirdPersonCam.SetActive(true); _lockCam.m_XAxis.Value = _tpsCam.m_XAxis.Value;
        if (newType == CameraType.Combat) _combatCam.SetActive(true); _tpsCam.m_XAxis.Value = _lockCam.m_XAxis.Value;

        _cameraType = newType;
    }
}
