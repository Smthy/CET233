using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    //Movement
    public float speed = 8f;
    public float gravity = -9.81f;

    //Gravity
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;  
    private Vector3 vel;
    private bool isGrounded;

    //Jump
    public float jumpHeight = 3f;

    //Crouch
    private Vector3 crouchScale = new Vector3(1.1f, 0.5f, 1.1f);
    private Vector3 playerScale;
    bool crouching;

    private void Start()
    {
        playerScale = transform.localScale;
        crouching = false;
    }

    void Update()
    {
        Movement();
        Gravity();
        Crouch();
        Sprint();
        //Jump();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    /*
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            vel.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    */

    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && vel.y < 0)
        {
            vel.y = -2f;
        }

        vel.y += gravity * Time.deltaTime;
        controller.Move(vel * Time.deltaTime);
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouching = true;
            transform.localScale = crouchScale;
            speed = 4f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouching = false;
            transform.localScale = playerScale;
            speed = 8f;
        }
    }

    void Sprint()
    {
        if(crouching == false && Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 12;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 8;
        }
    }
}
