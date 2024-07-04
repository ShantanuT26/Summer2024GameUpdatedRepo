using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerDetectedStateData : ScriptableObject
{
    public float attackDist= 1f;

    public float timeUntilCharge = 0.8f;
}
