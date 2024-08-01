using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsCraftingMenu : MonoBehaviour
{
    [SerializeField] private GameObject craftingMenu;
    public static event Action ToGame;
    public static event Action ToMainMenu;
    public static event Action ToPotionsViewMenu;
    [SerializeField] private CraftingRecipePanel[] craftingRecipePanels;

    private ScrObj[] ingredientsUsed;
    private void OnEnable()
    {
        PotionsMenu.ToPotionsCraftingMenu += OpenPotionsCraftingMenu;
        ToGame += ClosePotionsCraftingMenu;
        ToMainMenu += ClosePotionsCraftingMenu;
        ToPotionsViewMenu += ClosePotionsCraftingMenu;
    }
    private void PotionsCraftingMenu_ToMainMenu()
    {
        throw new NotImplementedException();
    }

    private void UpdateIngredientsArray()
    {

    }
    private void Start()
    {
        craftingMenu.SetActive(false);
    }
    private void OpenPotionsCraftingMenu()
    {
        craftingMenu.SetActive(true);
    }
    private void ClosePotionsCraftingMenu()
    {
        craftingMenu.SetActive(false);
    }
    public void GameButtonPressed()
    {
        ToGame.Invoke();
    }
    public void MainMenuButtonPressed()
    {
        ToMainMenu.Invoke();
    }
    public void PotionsViewButtonPressed()
    {
        ToPotionsViewMenu.Invoke();
    }
}
