using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {
    Text texts;
    public HealthMananger Player;
    private GameObject Hero;

	void Start()
    {

        texts = GetComponent<Text>();
        texts.enabled = false;
    }
	// Update is called once per frame
	void Update () {

        Hero = GameObject.FindGameObjectWithTag("Hero");
        if (Player.currentHealth <= 0){
            texts.enabled = true;
            texts.text = "GAME OVER";
        }
        else if (Hero != null && Hero.GetComponent<HealthMananger>().currentHealth <= 0)
        {
            texts.enabled = true;
            texts.text = "VICTORY";
        }
    }
}
