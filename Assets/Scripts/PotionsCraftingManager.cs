using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionsCraftingManager : MonoBehaviour
{
    [SerializeField] private Image[] images;
    public ScrObj[] possibleHerbs;
    public static event Action CheckHerbsOnPlayer;

    private void OnEnable()
    {
        CheckHerbsOnPlayer += AdjustHerbDisplay;
    }

    public static void InvokeCheckHerbsOnPlayerAction()
    {
        CheckHerbsOnPlayer.Invoke();
    }
    private void AdjustHerbDisplay()
    {

    }



}
