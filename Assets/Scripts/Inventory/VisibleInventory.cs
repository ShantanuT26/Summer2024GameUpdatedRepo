using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisibleInventory : MonoBehaviour
{
    [SerializeField]private VisPanel[] visPanels;
    [SerializeField] private Sprite itembackground;
    private void Awake()
    {
        for(int x = 0; x < visPanels.Length; x++) 
        {
            visPanels[x].setSprite(itembackground);
            visPanels[x].SetIndex(x);
        }
    }
    public VisPanel[] GetVisPanels()
    {
        return visPanels;
    }
    public void FillVisPanel(int i, int quant, Sprite sprite)
    {
        Debug.Log("vispanelfilledinvisinv");
        visPanels[i].setQuantity(quant);
        visPanels[i].setSprite(sprite);
    }
    public void DeselectAllPanels()
    {
        for(int i = 0; i< visPanels.Length; i++) 
        {
            visPanels[i].SetSelectedPanel(false);
        }
    }
}
