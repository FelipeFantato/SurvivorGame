using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private PlayerInput playerInput;
    private CharacterController characterController;
    private Animator animator;
    private bool ismoving;
    private bool isrotation;
    private bool isrunning;
    private bool isJumping;
    public bool isFiring;
    
    private float rotationVelocity = 5f; 


    private Vector2 playerMovementInput;
    private Vector3 playerMovement;
    private Vector3 runMovement;

    private int a_isWalking;
    private int a_isRunning;
    


    [SerializeField] private float velocidade;
    [SerializeField] private float runVelocity;
    [SerializeField] private float gravity;


    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        GetAnimatorParameters();

        playerInput.Movement.walk.started += OnMovementInput;
        playerInput.Movement.walk.canceled += OnMovementInput;
        playerInput.Movement.walk.performed += OnMovementInput;

        playerInput.Movement.Run.started += OnRunningInput;
        playerInput.Movement.Run.canceled += OnRunningInput;
        
        playerInput.Movement.Jump.started += OnJumpInput;
        playerInput.Movement.Jump.canceled += OnJumpInput;


    }


    private void GetAnimatorParameters()
    {
        a_isWalking = Animator.StringToHash("isWalking");
        a_isRunning = Animator.StringToHash("isRunning");
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {

        playerMovementInput = context.ReadValue<Vector2>();
        playerMovement.x = playerMovementInput.x;
        playerMovement.y = 0f;
        playerMovement.z = playerMovementInput.y;
        runMovement.x = playerMovement.x * runVelocity;
        runMovement.z = playerMovement.z * runVelocity;

        ismoving = playerMovementInput.y != 0 || playerMovementInput.x != 0;
        
    }


   private void OnRunningInput(InputAction.CallbackContext context)
    {
        isrunning = context.ReadValueAsButton();
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValueAsButton();
    }
    void Update()
    {
        MovePlayer();
        AnimationHandler();
        PlayerRotationHandler();

    }

    private void PlayerRotationHandler()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = playerMovement.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = playerMovement.z;
        Quaternion currentRotation = transform.rotation;


        if (ismoving)
        {
            Quaternion lookAtRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, lookAtRotation, rotationVelocity * Time.deltaTime);
        }
    }

    private void AnimationHandler()
    {
        bool isWalkinganimation = animator.GetBool(a_isWalking);
        bool isRunninganimation = animator.GetBool(a_isRunning);


        if (ismoving && !isWalkinganimation)
        {
            animator.SetBool(a_isWalking, true);
        }
        else if (!ismoving && isWalkinganimation)
        {
            animator.SetBool(a_isWalking,false);
        }

        if(ismoving && isrunning && !isRunninganimation)
        {
            animator.SetBool(a_isRunning, true);
        }

        else if (!ismoving || !isrunning && isRunninganimation)
        {
            animator.SetBool(a_isRunning,false);
        }


        if (isFiring)
        {
            animator.SetBool("isFiring", true);
            print("ATIRANDO!");
        }
        else
        {
            animator.SetBool("isFiring", false);
            print("NAO ATIRANDO");
        }


    }

    private void MovePlayer()

    {
       // print(this.gameObject.transform.position.y);
        characterController.Move(Vector3.down * gravity * Time.deltaTime);
        bool tanochao = characterController.isGrounded;
        if (tanochao && isJumping)
        {

            // print("pulou?");
               // characterController.Move(Vector3.up * Time.deltaTime * 50);
        }

        if (isrunning)
        {
            
            characterController.Move(runMovement * Time.deltaTime * velocidade);
        }
        else
        {
            characterController.Move(playerMovement * Time.deltaTime * velocidade);
        }

    }

    private void OnEnable()
    {
        playerInput.Movement.Enable();

    }

    private void OnDisable()
    {
        playerInput.Movement.Disable();
    }
}
