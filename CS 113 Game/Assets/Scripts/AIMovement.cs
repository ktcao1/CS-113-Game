using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (target != null) 
        {
            agent.SetDestination(target.position);
            
            // Flip sprite
            if (agent.desiredVelocity.x > 0) agent.gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
            else if (agent.desiredVelocity.x < 0) agent.gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
    }
}
