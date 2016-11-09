using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class ItemDatabase : MonoBehaviour {

    private List<Item> database = new List<Item>();
    private JsonData itemData;

    void Start()
    {                                                  //unity location.path to file
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAsserts/Items.json"));
        ConstructItemDatabase();
    }

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i< database.Count; ++i)
        {
            if(database[i].ID == id)
                return database[i];
        }
        return null;
    }
    
    void ConstructItemDatabase()
    {
        for ( int i = 0; i< itemData.Count; ++i)
        {
            database.Add(new Item((int)itemData[i]["id"], (string)itemData[i]["title"], (int)itemData[i]["value"], 
                (string)itemData[i]["description"], (string)itemData[i]["slug"], (bool)itemData[i]["stackable"]));
        }
    }

}

public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public bool Stackable { get; set; }
    public int Power { get; set; }
    public int Defence { get; set; }
    public int Vitality { get; set; }
    public Sprite image { get; set; }

    public Item(int id, string title, int value, string description, string slug, bool stackable)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Description = description;
        this.Slug = slug;
        this.Stackable = stackable;
        this.image = Resources.Load<Sprite>("Sprites/Inventory/Item/" + slug);
    }

    public Item(int id, string title, int value, string description, string slug, bool stackable, int power,int defence,int vitality)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Description = description;
        this.Slug = slug;
        this.Stackable = stackable;
        this.Power = power;
        this.Defence = defence;
        this.Vitality = vitality;
        this.image = Resources.Load<Sprite>("Sprites/Inventory/Item/" + slug);
    }

    public Item()
    {
        this.ID = -1; // empty item
    }
}
