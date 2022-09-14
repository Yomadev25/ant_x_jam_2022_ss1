using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    CharacterController characterController;
    public float speed = 6.0f;
    public float jumpSpeed = 10f;
    public float gravity = 300f;
    Vector3 inputVec;
    Vector3 targetDirection;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        speed = 6.0f;
        jumpSpeed = 10f;
        Time.timeScale = 1;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"),0.0f,0.0f);
        moveDirection *= speed;
        if (Input.GetButton("Jump")){
            moveDirection.y = jumpSpeed;
            animator.SetBool("Jump",true);
        }
        if (characterController.isGrounded){
            animator.SetBool("Jump",false);
        }            

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
        if (Input.GetMouseButton(0)){

            animator.SetBool("Attack",true);

        }
        else{
            animator.SetBool("Attack",false);

        }
    }

        void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy"){
            animator.SetBool("Dead",true);
            speed = 0f;
            jumpSpeed = 0f;
        }
    }
}
