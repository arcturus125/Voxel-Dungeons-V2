// Copyright(c) 2020 arcturus125 & StrangeDevTeam
// Free to use and modify as you please, Not to be published, distributed, licenced or sold without permission from StrangeDevTeam
// Requests for the above to be made here: https://www.reddit.com/r/StrangeDev/

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    public Enemy enemyReference; // th ereference to the original enemy file. changing these wil change default details of the enemy

    int health;
    // when the enenym spawns in
    public void Start()
    {
        health = enemyReference.health;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            Kill();
        }
    }
    void Kill()
    {
        List<Item> droppedItems = new List<Item>();
        for (int i = 0; i < enemyReference.drops.Length; i++)
        {
            float chance = UnityEngine.Random.Range(0.0f, 1.0f);
            if (chance <= enemyReference.dropChances[i])
            {
                droppedItems.Add(enemyReference.drops[i]);
            
            }

        }

        if (droppedItems.Count > 0)
        {
            GameObject itemObject = Instantiate(Resources.Load<GameObject>("DroppedItem"));
            itemObject.GetComponent<DroppedItemScript>().InstantiateDrops(droppedItems);
            itemObject.transform.position = this.transform.position;
        }

        Destroy(this.gameObject);
        PlayerInteraction.previousColliders.Remove(this.gameObject.GetComponent<Collider>());
    }
}
