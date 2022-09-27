using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallTrigger : MonoBehaviour
{
    private int nextLevel;
    Collider Gate;
    public float EscapeChance = 0.2f;
    List<GameObject> PatrolPoints = new List<GameObject>();
    public Transform Player;
    int RespawnPoint;

    private void Start()
    {
        Gate = GetComponent<Collider>();

        foreach (GameObject Patrol_points in GameObject.FindGameObjectsWithTag("Waypoints"))
        {
            PatrolPoints.Add(Patrol_points);
        }
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log(EscapeChance);
            if (UnityEngine.Random.Range(0f, 1f) <= EscapeChance)
            {
                SceneManager.LoadScene(3);
            }
            else
            {
                RespawnPoint = UnityEngine.Random.Range(0, PatrolPoints.Count);
                Player.position = PatrolPoints[RespawnPoint].transform.position;
            }
        }
    }
}
