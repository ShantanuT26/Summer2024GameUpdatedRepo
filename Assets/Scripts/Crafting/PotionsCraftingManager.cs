using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor.TerrainTools;

public class PotionsCraftingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] herbsPanels;
    [SerializeField] private Sprite background;
    [SerializeField] private PotionScrObj[] possiblePotions;
    [SerializeField] private CraftingRecipePanel[] attemptedRecipe;
    [SerializeField] private CraftingResultPanel craftingResultPanel;

    public ScrObj[] possibleHerbs;
    public static event Action<ScrObj, int> CheckHerbsOnPlayer;
    public static event Action<ScrObj, int> AdjustHerbsDisplayInCraftingMenu;
    public static event Action<ScrObj> SetHerbContentsInfo;
    public static event Action<ScrObj, int> AdjustHerbDisplayInHerbsMenu;
    public static event Action<ScrObj[], int> AdjustHerbDisplayInHerbsMenuMultiple;
    public static event Action<PotionScrObj> AddPotionToPotionsMenu;
    bool added = false;
    private void OnEnable()
    {
        CheckHerbsOnPlayer += AddToHerbImageDisplay;
        AdjustHerbsDisplayInCraftingMenu += AdjustHerbImageDisplay;
    }
    public static void InvokeCheckHerbsOnPlayerAction(ScrObj herb, int x)
    {
        CheckHerbsOnPlayer.Invoke(herb, x);
    }
    public static void InvokeAdjustHerbsDisplayInCraftingMenuAction(ScrObj herb, int x)
    {
        AdjustHerbsDisplayInCraftingMenu.Invoke(herb, x);
    }
    private void AddToHerbImageDisplay(ScrObj herb, int herbQuant)
    {
        bool added = false;
        for(int i = 0; i<herbsPanels.Length; i++) 
        {
            Debug.Log("added: " + added);
            if(herbsPanels[i].GetComponent<CraftingHerbPanel>().GetSprite() == herb.sprite)
            {
                Debug.Log("cond1met");
                Debug.Log("currherbsprite: " + herb.sprite);
                herbsPanels[i].GetComponent<CraftingHerbPanel>().AdjustQuantity(herbQuant);
                added = true;
            }
            else if (herbsPanels[i].GetComponent<CraftingHerbPanel>().GetSprite() == background)
            {
                herbsPanels[i].GetComponent<CraftingHerbPanel>().SetSlotContentsInfo(herb);
                herbsPanels[i].GetComponent<CraftingHerbPanel>().SetSprite(herb.sprite);
                Debug.Log("Herbspanelsprite: "+ herbsPanels[i].GetComponent<CraftingHerbPanel>().GetSprite());
                herbsPanels[i].GetComponent<CraftingHerbPanel>().SetQuantity(herbQuant);
                added = true;
                Debug.Log("added: " + added);
            }
            if(added)
            {
                return;
            }
        }
    }
    private void AdjustHerbImageDisplay(ScrObj herb, int x)
    {
        bool added = false;
        for (int i = 0; i < herbsPanels.Length; i++)
        {
            if (herbsPanels[i].GetComponent<CraftingHerbPanel>().GetSprite()== herb.sprite)
            {
                herbsPanels[i].GetComponent<CraftingHerbPanel>().AdjustQuantity(x);
                added = true;
            }
            if (added)
            {
                return;
            }
        }
    }
    public void CreatePotion()
    {
        Debug.Log("creatingpotion");
        ScrObj[] recipe = new ScrObj[4];
        for (int i = 0; i<attemptedRecipe.Length; i++)
        {
            recipe[i] = attemptedRecipe[i].GetComponent<CraftingRecipePanel>().itemInfo;
            
            Debug.Log("attemptedrecipe: " + recipe[i]);
        }
        AdjustHerbDisplayInHerbsMenuMultiple.Invoke(recipe, -1);
        for (int i = 0; i < attemptedRecipe.Length; i++)
        {
            attemptedRecipe[i].ClearPanel();
        }
        for (int i = 0; i<possiblePotions.Length; i++)
        {
            Debug.Log("potionrecipe: " + possiblePotions[i].ingredients[0]
                + ", " + possiblePotions[i].ingredients[1]
                + ", " + possiblePotions[i].ingredients[2]
                + ", " + possiblePotions[i].ingredients[3]);
            if (Enumerable.SequenceEqual(recipe,possiblePotions[i].ingredients))
            {
                Debug.Log("successfulattempt");
                craftingResultPanel.SetSprite(possiblePotions[i].sprite);
                AddPotionToPotionsMenu.Invoke(possiblePotions[i]);
                break;
            }
        }
    }
}
