using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionsCraftingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] herbsPanels;
    private Image[] images;
    public ScrObj[] possibleHerbs;
    public static event Action<HashSet<ScrObj>> CheckHerbsOnPlayer;

    private void OnEnable()
    {
        CheckHerbsOnPlayer += AdjustHerbDisplay;
    }

    private void Start()
    {
        images = new Image[herbsPanels.Length];
        for (int i = 0; i<herbsPanels.Length; i++)
        {
            images[i] = herbsPanels[i].GetComponent<CraftingHerbPanel>().GetImage();
        }
    }
    public static void InvokeCheckHerbsOnPlayerAction(HashSet<ScrObj> herbs)
    {
        CheckHerbsOnPlayer.Invoke(herbs);
    }
    private void AdjustHerbDisplay(HashSet<ScrObj> herbs)
    {
        for(int i = 0; i<possibleHerbs.Length; i++) 
        {
            if (herbs.Contains(possibleHerbs[i]))
            {
                AddToHerbImageDisplay(possibleHerbs[i]);
            }
        }
    }
    private void AddToHerbImageDisplay(ScrObj herb)
    {
        Debug.Log("addingtoherbimagedisplay");
        Debug.Log("addedherbinfo: " + herb.name);
        bool alreadyThere = false;
        for(int i = 0; i<images.Length; i++) 
        {
            if (images[i].sprite!=null)
            {
                if (images[i].sprite==herb.sprite)
                {
                    alreadyThere = true;
                }
            }
            else if (images[i].sprite==null && alreadyThere==false)
            {
                images[i].sprite = herb.sprite;
                alreadyThere = true;
            }
            if(alreadyThere)
            {
                return;
            }
        }
        
    }



}
