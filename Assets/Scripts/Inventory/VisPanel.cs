using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisPanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TMP_Text quantity;
    [SerializeField] private Image myImage;
    [SerializeField] private GameObject selectedPanel;
    private int index;
    private VisibleInventory visInventory;
    private InventoryManager inventoryManager;
    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

        visInventory = GameObject.Find("VisInvCanvas").GetComponent<VisibleInventory>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        visInventory.DeselectAllPanels();
        selectedPanel.SetActive(true);
        inventoryManager.GetItemSlot(index).changeStats();
        //inventoryManager.changeStats(index);
        inventoryManager.ToVisInv();
    }
    public void SetIndex(int i)
    {
        index = i;
    }
    public void SetSelectedPanel(bool x)
    {
        if(x==true)
        {
            selectedPanel.SetActive(true);
        }
        else
        {
            selectedPanel.SetActive(false);
        }
    }
    // Start is called before the first frame update
    public void setQuantity(int x)
    {
        quantity.text=x.ToString();
    }
    public void setSprite(Sprite s)
    {
        myImage.sprite = s;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("updating");
        //inventoryManager.ToVisInv();
    }
}
