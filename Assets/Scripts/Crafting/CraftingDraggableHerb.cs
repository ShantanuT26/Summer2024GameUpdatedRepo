using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CraftingDraggableHerb : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private GameObject dragCanvas;
    [SerializeField] private GameObject origParent;
    private Vector2 origPosition;
    private int siblingIndex;
    public ScrObj itemInfo { get; private set; }
    private void Awake()
    {
        origPosition = (Vector2)transform.localPosition;
        Debug.Log("origposition: " + origPosition);
    }
    public void SetItemInfo(ScrObj x)
    {
        itemInfo = x;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        siblingIndex = transform.GetSiblingIndex();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Mouse.current.position.ReadValue();
        this.GetComponent<Image>().raycastTarget = false;
        this.transform.SetParent(dragCanvas.transform);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<Image>().raycastTarget = true;
        this.transform.SetParent(origParent.transform);
        this.transform.SetSiblingIndex(siblingIndex);
        this.transform.localPosition = new Vector2(0f, 0f);
        PotionsCraftingManager.InvokeAdjustHerbsDisplayInCraftingMenuAction(itemInfo, -1);
    }
}
