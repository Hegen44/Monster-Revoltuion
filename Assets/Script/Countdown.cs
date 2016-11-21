using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {
    public SpawnHero sh;
	
	// Update is called once per frame
	void Update () {

        if (sh != null && sh.getCountDown()>= 0)
        {
            Text currenttext = GetComponent<Text>();
            int timer = sh.getCountDown();
            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = (timer % 60).ToString("00");
            currenttext.text = minutes + ":" + seconds;
        } else
        {
            Destroy(this.gameObject);
        }

    }
}
