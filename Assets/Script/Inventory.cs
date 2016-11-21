using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public int slotAmount = 20;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

	// Use this for initialization
	void Start () {
        createInventorySlot();
    }

    void createInventorySlot()
    {
        if (!inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        }
        database = GetComponent<ItemDatabase>();
        //inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        for (int i = 0; i < slotAmount; ++i)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().id = i; // set slot id
            slots[i].transform.SetParent(slotPanel.transform);
        }
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }

    public void CraftTrap()
    {
       if (items.Contains(database.FetchItemByID(0)) && slots[items.IndexOf(database.FetchItemByID(0))].transform.GetChild(0).GetComponent<ItemData>().amount >= 4)
       {
           RemoveItem(0);
           RemoveItem(0);
           RemoveItem(0);
           RemoveItem(0);
           AddItem(1);
       }
    }
    
    
    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);
        bool not_open = false; // check if the inventory is already open
        if (!inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
            not_open = true;
        }
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
                    itemObj.name = itemToAdd.Title;
                    break;
                }
            }
        }
        // if not originally opened, close it so that player not seeing it
        if (not_open)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        }
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = database.FetchItemByID(id);
        bool not_open = false; // check if the inventory is already open
        if (!inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
            not_open = true;
        }
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
        } else
        {
            for (int i = 0; i < items.Count; i++) if (items[i].ID != -1 && items[i].ID == id)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject); items[i] = new Item();
                    break;
                }

        }
        if (not_open)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        }

    }

}
