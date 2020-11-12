// Copyright(c) 2020 arcturus125 & StrangeDevTeam
// Free to use and modify as you please, Not to be published, distributed, licenced or sold without permission from StrangeDevTeam
// Requests for the above to be made here: https://www.reddit.com/r/StrangeDev/

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;

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
    public bool hit = false;

    public Rigidbody rb;
    public float walkSpeed = 0.7f;

    Animator anim;
    // when the enenym spawns in
    public void Start()
    {
        health = enemyReference.health;
        rb = GetComponentInParent<Rigidbody>();
    }

    public void Update()
    {
        EnemyAI();
        if (AnimationOnNearby)
        {
            anim = this.GetComponent<Animator>();
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
        hit = true;
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
    public bool SleepWhenBored = false; // when true, enemy will sleep when they lose sight of player instead of wandering

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

        distance = Vector3.Distance(Player.singleton.transform.position, this.transform.position);
        if (AI_type == AIType.Agressive)
        {
            if (distance < nearbyDistance)
            {
                FollowPlayer();
            }
            else
            {
                if (!SleepWhenBored)
                {
                    Wander();
                }
            }
        }
        else if (AI_type == AIType.Reactive)
        {
            if (hit && distance < nearbyDistance)
            {
                FollowPlayer();
            }
        }
        else
        {
            Wander();
        }
    }

    public float speed = 0.01f;
    void FollowPlayer()
    {
        Debug.Log("follow");
        // if player nearby
        RaycastHit hits;
        if (ADMIN.Debug_Mode)
            Debug.DrawRay(transform.position, Player.singleton.gameObject.transform.position- transform.position, Color.black);
        if (Physics.Raycast(transform.position, Player.singleton.gameObject.transform.position - transform.position, out hits))
        {
            // walk towards player
            transform.LookAt(new Vector3(Player.singleton.gameObject.transform.position.x,
                                         this.transform.position.y,
                                         Player.singleton.gameObject.transform.position.z)); //TODO: Ignore Y coordinate
            //rb.MovePosition(transform.position + Vector3.forward * walkSpeed);
            //transform.position += transform.TransformDirection(Vector3.forward);
            Debug.Log("Rotated");

            try
            {
                //anim = this.GetComponent<Animator>();
                anim.SetBool("Walking", true);
            }
            catch (Exception exc) {
                Debug.LogWarning("Animation parameter for walking is missing");
            };
        }
        else
        {
            hit = false;
        }
    }

    float currentWalkLength = 0.0f;
    float targetLength = 0.0f;
    public float maxWalkLength = 15.0f;
    void Wander()
    {
        if (currentWalkLength >= targetLength)
        {
            //Pick random direction
            float angle = UnityEngine.Random.Range(0.0f, 360.0f);
            transform.Rotate(new Vector3(0.0f, angle, 0.0f));

            //Pick random length
            targetLength = UnityEngine.Random.Range(0.0f, maxWalkLength);

            currentWalkLength = 0.0f;
        }
        else
        {
            rb.MovePosition(transform.position + Vector3.forward * walkSpeed);
            currentWalkLength += Vector3.forward.z * walkSpeed;
        }
    }
}
