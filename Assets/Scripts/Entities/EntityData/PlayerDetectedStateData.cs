using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerDetectedStateData : ScriptableObject
{
    public float playerDetectedMinDist { get; private set; } = 2f;
    public float playerDetectedMaxDist { get; private set; } = 4f;

    public float attackDist { get; private set; } = 1f;

    public float timeUntilCharge { get; private set; } = 0.8f;
}
