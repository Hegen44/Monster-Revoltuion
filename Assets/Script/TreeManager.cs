using UnityEngine;
using System.Collections;

public class TreeManager : MonoBehaviour {

    Inventory inv;
    //HealthMananger hm;

    void Start()
    {
        //hm = GetComponent<HealthMananger>();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void giveResources()
    {
        inv.AddItem(0); // stick
    }
}
