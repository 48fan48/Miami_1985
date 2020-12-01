using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
        
    public Transform[] points;
    
    private NavMeshAgent agent;
    private Enemy enemyScript;
    private Animator animator;
    private int destPoint = 0;
    private float distance;
    private bool isWalking = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        // Get the enemy script
        enemyScript = GetComponent<Enemy>();

        GotoNextPoint();
        animator.SetBool("WalkingForward", true);
    }

    // Update is called once per frame
    void Update()
    {

        // Make the enemey go to the next point
        if (!agent.pathPending && agent.remainingDistance < 0.5f) 
        {
            GotoNextPoint();
        }

        // Stop the enemy movement when health is less than or equal to zero
        if (enemyScript.health <= 0 || enemyScript.playerInRange)
        {
            isWalking = false;            
            animator.SetBool("WalkingForward", false);
            agent.isStopped = true;
        } else {
            if(!isWalking) 
            {
                isWalking = true;
                animator.SetBool("WalkingForward", true);
            }
            agent.isStopped = false;
        }

        
    }

    void GotoNextPoint() {
            
            // Returns if no points have been set up
            if (points.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % points.Length;
        }
}
