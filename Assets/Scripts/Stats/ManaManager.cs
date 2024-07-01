using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaManager : MonoBehaviour
{
    [SerializeField] private TMP_Text mytmp;
    private int mana;
    public void updateMana(int x)
    {
        mana += x;
        mytmp.text = "Mana: " + mana;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        mana = 0;
        mytmp.text = "Mana: 00";
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
