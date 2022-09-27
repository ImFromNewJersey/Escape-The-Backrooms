using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bork : MonoBehaviour
{
    public AudioSource bork;

    private void OnTriggerEnter(Collider collision)
    {

        if (!bork.isPlaying)
        {
            bork.Play();
        }
    }

}
