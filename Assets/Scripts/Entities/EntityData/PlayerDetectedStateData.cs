using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerDetectedStateData : ScriptableObject
{
    public float attackDist { get; private set; } = 1f;

    public float timeUntilCharge { get; private set; } = 0.8f;
}
