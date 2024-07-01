using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScrObj : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private int healing;
    [SerializeField] private int mana;
    // Start is called before the first frame update
    public string getName()
    {
        return name;
    }
    public int getHealing()
    {
        return healing;
    }
    public int getMana()
    {
        return mana;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
