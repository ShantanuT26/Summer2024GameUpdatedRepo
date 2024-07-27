using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public bool intoSameType;
    private InputAction rightClick;
    private PlayerInput myPlayerInput;
    [SerializeField] private UnityEngine.UI.Image image;
    [SerializeField] private ItemSlotScript mySlot;
    [SerializeField] private Sprite myBackground;
    private Transform parentafterdrag;
    private Vector2 initialposition;
    [SerializeField] private GameObject inventoryCanvas;
    private InventoryManager inventoryManager;
    [SerializeField] private GameObject selectedPanel;
    private void Awake()
    {
        intoSameType = false;
        myPlayerInput = GetComponent<PlayerInput>();
        rightClick = myPlayerInput.actions["RightClick"];
        parentafterdrag = this.gameObject.transform.parent;
        initialposition = new Vector2(0, 0);
    }
    private void Start()
    {
        inventoryManager = inventoryCanvas.GetComponent<InventoryManager>();
    }
    public UnityEngine.UI.Image GetImage()
    {
        return image;
    }
    public bool isDraggable()
    {
        return mySlot.name != "";
    }
    public ItemSlotScript getSlot()
    {
        return mySlot;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!isDraggable())
        {
            return;
        }
        image.raycastTarget = false;
        Debug.Log("settingdragroot");
        this.transform.SetParent(/*transform.root*/GameObject.Find("DragCanvas").transform);
        Debug.Log("dragroot: " + this.transform.parent);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable())
        {
            return;
        }
        this.gameObject.transform.position = Mouse.current.position.ReadValue();
        mySlot.SetQuantText("00");

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable())
        {
            return;
        }
        mySlot.SetDraggedInto(false);
        Debug.Log("slot" + mySlot.GetMyIndex() + ": dragged into: " + mySlot.GetDraggedInto());
        image.raycastTarget=true;
        this.transform.SetParent(parentafterdrag);
        this.transform.localPosition = initialposition;
        Debug.Log("Is dragged into?: " + inventoryManager.checkDraggedInto());
        //In the next if statement, I simply check to see if OnDrop was called with a boolean variable
        if (inventoryManager.GetWasDropped()==false)
        {
            mySlot.SetSprite(this.image.sprite);
            mySlot.SetMyQuant(mySlot.GetMyQuant());
            mySlot.SetQuantText(mySlot.GetMyQuant().ToString());
        }
        else
        {
          //I am letting it be handled by the OnDrop() function in ItemSlotScript
        }
        this.transform.SetAsFirstSibling();
        selectedPanel.transform.SetAsFirstSibling();
        Debug.Log("inventory manager check draggedinto: " + inventoryManager.checkDraggedInto());
        inventoryManager.SetWasDropped(false);

    }

}
