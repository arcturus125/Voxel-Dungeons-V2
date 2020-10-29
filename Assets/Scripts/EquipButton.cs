using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipButton : MonoBehaviour
{
    public InventoryMenuItem referenceToMenuItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEquipClick()
    {
        GameObject prefab = Player.playerInv.inv[referenceToMenuItem.inventoryIndexOfItem].convertItemToWeapon().WeaponModelPrefab;
        Player.singleton.EquipWeapon(prefab);
        Player.singleton.weaponEquippedIndex = referenceToMenuItem.inventoryIndexOfItem;
    }
}
