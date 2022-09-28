using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class Death : MonoBehaviour
{

    public float Deathtime;
    public Transform PlayerT;
    public Transform head;
    public GameObject Player;
    public Camera PlayerCam;
    public Animator Animator;
    public AudioSource Deathsound;
    public NavMeshAgent EnemyNav;
    public bool Caught;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Caught = true;
            EnemyNav.speed = 0;
            Deathsound.Play();
            PlayerT.LookAt(head.transform);
            gameObject.transform.LookAt(PlayerT);
            Animator.SetBool("sprinting", false);
            Animator.SetBool("Caughtplayer", true);
            Player.GetComponent<Movement>().enabled = false;
            Player.GetComponent<Footsteps>().enabled = true;
            PlayerCam.GetComponent<Lookscript>().enabled = false;
            PlayerCam.GetComponent<AnimationTriggers>().enabled = false;
            Invoke("Dead", Deathtime);
        }
    }

    void Dead()
    {
        SceneManager.LoadScene(2);
    }

}
