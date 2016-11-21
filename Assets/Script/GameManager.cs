using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject inventory;
    GameObject Hero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        if(GameObject.FindGameObjectWithTag("Hero") == null)
        {
            GameObject.FindGameObjectWithTag("Arrow").GetComponent<SpriteRenderer>().enabled = false;
        } else
        {
            GameObject.FindGameObjectWithTag("Arrow").GetComponent<SpriteRenderer>().enabled = true;
        }
	
	}

    void ToggleInventory()
    {
        inventory.SetActive(!inventory.activeInHierarchy);
        //Cursor.visible = inventory.activeInHierarchy;
    }
}
