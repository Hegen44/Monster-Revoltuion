using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    Rigidbody2D rbody;
    Animator anim;
    //static bool playerExist;

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
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
	void Update () {
        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movement_vector != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            // set new direction
            anim.SetFloat("Input_x", movement_vector.x);
            anim.SetFloat("Input_y", movement_vector.y);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime * speed);
    }
}
