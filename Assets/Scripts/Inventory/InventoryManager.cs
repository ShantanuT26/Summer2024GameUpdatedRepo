using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.UI;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private bool wasDropped;
    [SerializeField]private GameObject inventory;
    [SerializeField]private bool menuActive;
    private PlayerInput playerInput;
    private InputAction myInventory;
    [SerializeField]private ItemSlotScript[] itemslots;
    [SerializeField]private ScrObj[] scrobj;
    private VisibleInventory visInventory;

    private void Awake()
    {
       // cherry = GameObject.Find("Cherry").GetComponent<Item>();;
        playerInput = GetComponent<PlayerInput>();
        myInventory = playerInput.actions["Inventory"];
        inventory.SetActive(false);
        menuActive = false;
        visInventory = GameObject.Find("VisInvCanvas").GetComponent<VisibleInventory>();
        for(int i = 0;  i < itemslots.Length; i++)
        {
            itemslots[i].setMyIndex(i);
        }
    }
    private void OnEnable()
    {
        myInventory.started += invStarted;
        myInventory.performed += invPerformed;
        myInventory.canceled += invCanceled;
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
    private void invStarted(InputAction.CallbackContext context)
    {

    }
    private void invPerformed(InputAction.CallbackContext context)
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
            //Debug.Log(itemslots[0].mysprite);
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
    }
    private void OnClickPerformed(InputAction.CallbackContext context)
    {

    }
    public void DeselectAllSlots()
    {
        for(int i = 0; i<16; i++)
        {
            itemslots[i].SetHighlight(false);
        }
    }
    public void addItem(string n, int q, Sprite s)
    {
        /*if (itemslots[0]!=null)
        {
            itemslots[0].FillSlot(n, q, s);
            UnityEngine.Debug.Log(n + " " + q + "Sprite: " + s);
        }
        else
        {
            Debug.Log("Itemslot 0 is null");
        }*/
       // Debug.Log(itemslots[0].myname);

        for(int i = 0; i<16; i++)
        {
            if (itemslots[i].GetName()=="")
            {
                
                if (q<=64)
                {
                    itemslots[i].FillSlot(n, q, s);
                }
                else
                {
                    itemslots[i].FillSlot(n, 64, s);
                    addItem(n, q - 64, s);
                }
                break;
            }
            else
            {
                if (itemslots[i].GetName() == n && itemslots[i].GetMyQuant()!=64)
                {
                    if(itemslots[i].GetMyQuant() + q <= 64)
                    {
                        //itemslots[i].myquant += q;
                        itemslots[i].SetMyQuant(itemslots[i].GetMyQuant() + q);
                        itemslots[i].SetQuantText(itemslots[i].GetMyQuant().ToString());
                    }
                    else
                    {
                        int tempquant = itemslots[i].GetMyQuant();
                        itemslots[i].SetMyQuant(64);
                        itemslots[i].SetQuantText(itemslots[i].GetMyQuant().ToString());
                        addItem(n, tempquant + q - 64, s);
                    }
                    break;
                }
            }
        }
        ToVisInv();
    }
    /*public void changeStats(int i)
    {
        ToVisInv();
    }*/
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
        /*if(count == 0)
        {
            return false;
        }
        return true;*/
        return count;
    }
    public void ToVisInv()
    {
        for(int i = 0; i<visInventory.GetVisPanels().Length; i++)
        {
            visInventory.FillVisPanel(i, itemslots[i].GetMyQuant(), itemslots[i].getSprite());
        }
    }
}
