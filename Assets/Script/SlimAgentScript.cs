using UnityEngine;
using System.Collections;

public class SlimAgentScript : MonoBehaviour {

    Transform target;
    FieldOfView fov;
    NavMeshAgent agent;

    public float countdown;
    public float attackcooldown;
    public float TarDis;

    bool chasing = false;
    float lastseemtime;
    float lastattacktime;
    Animator anim;

    // Use this for initialization
    void Start () {
        lastseemtime = -attackcooldown;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
    }
	
	// Update is called once per frame
	void Update () {
          
        SettingTarget(); 
        Attacking();
        HandleWalkingAnimation();

    }

    void Attacking()
    {
        if (target != null && agent.remainingDistance < TarDis && Time.time - lastattacktime > attackcooldown)
        {
            agent.Stop();
            agent.ResetPath();
            chasing = false;
            anim.SetTrigger("isAttack");
            lastattacktime = Time.time;
        }
    }

    void SettingTarget()
    {
        target = fov.GetTarget();
        if (target != null)
        {
            agent.SetDestination(target.position);
            lastseemtime = Time.time;
            chasing = true;

        }
        else if (target == null && chasing)
        {
            if (Time.time - lastseemtime > countdown)
            {
                agent.Stop();
                agent.ResetPath();
                chasing = false;
            }

        }
    }

    void HandleWalkingAnimation()
    {
        Vector3 vel = agent.velocity;
        Vector3.Normalize(vel);
        /*
        if (vel.x > 0)
        {
            vel.x = 1;
        } else if (vel.x < 0)
        {
            vel.x = -1;
        }
        if (vel.z > 0)
        {
            vel.z = 1;
        }
        else if (vel.z < 0)
        {
            vel.z = -1;
        }
        */

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
}
