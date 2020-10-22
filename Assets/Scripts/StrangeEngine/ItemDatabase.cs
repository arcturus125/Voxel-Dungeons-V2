using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static List<Item> itemDatabase = new List<Item>()
    {
        new Item
            (
            Item.ItemType.Item,
            0,
            "Example Item",
            "this is an example item being added to the database",
            10,
            new Dictionary<string, int>
            {
                {"Item", 100},
                {"Example", 100}
            }
            ),
        new Item
            (
            Item.ItemType.Skill,
            2,
            "Example Skill",
            "this is an example Item being added to the database",
            10,
            new Dictionary<string, int>
            {
                {"Skill", 100},
                {"Example", 100}
            }
            ),
        new Item
            (
            Item.ItemType.Equipment,
            3,
            "Example Equipment",
            "this is an example Equipment being added to the database",
            10,
            new Dictionary<string, int>
            {
                {"Equipment", 100},
                {"Example", 100}
            }
            ),
        new Item
            (
            Item.ItemType.Equipment,
            4,
            "Sword",
            "This is a sword",
            100,
            new Dictionary<string, int>
            {
                { "Damage", 9000}
            }
            ),
        new Item
            (
            Item.ItemType.Item,
            5,
            "Apple",
            "i'm hungry",
            10,
            new Dictionary<string, int>
            {
                {"hunger", 4 }
            },
            true
            ),
        new Weapon
            (
            6,
            "sword 2",
            "this is a weapon",
            10,
            new Dictionary<string, int>
            {
            },
            10,
            Resources.Load<GameObject>("Weapons/maha")
            )
    };

    public static Item SearchDatabaseByID(int ID)
    {
        for(int i = 0; i < itemDatabase.Count; i++)
        {
            if(ID == itemDatabase[i].ID)
            {
                return itemDatabase[i];
            }
        }
        return null;
    }

    public static Item SearchDatabaseByName(string Name)
    {
        for(int i = 0; i< itemDatabase.Count; i++)
        {
            if(Name == itemDatabase[i].itemName)
            {
                return itemDatabase[i];
            }
        }
        Debug.LogError("Tried searching item database for '" +Name+"': Item not found");
        return null;
    }
}
