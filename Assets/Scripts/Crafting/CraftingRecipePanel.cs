using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingRecipePanel : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image myImage;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        CraftingDraggableHerb droppedHerb = dropped.GetComponent<CraftingDraggableHerb>();
        Image herbImage = droppedHerb.GetComponent<Image>();
        myImage.sprite = herbImage.sprite;
        //Debug.Log("itemdropped: " + eventData.)
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
