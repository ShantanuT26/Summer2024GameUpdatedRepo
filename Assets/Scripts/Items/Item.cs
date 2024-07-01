using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] string name;
    [SerializeField] int quantity;
    [SerializeField] Sprite sprite;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }
    void Start()
    {
        
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    public Sprite getSprite()
    {
        return sprite;
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.gameObject.SetActive(false);
        inventoryManager.addItem(name, quantity, sprite);
    }
}
