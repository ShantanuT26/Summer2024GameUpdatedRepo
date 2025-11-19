using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbItem : Item
{
    [SerializeField] private ScrObj itemInfo;
    //[SerializeField] string name;
    [SerializeField] int quantity;

    [SerializeField] private ItemSlotScript[] itemslots;
    
    //[SerializeField] Sprite sprite;
    [SerializeField] private GameObject inventoryCanvas;
    private InventoryManager inventoryManager;

    [SerializeField] private VisibleInventory visibleInventory;
    void Start()
    {
        inventoryManager = inventoryCanvas.GetComponent<InventoryManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.gameObject.SetActive(false);
        inventoryManager.addItem(itemInfo, quantity);
        for(int i = 0; i<4; i++)
        {
            visibleInventory.FillVisPanel(i, itemslots[i].myquant, itemslots[i].getSprite());
            
        }
        Debug.Log("herbcollisionsprite: " + itemInfo.sprite);
    }
}
