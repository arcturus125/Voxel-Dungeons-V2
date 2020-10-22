using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class john : MonoBehaviour
{

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
        //items
        Item sword = ItemDatabase.SearchDatabaseByName("Sword");
        Item example = ItemDatabase.SearchDatabaseByName("Example Item");
        Item apple = ItemDatabase.SearchDatabaseByName("Apple");
        Item example2 = ItemDatabase.SearchDatabaseByName("Example Skill");
        Item example3 = ItemDatabase.SearchDatabaseByName("Example Equipment");
        Item sword2 = ItemDatabase.SearchDatabaseByID(6);

        // end v
        KillQuest killSlimeObjectives = new KillQuest("Kill a slime", slime, 1);
        talkToFrank = new TalkQuest("Talk to Frank", Frank.hiFrank);
        List<QuestObjective> objectives = new List<QuestObjective> {killSlimeObjectives, talkToFrank };
        killSlimeQuest = new Quest("Kill slime", "Kill a slime", objectives);
        killSlimeQuest.setReward(sword);

        questDialogue = new Dialogue("I have a quest for you", killSlimeQuest);
        Dialogue bye = new Dialogue("Goodbye then.");

        questTurnIn = new Dialogue("Thank you!");
        //start ^

      
        
        //frankQuestDialogue = new Dialogue("Talk to Frank", talkToFrank);

        string[] replies = new string[] { "Reply 1", "Reply 2", "Reply 3" };
        //Dialogue[] branches = new Dialogue[] { questDialogue, bye };
        //hiJohn = new DialogueChoice(hello, replies, branches);


        Player.playerInv.AddItem(example);
        Player.playerInv.AddItem(example2);
        Player.playerInv.AddItem(example3);
        Player.playerInv.AddItem(sword);
        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);
        Player.playerInv.AddItem(apple);

        Player.playerInv.AddItem(sword);
        Player.playerInv.AddItem(sword2);

    }

    // Update is called once per frame
    void Update()
    {
        //InventoryMenu.singleton.UpdateInventoryUI(); // update the UI for the inventory

        //Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, 7.5f);
       
        //if (nearbyColliders.Length < 3)
        //{
        //    questDialogue.HideDialogue();
        //}
    }
    public void OnNearby()
    {
        UIController.ShowInteractionTooltip();
    }
    public void NoLongerNearby()
    {
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
