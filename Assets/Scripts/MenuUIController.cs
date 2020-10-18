using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;


public class MenuUIController : MonoBehaviour
{


    public  enum InventoryTypes
    {
        Items,
        Skills,
        Equipment
    }
    public static InventoryTypes SelectedInventory;

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
    public GameObject InventoriesPanel;

    private Color grey = new Color(50.0f/ 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);

    // Start is called before the first frame update
    void Start()
    {
        InventoriesPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnCharMenuClicked()
    {
        MenuButton.GetComponent<Image>().sprite = Clicked_Menu;
        MenuButton.GetComponentsInChildren<Image>()[1].sprite = Social_White;
        InventoriesPanel.SetActive(true);
    }

    public void OnItemsInvClicked()
    {
        UpdateItemsButton(true);
        UpdateSkillsButton(false);
        UpdateEquipsButton(false);
    }
    public void OnSkillsInvClicked()
    {
        UpdateItemsButton(false);
        UpdateSkillsButton(true);
        UpdateEquipsButton(false);
    }
    public void OnEquipsInvClicked()
    {
        UpdateItemsButton(false);
        UpdateSkillsButton(false);
        UpdateEquipsButton(true);
    }

    private void UpdateItemsButton(bool isClicked)
    {
        if(isClicked)
        {
            Items.GetComponent<Image>().sprite = Clicked_Submenu;
            Items.GetComponentsInChildren<Image>()[1].sprite = Item_White;
            Items.GetComponentInChildren<Text>().color = Color.white;
            SelectedInventory = InventoryTypes.Items;
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
            SelectedInventory = InventoryTypes.Skills;
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
            SelectedInventory = InventoryTypes.Equipment;
        }
        else
        {
            Equips.GetComponent<Image>().sprite = Normal_Submenu;
            Equips.GetComponentsInChildren<Image>()[1].sprite = Equipment_grey;
            Equips.GetComponentInChildren<Text>().color = grey;
        }
    }

}
