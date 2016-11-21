using UnityEngine;
using System.Collections;

public class FindingTarget : MonoBehaviour {


    GameObject target;

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Hero");
        if (target != null){
            Vector3 lospos = transform.position - target.transform.position;
            float ang = Mathf.Rad2Deg * Mathf.Atan2(lospos.z, lospos.x);
            transform.localEulerAngles = new Vector3(0, 0, ang);
        }
    }
}
