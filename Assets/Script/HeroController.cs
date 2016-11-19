using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HeroController : MonoBehaviour {


    //public Transform[] points;
    public GameObject Waypoints;
    private List<Transform> points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        points = new List<Transform>();
        print(Waypoints.transform.childCount);
        foreach (Transform child in Waypoints.transform)
        {
            points.Add(child);
        }
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Count == 0)
            return;

        // Set the agent to go to the currently selected destination.
        //agent.destination = points[destPoint].position;
        agent.SetDestination(points[destPoint].position);

        if (destPoint == 0)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Transform temp = points[i];
                int randomIndex = Random.Range(i, points.Count);
                points[i] = points[randomIndex];
                points[randomIndex] = temp;
            }
        }

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Count;
    }


    void Update()
    {
        Vector3 vel = agent.velocity;
        Vector3.Normalize(vel);
        if (vel.magnitude != 0){
            anim.SetBool("isWalking", true);
            anim.SetFloat("Input_x", vel.x);
            anim.SetFloat("Input_y", vel.z);
        } else
        {
            anim.SetBool("isWalking", false);
        }

        // Choose the next destination point when the agent gets
        // close to the current one.
        
        if (agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}
