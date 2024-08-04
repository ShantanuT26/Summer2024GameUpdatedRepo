using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.UI;
using UnityEngine.UI;
using System;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    private bool wasDropped;
    [SerializeField]private GameObject inventory;   
    [SerializeField]private bool menuActive;
    private PlayerInput playerInput;
    private InputAction myInventory;
    [SerializeField]private ItemSlotScript[] itemslots;
    [SerializeField]private ScrObj[] scrobj;
    
    public static event Action BackToGame;
    public static event Action BackToMainMenu;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        myInventory = playerInput.actions["Inventory"];
        
    }
    private void Start()
    {
        inventory.SetActive(false);
        menuActive = false;
        
        for (int i = 0; i < itemslots.Length; i++)
        {
            itemslots[i].setMyIndex(i);
        }
    }
        

    private void OnEnable()
    {
        ItemMenuManager.OpenHerbsMenu += OpenHerbsMenu;
        BackToGame += CloseHerbsMenu;
        BackToMainMenu += CloseHerbsMenu;
        PotionsCraftingManager.AdjustHerbDisplayInHerbsMenu += AdjustItemQuantity;
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
        for(int i = 0; i<16; i++)
        {
            if (itemslots[i].GetName()=="")
            {
                if (q<=64)
                {
                    Debug.Log("fillingitemslotfrominvmanager");
                    Debug.Log("quantitytofill: " + q);
                    itemslots[i].FillSlot(itemInfo, q);
                    ItemAdded(itemInfo);
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
                        ItemAdded(itemInfo);
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
        }
    }
    public void AdjustItemQuantity(ScrObj itemInfo, int q)
    {
        Debug.Log("adjustingitemq");
        for (int i = 15; i >= 0; i--)
        {
            if (itemslots[i].slotInfo.Equals(itemInfo))
            {
                Debug.Log("quantityadjusted");
                itemslots[i].AdjustQuantity(q);
                break;
            }
        }
    }
    private void ItemAdded(ScrObj info)
    {
        /* I Initially had the code that was in this method at the very end of the AddItem() method,
          but since AddItem() recursively calls itself, this got called multiple times too, which 
        messed up the AdjustHerbDisplay method in PotionsCraftingManager, since I kept invoking
        one of its actions with InvokeCheckHerbsOnPLayerAction*/
        int herbCount = CheckTotalHerbsCount(info);
        GlobalVariables.Instance.herbs.Add(info);
        PotionsCraftingManager.InvokeCheckHerbsOnPlayerAction(info, herbCount);
    }

    public int CheckTotalHerbsCount(ScrObj herbInfo)
    {
        int itemCount = 0;
        foreach (ItemSlotScript slot in itemslots)
        {
            if (slot.slotInfo.name == herbInfo.name)
            {
                itemCount += slot.myquant;
            }
        }
        return itemCount;
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
