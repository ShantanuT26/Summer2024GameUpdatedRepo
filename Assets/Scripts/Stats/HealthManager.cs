using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private TMP_Text mytmp;
    private int health;
    public void updateHealth(int x)
    {
        health += x;
        mytmp.text = "Health: " + health;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        health = 0;
        mytmp.text = "Health: 00";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
