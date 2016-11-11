using UnityEngine;
using System.Collections;

public class TrapManager : MonoBehaviour {

    Inventory inv;
    ItemDatabase database;
    public GameObject trap;

    // Use this for initialization
    void Start () {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        database = GameObject.Find("Inventory").GetComponent<ItemDatabase>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            if (inv.items.Contains(database.FetchItemByID(1)))
            {
                inv.RemoveItem(1);
                GameObject trapObj = Instantiate(trap);
                //itemObj.transform.SetParent(slots[i].transform);
                trapObj.transform.position = this.transform.position;
                trapObj.transform.rotation = this.transform.rotation;
            }
        }
    }
}
