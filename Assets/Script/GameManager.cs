using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject inventory;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
	
	}

    void ToggleInventory()
    {
        inventory.SetActive(!inventory.activeInHierarchy);
        Cursor.visible = inventory.activeInHierarchy;
    }
}
