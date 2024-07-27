using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.UI;
using UnityEngine.UI;
using System;

public class InventoryManager : MonoBehaviour
{
    private bool wasDropped;
    [SerializeField]private GameObject inventory;   
    [SerializeField]private bool menuActive;
    private PlayerInput playerInput;
    private InputAction myInventory;
    [SerializeField]private ItemSlotScript[] itemslots;
    [SerializeField]private ScrObj[] scrobj;
    [SerializeField] private HashSet<ScrObj> herbs;
    public static event Action BackToGame;
    public static event Action BackToMainMenu;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        myInventory = playerInput.actions["Inventory"];
        herbs = new HashSet<ScrObj>();
    }
    private void Start()
    {
        inventory.SetActive(false);
        menuActive = false;
        
        for (int i = 0; i < itemslots.Length; i++)
        {
            itemslots[i].setMyIndex(i);
        }
        for(int i = 0; i < scrobj.Length; i++)
        {
            herbs.Add(scrobj[i]);
        }
    }
    private void OnEnable()
    {
        /* myInventory.performed += invPerformed;
         myInventory.canceled += invCanceled;*/
        ItemMenuManager.OpenHerbsMenu += OpenHerbsMenu;
        BackToGame += CloseHerbsMenu;
        BackToMainMenu += CloseHerbsMenu;
        
        
    }
    public bool GetWasDropped()
    {
        return wasDropped;
    }
    public void SetWasDropped(bool x)
    {
        wasDropped = x;
    }
    public ScrObj[] GetScrObjArray()
    {
        return scrobj;
    }
    public ItemSlotScript[] GetItemSlotsArray()
    {
        return itemslots;
    }
    public ItemSlotScript GetItemSlot(int x)
    {
        return itemslots[x];
    }
    public ScrObj GetScrObj(int x)
    {
        return scrobj[x];
    }
    public void CloseHerbsMenu()
    {
        Time.timeScale = 1;
        inventory.SetActive(false);
        menuActive = false;
    }
    public void BackToGameButtonClicked()
    {
        BackToGame.Invoke();
    }
    public void BackToMainMenuButtonClicked()
    {
        BackToMainMenu.Invoke();
    }
    /*private void invPerformed(InputAction.CallbackContext context)
    {
        Time.timeScale = 0;
        if(menuActive)
        {
            inventory.SetActive(false);
            menuActive = false;
        }
        else if(!menuActive)
        {
            inventory.SetActive(true);
            menuActive = true;
            Debug.Log("slot0count: " + itemslots[0].GetMyQuant() + " sprite: " + itemslots[0].getSprite() + " name: " + itemslots[0].
            GetName());
            Debug.Log("slot1count: " + itemslots[1].GetMyQuant() + " sprite: " + itemslots[1].getSprite() + " name: " + itemslots[1].
            GetName());
        }
    }
    private void invCanceled(InputAction.CallbackContext context)
    {
        if (menuActive)
        {
            Time.timeScale = 0;
        }
        else if(!menuActive)
        {
            Time.timeScale = 1;
        }
    }*/
    private void OpenHerbsMenu()
    {
        Time.timeScale = 0;
        inventory.SetActive(true);
        menuActive = true;
    }
    public void DeselectAllSlots()
    {
        for(int i = 0; i<16; i++)
        {
            itemslots[i].SetHighlight(false);
        }
    }   
    public void addItem(ScrObj itemInfo, int q)
    {
        //PotionsCraftingManager.InvokeCheckHerbsOnPlayerAction();
        Debug.Log("itemaddedfrominvmanager");
        for(int i = 0; i<16; i++)
        {
            if (itemslots[i].GetName()=="")
            {
                
                if (q<=64)
                {
                    Debug.Log("fillingitemslotfrominvmanager");
                    Debug.Log("quantitytofill: " + q);
                    itemslots[i].FillSlot(itemInfo, q);
                }
                else
                {
                    itemslots[i].FillSlot(itemInfo, 64);
                    addItem(itemInfo, q - 64);
                }
                break;
            }
            else
            {
                if (itemslots[i].GetName() == itemInfo.name && itemslots[i].GetMyQuant()!=64)
                {
                    if(itemslots[i].GetMyQuant() + q <= 64)
                    {
                        itemslots[i].AdjustQuantity(q);
                    }
                    else
                    {
                        int tempquant = itemslots[i].GetMyQuant();
                        itemslots[i].SetQuantity(64);
                        addItem(itemInfo, tempquant + q - 64);
                    }
                    break;
                }
            }
            herbs.Add(itemInfo);
        }
        Debug.Log("slot0count: " + itemslots[0].GetMyQuant() + " sprite: " + itemslots[0].getSprite() + " name: " + itemslots[0].
            GetName());
        Debug.Log("slot1count: " + itemslots[1].GetMyQuant() + " sprite: " + itemslots[1].getSprite() + " name: " + itemslots[1].
            GetName());
    }
    public int checkDraggedInto()
    {
        int count = 0;
        for(int i = 0; i<itemslots.Length; i++) 
        {
            if (itemslots[i].GetDraggedInto()==true)
            {
                count++;
            }
        }
        return count;
    }
}
