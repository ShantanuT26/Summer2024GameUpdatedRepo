using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ScrObj itemInfo;
    //[SerializeField] string name;
    [SerializeField] int quantity;
    //[SerializeField] Sprite sprite;
    [SerializeField]private GameObject inventoryCanvas;
    private InventoryManager inventoryManager;

    private void Awake()
    {
    }
    void Start()
    {
        inventoryManager = inventoryCanvas.GetComponent<InventoryManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.gameObject.SetActive(false);
        inventoryManager.addItem(itemInfo, quantity);
        /* Change to inventoryManager.addItem(scriptableobject, quantity);  where scriptableobject
         contains name, sprite, healing, and mana (or whatever) (or replace healing and mana with description, 
        since potions will have healing and mana, and herbs will have nothing*/


    }
}
