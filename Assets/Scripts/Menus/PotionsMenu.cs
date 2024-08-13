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
    [SerializeField] private GameObject[] potionsSlots;
    public GlobalVariables globalVariables;
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
        PotionsCraftingManager.AddPotionToPotionsMenu += AddPotionToSlots;
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
    private void AddPotionToSlots(PotionScrObj potion)
    {
        for (int i = 0; i < 16; i++)
        {
            PotionSlotScript currPotionSlot = potionsSlots[i].GetComponent<PotionSlotScript>();
            if (currPotionSlot.slotInfo.name == potion.name)
            {
                Debug.Log("doingstuff1");
                currPotionSlot.AdjustQuantity(1);
                return;
            }
            else
            {
                if(currPotionSlot.slotInfo.name=="")
                {
                    Debug.Log("doingstuff2");
                    currPotionSlot.SetSlotInfo(potion);
                    currPotionSlot.SetQuantity(1);
                    return;          
                }
            }
        }
    }
    


}
