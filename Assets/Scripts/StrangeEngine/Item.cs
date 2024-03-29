﻿// Copyright(c) 2020 arcturus125 & StrangeDevTeam
// Free to use and modify as you please, Not to be published, distributed, licenced or sold without permission from StrangeDevTeam
// Requests for the above to be made here: https://www.reddit.com/r/StrangeDev/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "StrangeEngine/Item", order = 2)]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Item = 1,
        Skill = 2,
        Equipment = 3
    }
    public ItemType type;
    public int ID;
    public string itemName;
    public string info;
    public int worth;
    public bool isStackable = false;
    //public Sprite icon;
    //public Dictionary<string, int> itemStats = new Dictionary<string, int>(); // "stats" that come in pairs, a name (string) and a number. for example ("weight", 22)

    //public Item(ItemType pType, int pID, string pName, string pInfo, int pWorth, Dictionary<string,int> pStats)
    //{
    //    type = pType;
    //    ID = pID;
    //    itemName = pName;
    //    info = pInfo;
    //    worth = pWorth;
    //    //itemStats = pStats;
    //}
    //public Item(ItemType pType, int pID, string pName, string pInfo, int pWorth, Dictionary<string, int> pStats, bool pIsStackable)
    //{
    //    type = pType;
    //    ID = pID;
    //    itemName = pName;
    //    info = pInfo;
    //    worth = pWorth;
    //    //itemStats = pStats;
    //    isStackable = pIsStackable;
    //}

}



