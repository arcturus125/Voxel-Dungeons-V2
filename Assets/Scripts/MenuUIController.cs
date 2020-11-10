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
        Inventory,
        Quest
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

    public Sprite Msg_grey;
    public Sprite Msg_white;

    public Sprite Item_grey;
    public Sprite Item_White;

    public Sprite Skills_grey;
    public Sprite Skills_White;

    public Sprite Equipment_grey;
    public Sprite Equipment_White;



    [Header("Buttons")]
    public Button MenuButton;
    public Button QuestButton;

    public Button Items;
    public Button Skills;
    public Button Equips;

    public Button QuestHelperButton;
    public Button InProgressButton;
    public Button CompletedButton;


    [Header("Panels")]
    public GameObject InventoryMenusPanel;
    public GameObject InventoryPanel;
    public GameObject QuestPanel;

    private Color grey = new Color(50.0f/ 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
    private bool CharMenuOpen = false;
    private bool QuestMenuOpen = false;
    private Animator anim;

    void Start()
    {
        singleton = this;
        InventoryMenusPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        QuestPanel.SetActive(false);

        anim = GetComponent<Animator>();
    }
    void Update()
    {
        AnimationManager();
    }

    /// #########
    ///   main
    /// #########

    public void OnCharMenuClicked()
    {
        if (!CharMenuOpen)
        {
            UpdateCharMenu(true);
        }
        else
        {
            UpdateCharMenu(false);
        }
    }
    public void OnQuestMenuClicked()
    {
        if (!QuestMenuOpen)
        {
            UpdateQuestMenu(true);                                               
        }
        else
        {
            UpdateQuestMenu(false);
        }
    }

    private void UpdateCharMenu(bool isActivated, bool chainReaction = false)
    {
        if(isActivated)
        {
            MenuButton.GetComponent<Image>().sprite = Clicked_Menu;                // if Character menu is activated
            MenuButton.GetComponentsInChildren<Image>()[1].sprite = Social_White;  //
            InventoryMenusPanel.SetActive(true);                                   // open the sub-menus and change the buttons colour to orange
            CharMenuOpen = true;

            UpdateQuestMenu(false, true);

            currentMenuCentered = MenuCentered.Char;
        }
        else
        {
            MenuButton.GetComponent<Image>().sprite = Normal_Menu;                 // if charcter menu is deactivated
            MenuButton.GetComponentsInChildren<Image>()[1].sprite = Social_grey;   //
            InventoryMenusPanel.SetActive(false);                                  // hide the sub-menus and change the bbutton colour to white
            CharMenuOpen = false;                                                      //
            InventoryPanel.SetActive(false);                                       // 

            UpdateItemsButton(false);                                              // change the sub-menus button colours to their default
            UpdateSkillsButton(false);                                             //
            UpdateEquipsButton(false);                                             //

            currentMenuCentered = MenuCentered.None;

        }
    }
    private void UpdateQuestMenu(bool isActivated, bool chainReaction = false)
    {
        if(isActivated)
        {
            QuestButton.GetComponent<Image>().sprite = Clicked_Menu;
            QuestButton.GetComponentsInChildren<Image>()[1].sprite = Msg_white;
            QuestMenuOpen = true;
            QuestPanel.SetActive(true);

            UpdateCharMenu(false);

            currentMenuCentered = MenuCentered.Quest;

            UpdateQuestHelperButton(UIController.QuestHelperPanel.activeInHierarchy, true); // if the quest menu is active, update the button to show that
        }
        else
        {
            QuestButton.GetComponent<Image>().sprite = Normal_Menu;
            QuestButton.GetComponentsInChildren<Image>()[1].sprite = Msg_grey;
            QuestMenuOpen = false;
            QuestPanel.SetActive(false);

            if (!chainReaction)
            {
                InventoryPanel.SetActive(false);
                InventoryMenusPanel.SetActive(false);
            }

            currentMenuCentered = MenuCentered.None;
        }
    }

    /// #########
    ///   ISE
    /// #########
   
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
        {
            InventoryPanel.SetActive(true);
            
        }

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
        {
            InventoryPanel.SetActive(true);
        }

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
        {
            InventoryPanel.SetActive(true);
        }

            //open the correct  inventory window
            UpdateItemsButton(false);
        UpdateSkillsButton(false);
        UpdateEquipsButton(isClicked);
    }


    bool ItemsClicked = false;
    bool SkillsClicked = false;
    bool EquipsClicked = false;

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
            ItemsClicked = true;
        }
        else
        {
            Items.GetComponent<Image>().sprite = Normal_Submenu;
            Items.GetComponentsInChildren<Image>()[1].sprite = Item_grey;
            Items.GetComponentInChildren<Text>().color = grey;
            ItemsClicked = false;

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
            SkillsClicked = true;
        }
        else
        {
            Skills.GetComponent<Image>().sprite = Normal_Submenu_Point;
            Skills.GetComponentsInChildren<Image>()[1].sprite = Skills_grey;
            Skills.GetComponentInChildren<Text>().color = grey;
            SkillsClicked = false;
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
            EquipsClicked = true;
        }
        else
        {
            Equips.GetComponent<Image>().sprite = Normal_Submenu;
            Equips.GetComponentsInChildren<Image>()[1].sprite = Equipment_grey;
            Equips.GetComponentInChildren<Text>().color = grey;
            EquipsClicked = false;
        }
    }

    /// ################
    ///     Quest
    /// ################
 
    public void OnQuestHelperClick()
    {
        bool isClicked = true;
        if(UIController.QuestHelperPanel.activeInHierarchy)
        {
            isClicked = false;
        }
        UpdateQuestHelperButton(isClicked);
        UIController.QuestHelperPanel.SetActive(isClicked);

    }
    public void OnInProgressClick()
    {

    }
    public void OnCompletedClick()
    {

    }

    private void UpdateQuestHelperButton(bool isClicked, bool justUpdateButton = false)
    {
        if (isClicked)
        {
            QuestHelperButton.GetComponent<Image>().sprite = Clicked_Submenu;
            //QuestHelperButton.GetComponentsInChildren<Image>()[1].sprite = <quest helper sprite here> ;
            QuestHelperButton.GetComponentInChildren<Text>().color = Color.white;

            //currentMenuCentered = MenuCentered.ISE; // change to QuestSubmenu later
        }
        else
        {
            QuestHelperButton.GetComponent<Image>().sprite = Normal_Submenu;
            //QuestHelperButton.GetComponentsInChildren<Image>()[1].sprite = <quest helper sprite here> ;
            QuestHelperButton.GetComponentInChildren<Text>().color = grey;

        }
    }
    private void UpdateInProgressButton(bool isClicked)
    {
        QuestMenu.selectedQuestMenu = QuestMenu.ESelectedQuestMenu.InProgress;
    }
    private void UpdateCompletedButton(bool isClicked)
    {

    }



    /// #########
    ///   other
    /// #########

    public void CloseAllMenus()
    {
        currentMenuCentered = MenuCentered.None;


        UpdateCharMenu(false);    // change main menu button colours to their default
        UpdateQuestMenu(false);   //  and close ant panels left open



        UpdateItemsButton(false);   // change the sub-menus button colours to their default
        UpdateSkillsButton(false);  //
        UpdateEquipsButton(false);  //
    }
    private void AnimationManager()
    {
        if(anim.GetBool("QuickInvOpen"))
            anim.SetBool("QuickInvOpen", false);
        if (currentMenuCentered == MenuCentered.Char)
        {
            anim.SetBool("charMenu", true);
            anim.SetBool("Quest", false);
        }
        else if (currentMenuCentered == MenuCentered.None)
        {
            anim.SetBool("charMenu", false);
            anim.SetBool("Quest", false);
            anim.SetBool("ISE", false);
        }


        else if (currentMenuCentered == MenuCentered.ISE)
        {
            anim.SetBool("ISE", true);
        }

        else if(currentMenuCentered == MenuCentered.Quest)
        {
            anim.SetBool("Quest", true);
            anim.SetBool("charMenu", false);
            anim.SetBool("ISE", false);
        }

        if(ItemsClicked || SkillsClicked || EquipsClicked)
        {
            anim.SetBool("Inventory", true);
        }
        else
        {
            anim.SetBool("Inventory", false);
        }

        anim.SetBool("ItemInvOpen", ItemsClicked);
        anim.SetBool("SkillsInvOpen", SkillsClicked);
        anim.SetBool("EquipsInvOpen", EquipsClicked);
    }
    public void InventoryQuickOpen()
    {
        //Opens character menu
        MenuButton.GetComponent<Image>().sprite = Clicked_Menu;                
        MenuButton.GetComponentsInChildren<Image>()[1].sprite = Social_White;  
        InventoryMenusPanel.SetActive(true);                                   
        CharMenuOpen = true;

        //Opens item inventory menu
        InventoryPanel.SetActive(true);
        UpdateItemsButton(true);
        UpdateSkillsButton(false);
        UpdateEquipsButton(false);

        currentMenuCentered = MenuCentered.ISE;

        anim.SetBool("charMenu", true);
        anim.SetBool("ISE", true); // set
        anim.SetBool("QuickInvOpen", true);
    }
}
