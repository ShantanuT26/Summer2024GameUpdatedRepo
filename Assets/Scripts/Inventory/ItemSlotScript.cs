using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Xml.Linq;

public class ItemSlotScript : MonoBehaviour, IPointerDownHandler,/* IBeginDragHandler, IDragHandler, IEndDragHandler,*/ IDropHandler
{
    
    private bool draggedinto;
    private int myindex;
    public int myquant;
    [SerializeField]private Image myImage;
    [SerializeField]private TMP_Text quant;
    public GameObject selecthighlight;
    [SerializeField]private InventoryManager inventoryManager;
    [SerializeField] private ManaManager manaManager;
    [SerializeField] private HealthManager healthManager;
    private Transform parentAfterDrag;
    [SerializeField] private Sprite itembackground;
    [SerializeField]private Vector2 imgstartposition;
    public ScrObj slotInfo { get; private set; }

    private void Awake()
    {
        /*inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        healthManager = GameObject.Find("MyHealth").GetComponent<HealthManager>();
        manaManager=GameObject.Find("MyMana").GetComponent<ManaManager>();*/
        myquant = 0;
        imgstartposition = myImage.transform.position;
        slotInfo = new ScrObj();
        slotInfo.Empty();
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
        return slotInfo.name;
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
        return slotInfo.sprite;
    }
    public void SetSprite(Sprite x)
    {
        Debug.Log("itemslotspriteset");
        slotInfo.sprite = x;
    }
    public void ClearSlot()
    {
        Debug.Log("settingempty2");
        quant.text = "00";
        myquant = 0;
        slotInfo.name = "";
        slotInfo.sprite = itembackground;
        UpdateSlotAppearance();
    }
    private void FillSlotInfo(ScrObj x)
    {
        slotInfo.healing = x.healing;
        slotInfo.sprite = x.sprite;
        slotInfo.mana = x.mana;
        slotInfo.name = x.name;
    }
    public void FillSlot(ScrObj itemInfo, int quantity)
    {
        //slotInfo.name = itemInfo.name;
        //slotInfo.sprite = itemInfo.sprite;
        FillSlotInfo(itemInfo);
        SetQuantity(quantity);
        UpdateSlotAppearance();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //PotionsCraftingManager.InvokeCheckHerbsOnPlayerAction();
        Debug.Log("pointerdownonitemslot");
        inventoryManager.DeselectAllSlots();
        SetHighlight(true);
        changeStats();
    }
    public void changeStats()
    {
        if (myquant>0)
        {
            AdjustQuantity(-1);
            manaManager.updateMana(slotInfo.mana);
            healthManager.updateHealth(slotInfo.healing);
        }
        if(CheckIsEmpty())
        {
            Debug.Log("settingempty1");
            //make a (or implement an already existing) clearslot method
            slotInfo.name = "";
            slotInfo.sprite = itembackground;
            quant.text = "00";
        }
        UpdateSlotAppearance();
    }
    private void UpdateSlotAppearance()
    {
        myImage.sprite = slotInfo.sprite;
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
    private void KeepSlotSame(DraggableItem draggableitem)
    {
        myquant = draggableitem.getSlot().myquant;
        quant.text = myquant.ToString();
        slotInfo.name = draggableitem.getSlot().slotInfo.name;
        slotInfo.sprite = draggableitem.getSlot().slotInfo.sprite;
    }
    private void HandleDropIntoEmptySlot(DraggableItem draggableitem)
    {
        myquant = draggableitem.getSlot().myquant;
        quant.text = myquant.ToString();
        slotInfo.name = draggableitem.getSlot().slotInfo.name;
        draggableitem.getSlot().ClearSlot();
    }
    private void HandleNonOverflowDropIntoPartiallyFilledSlot(DraggableItem draggableitem)
    {
        myquant += draggableitem.getSlot().myquant;
        quant.text = myquant.ToString();
        draggableitem.getSlot().ClearSlot();
    }
    private void HandleOverflowDropIntoPartiallyFilledSlot(DraggableItem draggableitem)
    {
        int tempquant1 = myquant;
        myquant = 64;
        quant.text = myquant.ToString();
        int tempquant2 = tempquant1 + draggableitem.getSlot().myquant - 64;
        draggableitem.getSlot().ClearSlot();
        slotInfo.sprite=slotInfo.sprite;
        inventoryManager.addItem(slotInfo, tempquant2);
    }
    public void OnDrop(PointerEventData eventData)
    {
        inventoryManager.SetWasDropped(true);
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableitem = dropped.GetComponent<DraggableItem>();
        slotInfo.sprite = dropped.GetComponent<Image>().sprite;

        if (this == draggableitem.getSlot())
        {
            KeepSlotSame(draggableitem);
        }
        else if (slotInfo.name=="")
        {
            HandleDropIntoEmptySlot(draggableitem);
        }
       else if(slotInfo.name==draggableitem.getSlot().slotInfo.name)
        {
            draggableitem.intoSameType = true;
            if(this.myquant+draggableitem.getSlot().myquant<=64)
            {
                HandleNonOverflowDropIntoPartiallyFilledSlot(draggableitem);
            }
            else
            {
                HandleOverflowDropIntoPartiallyFilledSlot(draggableitem);

            }
        }
        else if(slotInfo.name != draggableitem.getSlot().slotInfo.name)
        {
            draggableitem.getSlot().myquant = draggableitem.getSlot().myquant;
            draggableitem.getSlot().quant.text = draggableitem.getSlot().myquant.ToString();
            draggableitem.getSlot().slotInfo.name = draggableitem.getSlot().slotInfo.name;
            draggableitem.getSlot().slotInfo.sprite = draggableitem.getSlot().slotInfo.sprite;
            KeepSlotSame(draggableitem);
        }
        
        draggedinto = true;
        UpdateSlotAppearance();
        
        //KEEP NEXT LINE!!
        //inventoryManager.prevDragCount = inventoryManager.checkDraggedInto();
        //  TempImage.sprite = dropped.GetComponent<Image>().sprite;
        //eventData.pointerDrag.transform.position = transform.position;
    }
}
