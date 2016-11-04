using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float scale;
    public float smoothness;
    Camera mycam;

	// Use this for initialization
	void Start () {

        mycam = GetComponent<Camera>();
	
	}
	
	// Update is called once per frame
	void Update () {
	    mycam.orthographicSize = (Screen.height / 100f) / scale;

        if (target)
        {
            //transform.position = Vector3.Lerp(From, To, how fast);
            transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(0, 10, 0), smoothness);
        }
	}
}
