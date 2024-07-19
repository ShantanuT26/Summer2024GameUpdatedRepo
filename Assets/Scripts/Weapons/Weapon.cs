using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]private Animator baseAnim;
    [SerializeField]private Animator weaponAnim;
    [SerializeField] private GameObject weaponBase;
    [SerializeField] private GameObject weapon;
    private SpriteRenderer baseSR;
    private SpriteRenderer weaponSR;

    private void Awake()
    {
        weaponSR = weapon.GetComponent<SpriteRenderer>();
        baseSR = weaponBase.GetComponent<SpriteRenderer>();
    }
    private Player player;

    public void SetAnimBools(string x, bool y)
    {
        baseAnim.SetBool(x, y);
        weaponAnim.SetBool(x, y);
        player.SetAnimBool(x, y);
    }
    public void SetPlayer(Player player)
    {
        this.player = player;
    }
    private void Update()
    {
        if(player!=null)
        {
            this.transform.position = player.transform.position;
            CheckFlip();
        }
    }
    private void CheckFlip()
    {
        switch (player.facingDirection)
        {
            case -1:
                baseSR.flipX = true; 
                weaponSR.flipX = true; break;
            case 1:
                baseSR.flipX = false; 
                weaponSR.flipX = false; break;
        }

    }
}


