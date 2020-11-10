using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frank : MonoBehaviour
{
    public Transform interactionButtonLocation;
    public static Dialogue hiFrank;
    public bool isWaving = false;
    Animator anim;


    public Enemy slime;
    Quest killSlimeQuest;
    // Start is called before the first frame update
    void Start()
    {
        KillQuest killSlimeObjectives = new KillQuest("Kill a slime", slime, 10);
        List<QuestObjective> objectives = new List<QuestObjective> { killSlimeObjectives };
        killSlimeQuest = new Quest("Kill golem", "Kill a slime", objectives);

        hiFrank = new Dialogue("Hi, I'm Frank", killSlimeQuest);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject == PlayerInteraction.ClosestInteractible)
            UIController.SetUiInteractionButtonPosition(this.interactionButtonLocation);
        anim = GetComponent<Animator>();
    }

    public void Use()
    {
        anim.SetBool("isWaving", true);
        hiFrank.ShowDialogue();
    }

    public void NoLongerNearby()
    {
        PlayerInteraction.RemoveInteractible(this.gameObject);
        hiFrank.HideDialogue();
        anim.SetBool("isWaving", false);

    }



    public void OnNearby()
    {
        PlayerInteraction.AddInteractible(this.gameObject);
    }

    public void IsInteractable(bool value)
    {
        
    }

}
