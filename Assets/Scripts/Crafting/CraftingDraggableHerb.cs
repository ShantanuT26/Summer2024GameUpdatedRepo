using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CraftingDraggableHerb : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject dragCanvas;
    [SerializeField] private GameObject origParent;
    private Vector2 origPosition;
    private void Awake()
    {
        origPosition = (Vector2)transform.localPosition;
        Debug.Log("origposition: " + origPosition);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
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
        this.transform.position = (Vector2)this.transform.parent.position + origPosition;
        ///this.transform.localPosition = origPosition;
        //Debug.Log("herbposition: " + this.transform.localPosition);
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
