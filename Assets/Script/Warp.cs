using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {

	public Transform warpTarget;
    //public string levelToLoad;

	IEnumerator OnTriggerStay(Collider other){

        if(other.CompareTag("Player") && Input.GetKeyDown("e"))
        {
            ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

            yield return StartCoroutine(sf.FadeToBlack());

            other.gameObject.transform.position = warpTarget.position;
            Camera.main.transform.position = warpTarget.position;
            /*
            if(other.gameObject.name == "player"){
                Application.LoadLevel(levelToLoad);
            }
            */

            yield return StartCoroutine(sf.FadeToClear());
        }

    }
}
