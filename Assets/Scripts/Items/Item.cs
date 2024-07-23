using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] int quantity;
    [SerializeField] Sprite sprite;
    [SerializeField]private GameObject inventoryCanvas;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }
    void Start()
    {
        inventoryManager = inventoryCanvas.GetComponent<InventoryManager>();
    }
    public Sprite getSprite()
    {
        return sprite;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.gameObject.SetActive(false);
        inventoryManager.addItem(name, quantity, sprite);
    }
}
