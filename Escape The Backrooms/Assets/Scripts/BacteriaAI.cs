using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BacteriaAI : MonoBehaviour
{
    //Entity Variables// 

    //Entity Objects/Components
    private NavMeshAgent EntityNavmesh;
    public Transform EntityHead;
    private Animator EntityAnimator;
    //Entity Patrol Variables
    List<GameObject> PatrolPoints = new List<GameObject>();
    int PatrolIndex;
    private bool Patroling;
    //Entity Detection Variables
    public float EnemyFov = 180;
    public float EnemyRadius = 90;
    public float heightMultiplayer = 1.5f;
    private bool detected = false;

    //Player Variables//

    //Player Objects/Components
    public GameObject PlayerObject;
    private Transform PlayerTransform;
    //Player Death Variables
    private bool Caught = false;
    public float Deathtime;
    public AudioSource Deathsound;

    void Start()
    {
        //Initiate variables
        EntityNavmesh = GetComponent<NavMeshAgent>();
        //EntityHead = whatever the head ends up being
        EntityAnimator = GetComponent<Animator>();
        //PlayerObject = however you figure that
        PlayerTransform = PlayerObject.GetComponent<Transform>();

        //Creates the list of all the Patrol Points
        foreach (GameObject Patrol_points in GameObject.FindGameObjectsWithTag("Waypoints"))
        {
            PatrolPoints.Add(Patrol_points);
        }

        //Initiate patrol
        EntityPatrol();
    }
    //waits untill enemy is at its destination, then calls functions ChangePatrolPoint and EnemyMovement
    void Update()
    {
        //If the player is not detected 
        if (!detected)
        {
            //If the entity is currently patroling and the entity has reached the patrol point and the player has not been caught
            if (Patroling && Vector3.Distance(transform.position, EntityNavmesh.destination) <= 2.0f && !Caught)
            {
                //set patroling to false
                Patroling = false;
            }
            else
            {
                EntityPatrol();
            }
        }
        //if the player has been detected and the player has not been caught        
        else if (detected && !Caught)
        {   //set destination to the player
            EntityNavmesh.destination = PlayerTransform.position;
        }
        //if the player has been caught
        else if (Caught)
        {
            //disable entity
            EntityNavmesh.enabled = false;
        }
    }
    //Idk why I used fixed update here, probrably a good reason
    public void FixedUpdate()
    {
        //Detected bool returns from inFOV 
        detected = PlayerDectection(transform, PlayerObject.transform, EnemyFov, EnemyRadius);

    }
    //Used to determine if the player has been detected
    public bool PlayerDectection(Transform Entity, Transform target, float EnemyFov, float EnemyRadius)
    {
        //create a vector3 equal to the distance between the enemy and the player
        Vector3 directionBetween = (target.position - Entity.position).normalized;
        //I dont know if setting the y to 0 is necessary because everything is on a even plane, but idk
        directionBetween.y *= 0;
        //Create a raycast called hit
        RaycastHit hit;
        //If a raycast shooting out of the entity hits the target
        if (Physics.Raycast(Entity.position + Vector3.up * heightMultiplayer, (target.position - Entity.position).normalized, out hit, EnemyRadius))
        {
            //if the target is the player
            if (hit.transform.CompareTag("Player"))
            {
                //idk what the point of the angle is
                float angle = Vector3.Angle(Entity.forward + Vector3.up * heightMultiplayer, directionBetween);

                //if the angle is less than or equal to the enemyfov 
                if (angle <= EnemyFov)
                {
                    //increase the speed of the entity, set its animation to sprinting, and return true
                    EntityNavmesh.speed = 15;
                    EntityAnimator.SetBool("sprinting", true);
                    return true;
                }

            }
        }
        //if none of that shit is true, set the speed to default, disable the sprinting animation, and return false
        EntityNavmesh.speed = 5;
        EntityAnimator.SetBool("sprinting", false);
        return false;

    }

    //Moves enemy towards patrolpoint
    void EntityPatrol()
    {
        //Create a random integer from the list of PatrolPoints and set the Entities destination to it, then set Patroling to true
        PatrolIndex = UnityEngine.Random.Range(0, PatrolPoints.Count);
        EntityNavmesh.SetDestination(PatrolPoints[PatrolIndex].transform.position);
        Patroling = true;
    }


    //On collision
    void OnTriggerEnter(Collider collision)
    {   //if colliding object has the tag "Player"
        if (collision.tag == "Player")
        {
            //Kill the player
            Caught = true;
            //pause entity, play death sound, and force player and entity to look at eachother
            EntityNavmesh.speed = 0;
            Deathsound.Play();
            PlayerObject.transform.LookAt(EntityHead.transform);
            gameObject.transform.LookAt(PlayerObject.transform);
            //set animations
            EntityAnimator.SetBool("sprinting", false);
            EntityAnimator.SetBool("Caughtplayer", true);
            //disable player movement
            PlayerObject.GetComponent<Camera>().GetComponent<Movement>().enabled = false;
            PlayerObject.GetComponent<Footsteps>().enabled = true;
            PlayerObject.GetComponent<Camera>().GetComponent<Lookscript>().enabled = false;
            PlayerObject.GetComponent<Camera>().GetComponent<AnimationTriggers>().enabled = false;
            //load the loss screen after specified time
            Invoke("Dead", Deathtime);
        }
    }

    void Dead()
    {
        //Load loss screen
        SceneManager.LoadScene(2);
    }

}
