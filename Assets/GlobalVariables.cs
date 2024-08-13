using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables: MonoBehaviour
{
    public static GlobalVariables Instance;
    public HashSet<ScrObj> herbs;
    public Sprite background;
    private void Awake()
    {
        Instance = this;
        herbs = new HashSet<ScrObj>();
        if (Instance!=null)
        {
            Destroy(this);
        }
    }


}
