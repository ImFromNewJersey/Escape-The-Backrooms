using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolpoints : MonoBehaviour
{

    public float GizmosRadius = 1.0f;

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, GizmosRadius);
    }

}
