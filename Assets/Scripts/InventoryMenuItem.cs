using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuItem : MonoBehaviour
{
    public static int SelectedItemID;
    public static GameObject EquipButtonGameObject;

    public int inventoryIndexOfItem;
    public float buttonOffset = 180.0f;
    //public GameObject panel;
    public Text Button_Text;
    //public GameObject equipbutton;
    // TODO: item image/ sprite

    public void SetItemListingInfo(InventorySlot slot, int index)
    {
        inventoryIndexOfItem = index;
        if (slot.quantity > 1)
        {
            Button_Text.text = " "+ slot.quantity + " - " + slot.item.itemName;
        }
        else
        {
            Button_Text.text = " - " + slot.item.itemName;
        }
    }

    public void OnItemClicked()
    {
        Debug.Log("ButtonClicked");
        //showEquipButton();

        //If equipment inventory is open equip clicked item
        if (MenuUIController.SelectedInventory == Item.ItemType.Equipment) {
            GameObject prefab = Player.playerInv.inv[inventoryIndexOfItem].convertItemToWeapon().WeaponModelPrefab;
            Player.singleton.EquipWeapon(prefab);
            Player.singleton.weaponEquippedIndex = inventoryIndexOfItem;
        }
        
    }

    // shows the equip button next to the inventory item that was clicked
    //private void showEquipButton()
    //{
    //    // generate and position button
    //    if(EquipButtonGameObject)
    //    {
    //        DestroyImmediate(EquipButtonGameObject);
    //    }
    //    EquipButtonGameObject = Instantiate(equipbutton); // TODO: delete button when user scrolls
    //    EquipButtonGameObject.name = "EquipItem  button";
    //    SelectedItemID = inventoryIndexOfItem;
    //    EquipButtonGameObject.transform.SetParent(GameObject.Find("InventoryPanel").transform);
    //    EquipButtonGameObject.transform.position = new Vector2(
    //        this.transform.position.x + buttonOffset,
    //        this.transform.position.y);

    //    //// if weapon is already equipped
    //    //if (InventoryMenuItem.SelectedItemID == PlayerData.data.equippedWeaponIndex)
    //    //{
    //    //    EquipButtonGameObject.GetComponentInChildren<Text>().text = "Equipped";
    //    //    EquipButtonGameObject.GetComponentInChildren<Text>().color = new Color(255, 255, 255, 255);
    //    //    EquipButtonGameObject.GetComponentInChildren<Button>().interactable = false;
    //    //    EquipButtonGameObject.GetComponentInChildren<Image>().color = new Color(255, 0, 99, 255);
    //    //}
    //    //// if item is not already equipped
    //    //else
    //    //{
    //    //    EquipButtonGameObject.GetComponentInChildren<Text>().text = "Equip";
    //    //    EquipButtonGameObject.GetComponentInChildren<Text>().color = new Color(255, 255, 255, 255);
    //    //    EquipButtonGameObject.GetComponentInChildren<Button>().interactable = true;
    //    //    EquipButtonGameObject.GetComponentInChildren<Image>().color = new Color(0, 204, 205, 255);
    //    //}


    //}
}
