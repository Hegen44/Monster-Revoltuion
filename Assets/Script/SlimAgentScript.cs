using UnityEngine;
using System.Collections;

public class SlimAgentScript : MonoBehaviour {

    Transform target;
    FieldOfView fov;
    NavMeshAgent agent;
    bool chasing = false;
    float lastseemtime;
    public float countdown;
    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
    }
	
	// Update is called once per frame
	void Update () {

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

        target = fov.GetTarget();
        if (target != null)
        {
            agent.SetDestination(target.position);
            lastseemtime = Time.time;

        } else if (target == null && chasing)
        {
            if (Time.time - lastseemtime > countdown)
            {
                agent.Stop();
                agent.ResetPath();
                chasing = false;
            }

        }
	}
}
