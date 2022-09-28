using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggers : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if (Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal"))
        {
            animator.SetBool("IsWalking", true);
        }
        if (!Input.GetButton("Vertical") && !Input.GetButtonDown("Horizontal"))
        {
            animator.SetBool("IsWalking", false);
        }
        if (Input.GetButtonDown("Left Shift"))
        {
            animator.SetBool("IsSprinting", true);
        }
        if (!Input.GetButton("Left Shift"))
        {
            animator.SetBool("IsSprinting", false);
        }
    }
}
