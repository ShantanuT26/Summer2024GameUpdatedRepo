using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVisibilityManager : MonoBehaviour
{
    [SerializeField] private Canvas fadeCanvas;
    [SerializeField] private Canvas saveLoadCanvas;
    [SerializeField] private Canvas potionsCraftingCanvas;
    [SerializeField] private Canvas potionsViewCanvas;
    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private Canvas itemMenuCanvas;
    [SerializeField] private Canvas dragCanvas;
    [SerializeField] private Canvas chestInventoryCanvas;
    [SerializeField] private Canvas statsCanvas;

    private CanvasGroup fadeCanvasGroup;
    private CanvasGroup saveLoadCanvasGroup;
    private CanvasGroup potionsCraftingCanvasGroup;
    private CanvasGroup potionsViewCanvasGroup;
    private CanvasGroup inventoryCanvasGroup;
    private CanvasGroup itemMenuCanvasGroup;
    private CanvasGroup dragCanvasGroup;
    private CanvasGroup chestInventoryCanvasGroup;
    private CanvasGroup statsCanvasGroup;

    void Start()
    {
        fadeCanvasGroup = fadeCanvas.GetComponent<CanvasGroup>();
        fadeCanvasGroup.interactable = false;
        fadeCanvasGroup.blocksRaycasts = false;

        saveLoadCanvasGroup = saveLoadCanvas.GetComponent<CanvasGroup>();
        saveLoadCanvasGroup.interactable = false;
        saveLoadCanvasGroup.blocksRaycasts = false;
        
        potionsCraftingCanvasGroup = potionsCraftingCanvas.GetComponent<CanvasGroup>();
        potionsCraftingCanvasGroup.interactable = false;
        potionsCraftingCanvasGroup.blocksRaycasts = false;

        potionsViewCanvasGroup = potionsViewCanvas.GetComponent<CanvasGroup>();
        potionsViewCanvasGroup.interactable = false;
        potionsViewCanvasGroup.blocksRaycasts = false;

        inventoryCanvasGroup = inventoryCanvas.GetComponent<CanvasGroup>();
        inventoryCanvasGroup.interactable = false;
        inventoryCanvasGroup.blocksRaycasts = false;

        itemMenuCanvasGroup = itemMenuCanvas.GetComponent<CanvasGroup>();
        itemMenuCanvasGroup.interactable = false;
        itemMenuCanvasGroup.blocksRaycasts = false;

        dragCanvasGroup = dragCanvas.GetComponent<CanvasGroup>();
        dragCanvasGroup.interactable = false;
        dragCanvasGroup.blocksRaycasts = false;

        chestInventoryCanvasGroup = chestInventoryCanvas.GetComponent<CanvasGroup>();
        chestInventoryCanvasGroup.interactable = false;
        chestInventoryCanvasGroup.blocksRaycasts = false;

        statsCanvasGroup = statsCanvas.GetComponent<CanvasGroup>();
        statsCanvasGroup.interactable = false;
        statsCanvasGroup.blocksRaycasts = false;
    }

    public void setInventoryCanvas(bool x)
    {
        if (x == true)
        {
            inventoryCanvasGroup.interactable = true;
            inventoryCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            inventoryCanvasGroup.interactable = false;
            inventoryCanvasGroup.blocksRaycasts = false;
        }
    }

    public void setItemMenuCanvas(bool x)
    {
        if (x == true)
        {
            itemMenuCanvasGroup.interactable = true;
            itemMenuCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            itemMenuCanvasGroup.interactable = false;
            itemMenuCanvasGroup.blocksRaycasts = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
