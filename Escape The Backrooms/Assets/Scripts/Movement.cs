using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float sprintspeed = 25f;
    public float gravity = -19.62f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 04f;
    public LayerMask groundMask;
    bool isGrounded;
    public float Jumpheight = 3;

    void Update()
    {

        //CHECKS THE PLAYER IS GROUNDED TO RESET GRAVITY
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //MOVEMENT
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (!Input.GetButtonDown("Left Shift")){
            if (move.sqrMagnitude > 1)
            {
                move.Normalize();
            }
            controller.Move(move * speed * Time.deltaTime);
        }
        if (Input.GetButton("Left Shift"))
        {
            if (move.sqrMagnitude > 1)
            {
                move.Normalize();
            }
            controller.Move(move * sprintspeed * Time.deltaTime);
        }

        //CREATES GRAVITY
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //JUMP FUNCTION
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(Jumpheight * -2f * gravity);
        }
    }
}