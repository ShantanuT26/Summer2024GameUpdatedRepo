using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject potionsMenu;
    public static event Action BackToGame;
    public static event Action BackToMainMenu;
    public static event Action ToPotionsCraftingMenu;
    private void Start()
    {
        potionsMenu.SetActive(false);
    }
    private void OnEnable()
    {
        ItemMenuManager.OpenPotionsMenu += OpenPotionsMenu;
        BackToGame += ClosePotionsMenu;
        BackToMainMenu += ClosePotionsMenu;
        ToPotionsCraftingMenu += ClosePotionsMenu;
        PotionsCraftingMenu.ToPotionsViewMenu += OpenPotionsMenu;
    }
    private void OpenPotionsMenu()
    {
        Debug.Log("openingpotionsmenu");
        potionsMenu.SetActive(true);
    }
    public void ToGameButtonClicked()
    {
        BackToGame.Invoke();
    }
    public void ToMainMenuButtonClicked()
    {
        BackToMainMenu.Invoke();
    }
    public void CraftingButtonClicked()
    {
        ToPotionsCraftingMenu.Invoke();
    }
    private void ClosePotionsMenu()
    {
        potionsMenu.SetActive(false);
    }
    


}
