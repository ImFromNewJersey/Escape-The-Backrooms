using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteUI : MonoBehaviour
{

    public Image NoteImage;
    public AudioSource pickup;
    public AudioSource putdown;
    public GameObject Player;
    public Camera PlayerCam;
    GameObject Cover;

    void Start()
    {
        NoteImage.enabled = false;
    }


    public void ShowNoteImage()
    {
        pickup.Play();
        //NoteImage.enabled = true;
        //Player.GetComponent<Movement>().enabled = false;
        //Player.GetComponent<Footsteps>().enabled = true;
        //PlayerCam.GetComponent<Lookscript>().enabled = false;
        //PlayerCam.GetComponent<AnimationTriggers>().enabled = false;
        foreach (GameObject GateCover in GameObject.FindGameObjectsWithTag("Gates"))
        {
            Cover = GateCover;
            Cover.GetComponent<WallTrigger>().EscapeChance += .1f;
        }
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Invoke("DestroyAfterPlay", .8f);
    }

    public void HideNoteImage()
    {
        //putdown.Play();
        //NoteImage.enabled = false;
        //Player.GetComponent<Movement>().enabled = true;
        //Player.GetComponent<Footsteps>().enabled = true;
        //PlayerCam.GetComponent<Lookscript>().enabled = true;
        //PlayerCam.GetComponent<AnimationTriggers>().enabled = true;
        //Destroy(gameObject);
    }

    public void DestroyAfterPlay()
    {
        Destroy(gameObject);
    }

}
