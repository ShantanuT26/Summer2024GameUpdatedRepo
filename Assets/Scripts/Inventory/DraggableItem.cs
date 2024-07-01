using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    private InventoryManager inventoryManager;
    [SerializeField] private GameObject selectedPanel;
    private void Awake()
    {
        intoSameType = false;
        myPlayerInput = GetComponent<PlayerInput>();
        rightClick = myPlayerInput.actions["RightClick"];
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        parentafterdrag = this.gameObject.transform.parent;
        // initialposition = this.transform.position;
        initialposition = new Vector2(0, 0);
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
        this.transform.SetParent(transform.root);
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
        //inventoryManager.currDragCount = inventoryManager.checkDraggedInto();
        Debug.Log("slot" + mySlot.GetMyIndex() + ": dragged into: " + mySlot.GetDraggedInto());
        image.raycastTarget=true;
        this.transform.SetParent(parentafterdrag);
        this.transform.localPosition = initialposition;
        Debug.Log("Is dragged into?: " + inventoryManager.checkDraggedInto());
        //In the next if statement, I simply check to see if OnDrop was called with a boolean variable
        if (inventoryManager.GetWasDropped()==false/*inventoryManager.currDragCount<inventoryManager.prevDragCount || inventoryManager.currDragCount==0*/  /* && !intoSameType*/)
        {
            //mySlot.SetDraggedInto(true);
            mySlot.SetSprite(this.image.sprite);
            mySlot.SetMyQuant(mySlot.GetMyQuant());
            mySlot.SetQuantText(mySlot.GetMyQuant().ToString());
        }
        else
        {
          //I am letting it be handled by the OnDrop() function in ItemSlotScript
        }
       // inventoryManager.prevDragCount = inventoryManager.currDragCount;
        this.transform.SetAsFirstSibling();
        selectedPanel.transform.SetAsFirstSibling();
        Debug.Log("inventory manager check draggedinto: " + inventoryManager.checkDraggedInto());
        // inventoryManager.prevDragCount = inventoryManager.currDragCount;
        inventoryManager.SetWasDropped(false);
        // mySlot.ClearSlot();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
