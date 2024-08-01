using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CraftingHerbPanel : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image herbImage;
    [SerializeField] private TMP_Text displayedQuantity;
    [SerializeField] private Sprite background;
    private DefaultCraftingHerbPanelValues defaultValues;
    private int quantity;
    public ScrObj slotContentsInfo { get; private set; }
    private void Awake()
    {
        defaultValues.defaultDisplayedQuant = "";
        defaultValues.defaultQuant = 0;
        defaultValues.defaultSprite = background;
    }
    void Start()
    {
        displayedQuantity.text = defaultValues.defaultDisplayedQuant;
        SetSprite(background);
    }
    private void OnEnable()
    {
        PotionsCraftingManager.SetHerbContentsInfo += SetSlotContentsInfo;
    }
    public void SetSprite(Sprite sprite)
    {
        herbImage.sprite = sprite;
    }
    public TMP_Text GetDiplayedQuantity()
    {
        return displayedQuantity;
    }
    public Sprite GetSprite()
    {
        return herbImage.sprite;
    }
    public UnityEngine.UI.Image GetImage()
    {
        return herbImage;
    }
    public void SetQuantity(int x)
    {
        quantity = x;
        displayedQuantity.text = x.ToString();
    }
    public void SetSlotContentsInfo(ScrObj x)
    {
        slotContentsInfo = x;
        herbImage.GetComponent<CraftingDraggableHerb>().SetItemInfo(x);
    }
}
[System.Serializable]
public struct DefaultCraftingHerbPanelValues
{
    [field: SerializeField]public Sprite defaultSprite;
    public string defaultDisplayedQuant;
    public int defaultQuant;
}
