using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu]
public class PotionScrObj : ScriptableObject
{
    public Sprite sprite;
    [SerializeField] private new string name;
    [SerializeField] private int healing;
    [SerializeField] private int mana;

    public ScrObj[] ingredients;
}
