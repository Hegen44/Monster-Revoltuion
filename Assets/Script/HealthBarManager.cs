using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour {

    Slider healthBar;
    public HealthMananger playerHealth;

	// Use this for initialization
	void Start () {
        healthBar = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.maxValue = playerHealth.MaxHealth;
        healthBar.value = playerHealth.currentHealth;
	}
}
