using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PotionsCraftingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] herbsPanels;
    [SerializeField] private Sprite background;
    [SerializeField] private PotionScrObj[] possiblePotions;
    [SerializeField] private CraftingRecipePanel[] attemptedRecipe;
    [SerializeField] private CraftingResultPanel craftingResultPanel;

    public ScrObj[] possibleHerbs;
    public static event Action<HashSet<ScrObj>, int> CheckHerbsOnPlayer;
    public static event Action<ScrObj> SetHerbContentsInfo;

    private void OnEnable()
    {
        CheckHerbsOnPlayer += AdjustHerbDisplay;
    }
    public static void InvokeCheckHerbsOnPlayerAction(HashSet<ScrObj> herbs, int x)
    {
        CheckHerbsOnPlayer.Invoke(herbs, x);
    }
    private void AdjustHerbDisplay(HashSet<ScrObj> herbs, int herbQuantity)
    {
        for(int i = 0; i<possibleHerbs.Length; i++) 
        {
            if (herbs.Contains(possibleHerbs[i]))
            {
                AddToHerbImageDisplay(possibleHerbs[i], herbQuantity);
            }
        }
    }
    private void AddToHerbImageDisplay(ScrObj herb, int herbQuant)
    {
        Debug.Log("addingtoherbimagedisplay");
        Debug.Log("addedherbinfo: " + herb.name);
        bool alreadyThere = false;
        for(int i = 0; i<herbsPanels.Length; i++) 
        {
            if (herbsPanels[i].GetComponent<CraftingHerbPanel>().GetSprite()!=background)
            {
                if (herbsPanels[i].GetComponent<CraftingHerbPanel>().GetSprite() == herb.sprite)
                {
                    alreadyThere = true;
                }
            }
            else if (herbsPanels[i].GetComponent<CraftingHerbPanel>().GetSprite() == background && alreadyThere==false)
            {
                //herbsPanels[i].GetComponent<CraftingHerbPanel>().SetSlotContentsInfo(herb);
                //this next line sets the iteminfo for every herb panel, not just one
                //SetHerbContentsInfo.Invoke(herb);
                herbsPanels[i].GetComponent<CraftingHerbPanel>().SetSlotContentsInfo(herb);
                herbsPanels[i].GetComponent<CraftingHerbPanel>().SetSprite(herb.sprite);
                herbsPanels[i].GetComponent<CraftingHerbPanel>().SetQuantity(herbQuant);
                alreadyThere = true;
            }
            if(alreadyThere)
            {
                return;
            }
        }
    }
    public void CreatePotion()
    {
        ScrObj[] recipe = new ScrObj[4];
        for(int i = 0; i<attemptedRecipe.Length; i++)
        {
            recipe[i] = attemptedRecipe[i].GetComponent<CraftingRecipePanel>().itemInfo;
            Debug.Log("attemptedrecipe: " + recipe[i]);
        }
        for(int i = 0; i<possiblePotions.Length; i++)
        {
            Debug.Log("potionrecipe: " + possiblePotions[i].ingredients[0]
                + ", " + possiblePotions[i].ingredients[1]
                + ", " + possiblePotions[i].ingredients[2]
                + ", " + possiblePotions[i].ingredients[3]);
            if (Enumerable.SequenceEqual(recipe,possiblePotions[i].ingredients))
            {
                Debug.Log("successfulattempt");
                craftingResultPanel.SetSprite(possiblePotions[i].sprite);
            }
        }
        //Debug.Log("attemptedrecipe: " + recipe[0] + ", " + recipe[1]);
    }
}
