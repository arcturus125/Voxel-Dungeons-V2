// Copyright(c) 2020 arcturus125 & StrangeDevTeam
// Free to use and modify as you please, Not to be published, distributed, licenced or sold without permission from StrangeDevTeam
// Requests for the above to be made here: https://www.reddit.com/r/StrangeDev/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//IMPORT:: attach this script to your player GameObject
public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction singleton;

    public float interactionDistance = 7.5f; // the distance in which the user can interact with a UsableObject
    public KeyCode useKey = KeyCode.F; // this is the key the user will press to use/ interact with an object. Default f
    public static List<Collider> previousColliders = new List<Collider>();

    private void Awake()
    {
        singleton = this;
    }
    void LateUpdate()
    {

        Collider[] nearbyColliders = Physics.OverlapSphere(this.transform.position, interactionDistance);  // get every collider within interactionDistance
        List<Collider> copyOfNearbyColliders = ArrayToList(ref nearbyColliders); // make a copy of the nearby colliders that can be manipulated

        calculatePreferredInteractible();

        ///OnNearby()
        //detect when player walks towards a new collider and attempt to run OnNearby() on it
        for (int h = 0; h < previousColliders.Count; h++)
        {
            for (int g = 0; g < copyOfNearbyColliders.Count; g++)
            {
                if (copyOfNearbyColliders[g] == previousColliders[h])
                {
                    copyOfNearbyColliders.Remove(copyOfNearbyColliders[g]);
                }
            }
        }
        for (int f = 0; f < copyOfNearbyColliders.Count; f++)
        {
            copyOfNearbyColliders[f].gameObject.SendMessage("OnNearby", SendMessageOptions.DontRequireReceiver);
        }

        ///NoLongerNearby()
        //detech when the plaeyr as walked away from a collider and run NoLongerNearby() on it if possible
        for (int g = 0; g < previousColliders.Count; g++)
        {
            for (int h = 0; h < nearbyColliders.Length; h++)
            {
                if (g >= 0 && g < previousColliders.Count)
                {
                    if (previousColliders[g] == nearbyColliders[h])
                    {
                        previousColliders.Remove(previousColliders[g]);
                    }
                }
            }
        }
        for (int f = 0; f < previousColliders.Count; f++)
        {
            if (previousColliders[f])
                previousColliders[f].gameObject.SendMessage("NoLongerNearby", SendMessageOptions.DontRequireReceiver);
        }

        ///WhileNearby()
        //while a user is nearby a collider, run WhileNearby on the collider if possible
        for (int i = 0; i < nearbyColliders.Length; i++)
        {
            GameObject NearbyObject = nearbyColliders[i].gameObject;
            NearbyObject.SendMessage("WhileNearby", SendMessageOptions.DontRequireReceiver);
        }

        ///Use()
        // while a user is wihtin range of a collider and they haev pressed F. run Use() on it if possible
        if (!Dialogue.isInDialogue)
        {
            if (Input.GetKeyDown(useKey))
            {
                if (ClosestInteractible)
                    ClosestInteractible.SendMessage("Use", SendMessageOptions.RequireReceiver);
            }
        }

        //at the end of the frame, set the current frame's collider to the previous frame's colliders ready for the next frame
        previousColliders = ArrayToList(ref nearbyColliders);

        if (ClosestInteractible == null)
            UIController.RemoveUiInteractionButtonPosition();

    }

    public static GameObject ClosestInteractible;
    public static List<GameObject> interactibles = new List<GameObject>();
    public static void AddInteractible(GameObject obj)
    {
        interactibles.Add(obj);
    }
    public static void RemoveInteractible(GameObject obj)
    {
        Debug.Log("interactible deleted");
        if (ClosestInteractible == obj)
        {
            Debug.Log("Closest interactible deleted");
            ClosestInteractible = null;
        }

        interactibles.Remove(obj);
    }
    public void calculatePreferredInteractible()
    {
        float closestDistance = interactionDistance + 0.1f;
        foreach (GameObject go in interactibles)
        {
            float dist = Vector3.Distance(this.gameObject.transform.position, go.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                ClosestInteractible = go;
                //Debug.Log("(" + interactibles.Count + ") calculating closest interactible: " + ClosestInteractible.name);
            }
        }
    }




    List<Collider> ArrayToList(ref Collider[] array) // converts a Collider array to a list of Colliders
    {
        List<Collider> tempList = new List<Collider>();
        foreach (Collider coll in array)
        {
            tempList.Add(coll);
        }
        return tempList;
    }
}
