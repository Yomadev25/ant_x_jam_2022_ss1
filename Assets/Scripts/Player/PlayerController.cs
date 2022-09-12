using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("MOVEMENT REFERENCES")]
    [SerializeField] private float _speed;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;
    private float _runSpeed;
    private float _moveSpeed;
    [SerializeField] private float _gravity;
    private float _horizontalInput;
    private float _verticalInput;
    Vector3 moveDirection;
    [HideInInspector] public bool isWalk;
    [HideInInspector] public bool isRun;
    [HideInInspector] public bool isDash;

    [Header("OTHER REFERENCES")]
    [SerializeField] private Transform _orientation;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _jetpack;
    [SerializeField] private AudioSource _dashSound;
    private CharacterController _characterController;
    private Rigidbody _rb;
    private PlayerCamera _playerCamera;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        _playerCamera = FindObjectOfType<PlayerCamera>();

        _runSpeed = _speed * 2.5f;
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
        moveDirection.y += Physics.gravity.y * _gravity * Time.deltaTime;

        if (_playerCamera.cameraType == PlayerCamera.CameraType.Basic) Run(Input.GetKey(KeyCode.LeftShift));
        else Run(false);

        if (_characterController.isGrounded && Input.GetButtonDown("Jump") && _playerCamera.cameraType == PlayerCamera.CameraType.Combat)
        {
            StartCoroutine(Dash());
        }
                 
        _characterController.Move(moveDirection.normalized * _moveSpeed * Time.deltaTime);
    }

    private void Run(bool _isRun)
    {
        _moveSpeed = _isRun ? _runSpeed : _speed;
        isRun = _isRun ? true : false;
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        isDash = true;
        _dashSound.Play();

        while (Time.time < startTime + _dashTime)
        {           
            _characterController.Move(moveDirection.normalized * _dashSpeed * Time.deltaTime);
            _anim.SetTrigger("Dash");
            _jetpack.SetActive(true);
            yield return null;
        }

        _jetpack.SetActive(false);
        isDash = false;
    }

    private void CharacterAnimation()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            isWalk = true;
            _anim.SetBool("isWalk", true);
        }
        else
        {
            isWalk = false;
            _anim.SetBool("isWalk", false);
        }

        if (_moveSpeed > _runSpeed - 0.1f && isWalk)
        {
            _anim.SetBool("isRun", true);
        }
        else
        {
            _anim.SetBool("isRun", false);
        }
    }
}
