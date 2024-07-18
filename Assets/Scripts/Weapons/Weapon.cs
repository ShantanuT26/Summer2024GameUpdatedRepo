using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]private Animator baseAnim;
    [SerializeField]private Animator weaponAnim;
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
        }
    }
}


