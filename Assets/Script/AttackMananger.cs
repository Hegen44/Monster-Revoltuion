﻿using UnityEngine;
using System.Collections;
using UnityEditor;

public class AttackMananger : MonoBehaviour
{

    public float damage = 1f;
    private bool isEnable = false;
    public int time = 0;
    public string[] tag;

    void OnEnable()
    {
        StartCoroutine(wait(time));
    }

    void OnTriggerEnter(Collider other)
    {
        foreach (string subtag in tag){
            if (other.isTrigger != true && other.CompareTag(subtag) && isEnable)
            {
                other.GetComponent<HealthMananger>().HPDamage(damage);
                isEnable = false;
            }

        }

    }

    

    IEnumerator wait(int t)
    {
        yield return new WaitForSeconds(t);
        isEnable = true;
    }
}

