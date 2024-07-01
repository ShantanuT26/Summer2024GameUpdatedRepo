using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ChestInvUIHandlerScript : MonoBehaviour
{
    private GraphicRaycaster myRaycaster;
    private PointerEventData clickData;
    private List<RaycastResult> clickResults;
    private ChestInventoryManager CInventoryManager;

    void Start()
    {
        CInventoryManager = GameObject.Find("ChestInventoryCanvas").GetComponent<ChestInventoryManager>();
        myRaycaster = GameObject.Find("ChestInventoryCanvas").GetComponent<GraphicRaycaster>();
        clickData = new PointerEventData(EventSystem.current);
        clickResults = new List<RaycastResult>();
      
    }
    void Update()
    {
        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            GetUIElementsClicked();
        }
    }
    private void GetUIElementsClicked()
    {
        clickData.position = Mouse.current.position.ReadValue();
        clickResults.Clear();
        myRaycaster.Raycast(clickData, clickResults);

        foreach(RaycastResult result in clickResults)
        {
            GameObject UIElement = result.gameObject;
            UnityEngine.UI.Button button = UIElement.GetComponent<UnityEngine.UI.Button>();
            if(button != null)
            {
                CInventoryManager.Deactivate();
                Debug.Log("NAME: " + UIElement.name);

            }
            
        }
    }
}
