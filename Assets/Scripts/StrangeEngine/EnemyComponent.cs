// Copyright(c) 2020 arcturus125 & StrangeDevTeam
// Free to use and modify as you please, Not to be published, distributed, licenced or sold without permission from StrangeDevTeam
// Requests for the above to be made here: https://www.reddit.com/r/StrangeDev/

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    public EnemySpawner home;
    public Enemy enemyReference; // the reference to the original enemy file. changing these wil change default details of the enemy

    [Header("AnimationOnNearby")]
    public bool AnimationOnNearby = false;
    public float nearbyDistance = 7.5f;
    public string ParameterName = "Wakeup";

    public float distance;

    public int health;
    // when the enenym spawns in
    public void Start()
    {
        health = enemyReference.health;
    }

    public void Update()
    {
        EnemyAI();
        if (AnimationOnNearby)
        {
            Animator anim = this.GetComponent<Animator>();
            distance = Vector3.Distance(Player.singleton.transform.position, this.transform.position);
            if (distance < nearbyDistance)
            {
                anim.SetBool(ParameterName, true);
            }
            else
            {
                anim.SetBool(ParameterName, false);
            }
        }
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

        home.removeFromHome(this.gameObject.transform.parent.gameObject);

        Destroy(this.gameObject.transform.parent.gameObject);
        PlayerInteraction.previousColliders.Remove(this.gameObject.GetComponent<Collider>());
    }



    public enum AIType
    {
        Passive,
        Reactive,
        Agressive
    }

    public AIType AI_type;
    bool SleepWhenBored = false; // when true, enemy will sleep when they lose sight of player instead of wandering

    public void EnemyAI()
    {
        // https://github.com/StrangeDevTeam/Voxel-Dungeons-V2/projects/1


        // if AI type = aggressive
            // if player nearby
                // if line of sight of player (do a raycast)
                    // walk towards player
                    // change to walk animation ( if there is one)
            // if player not nearby: wander  / sleep if SLeepWhenBored = true
        // if AI type = reactive
            // when enemy is attacked by player, switch to aggressive (hunt down player until LOS broken)
        // if AI type = passive
            //wander

    }
}
