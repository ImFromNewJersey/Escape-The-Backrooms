using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    private float InteractDistance = 3f;
    public LayerMask interactLayer;
    public Image PickupUI;
    public Image InteractIcon;

    void Start()
    {
        // At the start disable the UI for picking up
        PickupUI.enabled = false;
    }

    void Update()
    {
        //Create a raycast going forward
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        //If the raycast hits an interactable object
        if(Physics.Raycast(ray, out hit, InteractDistance, interactLayer))
        {
            //enable the interaction icon
            InteractIcon.enabled = true;
            //if you press the interact button
            if (Input.GetButtonDown("Interact"))
            {
                //and hit an object with the "remnant tag"
                if(hit.collider.tag == "Remnant")
                {
                    //Do the things to collect the remnant in the remnant script
                    if (hit.collider.GetComponent<Remnant>().RemnantImage.enabled == false)
                    {
                        hit.collider.GetComponent<Remnant>().Collect();
                    }
                }
            }
       
        }
        else
        {
            //disable the icon when its not in use
            InteractIcon.enabled = false;
        }

    }
}
