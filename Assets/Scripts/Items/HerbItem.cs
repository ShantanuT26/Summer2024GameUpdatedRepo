using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbItem : Item
{
    [SerializeField] private ScrObj itemInfo;
    //[SerializeField] string name;
    [SerializeField] int quantity;
    //[SerializeField] Sprite sprite;
    [SerializeField] private GameObject inventoryCanvas;
    private InventoryManager inventoryManager;
    void Start()
    {
        inventoryManager = inventoryCanvas.GetComponent<InventoryManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.gameObject.SetActive(false);
        inventoryManager.addItem(itemInfo, quantity);
        Debug.Log("herbcollisionsprite: " + itemInfo.sprite);
    }
}
