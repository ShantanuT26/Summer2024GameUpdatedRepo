using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class PlayerLoadData
{
    public float[] position;

    public PlayerLoadData(Player player)
    {
        position = new float[2];
        position[0]=player.gameObject.transform.position.x;
        position[1] = player.gameObject.transform.position.y;
    }
}
