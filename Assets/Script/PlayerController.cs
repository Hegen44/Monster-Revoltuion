﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    Rigidbody rbody;
    Animator anim;
    private bool isAttacking;
    //static bool playerExist;

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        /*
        if(!playerExist){
            playerExist = true;
            DontDestroyOnLoad(transform.gameObject);
        } else {
            Destory(gameObject);
        }
        */
	}

	// Update is called once per frame
    /*
	void Update () {
        HandleInput();
    }
    */
    void FixedUpdate()
    {
        HandleMovement();
    }
    /*
    private void HandleInput()
    {
        
        if (Input.GetKeyDown("f") && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("isAttack");
        }
        

    }
    */

    void HandleMovement()
    {
        Vector3 movement_vector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (movement_vector != Vector3.zero)
        {
            anim.SetBool("isWalking", true);
            // set new direction
            anim.SetFloat("Input_x", movement_vector.x);
            anim.SetFloat("Input_y", movement_vector.z);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        // rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime * speed);
        //rbody.AddForce(movement_vector * Time.deltaTime * speed);
        rbody.AddForce(movement_vector * speed);
    }
    /*
    public void AttackFinish()
    {
        isAttacking = false;
    }
    */
}
