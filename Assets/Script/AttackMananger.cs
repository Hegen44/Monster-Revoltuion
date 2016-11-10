using UnityEngine;
using System.Collections;
using UnityEditor;

public class AttackMananger : MonoBehaviour
{

    public float damage = 1f;
    private bool isEnable = false;

    void OnEnable()
    {
        isEnable = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger != true && other.CompareTag("Resource") && isEnable)
        {
            other.GetComponent<HealthMananger>().HPDamage(damage);
            isEnable = false;
        }
    }
}

