using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
     private Rigidbody rb;
    private Animator animator;
    private float Velocity = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * Velocity;
        float MoveZ = Input.GetAxis("Vertical") * Time.deltaTime * Velocity;

        if (moveX !=0|| MoveZ != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else if(moveX == 0  && MoveZ == 0) 
        {
            animator.SetBool("isWalking", false);
        }


        
        transform.Translate(moveX, 0, MoveZ);


    }
}
