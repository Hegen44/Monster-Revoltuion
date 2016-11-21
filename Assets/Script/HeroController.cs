using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HeroController : MonoBehaviour {


    //public Transform[] points;
    //public GameObject Waypoints;
    GameObject Waypoints;
    public FieldOfView fov;
    public float countdown;
    public float TarDis;
    public float sprSpeed = 2.1f;

    private List<Transform> points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    bool chasing = false;
    bool attacked = false;
    float novSpeed;
    float novAngle;
    float lastseemtime;
    float lastunderattack;
    Animator anim;
    Transform target;
    Vector3 lastvaild;


    void Start()
    {
        Waypoints = GameObject.FindGameObjectWithTag("waypoint");
        novAngle = fov.viewAngle;
        agent = GetComponent<NavMeshAgent>();
        novSpeed = agent.speed;
        anim = GetComponent<Animator>();
        points = new List<Transform>();
        foreach (Transform child in Waypoints.transform)
        {
            points.Add(child);
        }

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
        // check if there is a target
        handlefovcountdown();
        targethandeling();
        controlSpeed();
        HandleWalkingAnimation();
        attackhandeling();


    }

    void attackhandeling()
    {
        if(chasing && target != null && agent.remainingDistance < TarDis)
        {
            Vector3 vel = target.position - transform.position;
            anim.SetFloat("Input_x", vel.x);
            anim.SetFloat("Input_y", vel.z);
            anim.SetTrigger("isAttack");
        }
    }

    void targethandeling()
    {
        target = fov.GetTarget();
        if (target != null)
        {
            //print("chasing");
            agent.SetDestination(target.position);
            lastseemtime = Time.time;
            chasing = true;
            lastvaild = target.position;

        }
        else if (target == null && chasing)
        {
            agent.SetDestination(lastvaild);
            if (agent.remainingDistance < 0.5f)
                GotoNextPoint();
            //print("chasing blind");
            if (Time.time - lastseemtime > countdown)
            {
                //print("give up");
                agent.Stop();
                agent.ResetPath();
                chasing = false;
            }

        }
        else
        {
            // Choose the next destination point when the agent gets
            // close to the current one.
            if (agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    }


    void HandleWalkingAnimation()
    {
        Vector3 vel = agent.velocity;
        Vector3.Normalize(vel);
        if (vel.magnitude != 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("Input_x", vel.x);
            anim.SetFloat("Input_y", vel.z);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

 
    }

    void controlSpeed()
    {
        if (chasing)
        {
            agent.speed = sprSpeed;
        } else
        {
            agent.speed = novSpeed;
        }
    }

    public void HitAlert()
    {
        agent.Stop();
        agent.ResetPath();
        fov.viewAngle = 360;
        attacked = true;
        lastunderattack = Time.time; ;
    }

    void handlefovcountdown()
    {
        if (attacked && Time.time - lastunderattack > countdown)
        {
            fov.viewAngle = novAngle;
        }
    }
}
