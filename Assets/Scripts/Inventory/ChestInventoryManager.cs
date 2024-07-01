using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.UI;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class ChestInventoryManager : MonoBehaviour
{
    
    //public Item cherry;
    [SerializeField]private GameObject CInventory;
    private bool menuActive;
    [SerializeField]private ChestItemSlotScript[] TItemSlots;

    private void Awake()
    {
        CInventory.SetActive(false);
        menuActive = false;
        for (int i = 0; i < TItemSlots.Length; i++)
        {
            TItemSlots[i].setMyIndex(i);
        }
    }
    private void Start()
    {

    }
    public GameObject getCInventory()
    {
        return CInventory;
    }
    public void DeselectAllSlots()
    {
        for (int i = 0; i < 16; i++)
        {
            TItemSlots[i].GetHighlight().SetActive(false);
        }
    }
    
    public void Activate()
    {
        CInventory.SetActive(true);
    }
    public void Deactivate()
    {
        CInventory.SetActive(false);
    }
    public void addItem(string n, int q, Sprite s)
    {
        for (int i = 0; i < 16; i++)
        {
            if (TItemSlots[i].GetName() == "")
            {
                if (q <= 64)
                {
                    TItemSlots[i].FillSlot(n, q, s);
                }
                else
                {
                    TItemSlots[i].FillSlot(n, 64, s);
                    addItem(n, q - 64, s);
                }
                break;
            }
            else
            {
                if (TItemSlots[i].GetName() == n && TItemSlots[i].GetChestQuant() != 64)
                {
                    if (TItemSlots[i].GetChestQuant() + q <= 64)
                    {
                        //TItemSlots[i].myquant += q;
                        TItemSlots[i].SetChestQuant(TItemSlots[i].GetChestQuant()+q);
                        TItemSlots[i].SetQuantText(TItemSlots[i].GetChestQuant().ToString());
                    }
                    else
                    {
                        int tempquant = TItemSlots[i].GetChestQuant();
                        TItemSlots[i].SetChestQuant(64);
                        TItemSlots[i].SetQuantText(TItemSlots[i].GetChestQuant().ToString());
                        addItem(n, tempquant + q - 64, s);
                    }
                    break;
                }
            }
        }
    }

}
