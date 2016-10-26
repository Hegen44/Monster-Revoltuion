using UnityEngine;
using System.Collections;

public class HealthMananger : MonoBehaviour
{

    public float MaxHealth;
    public float currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
