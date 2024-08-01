using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingResultPanel : MonoBehaviour
{
    [SerializeField] private Image myImage;

    public void SetSprite(Sprite sprite)
    {
        myImage.sprite = sprite;
    }
}
