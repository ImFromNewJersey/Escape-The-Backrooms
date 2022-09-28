using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NoFOVDetect : MonoBehaviour
{
    public Transform player;
    public float EnemyFov = 180;
    public float EnemyRadius = 90;
    public bool detected = false;
    public float heightMultiplayer = 1.5f;
    public Animator animator;

    public bool inFOV(Transform checkingObject, Transform target, float EnemyFov, float EnemyRadius)
    {
        Vector3 directionBetween = (target.position - checkingObject.position).normalized;
        directionBetween.y *= 0;
        RaycastHit hit;
        if (Physics.Raycast(checkingObject.position + Vector3.up * heightMultiplayer, (target.position - checkingObject.position).normalized, out hit, EnemyRadius))
        {
            //if (LayerMask.LayerToName(hit.transform.gameObject.layer) == "Player")
            if (hit.transform.CompareTag("Player"))
            {
                float angle = Vector3.Angle(checkingObject.forward + Vector3.up * heightMultiplayer, directionBetween);

                //     isInFOV = true; 
                if (angle <= EnemyFov) 
                { 
                GetComponent<NavMeshAgent>().speed = 15;
                animator.SetBool("sprinting", true);
                return true;
                }
                
            }
        }
        //  Debug.Log ("target out");
        GetComponent<NavMeshAgent>().speed = 5;
        animator.SetBool("sprinting", false);
        return false;

    }

    private void FixedUpdate()
    {
        detected = inFOV(transform, player, EnemyFov, EnemyRadius);

    }

}



