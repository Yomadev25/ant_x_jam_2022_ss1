using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FixedJoystick joystick;
    CharacterController characterController;
    public float speed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;
    Rigidbody rigidbody;
    Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        animator.SetBool("Run", false);
        moveDirection = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical);
        moveDirection *= speed;
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            animator.SetBool("Run", true);
        }
        if (joystick.Vertical < 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
