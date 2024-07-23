using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject itemMenu;
    public static event Action OpenHerbsMenu;
    public static event Action OpenPotionsMenu;
    private void OnEnable()
    {
        OpenHerbsMenu += CloseItemMenu;
        OpenPotionsMenu += CloseItemMenu;
        InventoryManager.BackToMainMenu += OpenItemMenu;
    }
    private void Start()
    {
        itemMenu.SetActive(false);
    }
    public void OpenItemMenu(InputAction.CallbackContext context)
    {
        itemMenu.SetActive(true);
    }
    public void OpenItemMenu()
    {
        itemMenu.SetActive(true);
    }
    public void CloseItemMenu()
    {
        itemMenu.SetActive(false);
    }
    public void OnHerbsMenuClickFinished()   
    {
        OpenHerbsMenu.Invoke();
    }
    public void OnPotionsMenuButtonClick()
    {
        OpenPotionsMenu.Invoke();
    }

}
