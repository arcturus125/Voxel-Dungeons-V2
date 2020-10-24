using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemScript : MonoBehaviour
{
    public List<Item> items;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateDrops(List<Item> pItems)
    {
        items = pItems;
    }

    public void Use()
    {
        foreach (Item i in items)
        {
            Debug.Log("Item added");
            Player.playerInv.AddItem(i);
        }
    }

    public void OnNearby()
    {
        UIController.ShowInteractionTooltip("Press F to pick up items");
    }

    public void NoLongerNearby()
    {
        UIController.HideInteractionTooltip();
    }
}
