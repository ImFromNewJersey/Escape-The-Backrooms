using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public float InteractDistance = 3f;
    public LayerMask interactLayer;

    public Image InteractIcon;

    public bool Interacting;

    // Start is called before the first frame update
    void Start()
    {
        if(InteractIcon != null)
        {
            InteractIcon.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, InteractDistance, interactLayer))
        {
            if (!Interacting)
            {
                if(InteractIcon != null)
                {
                    InteractIcon.enabled = true;
                }

                if (Input.GetButtonDown("Interact"))
                {
                    if(hit.collider.tag == "Note")
                    {
                        if (hit.collider.GetComponent<NoteUI>().NoteImage.enabled == false)
                        {
                            hit.collider.GetComponent<NoteUI>().ShowNoteImage();
                        }
                        else
                        {
                            hit.collider.GetComponent<NoteUI>().HideNoteImage();
                        }
                    }
                }
            }
        }
        else
        {
            InteractIcon.enabled = false;
        }

    }
}
