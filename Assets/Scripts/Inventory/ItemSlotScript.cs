using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ItemSlotScript : MonoBehaviour, IPointerDownHandler,/* IBeginDragHandler, IDragHandler, IEndDragHandler,*/ IDropHandler
{
    
    private bool draggedinto;
    private int myindex;
    [SerializeField]private int myquant;
    private string myname="";
    [SerializeField]private Image myImage;
    [SerializeField]private TMP_Text quant;
    public GameObject selecthighlight;
    private InventoryManager inventoryManager;
    private ManaManager manaManager;
    private HealthManager healthManager;
    private Transform parentAfterDrag;
    [SerializeField] private Sprite itembackground;
    [SerializeField]private Vector2 imgstartposition;

    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        healthManager = GameObject.Find("MyHealth").GetComponent<HealthManager>();
        manaManager=GameObject.Find("MyMana").GetComponent<ManaManager>();
        myquant = 0;
        imgstartposition = myImage.transform.position;
    }
    public void SetMyQuant(int x)
    {
        myquant = x;
    }
    public int GetMyQuant()
    {
        return myquant;
    }
    public void SetHighlight(bool x)
    {
        selecthighlight.SetActive(x);
    }
    public void SetQuantText(string x)
    {
        quant.text = x;
    }
    public string GetName()
    {
        return myname;
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
    public void SetSprite(Sprite x)
    {
        Debug.Log("itemslotspriteset");
        myImage.sprite = x;
    }
    public void ClearSlot()
    {
        Debug.Log("settingempty2");
        quant.text = "00";
        myquant = 0;
        myname = "";
        myImage.sprite = itembackground;
    }
    public void FillSlot(string name, int quantity, Sprite sprite)
    {
        Debug.Log("q: " + quantity);
        myname = name;
        myImage.sprite = sprite;
        SetQuantity(quantity);
    }
    public void OnPointerDown(PointerEventData eventdata)
    {
        inventoryManager.DeselectAllSlots();
        SetHighlight(true);
        changeStats();
    }
    public void changeStats()
    {
        if (myquant>0)
        {
            AdjustQuantity(-1);
            for(int i = 0; i<this.inventoryManager.GetScrObjArray().Length; i++)
            {
                if (inventoryManager.GetScrObj(i).name == myname)
                {
                    manaManager.updateMana(inventoryManager.GetScrObj(i).getMana());
                    healthManager.updateHealth(inventoryManager.GetScrObj(i).getHealing());
                }
            }
        }
        if(CheckIsEmpty())
        {
            Debug.Log("settingempty1");
            //make a (or implement an already existing) clearslot method
            myname = "";
            myImage.sprite = itembackground;
            quant.text = "00";
        }
    }
    public void AdjustQuantity(int x)
    {
        Debug.Log("quantityadjusted");
        myquant += x;
        quant.text = myquant.ToString();
    }
    public void SetQuantity(int x)
    {
        Debug.Log("quantityset");
        myquant = x;
        quant.text = myquant.ToString();
    }
    public bool CheckIsEmpty()
    {
        if(myquant==0)
        {
            return true;
        }
        return false;
    }
    public void OnDrop(PointerEventData eventData)
    {
        inventoryManager.SetWasDropped(true);
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableitem = dropped.GetComponent<DraggableItem>();
        myImage.sprite = dropped.GetComponent<Image>().sprite;
        if (this == draggableitem.getSlot())
        {
            myquant = draggableitem.getSlot().myquant;
            quant.text = myquant.ToString();
            myname = draggableitem.getSlot().myname;
        }
        else if (myname=="")
        { 
            myquant = draggableitem.getSlot().myquant;
            quant.text = myquant.ToString();
            myname = draggableitem.getSlot().myname;
            draggableitem.getSlot().ClearSlot();
        }
       else if(myname==draggableitem.getSlot().myname)
        {
            draggableitem.intoSameType = true;
            if(this.myquant+draggableitem.getSlot().myquant<=64)
            {
                myquant += draggableitem.getSlot().myquant;
                quant.text = myquant.ToString();
                draggableitem.getSlot().ClearSlot();
            }
            else
            {
                int tempquant1 = myquant;
                myquant = 64;
                quant.text = myquant.ToString();
                int tempquant2 = tempquant1 + draggableitem.getSlot().myquant - 64;
                draggableitem.getSlot().ClearSlot();
                inventoryManager.addItem(myname, tempquant2, myImage.sprite);

            }
        }
        else if(myname != draggableitem.getSlot().myname)
        {
            draggableitem.getSlot().myquant = draggableitem.getSlot().myquant;
            draggableitem.getSlot().quant.text = draggableitem.getSlot().myquant.ToString();
            draggableitem.getSlot().myname = draggableitem.getSlot().myname;
            draggableitem.getSlot().myImage.sprite = draggableitem.getSlot().myImage.sprite;
            myquant = myquant;
            quant.text = myquant.ToString();
            myname = myname;
            myImage.sprite = myImage.sprite;
        }
        draggedinto = true;
        
        //KEEP NEXT LINE!!
        //inventoryManager.prevDragCount = inventoryManager.checkDraggedInto();
        //  TempImage.sprite = dropped.GetComponent<Image>().sprite;
        //eventData.pointerDrag.transform.position = transform.position;
    }
}
