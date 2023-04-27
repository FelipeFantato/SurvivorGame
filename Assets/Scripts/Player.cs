using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    static public int vida;
    [SerializeField] private Text vidaTxt;
    [SerializeField] private Text tempoTxt;
    [SerializeField] private Text Vitoria;
    [SerializeField] private Text Derrota;
    private float timer;


    private PlayerInput input;
    private CharacterController playerControl;
    private Vector2 playerVec2;
    private Vector3 playerVec3;
    private Vector3 currentRunVelocity;
    private float velocidadeRodar = 10f;
    private Animator animator;

    
   [SerializeField] private float velocidade;
    [SerializeField] private float RunVelocidade;
   
    private bool taMovendo;

    private void Awake()
    {
        Vitoria.enabled = false;
        Derrota.enabled = false;
        vida = 100;
        input = new PlayerInput();
        playerControl = GetComponent<CharacterController>();

        input.Movement.Andar.started += OnMovementInput;
        input.Movement.Andar.canceled += OnMovementInput;
        input.Movement.Andar.performed += OnMovementInput;
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
       
        taMovendo = playerVec2.y != 0 || playerVec2.x != 0;
       
    }

    void Update()
    {

        AttTempo();
        MovimentaJogador();
        TrocaAnim();
        RodaJogador();
    }
    private void AttTempo()
    {
       
        timer += Time.deltaTime;
        int seconds = (int)(timer % 60);

        tempoTxt.text = "Tempo- " + seconds;

        if(seconds == 10)
        {
            Vitoria.enabled = true;
            Time.timeScale = 0;
        }
    }
    private void RodaJogador()
    {
        Vector3 positionLookAt;
        positionLookAt.x = playerVec3.x;
        positionLookAt.y = playerVec3.y;
        positionLookAt.z = playerVec3.z;
        Quaternion currentRotation = transform.rotation;
        if (taMovendo)   
        {
            Quaternion lookAtRotation = Quaternion.LookRotation(positionLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, lookAtRotation, velocidadeRodar *Time.deltaTime);
        }
    }

    private void TrocaAnim()
    {
        if (taMovendo)
            animator.SetBool("andando", true);
        else if (!taMovendo)        
            animator.SetBool("andando", false);
    }

    private void MovimentaJogador()
    {
    playerControl.Move(playerVec3 * velocidade * Time.deltaTime );
        
    }

    private void OnEnable()
    {
        input.Movement.Enable();
    }
    private void OnDisable()
    {
        input.Movement.Disable();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        vida -=  10;
        vidaTxt.text = "vida " + vida;
        if (vida <= 0)
        {
            Derrota.enabled = true;
            Time.timeScale = 0; 
        }
    }
}