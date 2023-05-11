using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Gun : MonoBehaviour
{

    [SerializeField] private projctle projetil;
    [SerializeField] private float damage;
    [SerializeField] private Transform bulletSpawm;
    [SerializeField] private PlayerScript scriptPlayer;

    private PlayerInput playerInput;
    private bool fire;
    private bool isFiring;
    void Awake()
    {
        playerInput = new PlayerInput();
       
        playerInput.Movement.Jump.started += OnFireInput;
        playerInput.Movement.Jump.canceled += OnFireInput;
    }


    private void OnFireInput(InputAction.CallbackContext context)
    {
        fire = context.ReadValueAsButton();
        Fire();
        scriptPlayer.isFiring = fire;
       
    }
     

    private void Fire()
    {
        if (fire)
        {
            print("aitrou?");
            Instantiate(projetil, bulletSpawm.position, bulletSpawm.rotation);

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
