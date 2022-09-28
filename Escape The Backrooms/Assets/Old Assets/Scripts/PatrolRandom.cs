using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolRandom : MonoBehaviour
{
    public NavMeshAgent Enemy;
    public bool WaitingEnabled;
    public float WaitTime = 3f;
    public float TurnChance = 0.2f;
    List<GameObject> PatrolPoints = new List<GameObject>();
    public GameObject DetectionObject;
    public GameObject enemyparent;
    private NoFOVDetect Script;
    public Transform Player;
    private Death Death;


    int PatrolIndex;
    bool Traveling;
    bool Waiting;
    bool PatrolForward = true;
    float Timer;
    public Vector3 target;

    void Start()
    {
        Script = DetectionObject.GetComponent<NoFOVDetect>();
        Death = enemyparent.GetComponent<Death>();
        //Creates the list of Waypoints
        foreach (GameObject Patrol_points in GameObject.FindGameObjectsWithTag("Waypoints"))
        {
            PatrolPoints.Add(Patrol_points);
        }

        PatrolIndex = UnityEngine.Random.Range(0, PatrolPoints.Count);
        EnemyMovement();
    }
    //waits untill enemy is at its destination, then calls functions ChangePatrolPoint and EnemyMovement
    void Update()
    {
        if (!Script.detected)
        {
            if (Traveling && Vector3.Distance(transform.position, Enemy.destination) <= 2.0f && !Death.Caught)
            {

                Traveling = false;

                if (WaitingEnabled)
                {
                    Waiting = true;
                    Timer = 0f;
                }
                else
                {
                    ChangePatrolPoint();
                    EnemyMovement();
                }

                if (Waiting)
                {
                    Timer += Time.deltaTime;
                    if (Timer >= WaitTime)
                    {
                        Waiting = false;

                        ChangePatrolPoint();
                        EnemyMovement();
                    }
                }
            }
        }
        else if (Script.detected && !Death.Caught)
        {
            Enemy.destination = Player.position;
        }
        else if (Death.Caught)
        {
            Enemy.enabled = false;
        }


    }
    //Moves enemy towards patrolpoint
    void EnemyMovement()
    {
        if (PatrolPoints != null)
        {
            target = PatrolPoints[PatrolIndex].transform.position;
            Enemy.SetDestination(target);
            Traveling = true;
        }
    }
    //changes the patrol points after the enemy reaches its current destination
    void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= TurnChance)
        {
            PatrolForward = !PatrolForward;
        }

        if (PatrolForward)
        {
            PatrolIndex = UnityEngine.Random.Range(0, PatrolPoints.Count);
        }
        else
        {
            if (--PatrolIndex < 0)
            {
                PatrolIndex = PatrolPoints.Count - 1;
            }
        }
    }

}
