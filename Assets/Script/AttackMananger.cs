using UnityEngine;
using System.Collections;

public class AttackMananger : MonoBehaviour {

    public float damage = 1f;
    
    void OnTriggerEnter2D(Collider other)
    {
        if (other.isTrigger != true && other.CompareTag("Resource"))
        {
            other.GetComponent<HealthMananger>().HPDamage(damage);
        }
        /*
        if (other.tag == "Enemies" || other.tag == "Hero")
        {

        } else if (other.tag == "player")
        {

        }
        */
    }
    
}

