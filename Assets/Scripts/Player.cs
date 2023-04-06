using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    #region codigoPassado
    /*
    private Rigidbody rb;
    private float velocity = 10f;
    private Animator animator;

    rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * velocity;
        float moveY = Input.GetAxis("Vertical") * Time.deltaTime * velocity;
       if(moveX != 0 || moveY != 0)
        {
            if (moveX > 0)
            {
              //  transform.rotation.y =
            }
            animator.SetBool("andando", true);
        }
        else if(moveX == 0 && moveY == 0)
        {
            animator.SetBool("andando", false);
        }
        transform.Translate(moveX, 0, moveY);
    }
        */
    #endregion

    private PlayerInput input;
    private CharacterController characterController;
    
    private Vector2 playerVec2;
    private Vector3 playerVec3;

    private Vector3 currentRunVelocity;

    private bool isMovingAnimation;
    private bool isRotation;
    private bool isRunningPressed;
    private float rotationVelocity = 10f;
    private Animator animator;

    private int a_isWalking;
    private int a_isRunning;

   [SerializeField] private float velocidade;

    [SerializeField] private float RunVelocidade;
    private bool isRuningAnimation;
    private bool isMoving;

    private void Awake()
    {
        input = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        input.Movement.Walk.started += OnMovementInput;
        input.Movement.Walk.canceled += OnMovementInput;
        input.Movement.Walk.performed += OnMovementInput;

        input.Movement.Run.started += OnRunningInput;
        input.Movement.Run.canceled += OnRunningInput;
        GetAnimatorParameters();
          animator = GetComponent<Animator>();
       
    }
    private void OnMovementInput(InputAction.CallbackContext context)
    {
        playerVec2 = context.ReadValue<Vector2>();
        playerVec3.x = playerVec2.x;
        playerVec3.y = 0f;
        playerVec3.z = playerVec2.y;

        currentRunVelocity.x = playerVec3.x * RunVelocidade;
        currentRunVelocity.z = playerVec3.z * RunVelocidade;
       
        isMoving = playerVec2.y != 0 || playerVec2.x != 0;
       
    }

    private void GetAnimatorParameters()
    {
        a_isWalking = Animator.StringToHash("andando");
        a_isRunning = Animator.StringToHash("correndo");

    }

    private void OnRunningInput(InputAction.CallbackContext context)
    {
        isRunningPressed =context.ReadValueAsButton();
       
       
    }

    void Update()
    {
        MovePlayer();
        AnimationHandler();
        PlayerRotationHandler();
    }

    private void PlayerRotationHandler()
    {
        Vector3 positionLookAt;
        positionLookAt.x = playerVec3.x;
        positionLookAt.y = playerVec3.y;
        positionLookAt.z = playerVec3.z;
        Quaternion currentRotation = transform.rotation;


        if (isMoving)   
        {
            Quaternion lookAtRotation = Quaternion.LookRotation(positionLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, lookAtRotation, rotationVelocity *Time.deltaTime);
        }
    }

    private void AnimationHandler()
    {
        bool isRuningAnimation = animator.GetBool(a_isRunning);
        bool isMovingAnimation = animator.GetBool(a_isWalking);

        if (isMoving && !isMovingAnimation)
        {
            animator.SetBool(a_isWalking, true);
        }
        else if (!isMoving && isMovingAnimation)
        {
            animator.SetBool(a_isWalking, false);
        }
        if (isMoving && isRunningPressed && !isRuningAnimation)
        {
            animator.SetBool(a_isRunning, true);
        }
        else if (!isMoving || !isRunningPressed && isRuningAnimation)
        {
            animator.SetBool(a_isRunning, false);
        }
    }

    private void MovePlayer()
    {
      
        if (isRunningPressed)
        {
            characterController.Move(currentRunVelocity * Time.deltaTime * velocidade);

        }
        else
        {
            characterController.Move(playerVec3 * Time.deltaTime * velocidade);

        }
    }

    private void OnEnable()
    {
        input.Movement.Enable();
    }
    private void OnDisable()
    {
        input.Movement.Disable();
    }
}