using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public float impactDamage = 25f;

    NavMeshAgent navMeshAgent;

    public GameObject currentWaypoint;
    GameObject previousWaypoint;
    GameObject[] allWaypoints;

    bool travelling;
    private Vector3 targetVector;

    public GameObject player;
    private NavMeshHit hit;
    public float range = 30f;
    private bool blocked = false;   
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        allWaypoints = GameObject.FindGameObjectsWithTag("Node");
        currentWaypoint = GetRandomWaypoint();
        SetDestination();
    }

    void Update()
    {       
        if (travelling && navMeshAgent.remainingDistance <= 1f)
        {
            travelling = false;
            SetDestination();
        }
    }

    void FixedUpdate()
    {
        Eyes();
    }
    private void SetDestination()
    {
        previousWaypoint = currentWaypoint;
        currentWaypoint = GetRandomWaypoint();

        targetVector = currentWaypoint.transform.position;
        navMeshAgent.SetDestination(targetVector);
        travelling = true;
    }
    public GameObject GetRandomWaypoint()
    {
        if (allWaypoints.Length == 0)
        {
            return null;
        }
        else
        {
            int index = Random.Range(0, allWaypoints.Length);
            return allWaypoints[index];
        }
    }
    public void Eyes()
    {
        blocked = NavMesh.Raycast(transform.position, player.transform.position, out hit, NavMesh.AllAreas);
        Debug.DrawLine(transform.position, player.transform.position, blocked ? Color.red : Color.green);
        
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (!blocked && distance <= 50f)
        {
            currentWaypoint = player;

            targetVector = currentWaypoint.transform.position;
            navMeshAgent.SetDestination(targetVector);
            travelling = true;
        }
        else if(blocked)
        {
            if (travelling && navMeshAgent.remainingDistance <= 1f)
            {
                travelling = false;
                SetDestination();
            }
            else
            {
                currentWaypoint = previousWaypoint;
                targetVector = currentWaypoint.transform.position;
                navMeshAgent.SetDestination(targetVector);
                travelling = true;
            }           
        }       
    }   
}
    