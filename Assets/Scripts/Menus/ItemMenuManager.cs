using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject itemMenu;
    public static event Action OpenHerbsMenu;
    public static event Action OpenPotionsMenu;

    [SerializeField] private CanvasVisibilityManager canvasVisibilityManager;
    private void OnEnable()
    {
        OpenHerbsMenu += CloseItemMenu;
        OpenPotionsMenu += CloseItemMenu;
        InventoryManager.BackToMainMenu += OpenItemMenu;
        PotionsMenu.BackToMainMenu += OpenItemMenu;
        PotionsCraftingMenu.ToMainMenu += OpenItemMenu;
    }
    private void Start()
    {
        itemMenu.SetActive(false);
    }
    public void OpenItemMenu(InputAction.CallbackContext context)
    {
        UnityEngine.Debug.Log("Opening Item Menu");
        canvasVisibilityManager.setItemMenuCanvas(true);
        itemMenu.SetActive(true);
    }
    public void OpenItemMenu()
    {
        itemMenu.SetActive(true);
    }
    public void CloseItemMenu()
    {
        UnityEngine.Debug.Log("closingitemmenu");
        canvasVisibilityManager.setItemMenuCanvas(false);
        itemMenu.SetActive(false);
    }
    public void OnHerbsMenuClickFinished()
    {
        UnityEngine.Debug.Log("openHerbsMenuInvoked");
        OpenHerbsMenu.Invoke();
    }
    public void OnPotionsMenuButtonClick()
    {
        OpenPotionsMenu.Invoke();
    }

}
