using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChargeStateData : ScriptableObject
{
    public float chargeSpeed {get; private set;} = 7f;
    public float chargeTime { get; private set; } = 1.1f;
}
