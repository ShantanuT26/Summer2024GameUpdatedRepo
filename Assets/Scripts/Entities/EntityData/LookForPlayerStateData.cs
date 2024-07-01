using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LookForPlayerStateData : ScriptableObject
{
    public float timeBetweenFlips { get; private set; } = 0.3f;
    public int numFlips { get; private set; } = 6;
}
