using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChestItemSlotScript : MonoBehaviour, IPointerDownHandler,/* IBeginDragHandler, IDragHandler, IEndDragHandler,*/ IDropHandler
{
    private bool draggedinto;
    private int myindex;
    private int chestQuant;
    private string myname = "";
    [SerializeField]private Image myImage;
    [SerializeField]private TMP_Text quant;
    [SerializeField]private GameObject selecthighlight;
    private InventoryManager inventoryManager;
    private ManaManager manaManager;
    private HealthManager healthManager;
    private Transform parentAfterDrag;
    [SerializeField] private Sprite itembackground;
    [SerializeField] private Vector2 imgstartposition;

    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        healthManager = GameObject.Find("MyHealth").GetComponent<HealthManager>();
        manaManager = GameObject.Find("MyMana").GetComponent<ManaManager>();
        chestQuant = 0;
        imgstartposition = myImage.transform.position;
    }
    public GameObject GetHighlight()
    {
        return selecthighlight;
    }
    public void SetQuantText(string x)
    {
        quant.text = x;
    }
    public string GetName()
    {
        return myname;
    }
    public int GetChestQuant()
    {
        return chestQuant;
    }
    public void SetChestQuant(int x)
    {
        chestQuant = x;
    }
    public bool GetDraggedInto()
    {
        return draggedinto;
    }
    public void SetDraggedInto(bool x)
    {
        draggedinto = x;
    }
    public void setMyIndex(int x)
    {
        myindex = x;
    }
    public int GetMyIndex()
    {
        return myindex;
    }
    public Sprite getSprite()
    {
        return myImage.sprite;
    }
    public void setSprite(Sprite x)
    {
        myImage.sprite = x;
    }
    public void ClearSlot()
    {
        quant.text = "00";
        chestQuant = 0;
        myname = "";
        myImage.sprite = itembackground;
    }
    public void FillSlot(string name, int quantity, Sprite sprite)
    {
        chestQuant = quantity;
        Debug.Log(myImage.name);
        myname = name;
        myImage.sprite = sprite;
        quant.text = quantity.ToString();
    }
    public void OnPointerDown(PointerEventData eventdata)
    {
        inventoryManager.DeselectAllSlots();
        selecthighlight.SetActive(true);
    }
    void Update()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        /*
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableitem = dropped.GetComponent<DraggableItem>();
        myImage.sprite = dropped.GetComponent<Image>().sprite;
        if (myname == "")
        {
            chestQuant = draggableitem.getSlot().myquant;
            quant.text = chestQuant.ToString();
            myname = draggableitem.getSlot().myname;
            draggableitem.getSlot().ClearSlot();
        }
        draggedinto = true;*/
    }
}
