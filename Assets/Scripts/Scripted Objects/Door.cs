using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float rotateAmount = 90.0f;
    public float rotateStep = 0.5f;
    //public int doorOpen = -1;

    float frames; // the amount of iterations needed to rotate the door a full 90 degrees

    public Transform interactionButtonLocation;

    public enum DoorState
    {
        Open = -1,
        Closed = 1
    };
    public DoorState isDoorOpen = DoorState.Closed;

    public enum DoorAxis
    {
        Open_outwards = -1,
        Open_Inwards = 1
    };
    public DoorAxis howDoorOpens = DoorAxis.Open_outwards;

    // Start is called before the first frame update
    void Start()
    {
        frames = rotateAmount / rotateStep;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInteraction.ClosestInteractible)
        {
            if (this.gameObject == PlayerInteraction.ClosestInteractible)
            {
                UIController.SetUiInteractionButtonPosition(this.interactionButtonLocation);
            }
        }
        DoorMotion();
        
    }

    public void OnNearby()
    {
        PlayerInteraction.AddInteractible(this.gameObject);
    }

    public void NoLongerNearby()
    {
        PlayerInteraction.RemoveInteractible(this.gameObject);
    }

    float current_frame = 0;

    public void DoorMotion()
    {
        if(isDoorCurrentlyInAnimation)
        {
            if (current_frame < frames)
            {
                transform.Rotate(new Vector3(0.0f, rotateStep * (int)isDoorOpen * (int)howDoorOpens, 0.0f));
                current_frame ++;
            }
            else
            {
                isDoorCurrentlyInAnimation = false;
            }
        }
    }
    public void activateDoor()
    {
        current_frame = 0;
        isDoorCurrentlyInAnimation = true;
    }





    bool isDoorCurrentlyInAnimation = false;

    public void Use()
    {
        if (!isDoorCurrentlyInAnimation)
        {
            activateDoor();
            if (isDoorOpen == DoorState.Open)
                isDoorOpen = DoorState.Closed;
            else
                isDoorOpen = DoorState.Open;
        }
    }
}
    