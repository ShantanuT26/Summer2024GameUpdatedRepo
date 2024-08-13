using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScrObj : ScriptableObject
{
    public new string name;
    public int healing;
    public int mana;
    public Sprite sprite;
    public Sprite background;

    public void Empty()
    {
        name = "";
        healing = 0;
        mana = 0;
        sprite = background;
    }
    public override bool Equals(object other)
    {
        if (other == null || GetType() != other.GetType())
        {
            return false;
        } 
        ScrObj temp = (ScrObj)other;
        return temp.name==name && temp.mana==mana && temp.sprite==sprite && temp.healing == healing;
    }
    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash*23 + name.GetHashCode();
        hash = hash*23 + healing.GetHashCode();
        hash=hash*23 + mana.GetHashCode();
        hash=hash*23 + sprite.GetHashCode();
        return hash;
    }
}
