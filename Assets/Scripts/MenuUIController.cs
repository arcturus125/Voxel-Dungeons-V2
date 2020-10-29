using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;


public class MenuUIController : MonoBehaviour
{
    public enum MenuCentered
    {
        None,
        Main,
        Char,
        ISE,
        Inventory
    }

    public static MenuCentered currentMenuCentered;


    public static MenuUIController singleton;
    public static Item.ItemType SelectedInventory;

    [Header("Sprites")]
    public Sprite Clicked_Menu;
    public Sprite Hover_Menu;
    public Sprite Normal_Menu;

    public Sprite Clicked_Submenu;
    public Sprite Hover_Submenu;
    public Sprite Normal_Submenu;

    public Sprite Clicked_Submenu_Point;
    public Sprite Hover_Submenu_Point;
    public Sprite Normal_Submenu_Point;


    public Sprite Social_grey;
    public Sprite Social_White;

    public Sprite Item_grey;
    public Sprite Item_White;

    public Sprite Skills_grey;
    public Sprite Skills_White;

    public Sprite Equipment_grey;
    public Sprite Equipment_White;



    [Header("Buttons")]
    public Button MenuButton;

    public Button Items;
    public Button Skills;
    public Button Equips;


    [Header("Panels")]
    public GameObject InventoryMenusPanel;
    public GameObject InventoryPanel; 

    private Color grey = new Color(50.0f/ 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
    private bool CharMenu = false;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        InventoryMenusPanel.SetActive(false);
        InventoryPanel.SetActive(false);

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationManager();
    }


    public void OnCharMenuClicked()
    {

        currentMenuCentered = MenuCentered.Char;
        if (!CharMenu)
        {
            MenuButton.GetComponent<Image>().sprite = Clicked_Menu;                // if Character menu is activated
            MenuButton.GetComponentsInChildren<Image>()[1].sprite = Social_White;  //
            InventoryMenusPanel.SetActive(true);                                   // open the sub-menus and change the buttons colour to orange
            CharMenu = true;                                                       //
        }
        else
        {
            MenuButton.GetComponent<Image>().sprite = Normal_Menu;                 // if charcter menu is deactivated
            MenuButton.GetComponentsInChildren<Image>()[1].sprite = Social_grey;   //
            InventoryMenusPanel.SetActive(false);                                  // hide the sub-menus and change the bbutton colour to white
            CharMenu = false;                                                      //
            InventoryPanel.SetActive(false);                                       // 

                                                                                   //
            UpdateItemsButton(false);                                              // change the sub-menus button colours to their default
            UpdateSkillsButton(false);                                             //
            UpdateEquipsButton(false);                                             //

            currentMenuCentered = MenuCentered.None;
        }
    }

    public void OnItemsInvClicked()
    {
        bool isClicked = true;
        //toggle the inventory window each time the button is clicked
        if (InventoryPanel.activeInHierarchy && (int)SelectedInventory == 1)
        {
            InventoryPanel.SetActive(false);
            isClicked = false;
        }
        else
            InventoryPanel.SetActive(true);
        
        //open the correct  inventory window
        UpdateItemsButton(isClicked);
        UpdateSkillsButton(false);
        UpdateEquipsButton(false);
    }
    public void OnSkillsInvClicked()
    {
        bool isClicked = true;
        //toggle the inventory window each time the button is clicked
        if (InventoryPanel.activeInHierarchy && (int)SelectedInventory == 2)
        {
            InventoryPanel.SetActive(false);
            isClicked = false;
        }
        else
            InventoryPanel.SetActive(true);

        //open the correct  inventory window
        UpdateItemsButton(false);
        UpdateSkillsButton(isClicked);
        UpdateEquipsButton(false);
    }
    public void OnEquipsInvClicked()
    {
        bool isClicked = true;
        //toggle the inventory window each time the button is clicked
        if (InventoryPanel.activeInHierarchy && (int)SelectedInventory == 3)
        {
            InventoryPanel.SetActive(false);
            isClicked = false;
        }
        else
            InventoryPanel.SetActive(true);

        //open the correct  inventory window
        UpdateItemsButton(false);
        UpdateSkillsButton(false);
        UpdateEquipsButton(isClicked);
    }

    private void UpdateItemsButton(bool isClicked)
    {
        if(isClicked)
        {
            Items.GetComponent<Image>().sprite = Clicked_Submenu;
            Items.GetComponentsInChildren<Image>()[1].sprite = Item_White;
            Items.GetComponentInChildren<Text>().color = Color.white;
            SelectedInventory = Item.ItemType.Item;
            InventoryMenu.singleton.UpdateInventoryUI();

            currentMenuCentered = MenuCentered.ISE;
        }
        else
        {
            Items.GetComponent<Image>().sprite = Normal_Submenu;
            Items.GetComponentsInChildren<Image>()[1].sprite = Item_grey;
            Items.GetComponentInChildren<Text>().color = grey;

        }
    }
    private void UpdateSkillsButton(bool isClicked)
    {
        if (isClicked)
        {
            Skills.GetComponent<Image>().sprite = Clicked_Submenu_Point;
            Skills.GetComponentsInChildren<Image>()[1].sprite = Skills_White;
            Skills.GetComponentInChildren<Text>().color = Color.white;
            SelectedInventory = Item.ItemType.Skill;
            InventoryMenu.singleton.UpdateInventoryUI();

            currentMenuCentered = MenuCentered.ISE;
        }
        else
        {
            Skills.GetComponent<Image>().sprite = Normal_Submenu_Point;
            Skills.GetComponentsInChildren<Image>()[1].sprite = Skills_grey;
            Skills.GetComponentInChildren<Text>().color = grey;
        }
    }
    private void UpdateEquipsButton(bool isClicked)
    {
        if (isClicked)
        {
            Equips.GetComponent<Image>().sprite = Clicked_Submenu;
            Equips.GetComponentsInChildren<Image>()[1].sprite = Equipment_White;
            Equips.GetComponentInChildren<Text>().color = Color.white;
            SelectedInventory = Item.ItemType.Equipment;
            InventoryMenu.singleton.UpdateInventoryUI();

            currentMenuCentered = MenuCentered.ISE;
        }
        else
        {
            Equips.GetComponent<Image>().sprite = Normal_Submenu;
            Equips.GetComponentsInChildren<Image>()[1].sprite = Equipment_grey;
            Equips.GetComponentInChildren<Text>().color = grey;
        }
    }



    public void CloseAllMenus()
    {
        currentMenuCentered = MenuCentered.None;


        MenuButton.GetComponent<Image>().sprite = Normal_Menu;                 // if charcter menu is deactivated
        MenuButton.GetComponentsInChildren<Image>()[1].sprite = Social_grey;   //
        InventoryMenusPanel.SetActive(false);                                  // hide the sub-menus and change the bbutton colour to white
        CharMenu = false;                                                      //
        InventoryPanel.SetActive(false);                                       // 

        
        UpdateItemsButton(false);                                              // change the sub-menus button colours to their default
        UpdateSkillsButton(false);                                             //
        UpdateEquipsButton(false);                                             //
    }

    private void AnimationManager()
    {
        if (currentMenuCentered == MenuCentered.Char)
        {
            anim.SetBool("charMenu", true);
        }
        else if (currentMenuCentered == MenuCentered.None)
        {
            anim.SetBool("charMenu", false);
            anim.SetBool("ISE", false);
        }


        if (currentMenuCentered == MenuCentered.ISE)
        {
            anim.SetBool("ISE", true);
        }
    }

    public void InventoryQuickOpen()
    {
        //Opens character menu
        MenuButton.GetComponent<Image>().sprite = Clicked_Menu;                
        MenuButton.GetComponentsInChildren<Image>()[1].sprite = Social_White;  
        InventoryMenusPanel.SetActive(true);                                   
        CharMenu = true;

        //Opens item inventory menu
        InventoryPanel.SetActive(true);
        UpdateItemsButton(true);
        UpdateSkillsButton(false);
        UpdateEquipsButton(false);

        currentMenuCentered = MenuCentered.ISE;

        anim.SetBool("charMenu", true);
        anim.SetBool("ISE", true);
    }
}
