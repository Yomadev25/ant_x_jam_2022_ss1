using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _playerPos;
    [SerializeField] private Transform _playerObjPos;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 direction = _playerPos.position - new Vector3(transform.position.x, _playerPos.position.y, transform.position.z);
        _orientation.forward = direction.normalized;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDirection = _orientation.forward * vertical + _orientation.right * horizontal;

        if (inputDirection != Vector3.zero)
            _playerObjPos.forward = Vector3.Slerp(_playerObjPos.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
    }
}
