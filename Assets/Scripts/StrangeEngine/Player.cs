using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// IMPORT:: attach this script to the player
public class Player : MonoBehaviour
{
    public static Player singleton;

    public static Inventory playerInv = new Inventory(); // create an inventory for the player
    public GameObject WeaponSlot;

    public int weaponEquippedIndex = -1; //-1 is rogue value

    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
    }

    void Update()
    {
        for(int i = 0; i< playerInv.inv.Count; i++)
        {
            //Debug.Log(playerInv.inv[i].quantity +" - "+ playerInv.inv[i].item.itemName);
        }
    }

    public void EquipWeapon(GameObject prefab)
    {
        Instantiate(prefab, WeaponSlot.transform);
        //prefab.transform.SetParent(WeaponSlot.transform);
        //prefab.transform.position = Vector3.zero;
    }

    public void Hit(Collider enemyHit)
    {
        if (weaponEquippedIndex != -1)
        {
            //If player has a weapon equipped
            Weapon equippedWeapon = playerInv.inv[weaponEquippedIndex].convertItemToWeapon();
            EnemyComponent enemy = enemyHit.gameObject.GetComponent<EnemyComponent>();
            enemy.Damage(equippedWeapon.Damage);
        }
    }

}
