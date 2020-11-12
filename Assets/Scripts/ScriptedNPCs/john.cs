using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class john : MonoBehaviour
{
    public Transform interactionButtonLocation;
    //items
    public Item apple ;
    public Item sword2;

    public Enemy slime;

    string hello = "Hello there!";
    DialogueChoice hiJohn;
    Dialogue questDialogue;
    Quest killSlimeQuest;
    Dialogue questTurnIn;
    TalkQuest talkToFrank;

    Dialogue frankQuestDialogue;

    // Start is called before the first frame update

    void Start()
    {

        // end v
        KillQuest killSlimeObjectives = new KillQuest("Kill a slime", slime, 1);
        talkToFrank = new TalkQuest("Talk to Frank", Frank.hiFrank);
        List<QuestObjective> objectives = new List<QuestObjective> {killSlimeObjectives, talkToFrank };
        killSlimeQuest = new Quest("Kill slime", "Kill a slime", objectives);
        killSlimeQuest.setReward(sword2);

        questDialogue = new Dialogue("I have a quest for you", killSlimeQuest);
        Dialogue bye = new Dialogue("Goodbye then.");

        questTurnIn = new Dialogue("Thank you!");
        //start ^

      
        
        //frankQuestDialogue = new Dialogue("Talk to Frank", talkToFrank);

        string[] replies = new string[] { "Reply 1", "Reply 2", "Reply 3" };
        //Dialogue[] branches = new Dialogue[] { questDialogue, bye };
        //hiJohn = new DialogueChoice(hello, replies, branches);


        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);

        Player.playerInv.AddItem(sword2);
        Player.playerInv.AddItem(sword2);


    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject == PlayerInteraction.ClosestInteractible)
            UIController.SetUiInteractionButtonPosition(this.interactionButtonLocation);
    }
    public void OnNearby()
    {
        PlayerInteraction.AddInteractible(this.gameObject);
    }
    public void NoLongerNearby()
    {
        PlayerInteraction.RemoveInteractible(this.gameObject);
        UIController.HideInteractionTooltip();
        questDialogue.HideDialogue();
    }

    public void Use()
    {
        if (!killSlimeQuest.isTurnedIn)
        {
            if (killSlimeQuest.complete)
            {
                killSlimeQuest.TurnInQuest();
                questTurnIn.ShowDialogue();
            }
            else
            {
                questDialogue.ShowDialogue();
            }
        }
        else
        {
           // hiJohn.ShowDialogue();
        }


    }
}
