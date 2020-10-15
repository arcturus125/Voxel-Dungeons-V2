using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class john : MonoBehaviour
{

    public Enemy slime;


    string hello = "Hello there!";
    DialogueChoice hiJohn;

    // Start is called before the first frame update
    void Start()
    {
        Dialogue name = new Dialogue("My name is John.");
        Dialogue bye = new Dialogue("Goodbye then.");

        string[] replies = new string[] { "Hi, who are you?", "I don't like cylinders." };
        Dialogue[] branches = new Dialogue[] { name, bye };
        hiJohn = new DialogueChoice(hello, replies, branches);
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
        hiJohn.ShowDialogue();
    }
}
