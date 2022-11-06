using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Remnant : MonoBehaviour
{

    public Image RemnantImage;
    public AudioSource pickupsound;
    public GameObject Player;
    public Camera PlayerCam;
    GameObject Cover;

    void Start()
    {
        //disable the image of the remnant (Idk what this is it might be redundancy between this and the pickup ui
        RemnantImage.enabled = false;
    }


    public void Collect()
    {
        //Play the pickup sound 
        pickupsound.Play();
        //In every escape object
        foreach (GameObject GateCover in GameObject.FindGameObjectsWithTag("Gates"))
        {
            //idk what the first line is there for, but increase the escape change in every escape object
            Cover = GateCover;
            Cover.GetComponent<EscapeObject>().EscapeChance += .1f;
        }
        //turn off the mesh and then invoke a function to delete the remnant
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Invoke("DestroyAfterPlay", .8f);
    }

    public void DestroyAfterPlay()
    {
        //This is made in a seperate function and invoked so that the sound has enough time to play
        Destroy(gameObject);
    }

    //Draw gizmos on each remnant 
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1);
    }

}
