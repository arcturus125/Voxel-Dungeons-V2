
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IMPORT:: attach this to the menu game object panel
public class InventoryMenu : MonoBehaviour
{
    public static InventoryMenu singleton; // singleton
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private InventoryMenuItem _itemListing;

    [SerializeField]
    private  float nextListingOffset= 25;
    [SerializeField]
    private  float heightOflisting = 25.7f;

    public List<InventoryMenuItem> prefabList;


    void Awake()
    {
        singleton = this;
    }

    private void Update()
    {
        //if(MenuUIController.singleton.InventoryPanel.activeInHierarchy)
        //{
        //    UpdateInventoryUI();
        //}
    }

    public void UpdateInventoryUI()
    {
        foreach(InventoryMenuItem imi in prefabList)
        {
            DestroyImmediate(imi.gameObject);
        }
        prefabList = new List<InventoryMenuItem>();
        int index = 0;
        foreach( InventorySlot item in Player.playerInv.inv)
        {
            if ((int)item.item.type == (int)MenuUIController.SelectedInventory)
            {
                InventoryMenuItem itemListing = Instantiate(_itemListing, _content);
                itemListing.SetItemListingInfo(item, Player.playerInv.inv.IndexOf(item));
                itemListing.transform.Translate(new Vector2(0, (-nextListingOffset * index)));
                prefabList.Add(itemListing);


                index++;
            }
        }
        RectTransform r = _content.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(
            r.sizeDelta.x,
            heightOflisting * index);
    }

    public void OnScroll()
    {
        DestroyImmediate(InventoryMenuItem.EquipButtonGameObject);
    }


}
