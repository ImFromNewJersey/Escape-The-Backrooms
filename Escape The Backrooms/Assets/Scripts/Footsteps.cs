using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public CharacterController Controller;
    public AudioSource WalkSound;
    public AudioSource SprintSound;
    public float WalkDistance = .65f;
    public float SprintDistance = .80f;
    private float TimeBetween;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Step();
    }
    void Step()
    {
        if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
        {
            if (WalkSound.isPlaying || SprintSound.isPlaying)      
            {
                SprintSound.Stop();
                WalkSound.Stop();
            }
        }
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (Input.GetButton("Left Shift"))
            {
                TimeBetween += Time.deltaTime;
                if (TimeBetween > SprintDistance)
                {
                    if (WalkSound.isPlaying)
                    {
                        WalkSound.Stop();
                    }
                    SprintSound.Play();
                    TimeBetween = 0f;
                }
            }
            if (!Input.GetButton("Left Shift"))
            {
                TimeBetween += Time.deltaTime;
                if (TimeBetween > WalkDistance)
                {
                    WalkSound.Play();
                    TimeBetween = 0f;
                }
            }
        }
    }

}


