using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {


    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("isAttack");
        }

    }
}
