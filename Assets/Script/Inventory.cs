using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public int slotAmount = 20;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

	// Use this for initialization
	void Start () {
        database = GetComponent<ItemDatabase>();
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        for (int i = 0; i < slotAmount; ++i)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().id = i; // set slot id
            slots[i].transform.SetParent(slotPanel.transform);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            AddItem(0);
        }
        if (Input.GetKeyDown("j"))
        {
            AddItem(1);
        }
    }

    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);
        if (items.Contains(itemToAdd) && itemToAdd.Stackable)
        {
            ItemData data = slots[items.IndexOf(itemToAdd)].transform.GetChild(0).GetComponent<ItemData>();
            data.amount++;
            data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
        } else {
            for (int i = 0; i < items.Count; ++i)
            {

                if (items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent <ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.GetComponent<Image>().sprite = itemToAdd.image;
                    itemObj.transform.localPosition = Vector3.zero;
                    Debug.Log(itemObj.transform.position);
                    itemObj.name = itemToAdd.Title;
                    break;
                }
            }
        }

    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = database.FetchItemByID(id);
        if (items.Contains(itemToRemove) && itemToRemove.Stackable)
        {
            for (int j = 0; j < items.Count; j++) { if (items[j].ID == id)
                { ItemData data = slots[j].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount--; data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    if (data.amount == 0) { Destroy(slots[j].transform.GetChild(0).gameObject);
                        items[j] = new Item();
                        break;
                    } if (data.amount == 1) {
                        slots[j].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "";
                        break;
                    }
                    break;
                }
            }
        }
        else
            for (int i = 0; i < items.Count; i++) if (items[i].ID != -1 && items[i].ID == id)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject); items[i] = new Item();
                    break;
                }
    }

}
