using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float velocity = 10f;
    private Animator animator;
  
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * velocity;
        float moveY = Input.GetAxis("Vertical") * Time.deltaTime * velocity;
       if(moveX != 0 || moveY != 0)
        {
            if (moveX > 0)
            {
              transform.Rotate(transform.rotation.x, 90, transform.rotation.z);
            }
            animator.SetBool("andando", true);
        }
        else if(moveX == 0 && moveY == 0)
        {
            animator.SetBool("andando", false);
        }
        transform.Translate(moveX, 0, moveY);
    }
}
