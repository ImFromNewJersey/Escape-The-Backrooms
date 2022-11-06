using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeObject : MonoBehaviour
{
    public float EscapeChance = 0.2f;
    List<GameObject> PatrolPoints = new List<GameObject>();
    public Transform Player;
    int RespawnPoint;

    private void Start()
    {
        //create a list of all the patrol points
        foreach (GameObject Patrol_points in GameObject.FindGameObjectsWithTag("Waypoints"))
        {
            PatrolPoints.Add(Patrol_points);
        }
    }
    //when you enter the object
    void OnTriggerEnter(Collider collision)
    {
        //colliding object is player
        if (collision.tag == "Player")
        {
            //If a random number is lower than the escape chance
            if (UnityEngine.Random.Range(0f, 1f) <= EscapeChance)
            {
                //load whatever the win screen is
                SceneManager.LoadScene(3);
            }
            else
            {   //if the player doesnt escape, teleport their ass to a random patrol point
                RespawnPoint = UnityEngine.Random.Range(0, PatrolPoints.Count);
                Player.position = PatrolPoints[RespawnPoint].transform.position;
            }
        }
    }
    public float GizmosRadius = 1.0f;
    //draw gizmos over the escape objects
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1);
    }

}
