using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class QuestListing : MonoBehaviour
{
    public Text button_text;

    public Quest quest;
    public int indexOfListing;

    public void SetQuestListingInfo(Quest q, int Index)
    {
        quest = q;
        indexOfListing = Index;

        button_text.text = " " + q.title;


    }
}
