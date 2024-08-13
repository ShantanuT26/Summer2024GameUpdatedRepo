using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PotionSlotScript : MonoBehaviour
{
       [SerializeField] private TMP_Text quantityText;
    public PotionScrObj slotInfo { get; private set; }
    public UnityEngine.UI.Image myImage;
    public Sprite background;
    //public GlobalVariables globalVariables;
    
    public int quantity { get; private set;}
    private void Awake()
    {
        slotInfo = new PotionScrObj();
    }
    private void Start()
    {
        Debug.Log("potionSlotImage: " + myImage);
        SetDefaultValues();
    }
    public void SetDefaultValues()
    {
        quantity = 0;
            
        slotInfo.sprite = background;
        slotInfo.name = "";
    }
    public void SetSlotInfo(PotionScrObj info)
    {
        slotInfo.name = info.name;
        slotInfo.healing = info.healing;
        slotInfo.mana = info.mana;
        slotInfo.ingredients = info.ingredients;
        slotInfo.sprite= info.sprite;
        UpdateAppearance();
    }
    public void UpdateAppearance()
    {
        myImage.sprite = slotInfo.sprite;
    }
    public void SetQuantity(int x)
    {
        quantity = x;
        quantityText.text = quantity.ToString();
    }
    public void AdjustQuantity(int x)
    {
        quantity += x;
        quantityText.text = quantity.ToString();
    }
}
