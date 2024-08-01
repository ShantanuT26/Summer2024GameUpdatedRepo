using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingRecipePanel : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image myImage;
    public ScrObj itemInfo {get; private set;}
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        CraftingDraggableHerb droppedHerb = dropped.GetComponent<CraftingDraggableHerb>();
        Image herbImage = droppedHerb.GetComponent<Image>();
        myImage.sprite = herbImage.sprite;
        itemInfo = droppedHerb.itemInfo;
        //Debug.Log("itemdropped: " + eventData.)
        Debug.Log("recipeslotinfo: " + itemInfo.name);
    }
}
