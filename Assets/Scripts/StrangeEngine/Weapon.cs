using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "StrangeEngine/Weapon", order = 3)]
public class Weapon : Item
{
    public int Damage;
    public GameObject WeaponModelPrefab;


    private void Awake()
    {
        Debug.Log(name);
    }

    //public Weapon(int pID, string pName, string pInfo, int pWorth, Dictionary<string, int> pStatsfloat, int pDamage, GameObject pWeaponPrefab) : base(ItemType.Equipment, pID, pName, pInfo, pWorth, pStatsfloat)
    //{
    //    Damage = pDamage;
    //    WeaponModelPrefab = pWeaponPrefab;
    //}
}
