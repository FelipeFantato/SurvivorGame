using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Gun : MonoBehaviour
{

    [SerializeField] private projctle projetil;
    [SerializeField] private float damage;
    [SerializeField] private Transform bulletSpawm;
    private PlayerInput playerInput;
    private bool fire;

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
