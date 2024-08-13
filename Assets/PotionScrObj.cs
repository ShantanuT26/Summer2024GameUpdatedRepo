using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu]
public class PotionScrObj : ScriptableObject
{
    public Sprite sprite;
    public new string name;
    public int healing;
    public int mana;

    public ScrObj[] ingredients;
}
