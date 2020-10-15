using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float rotateAmount = 90.0f;
    public float rotateStep = 0.5f;
    //public int doorOpen = -1;

    public enum DoorState
    {
        Open = -1,
        Closed = 1
    };
    public DoorState isDoorOpen = DoorState.Closed;

    public enum DoorAxis
    {
        Open_outwards = 1,
        Open_Inwards = -1
    };
    public DoorAxis howDoorOpens = DoorAxis.Open_outwards;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNearby()
    {
        UIController.ShowInteractionTooltip();
    }

    public void NoLongerNearby()
    {
        UIController.HideInteractionTooltip();
    }

    public void Use()
    {

        //transform.Rotate(new Vector3(0.0f, 40.0f, 0.0f));
        transform.Rotate(new Vector3(0.0f, rotateAmount * (int)isDoorOpen * (int)howDoorOpens, 0.0f));

        if (isDoorOpen == DoorState.Open)
        {
            isDoorOpen = DoorState.Closed;
        }
        else
        {
            isDoorOpen = DoorState.Open;
        }
    }
}
    