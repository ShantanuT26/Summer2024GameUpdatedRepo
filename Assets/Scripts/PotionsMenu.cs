using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject potionsMenu;
    private void Start()
    {
        potionsMenu.SetActive(false);
    }
    private void OnEnable()
    {
        ItemMenuManager.OpenPotionsMenu += OpenPotionsMenu;
    }
    private void OpenPotionsMenu()
    {
        potionsMenu.SetActive(true);
    }
}
