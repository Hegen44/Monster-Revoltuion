using UnityEngine;
using System.Collections;

public class HealthMananger : MonoBehaviour
{

    public float MaxHealth;
    public float currentHealth;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        currentHealth = MaxHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            anim.SetTrigger("isDead");
        }
    }

    public void HPDamage(float damage)
    {
        anim.SetTrigger("isHit");
        currentHealth -= damage;
    }

    public void kill()
    {
        Destroy(this.gameObject);
    }
}
