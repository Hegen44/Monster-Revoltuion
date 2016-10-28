using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public bool displayPath = false;
    public Transform target;
    public float speed = 1;
    Vector3[] path;
    int targetIndex;
    Animator anim;

	// Use this for initialization
	void Start () {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        anim = GetComponent<Animator>();
	}

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful) {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
	
	IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while(true)
        {
            if(transform.position == currentWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    path = new Vector3[0];
                    anim.SetBool("isWalking", false);
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            Vector2 movement_vector = new Vector2(currentWaypoint.x - transform.position.x, currentWaypoint.y - transform.position.y);
            movement_vector.Normalize();

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

            //rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime * speed);


            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if(path!= null && displayPath)
        {
            for (int i = targetIndex; i< path.Length; ++i)
            {
                Gizmos.color = Color.green;
                //Gizmos.DrawCube(path[i], Vector3.one);

                if(i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                } else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }


}
