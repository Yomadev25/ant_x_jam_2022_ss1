using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerType { Human, Robot }
    [SerializeField] private PlayerType playerType;

    [Header("MOVEMENT REFERENCES")]
    [SerializeField] private float _speed;
    private float _runSpeed;
    private float _moveSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    private bool _readyToJump = true;
    private float _horizontalInput;
    private float _verticalInput;
    Vector3 moveDirection;
    bool isWalk;

    [Header("OTHER REFERENCES")]
    [SerializeField] private Transform _orientation;
    [SerializeField] private Animator _anim;
    private CharacterController _characterController;
    private Rigidbody _rb;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();

        _runSpeed = _speed * 2;
    }

    void Update()
    {
        ControllerInput();
        CharacterAnimation();
    }

    private void ControllerInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;

        Run(Input.GetKey(KeyCode.LeftShift));

        if (Input.GetButton("Jump") && _readyToJump && _characterController.isGrounded)
        {
            Debug.Log("Jumped!!");
            _readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), _jumpCooldown);
        }

        moveDirection.y -= _gravity * Time.deltaTime;
        _characterController.Move(moveDirection.normalized * _moveSpeed * Time.deltaTime);
    }

    private void Run(bool isRun)
    {
        _moveSpeed = isRun ? _runSpeed : _speed;
    }

    private void Jump()
    {
        moveDirection.y = _jumpForce;
    }

    private void ResetJump()
    {
        _readyToJump = true;
    }

    private void CharacterAnimation()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            _anim.SetBool("isWalk", true);
        }
        else
        {
            _anim.SetBool("isWalk", false);
        }
    }
}
