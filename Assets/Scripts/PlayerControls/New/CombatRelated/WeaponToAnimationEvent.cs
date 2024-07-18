using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponToAnimationEvent : MonoBehaviour
{
    //private Weapon weapon;
    private Player player;

    public void WeaponAnimationFinished()
    {
        if(player!=null)
        {
            player.attackState.FinishAnimation();
        }
        
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
