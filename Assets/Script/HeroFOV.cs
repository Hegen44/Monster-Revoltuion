using UnityEngine;
using System.Collections;

public class HeroFOV : MonoBehaviour {

    public GameObject hero;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = hero.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = hero.transform.position;
        transform.rotation = Quaternion.LookRotation(agent.velocity);
    }
}
